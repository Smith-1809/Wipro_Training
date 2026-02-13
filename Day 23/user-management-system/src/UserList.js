import React, { Component } from 'react';

class UserList extends Component {
  // Lifecycle method example (runs when component mounts)
  componentDidMount() {
    console.log("UserList Component Mounted");
  }

  render() {
    const { users } = this.props; // Destructuring props

    return (
      <div style={{ border: '1px solid #ccc', padding: '10px', marginTop: '20px' }}>
        <h3>User List (Class Component)</h3>
        {users.length === 0 ? (
          <p>No users found.</p>
        ) : (
          <ul>
            {users.map((user) => (
              <li key={user.id}>
                <strong>{user.name}</strong> - {user.email}
              </li>
            ))}
          </ul>
        )}
      </div>
    );
  }
}

export default UserList;