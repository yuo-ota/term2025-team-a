// const express = require("express");
// const http = require("http");
// const WebSocket = require("ws");

// const app = express();
// app.use(express.json());

// const server = http.createServer(app);
// const wss = new WebSocket.Server({ server });

// let clients = [];

// // UnityのWebSocket
// wss.on("connection", (ws) => {
//     console.log("WebSocket connected");
//     clients.push(ws);

//     ws.on("close", () => {
//         clients = clients.filter(c => c !== ws);
//     });
// });

// // M5StackのHTTP
// app.post("/api/m5data", (req, res) => {
//     console.log("M5Stackから来た:", req.body);

//     const { temperature, humidity } = req.body;

//     const timestamp = new Date().toISOString();

//     // Unity 用データに変換
//     const messages = [];

//     if (typeof temperature === "number") {
//         messages.push({
//             type: "temperature",
//             value: temperature,
//             timestamp: timestamp
//         });
//     }

//     if (typeof humidity === "number") {
//         messages.push({
//             type: "humidity",
//             value: humidity,
//             timestamp: timestamp
//         });
//     }

//     // WebSocketでUnity に送信
//     messages.forEach(msg => {
//         clients.forEach(ws => {
//             if (ws.readyState === WebSocket.OPEN) {
//                 ws.send(JSON.stringify(msg));
//             }
//         });
//     });

//     res.sendStatus(200);
// });

// server.listen(3000, () => {
//     console.log("Backend listening on port 3000");
// });

const express = require("express");
const http = require("http");
const WebSocket = require("ws");

const app = express();
app.use(express.json());

const server = http.createServer(app);
const wss = new WebSocket.Server({ server });

let clients = [];

// UnityのWebSocket接続
wss.on("connection", (ws) => {
    console.log("WebSocket connected");
    clients.push(ws);

    ws.on("close", () => {
        clients = clients.filter(c => c !== ws);
    });
});

// M5Stackからデータ受信
app.post("/api/m5data", (req, res) => {
    console.log("M5Stackから来た:", req.body);

    const { temperature, humidity, light } = req.body;

    const timestamp = new Date().toISOString();

    // Unity用データ
    const messages = [];

    // 温度
    if (typeof temperature === "number") {
        messages.push({
            type: "temperature",
            value: temperature,
            timestamp: timestamp
        });
    }

    // 湿度
    if (typeof humidity === "number") {
        messages.push({
            type: "humidity",
            value: humidity,
            timestamp: timestamp
        });
    }

    // 明るさ（照度）
    if (typeof light === "number") {
        messages.push({
            type: "brightness",
            value: light,
            timestamp: timestamp
        });
    }

    // Unityへ送信
    messages.forEach(msg => {
        clients.forEach(ws => {
            if (ws.readyState === WebSocket.OPEN) {
                ws.send(JSON.stringify(msg));
            }
        });
    });

    res.sendStatus(200);
});

server.listen(3000, () => {
    console.log("Backend listening on port 3000");
});