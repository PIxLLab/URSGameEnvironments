using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class protobuff : MonoBehaviour {

    public string serverAddress = "92.168.43.79";
    public int serverPort =8080;

    private TcpClient _client;
    private NetworkStream _stream;
    private Thread _thread;

    private byte[] _buffer = new byte[1024];
    private string receiveMsg = "";
    private bool isConnected = false;

    // Use this for initialization
    void Start () {
        SetupConnection();
        

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnApplicationQuit()
    {
        CloseConnection();
    }

    private void SetupConnection()
    {
        try
        {
            _thread = new Thread(ReceiveData);
            _thread.IsBackground = true;
            _client = new TcpClient(serverAddress, serverPort);
            _stream = _client.GetStream();
            _thread.Start();
            isConnected = true;
            Debug.Log("connected");
        }
        catch (Exception e)
        {
            CloseConnection();
            Debug.Log(e.ToString());
        }
    }

    private void ReceiveData()
    {
        if (!isConnected)
        {
            Debug.Log("not connected");
            return;
        }

        int numberOfBytesRead = 0;
        while (isConnected && _stream.CanRead)
        {
            try
            {
                numberOfBytesRead = _stream.Read(_buffer, 0, _buffer.Length);
                receiveMsg = Encoding.ASCII.GetString(_buffer, 0, numberOfBytesRead);
                _stream.Flush();
                Debug.Log(receiveMsg);
                receiveMsg = "";
            }
            catch (Exception e)
            {
                CloseConnection();
                Debug.Log(e.ToString());
            }
        }
    }

    private void CloseConnection()
    {
        if (isConnected)
        {
            _thread.Interrupt();
            _stream.Close();
            _client.Close();
            isConnected = false;
            receiveMsg = "";
        }
    }
}
