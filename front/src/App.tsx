import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { UserEntity } from './models/UserEntity';

const App: React.FC = () => {
  const [user, setUser] = useState<UserEntity | null>(null);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    // Fetch the user data from the API
    axios.get<UserEntity>('http://localhost:5146/api/users/1')
      .then(response => {
        setUser(response.data);
        setLoading(false);
      })
      .catch(error => {
        console.error(error);
        setError("Error fetching user data");
        setLoading(false);
      });
  }, []);

  if (loading) {
    return <div>Loading...</div>;
  }

  if (error) {
    return <div>{error}</div>;
  }

  if (!user) {
    return <div>No user data available</div>;
  }

  return (
    <div>
      <h1>User Profile</h1>
      <p><strong>Name:</strong> {user.name || "Unknown"}</p>
      <p><strong>Telegram Nick:</strong> {user.telegramNick}</p>
      <p><strong>Email:</strong> {user.email || "Unknown"}</p>
      <p><strong>Phone Number:</strong> {user.phoneNumber || "Unknown"}</p>
      <p><strong>Birth Date:</strong> {user.birthDate ? new Date(user.birthDate).toLocaleDateString() : "Unknown"}</p>
      <p><strong>Trips Count:</strong> {user.tripsCount}</p>
      <p><strong>Discount:</strong> {user.discount || 0}%</p>
      <p><strong>Subscription:</strong> {user.isSubscriptionActive ? `Active (${user.subscriptionType})` : "Inactive"}</p>
      {/* Add more fields as necessary */}
    </div>
  );
}

export default App;
