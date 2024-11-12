import { Link, useNavigate } from 'react-router-dom';
import './Navbar.css';
import { useAuth } from '../../contexts/AuthContext';

const Navbar: React.FC = () => {
    const navigate = useNavigate();
    const { currentUser: user, logout, hasRole } = useAuth();

      const handleLogout = () => {
        logout();
        navigate('/');
      };

    return (
      <nav className="navbar">
          <ul className="navbar-links">
              <li><Link to="/">Home</Link></li>
          </ul>
          <ul className="navbar-links right-side">
              {user ? (
                <>
                    <div className="user-info">
                      <Link to="/user-account" className='account-name'>{user.userName}</Link>
                    <div>
                      {hasRole('Admin') && <Link to="/admin-account">Admin Panel</Link>}
                      {hasRole('Moderator') && <Link to="/moderator-account">Moderator Panel</Link>}
                    </div>
                      <li className="role-name">{user.userRoles.join(' ')}</li>
                    </div>
                </>
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