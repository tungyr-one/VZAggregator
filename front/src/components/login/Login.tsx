import React, { useState } from 'react';
import axios, { Axios, AxiosError } from 'axios';
import { useNavigate } from 'react-router-dom';
import { useUser } from '../../contexts/UserContext';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import './Login.css';

interface LoginData {
  username: string;
  password: string;
}

const Login: React.FC = () => {
  const { setUser } = useUser();
  const [error, setError] = useState<string | null>(null);
  const [success, setSuccess] = useState<boolean>(false);

  const [formData, setFormData] = useState<LoginData>({
    username: 'vasya',
    password: 'Passw0rd',
  });

  const navigate = useNavigate();
  const [showPassword, setShowPassword] = useState<boolean>(false);

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData({ ...formData, [name]: value });
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    try {
      const response = await axios.post('http://localhost:5146/api/account/login', {
        username: formData.username,
        password: formData.password,
      });

      if (response.status === 200) {
        setSuccess(true);
        setError(null);
        setUser(response.data);
        console.log(response.data)
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
      <form onSubmit={handleSubmit}>

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
