import React from 'react';
import { NavLink, useNavigate } from 'react-router-dom';
import useAuth from '../../hooks/useAuth';

const linkClass = ({ isActive }: { isActive: boolean }) =>
  isActive ? 'sidebar-link active' : 'sidebar-link';

export const Sidebar: React.FC = () => {
  const { user, logout } = useAuth();
  const navigate = useNavigate();

  const handleLogout = () => {
    logout();
    navigate('/login');
  };

  return (
    <aside>
      <nav>
        <ul className="sidebar-list">
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
            </>
          )}

          {user?.role === 'customer' && (
            <>
              <li>
                <NavLink to="/customer" className={linkClass}>
                  Dashboard
                </NavLink>
              </li>
              <li>
                <NavLink to="/customer/book" className={linkClass}>
                  Book Guru
                </NavLink>
              </li>
              <li>
                <NavLink to="/customer/bookings" className={linkClass}>
                  My Bookings
                </NavLink>
              </li>
            </>
          )}

          {user?.role === 'guru' && (
            <>
              <li>
                <NavLink to="/guru" className={linkClass}>
                  Dashboard
                </NavLink>
              </li>
              <li>
                <NavLink to="/guru/availability" className={linkClass}>
                  Availability
                </NavLink>
              </li>
              <li>
                <NavLink to="/guru/bookings" className={linkClass}>
                  Bookings
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
              <li>
                <NavLink to="/admin/users" className={linkClass}>
                  Users
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
    </aside>
  );
};

export default Sidebar;
