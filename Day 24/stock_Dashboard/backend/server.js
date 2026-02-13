const express = require("express");
const http = require("http");
const socketIO = require("socket.io");

const app = express();
const server = http.createServer(app);
const io = socketIO(server, {
  cors: {
    origin: "*",
  },
});

// Mock stock data
let stocks = {
  AAPL: 180,
  TSLA: 250,
  GOOGL: 135,
  MSFT: 320,
};

io.on("connection", (socket) => {
  console.log("Client connected");

  // Send initial data
  socket.emit("stockData", stocks);

  // Update prices every 3 seconds
  const interval = setInterval(() => {
    Object.keys(stocks).forEach((symbol) => {
      stocks[symbol] += Number((Math.random() * 4 - 2).toFixed(2));
    });

    socket.emit("stockData", stocks);
  }, 3000);

  socket.on("disconnect", () => {
    clearInterval(interval);
    console.log("Client disconnected");
  });
});

server.listen(5000, () => {
  console.log("Backend running on http://localhost:5000");
});
