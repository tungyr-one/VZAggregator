import React, { useState } from 'react';
import { User } from '../../models/User';
import './UserProfile.css';
import ConfirmModal from '../confirm-modal/ConfirmModal';
import { deleteUser, updateUser } from '../../services/UserService';
import UserInfoEditForm from '../user-edit/UserInfoEditForm';
import { UserEditData } from '../../models/UserEditData';
import { useAuth } from '../../contexts/AuthContext';

interface UserProfileProps {
  user: User | undefined;
  onDelete: () => void;
}

const UserProfile: React.FC<UserProfileProps> = ({ user, onDelete  }) => {
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [editMode, setEditMode] = useState(false);
    const [updatedUserData, setUpdatedUserData] = useState<User | undefined>(user);
    const { currentUser, login, logout } = useAuth();

    const handleOpenModal = () => {
        setIsModalOpen(true);
    };

    const handleCloseModal = () => {
        setIsModalOpen(false);
    }

    const handleDeleteAccount = async () => {
      const success = await deleteUser(user?.id);
      if (success) {
        onDelete();
    };
  }

  //TODO: rename to update user
  const handleSaveUser = async (editData: UserEditData) => {
    const updateData = { ...updatedUserData, ...editData };
    const updatedUser = await updateUser(user?.id, updateData);
    if(updatedUser && currentUser?.id === updatedUser.id) {
      logout();
      login(updatedUser);
    }
  }

  const handleFormChange = () =>{

  }

  const handleCancelEdit = () =>{
    setUpdatedUserData(user);
    setEditMode(false)
  }

  return (
    <div className="user-profile">
      {editMode ? (
        <UserInfoEditForm 
          user={updatedUserData}
          onCancel={handleCancelEdit} 
          onSave={handleSaveUser} 
          onChange={(updatedUser) => setUpdatedUserData(updatedUser)}
        />
      ) : (
        <>
          <section className="user-section">
            <h2>Basic Information</h2>
            <p><strong>Username:</strong> {user?.userName || 'N/A'}</p>
            <p><strong>Telegram Nick:</strong> {user?.telegramNick}</p>
            <p><strong>Phone Number:</strong> {user?.phoneNumber || 'N/A'}</p>
            <p><strong>Email:</strong> {user?.email || 'N/A'}</p>
          </section>

          <section className="user-section">
            <h2>Account Dates</h2>
            <p><strong>Created:</strong> {user?.created ? new Date(user?.created).toLocaleDateString() : 'N/A'}</p>
            <p><strong>Last Updated:</strong> {user?.updated ? new Date(user?.updated).toLocaleDateString() : 'N/A'}</p>
            <p><strong>Last Trip:</strong> {user?.lastTrip ? new Date(user?.lastTrip).toLocaleDateString() : 'N/A'}</p>
          </section>

          <section className="user-section">
            <h2>Subscription</h2>
            <p><strong>Subscription Type:</strong> {user?.subscriptionType || 'N/A'}</p>
            <p><strong>Is Active:</strong> {user?.isSubscriptionActive ? 'Yes' : 'No'}</p>
            <p><strong>Trips with Subscription:</strong> {user?.subscriptionTripsCount || 0}</p>
          </section>

          <section className="user-section">
            <h2>Additional Info</h2>
            <p><strong>Notes:</strong> {user?.notes || 'N/A'}</p>
            <p><strong>Discount:</strong> {user?.discount ? `${user?.discount}%` : 'N/A'}</p>
            <p><strong>Trips Count:</strong> {user?.tripsCount}</p>
            <p><strong>Roles:</strong> {user?.userRoles.join(', ')}</p>
          </section>

          <button className="edit-button" onClick={() => setEditMode(true)}>Edit</button>
        </>
      )}

      {isModalOpen ? (
        <ConfirmModal
          message="Do you really want to delete this account? This action cannot be undone."
          isOpen={isModalOpen}
          onClose={handleCloseModal}
          onConfirm={handleDeleteAccount}
        />
      ) : (
        <button className="delete-account-btn" onClick={handleOpenModal}>
          Delete account
        </button>
      )}
    </div>
  );
};

export default UserProfile;
