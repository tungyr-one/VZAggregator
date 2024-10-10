import { Link, useNavigate } from 'react-router-dom';
import './Navbar.css';
import { useUser } from '../../contexts/UserContext';

const Navbar: React.FC = () => {
    const { setUser } = useUser();
    const navigate = useNavigate();

    const handleLogout = () => {
        setUser(null);
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

