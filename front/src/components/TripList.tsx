import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { Trip } from '../models/Trip';
import { UserParams } from '../models/UserParams';
import { Pagination } from '../models/Pagination';
import './TripList.css';

const TripList: React.FC = () => {
  const [trips, setTrips] = useState<Trip[]>([]);
  const [filter, setFilter] = useState<string>('');
  const [sortBy, setSortBy] = useState<string>('CarriersName');
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  const [params, setParams] = useState({
    filterBy: "",
    sortBy: "date",
    sortOrder: "asc",
    offset:0,
    pageSize:10
  });



  // const [count, setCount] = useState(0);

  // const userParams: UserParams = new UserParams();

  useEffect(() => {
    fetchTrips();
  }, [params]);


  const fetchTrips = async () => {
    try {
      const response = await axios.post<Pagination<Trip>>(`http://localhost:5146/api/trips`, params);
      setTrips(response.data.items);
      setLoading(false);
    } catch (error) {
      console.error('Error fetching trips:', error);
    }
  };

  const handleFilterChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setParams((previousState) => ({
      ...previousState,
      filterBy: e.target.value
    }));
  };
  
  const handleSortChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    setParams((previousState) => ({
      ...previousState,
      sortBy: e.target.value
    }));
  };

  const handleSortOrderChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    setParams((previousState) => ({
      ...previousState,
      sortOrder: e.target.value
    }));
  };

  return (
    <div>
      {/* <h2>{count}</h2>
      <button onClick={() => setCount(count + 1)}> button </button> */}
      <h1>Trips</h1>

      {/* Filter */}
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

            {/* Sort order*/}
            <div>
        <label>Sort order: </label>
        <select value={params.sortOrder} onChange={handleSortOrderChange}>
          <option value="asc">Ascending</option>
          <option value="desc">Descending</option>
        </select>
      </div>

      {/* Loading/Error Handling */}
      {loading && <div>Loading...</div>}
      {error && <div>{error}</div>}

      <div className="tableContainer">
        {/* Trip List */}
      {!loading && !error && trips.length > 0 && (
        <table >
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
