import React, { useEffect, useState } from 'react';
import axios from 'axios';
import './TripList.css';
import { Link } from 'react-router-dom';
import { Trip } from '../../models/Trip';
import { Pagination } from '../../models/Pagination';
import { useUser } from '../../contexts/UserContext';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

const TripList: React.FC = () => {
  const [trips, setTrips] = useState<Trip[]>([]);
  const [params, setParams] = useState({
    filterBy: '',
    sortBy: 'date',
    sortOrder: 'asc',
    offset: 0,
    pageSize: 10
  });
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);
  const { user } = useUser();

  useEffect(() => {
    fetchTrips();
  }, [params]);

  const fetchTrips = async () => {
    try {
      const response = await axios.post<Pagination<Trip>>(
        `http://localhost:5146/api/trips`,
        params
      );
      setTrips(response.data.items);
      setLoading(false);
    } catch (error) {
      console.error('Error fetching trips:', error);
      setError('Error fetching trips');
    }
  };

  const handleFilterChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setParams({ ...params, filterBy: e.target.value });
  };

  const handleSortChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    setParams({ ...params, sortBy: e.target.value });
  };

  const handleSortOrderChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    setParams({ ...params, sortOrder: e.target.value });
  };

  return (
    <div>
        <div>
        <ToastContainer />
      </div>
      <h1>{user ? `Welcome, ${user.username}` : 'Please log in'}</h1>
        <Link to="/login">
          <button>Login</button>
        </Link>
        <Link to="/register">
          <button>Register</button>
        </Link>

      <h1>Trips</h1>

      <div>
        <label>Search by carrier name: </label>
        <input
          type="text"
          placeholder="Filter by Carriers Name"
          value={params.filterBy}
          onChange={handleFilterChange}
        />
      </div>

      {/* Sort */}
      <div>
        <label>Sort by: </label>
        <select value={params.sortBy} onChange={handleSortChange}>
          <option value="Name">Carriers Name</option>
          <option value="TripPrice">Price</option>
          <option value="City">City</option>
          <option value="Created">Created</option>
          <option value="TripDateTime">Date</option>
          <option value="TripType">Type</option>
        </select>
      </div>

      <div>
        <label>Sort order: </label>
        <select value={params.sortOrder} onChange={handleSortOrderChange}>
          <option value="asc">Ascending</option>
          <option value="desc">Descending</option>
        </select>
      </div>

      {loading && <div>Loading...</div>}
      {error && <div>{error}</div>}

      <div className="tableContainer">
        {!loading && !error && trips.length > 0 && (
          <table>
            <thead>
              <tr>
                <th>Carrier</th>
                <th>Departure City</th>
                <th>Destination City</th>
                <th>Date</th>
                <th>Passengers</th>
                <th>Price</th>
              </tr>
            </thead>
            <tbody>
              {trips.map((trip) => (
                <tr key={trip.tripId}>
                  <td>{trip.carrier.name}</td>
                  <td>{trip.departureAddress?.city}</td>
                  <td>{trip.destinationAddress?.city}</td>
                  <td>{new Date(trip.tripDateTime).toLocaleDateString()}</td>
                  <td>{trip.passengersCapacity}</td>
                  <td>{trip.tripPrice}</td>
                </tr>
              ))}
            </tbody>
          </table>
        )}
      </div>

      {!loading && trips.length === 0 && <div>No trips found</div>}


    </div>
  );
};

export default TripList;
