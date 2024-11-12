import React, { useState } from 'react';
import { User } from '../../models/User';
import { UserEditData } from '../../models/UserEditData';

interface UserInfoEditFormProps {
  user: User | undefined;
  onChange: (updatedUser: User | undefined) => void;
  onCancel: () => void;
  onSave: (editData: UserEditData) => void;
}

const UserInfoEditForm: React.FC<UserInfoEditFormProps> = ({ user, onChange, onCancel, onSave }) => {
  const [formData, setFormData] = useState<UserEditData>({
    userName: user?.userName,
    email: user?.email,
    telegramNick: user?.telegramNick,
    birthDate: user?.birthDate,
    phoneNumber: user?.phoneNumber
  });

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    const { name, value } = e.target;
    
    setFormData((prevData) => ({
      ...prevData,
      [name]: value
    }));
    
    // onChange(userUpdateData);
  };
 
    return (
    <section>
      <form>
        <h2>Edit User Information</h2>

        <label>
          Username:
          <input
            type="text"
            name="userName"
            value={formData.userName || ''}
            onChange={handleInputChange}
            placeholder="Enter username"
          />
        </label>

        <label>
          Telegram Nick:
          <input
            type="text"
            name="telegramNick"
            value={formData.telegramNick}
            onChange={handleInputChange}
            placeholder="Enter Telegram nick"
            required
          />
        </label>

        <label>
          Birth Date:
          <input
            type="date"
            name="birthDate"
            value={formData.birthDate || ''}
            onChange={handleInputChange}
          />
        </label>

        <label>
          Phone Number:
          <input
            type="tel"
            name="phoneNumber"
            value={formData.phoneNumber || ''}
            onChange={handleInputChange}
            placeholder="Enter phone number"
          />
        </label>

        <label>
          Email:
          <input
            type="email"
            name="email"
            value={formData.email || ''}
            onChange={handleInputChange}
            placeholder="Enter email"
          />
        </label>

        <button type="button" onClick={onCancel}>Cancel</button>
        <button
          type="button"
          onClick={() => onSave(formData)}
        >
          Save
        </button>
      </form>
    </section>
  );
};

export default UserInfoEditForm;