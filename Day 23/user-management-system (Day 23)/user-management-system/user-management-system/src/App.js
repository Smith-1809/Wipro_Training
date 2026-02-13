import React, { useState, useEffect } from 'react';
import UserList from './UserList';
import UserForm from './UserForm';

const App = () => {
  const [users, setUsers] = useState([]);

  // Fetch users from Node.js backend on load
  useEffect(() => {
    fetch('http://localhost:5000/users')
      .then((res) => res.json())
      .then((data) => setUsers(data))
      .catch((err) => console.error("Error fetching users:", err));
  }, []);

  // Callback to update state when a new user is added via the form
  const handleUserAdded = (newUser) => {
    setUsers([...users, newUser]);
  };

  return (
    <div style={{ padding: '20px', fontFamily: 'Arial' }}>
      <h1>User Management System</h1>
      <p>Backend: Node/Express | Frontend: React (Webpack/Babel)</p>
      
      {/* Functional Component for Adding */}
      <UserForm onUserAdded={handleUserAdded} />

      {/* Class Component for Displaying */}
      <UserList users={users} />
    </div>
  );
};

export default App;