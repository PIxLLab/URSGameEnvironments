using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Rosbridge.Client;
using System.Net.Sockets;
using System;
using System.Security.Policy;
using ROSBridgeLib;

public class ros2 : MonoBehaviour {
    private ROSBridgeWebSocketConnection ros = null;

    // Use this for initialization

   

    void Start () {
        // Where the rosbridge instance is running, could be localhost, or some external IP

      

        ros = new ROSBridgeWebSocketConnection("ws://spitfire.cs.nmsu.edu", 9090);
      //  ros = new ROSBridgeWebSocketConnection("ws://128.123.63.68", 9090);



        // Add subscribers and publishers (if any)
        ros.AddSubscriber(typeof(ROSBridgeSubscriber));
        
       // ros.AddPublisher(typeof(BallControlPublisher));

        // Fire up the subscriber(s) and publisher(s)
        ros.Connect();
        
    }

    // Extremely important to disconnect from ROS. Otherwise packets continue to flow
    void OnApplicationQuit()
    {
        if (ros != null)
        {
            ros.Disconnect();
        }
    }
    // Update is called once per frame in Unity
    void Update()
    {
        
        ros.Render();
        
        
        
       
        
    }

    
    

}
	
	
