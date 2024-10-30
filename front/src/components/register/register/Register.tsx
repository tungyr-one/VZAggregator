import React, { useState } from 'react';
import axios from 'axios';
import { useUser } from '../../../contexts/UserContext';
import { useNavigate } from 'react-router-dom';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import './Register.css'

interface RegistrationData {
  username: string;
  email: string;
  password: string;
  confirmPassword: string;
  city: string;
}

const Register: React.FC = () => {
  // State for form fields
  const [formData, setFormData] = useState<RegistrationData>({
    username: 'vasya',
    email: 'vasya@gmail.com',
    password: 'Passw0rd',
    confirmPassword: 'Passw0rd',
    city: 'NS'
  });
  const { setUser } = useUser();

  const [showPassword, setShowPassword] = useState<boolean>(false);

  const [success, setSuccess] = useState<boolean>(false);
  const navigate = useNavigate();

  const validatePassword = (password: string) => {
    const errors: string[] = [];
  
    if (formData.password !== formData.confirmPassword) {
      errors.push('Passwords do not match!');
    }
  
    if (password.length < 6 || password.length > 8) {
      errors.push("Password must be between 6 and 8 characters.");
    }
  
    if (!/\d/.test(password)) {
      errors.push("Password must contain at least one digit.");
    }
  
    if (errors.length > 0) {
      errors.forEach(error => toast.error(error));
      return false;
    }
  
    return true;
  };
  

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData({ ...formData, [name]: value });
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    if(!validatePassword(formData.password))
      return;

    try {
      const response = await axios.post('http://localhost:5146/api/account/register', {
        username: formData.username,
        email: formData.email,
        password: formData.password,
        city: formData.city,
      });

      if (response.status === 200) {
        setSuccess(true);
        setUser(response.data);
        toast.success('Register successful!');
        setTimeout(() => {
          navigate('/');
        }, 1000);
      }
    } catch (error) {
      let errorMessage = 'Failed to register. Please try again.'
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
    <div className="registration-container">
    <ToastContainer />
    <h2>Register</h2>
    {success && <div style={{ color: 'green' }}>Registration successful!</div>}
    
    <form onSubmit={handleSubmit}>
      <div className="form-group">
        <label>Username:</label>
        <input
          type="text"
          name="username"
          value={formData.username}
          onChange={handleInputChange}
          required
        />
      </div>
  
      <div className="form-group">
        <label>Email:</label>
        <input
          type="email"
          name="email"
          value={formData.email}
          onChange={handleInputChange}
          required
        />
      </div>

      <div className="form-group">
        <label>City:</label>
        <input
          type="text"
          name="city"
          value={formData.city}
          onChange={handleInputChange}
          required
        />
      </div>
  
      <div className="form-group">
        <label>Password:</label>
        <input
          type={showPassword ? "text" : "password"}
          name="password"
          value={formData.password}
          onChange={handleInputChange}
          required
        />
      </div>
  
      <div className="form-group">
        <label>Confirm Password:</label>
        <input
          type={showPassword ? "text" : "password"}
          name="confirmPassword"
          value={formData.confirmPassword}
          onChange={handleInputChange}
          required
        />
      </div>
    
      <div className="form-group checkbox-group">
        <div>
          <input
            type="checkbox"
            id="showPassword"
            checked={showPassword}
            onChange={() => setShowPassword(!showPassword)}
          />
          <label htmlFor="showPassword">Show Password</label>
        </div>

      </div>
  
      <button type="submit">Submit</button>
    </form>
  </div>
  
  );
};

export default Register;
