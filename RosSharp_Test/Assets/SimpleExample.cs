using System;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
public class SimpleExample : MonoBehaviour {
    Telepathy.Client client = new Telepathy.Client(1024);
    Telepathy.Server server = new Telepathy.Server(1024);

    void Awake() {
        // update even if window isn't focused, otherwise we don't receive.
        Application.runInBackground = true;

        // use Debug.Log functions for Telepathy so we can see it in the console
        Telepathy.Log.Info = Debug.Log;
        Telepathy.Log.Warning = Debug.LogWarning;
        Telepathy.Log.Error = Debug.LogError;

        // hook up events
        client.OnConnected = () => Debug.Log("Client Connected");
        client.OnData = (message) => Debug.Log("Client Data: " + Encoding.UTF8.GetString(message));
        client.OnDisconnected = () => Debug.Log("Client Disconnected");

        server.OnConnected = (connectionId) => Debug.Log(connectionId + " Connected");
        server.OnData = (connectionId, message) => Debug.Log(connectionId + " Data: " + BitConverter.ToString(message.Array, message.Offset, message.Count));
        server.OnDisconnected = (connectionId) => Debug.Log(connectionId + " Disconnected");
        client.Connect("192.168.0.27", 8079);
    }
    float lastSendTime = 0;

    void Update() {
        // send data to server every 5 seconds
        if (Time.time - lastSendTime > 5) {
            lastSendTime = Time.time;
            byte[] message = Encoding.UTF8.GetBytes("Hello World");
            client.Send(new ArraySegment<byte>(message));
        }
        // client
        if (client.Connected) {
            // send message on key press
            if (Input.GetKeyDown(KeyCode.Alpha2)) {

                client.Send(new ArraySegment<byte>(new byte[] { 0x1 }));
            }
        }

        // tick to process messages
        // (even if not connected so we still process disconnect messages)
        client.Tick(100);

        // server
        if (server.Active) {
            if (Input.GetKeyDown(KeyCode.Space))
                server.Send(0, new ArraySegment<byte>(new byte[] { 0x2 }));

        }

        // tick to process messages
        // (even if not active so we still process disconnect messages)
        server.Tick(100);
    }

    // void OnGUI() {
    //     // client
    //     GUI.enabled = !client.Connected;
    //     if (GUI.Button(new Rect(0, 0, 120, 20), "Connect Client"))
    //         client.Connect("192.168.0.27", 8081);

    //     GUI.enabled = client.Connected;
    //     if (GUI.Button(new Rect(130, 0, 120, 20), "Disconnect Client"))
    //         client.Disconnect();

    //     // server
    //     GUI.enabled = !server.Active;
    //     if (GUI.Button(new Rect(0, 25, 120, 20), "Start Server"))
    //         server.Start(1337);

    //     GUI.enabled = server.Active;
    //     if (GUI.Button(new Rect(130, 25, 120, 20), "Stop Server"))
    //         server.Stop();

    //     GUI.enabled = true;
    // }

    void OnApplicationQuit() {
        // the client/server threads won't receive the OnQuit info if we are
        // running them in the Editor. they would only quit when we press Play
        // again later. this is fine, but let's shut them down here for consistency
        client.Disconnect();
        server.Stop();
    }
}