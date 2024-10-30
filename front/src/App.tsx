import React from 'react';
import './App.css';
import TripList from './components/triplist/TripList';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Register from './components/register/register/Register';
import Login from './components/login/Login';
import { UserProvider } from './contexts/UserContext';
import Navbar from './components/navbar/Navbar';
import 'react-toastify/dist/ReactToastify.css';

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
          </Routes>
        </BrowserRouter>
      </UserProvider>
    </div>
  );
};
export default App;
