import React, { useState } from 'react';
import { User } from '../../models/User';
import './UserProfile.css';
import axios from 'axios';
import { toast } from 'react-toastify';
import ConfirmModal from '../confirm-modal/ConfirmModal';

interface UserProfileProps {
  user: User;
}

const UserProfile: React.FC<UserProfileProps> = ({ user }) => {
    const [isModalOpen, setIsModalOpen] = useState(false);

    const handleOpenModal = () => {
        setIsModalOpen(true);
    };

    const handleCloseModal = () => {
        setIsModalOpen(false);
    }

    const handleDeleteUsersAccount = async () => {
        setIsModalOpen(false);
        try {
          const response = await axios.delete(`http://localhost:5146/api/account/${user.userName}`);
          if (response.status === 200) {
            toast.success('Users account deleted successfully.');
          }
        } catch (error) {
          toast.error('Failed to delete account. Please try again.');
        }
      };


  return (
    <div className="user-profile">
      <section className="user-section">
        <h2>Basic Information</h2>
        <p><strong>Username:</strong> {user.userName || 'N/A'}</p>
        <p><strong>Telegram Nick:</strong> {user.telegramNick}</p>
        <p><strong>Phone Number:</strong> {user.phoneNumber || 'N/A'}</p>
        <p><strong>Email:</strong> {user.email || 'N/A'}</p>
      </section>

      <section className="user-section">
        <h2>Account Dates</h2>
        <p><strong>Created:</strong> {new Date(user.created).toLocaleDateString()}</p>
        <p><strong>Last Updated:</strong> {user.updated ? new Date(user.updated).toLocaleDateString() : 'N/A'}</p>
        <p><strong>Last Trip:</strong> {user.lastTrip ? new Date(user.lastTrip).toLocaleDateString() : 'N/A'}</p>
      </section>

      <section className="user-section">
        <h2>Subscription</h2>
        <p><strong>Subscription Type:</strong> {user.subscriptionType || 'N/A'}</p>
        <p><strong>Is Active:</strong> {user.isSubscriptionActive ? 'Yes' : 'No'}</p>
        <p><strong>Trips with Subscription:</strong> {user.subscriptionTripsCount || 0}</p>
      </section>

      <section className="user-section">
        <h2>Addresses</h2>
        {user.addresses.length > 0 ? (
          <ul>
            {/* {user.addresses.map((address, index) => (
            //   <li key={index}>{address}</li>)
            )} */}
          </ul>
        ) : (
          <p>No addresses found.</p>
        )}
      </section>

      {/* Orders */}
      <section className="user-section">
        <h2>Orders</h2>
        {user.orders.length > 0 ? (
          <ul>
            {user.orders.map((order, index) => (
              <li key={index}>
                {/* Order #{order.id} - {order.description} (${order.amount}) */}
              </li>
            ))}
          </ul>
        ) : (
          <p>No orders available.</p>
        )}
      </section>

      <section className="user-section">
        <h2>Additional Info</h2>
        <p><strong>Notes:</strong> {user.notes || 'N/A'}</p>
        <p><strong>Discount:</strong> {user.discount ? `${user.discount}%` : 'N/A'}</p>
        <p><strong>Trips Count:</strong> {user.tripsCount}</p>
        <p><strong>Roles:</strong> {user.userRoles.join(', ')}</p>
      </section>

      <button className='delete-account-btn' onClick={handleOpenModal}>Delete account</button> 

      <ConfirmModal
      isOpen = {isModalOpen}
      onClose = {handleCloseModal}
      onConfirm = {handleDeleteUsersAccount}
      />

    </div>
  );
};

export default UserProfile;
