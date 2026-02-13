const express = require("express");
const http = require("http");
const { Server } = require("socket.io");
const cors = require("cors");

const app = express();
const server = http.createServer(app);

const io = new Server(server, {
  cors: { origin: "*" },
});

app.use(cors());
app.use(express.json());

let tasks = [];
let users = [];

io.on("connection", (socket) => {
  console.log("User connected:", socket.id);

  // Send existing tasks
  socket.emit("loadTasks", tasks);

  socket.on("addTask", (task) => {
    tasks.push(task);
    io.emit("taskAdded", task);

    if (task.assignee) {
      io.emit("userNotified", {
        message: `New task assigned to ${task.assignee}`,
      });
    }
  });

  socket.on("updateTask", (updatedTask) => {
    tasks = tasks.map((t) => (t.id === updatedTask.id ? updatedTask : t));
    io.emit("taskUpdated", updatedTask);
  });

  socket.on("deleteTask", (taskId) => {
    tasks = tasks.filter((t) => t.id !== taskId);
    io.emit("taskDeleted", taskId);
  });

  socket.on("disconnect", () => {
    console.log("User disconnected:", socket.id);
  });
});

server.listen(5000, () =>
  console.log(" Server running on http://localhost:5000")
);