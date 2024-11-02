import React, { useEffect } from 'react';
import './App.css';
import TripList from './components/triplist/TripList';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Register from './components/register/register/Register';
import Login from './components/login/Login';
import { useCurrentUser, UserProvider } from './contexts/CurrentUserContext';
import Navbar from './components/navbar/Navbar';
import 'react-toastify/dist/ReactToastify.css';
import UserAccount from './components/user-account/UserAccount';
import AdminAccount from './components/admin-account/AdminAccount';

const App: React.FC = () => {

  return (
    <div className="App">
      <UserProvider>
        <BrowserRouter> 
        <Navbar />
          <Routes>
            <Route path="/" element={<TripList />} />
            <Route path="/register" element={<Register />} />
            <Route path="/login" element={<Login />} />
            <Route path="/user-account" element={<UserAccount />} />
            <Route path="/admin-account" element={<AdminAccount />} />
          </Routes>
        </BrowserRouter>
      </UserProvider>
    </div>
  );
};
export default App;
