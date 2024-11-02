import { Link, useNavigate } from 'react-router-dom';
import './Navbar.css';
import { useCurrentUser } from '../../contexts/CurrentUserContext';
import { useEffect } from 'react';

const Navbar: React.FC = () => {
    const { currentUser: user, setCurrentUser: setUser } = useCurrentUser();
    const navigate = useNavigate();
    console.log('user2:', user);
    // console.log('roles:', user?.userRoles);
    console.log('userrole:', user?.userRoles.some(role => role.roleName == 'Admin'));
    // console.log(user?.userRoles.map(role => role.roleName));
    
    // useEffect(() => {
    //     if (user) {
    //       localStorage.setItem('user', JSON.stringify(user));
    //     } else {
    //       localStorage.removeItem('user'); // Clear storage on logout
    //     }
    //   }, [user]);
  
      useEffect(() => {
        const storedUser = localStorage.getItem('user');
        if (storedUser) {
          setUser(JSON.parse(storedUser));
          console.log('user:', user);
        }
      }, [setUser]);

    const handleLogout = () => {
        setUser(null);
        localStorage.removeItem('user');
        navigate('/');
    }

    return (
      <nav className="navbar">
          <ul className="navbar-links">
              <li><Link to="/">Home</Link></li>
          </ul>
          <ul className="navbar-links right-side">
              {user ? (
                <>
                    <div className="user-info">
                    <Link 
                           to={
                            user.userRoles.some(role => role.roleName === "Admin")
                              ? "/admin-account"
                              : user.userRoles.some(role => role.roleName === "User")
                              ? "/user-account"
                              : "/admin-account"
                          }
                          className="account-name"
                        >
                          {user.userName}
                    </Link>
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