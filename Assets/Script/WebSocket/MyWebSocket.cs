using System;
using System.Runtime.InteropServices;

public class MyWebSocket
{
    [DllImport("__Internal")]
    private static extern string MyWS_Create(string url);

    [DllImport("__Internal")]
    private static extern void MyWS_Send(string id, string message);

    [DllImport("__Internal")]
    private static extern void MyWS_Close(string id);

    public event Action OnOpen;
    public event Action<string> OnMessage;
    public event Action OnClose;

    private readonly string _id;

    public MyWebSocket(string url)
    {
        _id = MyWS_Create(url);
    }

    public void Send(string message)
    {
        MyWS_Send(_id, message);
    }

    public void Close()
    {
        MyWS_Close(_id);
    }

    // internal callbacks
    internal void HandleOpen() => OnOpen?.Invoke();
    internal void HandleMessage(string msg) => OnMessage?.Invoke(msg);
    internal void HandleClose() => OnClose?.Invoke();
}