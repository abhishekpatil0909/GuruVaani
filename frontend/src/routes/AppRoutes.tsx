import React from 'react';
import { Routes, Route, Navigate } from 'react-router-dom';
import Login from '../pages/auth/Login';
import Register from '../pages/auth/Register';
import CustomerDashboard from '../pages/customer/CustomerDashboard';
import GuruDashboard from '../pages/guru/GuruDashboard';
import AdminDashboard from '../pages/admin/AdminDashboard';

export const AppRoutes: React.FC = () => (
  <Routes>
    {/* Public routes */}
    <Route path="/" element={<Navigate to="/login" replace />} />
    <Route path="/login" element={<Login />} />
    <Route path="/register" element={<Register />} />

    {/*
      Customer routes
      TODO: Protect these routes with a role-guard HOC or component, e.g.
      <Route element={<RequireAuth role="customer" />}>
        <Route path="/customer" element={<CustomerDashboard />} />
      </Route>
    */}
    <Route path="/customer" element={<CustomerDashboard />} />

    {/*
      Guru routes
      TODO: Add role-based protection here, e.g.
      <Route element={<RequireAuth role="guru" />}>
        <Route path="/guru" element={<GuruDashboard />} />
      </Route>
    */}
    <Route path="/guru" element={<GuruDashboard />} />

    {/*
      Admin routes
      TODO: Add admin-only guard here later, e.g.
      <Route element={<RequireAuth role="admin" />}>
        <Route path="/admin" element={<AdminDashboard />} />
      </Route>
    */}
    <Route path="/admin" element={<AdminDashboard />} />
  </Routes>
);

export default AppRoutes;
