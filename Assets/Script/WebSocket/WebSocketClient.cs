using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebSocketClient : MonoBehaviour
{
    MyWebSocket ws;
    void Start()
    {
        ws = new MyWebSocket("wss://echo.websocket.org");
        
        ws.OnOpen += () => Debug.Log("Connected");
        ws.OnMessage += msg => Debug.Log("Received: " + msg);
        ws.OnClose += () => Debug.Log("Closed");
        
        ws.Send("Hello from Unity!");
    }
    
}
