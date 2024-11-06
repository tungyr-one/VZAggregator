import React, { useState, useEffect, FormEvent } from 'react';
import axios from 'axios';
import { toast, ToastContainer } from 'react-toastify';
import { useNavigate } from 'react-router-dom';
import './AdminAccount.css';
import { useAuth } from '../../contexts/AuthContext';
import { User } from '../../models/User';
import UserProfile from '../user-profile/UserProfile';

const AdminAccount: React.FC = () => {
  const { user, login, logout, hasRole } = useAuth();
  const [activeTab, setActiveTab] = useState("Admin Info");
  const navigate = useNavigate();

  const [userData, setUserData] = useState({
    userName: user?.userName,
    email: user?.email,
    balance: 0,
    discounts: [],
    orders: [],
  });

  const [editMode, setEditMode] = useState(false);
  const [formData, setFormData] = useState({ userName: user?.userName, email: user?.email });
  const [userSearchFormData, setUserSearchFormData] = useState({ userName: 'vasya'});
  const [fetchedUserData, setFetchedUserData] = useState<User | null>(null);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
  }, []);

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData((prev) => ({ ...prev, [name]: value }));
  };

  const handleUpdate = async () => {
    setUserData((prev) => ({ ...prev, ...formData }));
    setEditMode(false);
    try {
      const response = await axios.put(`http://localhost:5146/api/users/${user?.id}`, formData);
      if (response.status === 200) {
        login(response.data);
        toast.success('Update successful!');
      }
    } catch (error) {
      toast.error('Failed to update. Please try again.');
    }
  };

  const handleDeleteSelfAccount = async () => {
    try {
      const response = await axios.delete(`http://localhost:5146/api/users/${user?.id}`);
      if (response.status === 200) {
        // setUser(null);
        toast.success('Account deleted successfully.');
        navigate('/');
      }
    } catch (error) {
      toast.error('Failed to delete account. Please try again.');
    }
  };

  const handleFindUser = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault()
    try {
      const response = await axios.get(`http://localhost:5146/api/users/username/${userSearchFormData.userName}`, {
      });

      console.log("user by name:", response.data);
      if (response.status === 200) {
        setFetchedUserData(response.data);
        setError(null);
      }
    } catch (error: unknown) {
      let errorMessage = 'Failed to get user data. Please try again.'
      if (axios.isAxiosError(error)) {
        if (error.response?.data?.message) {
          errorMessage = error.response.data.message;
        } 
        setError(error.response?.data?.message || errorMessage);
        toast.error(errorMessage);
      } else {
        errorMessage = 'An unexpected error occurred. Please try again.'
        setError(errorMessage);
        toast.error(errorMessage);
      }
    }
  };

  return (
    <div className="user-account-page">
      <ToastContainer />

      <aside className="sidebar">
        <ul>
          <li onClick={() => setActiveTab("Admin Info")} className={activeTab === "Admin Info" ? "active" : ""}>Admin Info</li>
          <li onClick={() => setActiveTab("Manage Users")} className={activeTab === "Manage Users" ? "active" : ""}>Manage Users</li>
          <li onClick={() => setActiveTab("Discounts")} className={activeTab === "Discounts" ? "active" : ""}>Discounts</li>
          <li onClick={() => setActiveTab("Orders")} className={activeTab === "Orders" ? "active" : ""}>Order History</li>
        </ul>
      </aside>

      <main className="content">
        <h2>{activeTab}</h2>

        {activeTab === "Admin Info" && (
          <div className="user-info">
            {editMode ? (
              <>
                <input type="text" name="userName" value={formData.userName} onChange={handleInputChange} placeholder="Name" />
                <input type="email" name="email" value={formData.email} onChange={handleInputChange} placeholder="Email" />
                <div className="buttons-container">
                  <button onClick={handleUpdate}>Save</button>
                  <button onClick={() => setEditMode(false)}>Cancel</button>
                </div>
              </>
            ) : (
              <>
                <p><strong>Name:</strong> {userData.userName}</p>
                <p><strong>Email:</strong> {userData.email}</p>
                <button className="edit-button" onClick={() => setEditMode(true)}>Edit</button>
              </>
            )}
             <button onClick={handleDeleteSelfAccount} className="delete-account-btn">Delete Account</button>
          </div>
        )}

        {activeTab === "Manage Users" && (
          <div className="manage-users">
            <h3>Get users info</h3>

            <form onSubmit={handleFindUser}>
              <div className='admin-form-group'>
              <strong>Name:</strong>
                <input type="text"
                placeholder="Enter username"
                value={userSearchFormData.userName}
                onChange={(e) => setUserSearchFormData({...userSearchFormData, userName: e.target.value})} />
              <button type='submit' className='Find-user-btn'>Find</button>
              </div>
            {fetchedUserData && <UserProfile user={fetchedUserData} />}
            </form>

            
          </div>
        )}

        {activeTab === "Discounts" && (
          <div className="discounts">
            <h3>Your Discounts</h3>
            <ul>
              {userData.discounts.map((discount, index) => (
                <li key={index}>{discount}</li>
              ))}
            </ul>
          </div>
        )}

        {activeTab === "Orders" && (
          <div className="order-history">
            <h3>Order History</h3>
            <p>No orders found.</p>
          </div>
        )}
      </main>
    </div>
  );
};

export default AdminAccount;


