import React, { useState } from 'react';

const UserForm = ({ onUserAdded }) => {
  const [name, setName] = useState('');
  const [email, setEmail] = useState('');

  const handleSubmit = async (e) => {
    e.preventDefault();

    const newUser = { name, email };

    try {
      // POST request to Backend
      const response = await fetch('http://localhost:5000/users', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(newUser)
      });

      if (response.ok) {
        const savedUser = await response.json();
        onUserAdded(savedUser); // Update parent state
        setName('');
        setEmail('');
      }
    } catch (error) {
      console.error("Error adding user:", error);
    }
  };

  return (
    <div style={{ border: '1px solid #007bff', padding: '10px' }}>
      <h3>Add New User (Functional Component)</h3>
      <form onSubmit={handleSubmit}>
        <div style={{ marginBottom: '5px' }}>
          <input
            type="text"
            placeholder="Name"
            value={name}
            onChange={(e) => setName(e.target.value)}
            required
            style={{ marginRight: '10px' }}
          />
          <input
            type="email"
            placeholder="Email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
          />
        </div>
        <button type="submit">Add User</button>
      </form>
    </div>
  );
};

export default UserForm;