import axios from 'axios';
import { toast } from 'react-toastify';
import { UserEditData } from '../models/UserEditData';

export const fetchUser = async (userName: string | undefined) => {
    try {
      const response = await axios.get(`http://localhost:5146/api/users/username/${userName}`, {
      });
      if (response.status === 200) {
        return response.data;
      }
    } catch (error: unknown) {
      let errorMessage = 'Failed to get user data. Please try again.'
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

  export const updateUser = async (userId: number | undefined, userData:UserEditData) => {
    console.log('Updated service id and user:', userId, userData);
    if(userId !== undefined)
    {
        try {
            const response = await axios.put(`http://localhost:5146/api/users/${userId}`, userData);
            if (response.status === 200) {
                toast.success('User updated successfully.');
                return response.data;
            }
        } catch (error) {
            toast.error('Failed to update user. Please try again.');
            }
            return false;
    }
    else
        {
            toast.error('Invalid user. Please try again.');
        }
  };

export const deleteUser = async (userId: number | undefined) => {
    if(userId !== undefined)
    {
        try {
            const response = await axios.delete(`http://localhost:5146/api/users/${userId}`);
            if (response.status === 200) {
                toast.success('Account deleted successfully.');
                return true;
            }
        } catch (error) {
            toast.error('Failed to delete account. Please try again.');
            }
            return false;
    }
    else
        {
            toast.error('Invalid user. Please try again.');
        }
  };