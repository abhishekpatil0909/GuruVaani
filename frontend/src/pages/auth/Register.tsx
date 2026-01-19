import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import '../../assets/styles/auth.css';

export const Register: React.FC = () => {
  const navigate = useNavigate();

  const [name, setName] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [role, setRole] = useState('customer');

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();

    const payload = {
      name,
      email,
      password,
      role,
    };

    console.log('Register Data:', payload);

    // later → API call
    navigate('/login');
  };

  return (
    <div className="auth-page">
      <div className="auth-card slide-up">
        <h2 className="auth-title">Create Account</h2>
        <div className="auth-sub">
          Join <b>GuruVani</b> — select your role
        </div>

        <form className="auth-form" onSubmit={handleSubmit}>
          <div>
            <label>Full Name</label>
            <input
              value={name}
              onChange={(e) => setName(e.target.value)}
              required
              placeholder="Enter your name"
            />
          </div>

          <div>
            <label>Email</label>
            <input
              type="email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              required
              placeholder="Enter email"
            />
          </div>

          <div>
            <label>Password</label>
            <input
              type="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              required
              placeholder="Create password"
            />
          </div>

          {/* ROLE SELECTION */}
          <div>
  <label>Select Role</label>

  <div className="role-group">

    <label className="role-option">
      <input
        type="radio"
        value="admin"
        checked={role === 'admin'}
        onChange={(e) => setRole(e.target.value)}
      />
      <div className="role-card">
        <span>👑</span>
        Admin
      </div>
    </label>

    <label className="role-option">
      <input
        type="radio"
        value="guru"
        checked={role === 'guru'}
        onChange={(e) => setRole(e.target.value)}
      />
      <div className="role-card">
        <span>🧘</span>
        Guru
      </div>
    </label>

    <label className="role-option">
      <input
        type="radio"
        value="customer"
        checked={role === 'customer'}
        onChange={(e) => setRole(e.target.value)}
      />
      <div className="role-card">
        <span>🙋</span>
        Customer
      </div>
    </label>

  </div>
</div>


          <div className="auth-actions">
            <button className="btn btn-primary" type="submit">
              Create Account
            </button>

            <button
              type="button"
              className="btn btn-ghost"
              onClick={() => navigate('/login')}
            >
              Back to Login
            </button>
          </div>
        </form>

        <div className="auth-footer">
          Your data is secure with GuruVani 🔒
        </div>
      </div>
    </div>
  );
};

export default Register;
