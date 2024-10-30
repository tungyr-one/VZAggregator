import { Link, useNavigate } from 'react-router-dom';
import './Navbar.css';
import { useUser } from '../../contexts/UserContext';
import { User } from '../../models/User';

const Navbar: React.FC = () => {
    const { user, setUser } = useUser();
    const navigate = useNavigate();

    const handleLogout = () => {
        setUser(null);
        navigate('/');
    }

    return (
      <nav className="navbar">
          <ul className="navbar-links">
              <li><Link to="/">Home</Link></li>
          </ul>
          <ul className="navbar-links right-side">
              {user ? (
                  <li><Link to="/account" className="account-name">{user.name}</Link></li>
              ) 
              :               
              (
                  <>
                      <li><Link to="/login">Login</Link></li>
                      <li><Link to="/register">Register</Link></li>
                  </>
              )}
              {user && <li><Link to="/" onClick={handleLogout}>Sign Out</Link></li>}
          </ul>
      </nav>
  );
};

export default Navbar;