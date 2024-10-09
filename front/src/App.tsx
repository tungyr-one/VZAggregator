import React from 'react';
import './App.css';
import TripList from './components/trip-list/TripList';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import RegistrationForm from './components/register/register/register.component';
import LoginForm from './components/login/login.component';
import { UserProvider } from './contexts/UserContext';
import Navbar from './components/navbar/navbar';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

const App: React.FC = () => {
  return (
    <div className="App">
      <UserProvider>
        <BrowserRouter> 
        <Navbar />
          <Routes>
            <Route path="/" element={<TripList />} />
            <Route path="/register" element={<RegistrationForm />} />
            <Route path="/login" element={<LoginForm />} />
          </Routes>
        </BrowserRouter>
      </UserProvider>
    </div>
  );
};
export default App;
