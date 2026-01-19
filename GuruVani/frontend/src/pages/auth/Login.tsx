import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import useAuth from '../../hooks/useAuth';
import '../../assets/styles/auth.css';

const Login: React.FC = () => {
  const navigate = useNavigate();
  const { login } = useAuth();

  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [role, setRole] = useState<'customer' | 'guru' | 'admin'>('customer');

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    // Call mock login with chosen role
    login(role);

    // Redirect based on role
    if (role === 'admin') navigate('/admin');
    else if (role === 'guru') navigate('/guru');
    else navigate('/customer');
  };

  return (
    <div className="auth-page">
      <div className="auth-card">
        <h2 className="auth-title">Welcome back</h2>
        <div className="auth-sub">Sign in to continue to GuruVani</div>

        <form className="auth-form" onSubmit={handleSubmit}>
          <div>
            <label>Email</label>
            <input type="email" value={email} onChange={(e) => setEmail(e.target.value)} required />
          </div>

          <div>
            <label>Password</label>
            <input type="password" value={password} onChange={(e) => setPassword(e.target.value)} required />
          </div>

          <div>
            <label>Role (mock)</label>
            <select value={role} onChange={(e) => setRole(e.target.value as 'customer' | 'guru' | 'admin')}>
              <option value="customer">Customer</option>
              <option value="guru">Guru</option>
              <option value="admin">Admin</option>
            </select>
          </div>

          <div className="auth-actions">
            <button type="submit" className="btn btn-primary">Sign in</button>
            <button type="button" className="btn btn-ghost" onClick={() => navigate('/register')}>Create account</button>
          </div>
        </form>

        <div className="auth-footer">By signing in you agree to our terms of service.</div>
      </div>
    </div>
  );
};

export default Login;
