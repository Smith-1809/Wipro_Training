const express = require('express');
const cors = require('cors');
const app = express();
const PORT = 5000;

// Middleware
app.use(cors()); // Allow requests from our React frontend
app.use(express.json()); // Parse JSON bodies

// In-memory "Database"
let users = [
    { id: 1, name: 'Alice Johnson', email: 'alice@example.com' },
    { id: 2, name: 'Bob Smith', email: 'bob@example.com' }
];

// Routes
// GET: Fetch all users
app.get('/users', (req, res) => {
    res.json(users);
});

// POST: Add a new user
app.post('/users', (req, res) => {
    const newUser = {
        id: users.length + 1,
        name: req.body.name,
        email: req.body.email
    };
    users.push(newUser);
    res.status(201).json(newUser);
});

app.listen(PORT, () => {
    console.log(`Backend Server running on http://localhost:${PORT}`);
});