import React from 'react';
import { Link, useNavigate } from 'react-router-dom';
import './navbar.css';
import { useUser } from '../../contexts/UserContext';

const Navbar: React.FC = () => {
    const { setUser } = useUser();
    const navigate = useNavigate();

    const handleLogout = () => {
        // Set the username to null or clear the user session
        // useUser(null);
        
        // Optionally, you can also clear any token or session stored
        // localStorage.removeItem('token');
        // sessionStorage.removeItem('userSession');
        
        // Redirect the user to the homepage after logout
        navigate('/');
    }



  return (
    <nav className="navbar">
      <ul className="navbar-links">
        <li><Link to="/">Home</Link></li>
        <li><Link to="/login">Login</Link></li>
        <li><Link to="/register">Register</Link></li>
        <li><Link to="/" onClick={handleLogout}>Sign Out</Link></li>
      </ul>
    </nav>
  );
};

export default Navbar;
function setUser(arg0: { username: any; }) {
    throw new Error('Function not implemented.');
}

