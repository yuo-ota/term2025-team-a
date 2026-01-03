const express = require("express");
const http = require("http");
const WebSocket = require("ws");

const app = express();
app.use(express.json());

const server = http.createServer(app);
const wss = new WebSocket.Server({ server });

let clients = [];

// Unity(WebGL) がつなぐ WebSocket
wss.on("connection", (ws) => {
    console.log("WebSocket connected");
    clients.push(ws);

    ws.on("close", () => {
        clients = clients.filter(c => c !== ws);
    });
});

// M5Stack からの HTTP
app.post("/api/m5data", (req, res) => {
    console.log("M5Stackから来た:", req.body);

    // Unityへ中継
    clients.forEach(ws => {
        ws.send(JSON.stringify(req.body));
    });

    res.sendStatus(200);
});

server.listen(3000, () => {
    console.log("Backend listening on port 3000");
});
