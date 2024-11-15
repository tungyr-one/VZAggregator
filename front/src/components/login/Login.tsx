import React, { useState } from 'react';
import axios, { Axios, AxiosError } from 'axios';
import { useNavigate } from 'react-router-dom';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import './Login.css';
import { useAuth } from '../../contexts/AuthContext';

interface LoginData {
  username: string;
  password: string | undefined;
}

const Login: React.FC = () => {
  const { login } = useAuth();
  const navigate = useNavigate();


  const [error, setError] = useState<string | null>(null);
  const [success, setSuccess] = useState<boolean>(false);

  const [formData, setFormData] = useState<LoginData>({
    username: 'vasya',
    password: process.env.REACT_APP_DEFAULT_DEV_PASSWORD,
  });

  const [showPassword, setShowPassword] = useState<boolean>(false);

  const handleLogin = async (e: React.FormEvent) => {
    e.preventDefault();

    try {
      const response = await axios.post(process.env.REACT_APP_API_URL + `/account/login`, {
        username: formData.username,
        password: formData.password,
      });

      if (response.status === 200) {
        console.log("rolename login", response.data.userRoles[0]);
        setSuccess(true);
        setError(null);
        login(response.data);
        toast.success('Login successful!');
        setTimeout(() => {
          navigate('/');
        }, 1000);
      }
    } catch (error: unknown) {
      let errorMessage = 'Failed to login. Please try again.'
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
    <div className="login-container">
      	<div>
        <ToastContainer />
      </div>
      <form onSubmit={handleLogin}>

        <div className='form-group'>
            <input
              type="text"
              placeholder="Username"
              value={formData.username}
              onChange={(e) => setFormData({ ...formData, username: e.target.value })}
          />
        </div>

        <div className='form-group'>
          <input
            type={showPassword ? "text" : "password"}
            placeholder="Password"
            value={formData.password}
            onChange={(e) => setFormData({ ...formData, password: e.target.value })}
          />
        </div>

        <div>
        <input
          type="checkbox"
          id="showPassword"
          checked={showPassword}
          onChange={() => setShowPassword(!showPassword)}
        />
      </div>
      <label htmlFor="showPassword">Show Password</label>
      <div>

      </div>
        <button type="submit">Login</button>
      </form>

  </div>
  );
};

export default Login;
