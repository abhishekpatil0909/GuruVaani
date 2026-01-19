import React from 'react';
import { NavLink, useNavigate } from 'react-router-dom';
import useAuth from '../../hooks/useAuth';

const linkClass = ({ isActive }: { isActive: boolean }) =>
  isActive ? 'nav-link active' : 'nav-link';

export const Header: React.FC = () => {
  const { user, logout } = useAuth();
  const navigate = useNavigate();

  const handleLogout = () => {
    logout();
    navigate('/login');
  };

  return (
    <header>
      <div className="brand">
        <h1>GuruVani</h1>
      </div>

      <nav>
        <ul className="nav-list">
          <li>
            <NavLink to="/" className={linkClass}>
              Home
            </NavLink>
          </li>

          {!user && (
            <>
              <li>
                <NavLink to="/login" className={linkClass}>
                  Login
                </NavLink>
              </li>
              <li>
                <NavLink to="/register" className={linkClass}>
                  Register
                </NavLink>
              </li>
            </>
          )}

          {user?.role === 'admin' && (
            <>
              <li>
                <NavLink to="/admin" className={linkClass}>
                  Admin Dashboard
                </NavLink>
              </li>
            </>
          )}

          {user?.role === 'guru' && (
            <>
              <li>
                <NavLink to="/guru" className={linkClass}>
                  Guru Dashboard
                </NavLink>
              </li>
            </>
          )}

          {user?.role === 'customer' && (
            <>
              <li>
                <NavLink to="/customer" className={linkClass}>
                  Customer Dashboard
                </NavLink>
              </li>
            </>
          )}

          {user && (
            <li>
              <button onClick={handleLogout} className="btn-logout">
                Logout
              </button>
            </li>
          )}
        </ul>
      </nav>
    </header>
  );
};

export default Header;
