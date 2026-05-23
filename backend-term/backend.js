const express = require("express");
const http = require("http");
const WebSocket = require("ws");

const app = express();
app.use(express.json());

const server = http.createServer(app);
const wss = new WebSocket.Server({ server });

let clients = [];

const SEND_SPAN_MS = 1500;

// WebSocket ブロードキャスト用ヘルパー
const broadcast = (msg) => {
    clients.forEach(ws => {
        if (ws.readyState === WebSocket.OPEN) {
            ws.send(JSON.stringify(msg));
        }
    });
};

// UnityのWebSocket
wss.on("connection", (ws) => {
    console.log("WebSocket connected");
    clients.push(ws);

    ws.on("close", () => {
        clients = clients.filter(c => c !== ws);
    });
});

// M5StackのHTTP
app.post("/api/m5data", (req, res) => {
    console.log("M5Stackから来た:", req.body);

    const { temperature, humidity } = req.body;

    const timestamp = new Date().toISOString();

    // Unity 用データに変換
    const messages = [];

    if (typeof temperature === "number") {
        messages.push({
            type: "temperature",
            value: temperature,
            timestamp: timestamp
        });
    }

    if (typeof humidity === "number") {
        messages.push({
            type: "humidity",
            value: humidity,
            timestamp: timestamp
        });
    }

    // WebSocketでUnity に送信（temperature→humidity の順で少し間隔を空ける）
    messages.forEach((msg, index) => {
        setTimeout(() => broadcast(msg), index * SEND_SPAN_MS);
    });

    res.sendStatus(200);
});

server.listen(3000, () => {
    console.log("Backend listening on port 3000");
});
