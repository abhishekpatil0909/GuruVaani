import React, { useEffect, useState } from "react";
import { motion } from "framer-motion";
import  "./CustomerDAshboard.css";

interface Booking {
  id: number;
  guruName: string;
  date: string;
  time: string;
  status: string;
}

const CustomerDashboard: React.FC = () => {
  const [bookings, setBookings] = useState<Booking[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    setTimeout(() => {
      setBookings([
        {
          id: 1,
          guruName: "Astro Guru Rahul",
          date: "2026-01-20",
          time: "10:00 AM - 10:30 AM",
          status: "Confirmed",
        },
        {
          id: 2,
          guruName: "Vastu Expert Meera",
          date: "2026-01-22",
          time: "02:00 PM - 02:30 PM",
          status: "Pending",
        },
      ]);
      setLoading(false);
    }, 800);
  }, []);

  return (
    <motion.div
      className="customer-dashboard"
      initial={{ opacity: 0, y: 20 }}
      animate={{ opacity: 1, y: 0 }}
      transition={{ duration: 0.6 }}
    >
      <motion.h2
        initial={{ opacity: 0, x: -20 }}
        animate={{ opacity: 1, x: 0 }}
        transition={{ delay: 0.2 }}
      >
        Welcome 👋
      </motion.h2>

      <p className="subtitle">Your bookings overview</p>

      {/* Stats Cards */}
      <div className="stats">
        {[
          { label: "Total Bookings", value: bookings.length },
          {
            label: "Upcoming",
            value: bookings.filter((b) => b.status === "Confirmed").length,
          },
          {
            label: "Pending",
            value: bookings.filter((b) => b.status === "Pending").length,
          },
        ].map((item, index) => (
          <motion.div
            key={item.label}
            className="card"
            whileHover={{ scale: 1.05 }}
            initial={{ opacity: 0, y: 30 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ delay: index * 0.15 }}
          >
            <h3>{item.label}</h3>
            <span>{item.value}</span>
          </motion.div>
        ))}
      </div>

      {/* Booking Table */}
      <motion.div
        className="booking-section"
        initial={{ opacity: 0 }}
        animate={{ opacity: 1 }}
        transition={{ delay: 0.6 }}
      >
        <h3>My Bookings</h3>

        {loading ? (
          <motion.p
            animate={{ opacity: [0.3, 1, 0.3] }}
            transition={{ repeat: Infinity, duration: 1.2 }}
          >
            Loading bookings...
          </motion.p>
        ) : bookings.length === 0 ? (
          <p>No bookings yet</p>
        ) : (
          <table>
            <thead>
              <tr>
                <th>Guru</th>
                <th>Date</th>
                <th>Time</th>
                <th>Status</th>
              </tr>
            </thead>
            <tbody>
              {bookings.map((booking, i) => (
                <motion.tr
                  key={booking.id}
                  initial={{ opacity: 0, y: 10 }}
                  animate={{ opacity: 1, y: 0 }}
                  transition={{ delay: i * 0.1 }}
                >
                  <td>{booking.guruName}</td>
                  <td>{booking.date}</td>
                  <td>{booking.time}</td>
                  <td>
                    <span
                      className={`status ${booking.status.toLowerCase()}`}
                    >
                      {booking.status}
                    </span>
                  </td>
                </motion.tr>
              ))}
            </tbody>
          </table>
        )}
      </motion.div>
    </motion.div>
  );
};

export default CustomerDashboard;
