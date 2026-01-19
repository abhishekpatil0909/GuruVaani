import React from 'react'
import { Navigate } from 'react-router-dom'
import useAuth from '../hooks/useAuth'

type ProtectedRouteProps = {
  children: JSX.Element
}

const ProtectedRoute: React.FC<ProtectedRouteProps> = ({ children }) => {
  const { user } = useAuth()

  // If no user in context, redirect to login
  if (!user) {
    return <Navigate to="/login" replace />
  }

  return children
}

export default ProtectedRoute
