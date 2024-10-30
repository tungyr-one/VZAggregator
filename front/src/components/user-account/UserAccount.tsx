import React, { useState, useEffect } from 'react';
import { useUser } from '../../contexts/UserContext';
import axios from 'axios';
import { toast, ToastContainer } from 'react-toastify';
import { useNavigate } from 'react-router-dom';

const UserAccount: React.FC = () => {
const { user, setUser } = useUser();
  const [userData, setUserData] = useState({
    username: user?.name,
    email: user?.email,
    balance: 0,
    discounts: [],
    orders: [],
  });

  const [editMode, setEditMode] = useState(false);
  const [formData, setFormData] = useState({ name: user?.name, email: user?.email });

  useEffect(() => {
    // Load user data (example with mock data)
    fetchUserData();
  }, []);

  const [success, setSuccess] = useState<boolean>(false);
  const navigate = useNavigate();

  const fetchUserData = async () => {
    // Replace with API call to fetch user data
    const mockData = {
      name: 'John Doe',
      email: 'john@example.com',
      balance: 150,
      discounts: ['10% off on electronics', '5% off on next purchase'],
      orders: [
        { id: 1, item: 'Laptop', price: 1000, date: '2024-08-15' },
        { id: 2, item: 'Headphones', price: 50, date: '2024-07-10' },
      ],
    };
    // setUserData(mockData);
    setFormData({ name: mockData.name, email: mockData.email });
  };

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData((prev) => ({ ...prev, [name]: value }));
  };

  const handleUpdate = async () => {
    setUserData((prev) => ({ ...prev, ...formData }));
    setEditMode(false);
  };

  const handleDeleteAccount = async () => {

    try {
      const response = await axios.delete(`http://localhost:5146/api/account/delete/${user?.name}`, {
      });

      if (response.status === 200) {
        setSuccess(true);
        setUser(null);
        toast.success('Delete successful!');
        setTimeout(() => {
          navigate('/');
        }, 1000);
      }
    } catch (error) {
      let errorMessage = 'Failed to delete. Please try again.'
      if (axios.isAxiosError(error)) {
        if (error.response?.data?.message) {
          errorMessage = error.response.data.message;
        } 
        toast.error(errorMessage);
      } else {
        errorMessage = 'An unexpected error occurred. Please try again.'
        toast.error(errorMessage);
      }
    }
  };

  return (
    <div className="user-account">
        <ToastContainer />
      <h2>User Account</h2>
      <div>
        <h3>Account Information</h3>
        {editMode ? (
          <>
            <input
              type="text"
              name="name"
              value={formData.name}
              onChange={handleInputChange}
              placeholder="Name"
            />
            <input
              type="email"
              name="email"
              value={formData.email}
              onChange={handleInputChange}
              placeholder="Email"
            />
            <button onClick={handleUpdate}>Save</button>
            <button onClick={() => setEditMode(false)}>Cancel</button>
          </>
        ) : (
          <>
            <p><strong>Name:</strong> {userData.username}</p>
            <p><strong>Email:</strong> {userData.email}</p>
            <button onClick={() => setEditMode(true)}>Edit</button>
          </>
        )}
      </div>

      <div>
        <h3>Account Balance</h3>
        <p>${userData.balance}</p>
      </div>

      <div>
        <h3>Your Discounts</h3>
        <ul>
          {userData.discounts.map((discount, index) => (
            <li key={index}>{discount}</li>
          ))}
        </ul>
      </div>

      <div>
        <h3>Order History</h3>
        {/* <ul>
          {userData.orders.map((order) => (
            <li key={order.id}>
              {order.item} - ${order.price} (Date: {order.date})
            </li>
          ))}
        </ul> */}
      </div>

      <button onClick={handleDeleteAccount} style={{ color: 'red' }}>Delete Account</button>
    </div>
  );
};

export default UserAccount;
