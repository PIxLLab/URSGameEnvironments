using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Rosbridge.Client;
using System.Net.Sockets;
using System;
using System.Security.Policy;
using ROSBridgeLib;
using System.Xml;
using UnityEngine;
using System.IO;
using System.Threading;


public class ros2 : MonoBehaviour
{
    private ROSBridgeWebSocketConnection ros = null;
   
   
    string network; int port;public static int id = 0;
    void Awake()
    {
        System.Random n = new System.Random();
        id = n.Next(1, 10000);
        string filepath = @"C:\Users\zahra\Desktop\configurations\config3.xml";

        XmlDocument xmlDoc = new XmlDocument();
        if (File.Exists(filepath))
        {
            xmlDoc.Load(filepath);
            XmlNodeList transformList = xmlDoc.GetElementsByTagName("Topic");
            XmlNodeList transformList10 = xmlDoc.GetElementsByTagName("network");
            foreach (XmlNode xn10 in transformList10)
            {
                network = xn10.InnerText.ToString();

            }
            XmlNodeList transformList11 = xmlDoc.GetElementsByTagName("port");
            foreach (XmlNode xn11 in transformList11)
            {
                port = Convert.ToInt32(xn11.InnerText);

            }
        }

        ros = new ROSBridgeWebSocketConnection(network, port);
        ros.AddPublisher(typeof(publisher));
        ros.AddPublisher(typeof(publisher2));
        ros.AddPublisher(typeof(publisher3));
        ros.Connect();
        Thread.Sleep(1000);
        
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
        

        ROSBridgeLib.std_msgs.StringMsg ms = new ROSBridgeLib.std_msgs.StringMsg(Person_movement.p_location);
        //Debug.Log(ms);
        ros.Publish(publisher.GetMessageTopic(), ms);
      //  Thread.Sleep(100);
        ROSBridgeLib.std_msgs.StringMsg ms2 = new ROSBridgeLib.std_msgs.StringMsg(Person_movement.playerpointaddition);
        ////Debug.Log(ms2);
        ROSBridgeLib.std_msgs.StringMsg ms3 = new ROSBridgeLib.std_msgs.StringMsg(Person_movement.playerpointdeductdz);

        if (Person_movement.playerpointadditionbool == true)
        {
            ros.Publish(publisher2.GetMessageTopic(), ms2);
           // Debug.Log(ms2);
            Person_movement.playerpointadditionbool = false;

        }
        if (Person_movement.playerpointdeductionbool == true)
        {
            ros.Publish(publisher3.GetMessageTopic(), ms3);
            Person_movement.playerpointdeductionbool = false;

        }

        ros.Render();



    }




}


