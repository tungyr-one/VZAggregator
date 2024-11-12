import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { toast, ToastContainer } from 'react-toastify';
import { useNavigate } from 'react-router-dom';
import './UserDashboard.css';
import { useAuth } from '../../contexts/AuthContext';
import { deleteUserAccount } from '../../services/AccountService';
import UserProfile from '../user-profile/UserProfile';
import UserInfoEditForm from '../user-edit/UserInfoEditForm';

const UserDashboard: React.FC = () => {
  const {currentUser: user, login, logout, logIt} = useAuth();
  const [activeTab, setActiveTab] = useState("User Info");
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

  useEffect(() => {
  }, []);

  const resetTab = () => {

  };

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

  const handleSelfDeleteAccount = async () => {
    const success = await deleteUserAccount(user?.id);
    if (success) {
       logout();
       navigate('/');
  };
}

  return (
    <div className="user-account-page">
      <ToastContainer />

      <aside className="sidebar">
        <ul>
          <li onClick={() => setActiveTab("User Info")} className={activeTab === "User Info" ? "active" : ""}>User Info</li>
          <li onClick={() => setActiveTab("Balance")} className={activeTab === "Balance" ? "active" : ""}>Account Balance</li>
          <li onClick={() => setActiveTab("Discounts")} className={activeTab === "Discounts" ? "active" : ""}>Discounts</li>
          <li onClick={() => setActiveTab("Orders")} className={activeTab === "Orders" ? "active" : ""}>Order History</li>
        </ul>
      </aside>

      <main className="content">
        <h2>{activeTab}</h2>

        {activeTab === "User Info" && (
          <div className="user-info">
            {user && <UserProfile user={user} onDelete={logout}/>}
            {/* {editMode ? (
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

                <UserProfile user={user} onDelete={resetTab}/>
                
                <button className="edit-button" onClick={() => setEditMode(true)}>Edit</button>
              </>
            )} */}
             {/* <button onClick={handleSelfDeleteAccount} className="delete-account-btn">Delete Account</button> */}
          </div>
        )}

        {activeTab === "Balance" && (
          <div className="account-balance">
            <h3>Balance</h3>
            <p>${userData.balance}</p>
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
            {/* Replace with real orders data if available */}
            <p>No orders found.</p>
          </div>
        )}
      </main>
    </div>
  );
};

export default UserDashboard;
