using WebSocketSharp;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;


// WebsocketAPIで通信するためのメッセージ(クライアント→サーバー)の型
[Serializable]
public class WebSocketMessage
{
    public string type;
    public object data;
}

// WebsocketAPIで通信するためのメッセージ(サーバー→クライアント)の型
[Serializable]
public class ConnectionResponse
{
    public string type;
    public ConnectionData data;

    [Serializable]
    public class ConnectionData
    {
        public string message;
    }
}

// WebSocketManager: WebSocketの接続・送受信・イベントハンドリングを管理
public class WebSocketManager : MonoBehaviour
{
    private WebSocket ws;
    private string serverUrl = "ws://localhost:8080/ws/test";

    // メインスレッド実行用
    private readonly Queue<Action> _mainThreadActions = new Queue<Action>();
    private readonly object _actionsLock = new object();

    // 再接続設定
    private int maxRetryAttempts = 3;
    private int retryCount = 0;
    private float retryDelay = 3f;

    void Start()
    {
        // WebSocketの接続を開始
        ConnectToServer();
    }

    void Update()
    {
        // メインスレッドで実行するアクションを処理
        ProcessMainThreadActions();
    }

    void ConnectToServer()
    {
        Thread thread = new Thread(() =>
        {
            try
            {
                ws = new WebSocket(serverUrl);

                ws.OnOpen += OnWebSocketOpen;
                ws.OnMessage += OnWebSocketMessage;
                ws.OnError += OnWebSocketError;
                ws.OnClose += OnWebSocketClose;

                ws.Connect();

            }
            catch (Exception ex)
            {
                Debug.LogError($"Connection thread error: {ex.Message}");
            }
        });

        thread.IsBackground = true;
        thread.Start();
    }


    // WebSocketの接続が開いた
    void OnWebSocketOpen(object sender, EventArgs e)
    {
        Debug.Log("WebSocket接続成功");
        retryCount = 0; // 接続成功でリセット

        // WebsocketAPIで通信するためのメッセージ(クライアント→サーバー)を構築
        string message = "{\"type\":\"connection_check\",\"data\":\"this is test.\"}";

        // メッセージを送信
        ws.Send(message);
    }

    // WebSocketのメッセージを受信
    void OnWebSocketMessage(object sender, MessageEventArgs e)
    {
        // メインスレッドで実行するアクションを処理
        lock (_actionsLock) {
            // メインスレッドで実行するアクションを処理
            _mainThreadActions.Enqueue(() => {
                // メッセージを処理
                ProcessMessage(e.Data);
            });
        }
    }


    // WebSocketのエラーを受信
    void OnWebSocketError(object sender, ErrorEventArgs e)
    {
        Debug.LogError($"WebSocket エラー: {e.Message}");
    }

    // WebSocketの接続が切れた
    void OnWebSocketClose(object sender, CloseEventArgs e)
    {
        Debug.LogWarning($"WebSocket 切断: {e.Code}, {e.Reason}");

        if (retryCount < maxRetryAttempts)
        {
            // 再接続試行回数を増やす
            retryCount++;
            Debug.Log($"再接続試行 {retryCount}/{maxRetryAttempts} - {retryDelay}秒後");
            // 再接続を試行
            Invoke(nameof(RetryConnection), retryDelay);
        }
        else
        {
            Debug.LogError("最大再接続試行回数に達しました");
            HandleConnectionFailure();
        }
    }

    // WebSocketの再接続を試行
    void RetryConnection()
    {
        if (ws?.ReadyState != WebSocketState.Open)
        {
            Debug.Log("WebSocket再接続中...");
            ConnectToServer();
        }
    }

    // WebSocketの接続に失敗した
    void HandleConnectionFailure()
    {
        Debug.LogError("WebSocket接続に失敗しました");
        // 必要であればUI表示やオフラインモード遷移など
    }

    // メインスレッドで実行するアクションを処理
    void ProcessMainThreadActions()
    {
        // メインスレッドで実行するアクションがない場合は処理を終了
        if (_mainThreadActions.Count == 0) return;
        // メインスレッドで実行するアクションを処理
        lock (_actionsLock)
        {
            while (_mainThreadActions.Count > 0)
            {
                // メインスレッドで実行するアクションを取得
                var action = _mainThreadActions.Dequeue();
                try {
                    // メインスレッドで実行するアクションを実行
                    action?.Invoke();
                } catch (Exception ex) {
                    Debug.LogError($"メインスレッドアクションエラー: {ex.Message}");
                }
            }
        }
    }

    // WebSocketのメッセージを処理
    void ProcessMessage(string message)
    {
        try
        {
            var typeCheck = JsonUtility.FromJson<WebSocketMessage>(message);
            switch (typeCheck.type)
            {
                case "connection_response":
                    // WebSocketの接続応答メッセージを処理
                    HandleConnectionResponse(message);
                    break;
                case "server_ping":
                    HandlePingResponse(message);
                    break;
                default:
                    Debug.LogWarning($"未知のメッセージタイプ: {typeCheck.type}");
                    break;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"メッセージ解析エラー: {ex.Message}");
        }
    }

    // WebSocketの接続応答メッセージを処理
    void HandleConnectionResponse(string message)
    {
        // WebSocketの接続応答メッセージを処理
        var response = JsonUtility.FromJson<ConnectionResponse>(message);
        Debug.Log($"接続確認完了: {response.data.message}");
    }

    // WebSocketの接続応答メッセージを処理
    void HandlePingResponse(string message)
    {
        // WebSocketの接続応答メッセージを処理
        var response = JsonUtility.FromJson<ConnectionResponse>(message);
        Debug.Log($"定期実行確認: {response.data.message}");
    }

    // アプリがポーズされた
    void OnApplicationPause(bool pauseStatus)
    {
        if (!pauseStatus)
        {
            Debug.Log("アプリ再開 - 接続状態を確認");
            // WebSocketの接続状態を確認
            CheckConnectionStatus();
        }
    }

    // アプリがフォーカスされた
    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            Debug.Log("フォーカス獲得 - 接続状態を確認");
            // WebSocketの接続状態を確認
            CheckConnectionStatus();
        }
    }

    // WebSocketの接続状態を確認
    void CheckConnectionStatus()
    {
        if (ws?.ReadyState != WebSocketState.Open)
        {
            Debug.Log("接続が失われています - 再接続を試行");
            // WebSocketの接続を再試行
            ConnectToServer();
        }
    }

    // WebSocketの接続を終了
    void OnDestroy()
    {
        if (ws != null)
        {
            try
            {
                // WebSocketの接続状態を確認
                if (ws.ReadyState == WebSocketState.Open)
                {
                    // WebSocketの接続を終了
                    ws.Close();
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"WebSocket終了エラー: {ex.Message}");
            }
            finally
            {
                ws = null;
            }
        }
    }
}
