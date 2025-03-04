﻿using System.Collections;
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
using System.Linq;
using Assets.Scripts;
using Newtonsoft.Json;
using System.Text;

public class ros2 : MonoBehaviour {
    private ROSBridgeWebSocketConnection ros = null;
    private ROSBridgeWebSocketConnection roos2 = null;
    public ROSBridgeWebSocketConnection roos3 = null;
    public Dictionary<GameObject, drone> npcList = new Dictionary<GameObject, drone>();
    // Use this for initialization
    public  ROSBridgeSubscriber[] allsubscribers;
    int buildcheck;
    string[] builds = new string[35];
    public GameObject[] drones2;
    //config c = new config();
    public static int number_of_Drones;
    public string[] Message_Topics;
    int counter = 0;
    public  drone[] drones ;
    Vector3 drco;
    string network;int port;
    string total_battery_value;
    public static string playerid; double currentbattery;
    int minutecounter;bool deductbattery;
    void Awake () {
        // Where the rosbridge instance is running, could be localhost, or some external IP

        string filepath = @"C:\Users\GDC-Admin\Desktop\newsetting\config2.xml";

        XmlDocument xmlDoc = new XmlDocument();
        if (File.Exists(filepath))
        {
            xmlDoc.Load(filepath);



            XmlNodeList transformList = xmlDoc.GetElementsByTagName("Topic");
            number_of_Drones = transformList.Count;
            
            Message_Topics = new string[number_of_Drones];
            drones = new drone[number_of_Drones];
            foreach (XmlNode xn in transformList)
            {
                Message_Topics[counter] = xn.InnerText.ToString();
                counter++;
            }
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
        
        //  ros = new ROSBridgeWebSocketConnection("ws://128.123.63.68", 9090);
        allsubscribers = new ROSBridgeSubscriber[number_of_Drones];
        /*---------------Movaghat-------------------*/
        roos2 = new ROSBridgeWebSocketConnection(network, port);
        RSB2 rs2 = new RSB2();
        roos2.Connect();
        Thread.Sleep(1000);
        roos2._ws.Send(ROSBridgeMsg.Subscribe("/w_player_id", "std_msgs/String"));
        roos2._subscribers2.Add(rs2);

        /*---------------Movaghat-------------------*/
        /*---------------Battery---------------*/

        roos3 = new ROSBridgeWebSocketConnection(network, port);
        RSB3 rs3 = new RSB3();
        roos3.Connect();
        Thread.Sleep(1000);
        roos3._ws.Send(ROSBridgeMsg.Subscribe("/urs_wearable/state", "urs_wearable/State" +
            ""));
        roos3._subscribers3.Add(rs3);


        /*-------------Battery----------------*/

        //for(int i=0;i< number_of_Drones; ++i)
        //{
        //    allsubscribers[i] = new ROSBridgeSubscriber();
        //    //allsubscribers[i].Topic = Message_Topics[i] ;
        //    // ros.AddSubscriber(allsubscribers[i]);
        //    //ros._ws.Send(ROSBridgeMsg.Subscribe(Message_Topics[i], "geometry_msgs/PoseStamped"));
        //    Debug.Log(allsubscribers[i].topic);
        //}

        // Add subscribers and publishers (if any)

        // ros.AddSubscriber(typeof(ROSBridgeSubscriber));
        ros.AddPublisher(typeof(publisher));
        ros.AddPublisher(typeof(publisher2));
        ros.AddPublisher(typeof(publisher3));
        ros.AddPublisher(typeof(publisher4));
        ros.AddPublisher(typeof(publisher5));
        ros.AddPublisher(typeof(publisher6));
        ros.AddPublisher(typeof(publisher7));

        // Fire up the subscriber(s) and publisher(s)
        ros.Connect();
        Thread.Sleep(1000);
       
        for (int i = 0; i < number_of_Drones; ++i)
        {
            allsubscribers[i] = new ROSBridgeSubscriber();
            
            allsubscribers[i].topic = Message_Topics[i] ;
            // ros.AddSubscriber(allsubscribers[i]);
            ros._ws.Send(ROSBridgeMsg.Subscribe(Message_Topics[i], "geometry_msgs/PoseStamped"));
            ros._subscribers.Add(allsubscribers[i]);
           // Debug.Log(allsubscribers[i].topic);
        }

        drones2 = new GameObject[number_of_Drones];
       

        for (int i = 0; i < number_of_Drones; ++i)
        {
             //drones[i] = new drone();
            
            drco = new Vector3((float)0, (float)1, (float)0);

            drones2[i] = Instantiate(Resources.Load("ddddrone"), drco, Quaternion.Euler(-90, 0, 90)) as GameObject;
            
            drones[i] = drones2[i].AddComponent<drone>();
            //drones2[i].GetComponent<drone>();
            drones[i].drone_subscriber = allsubscribers[i];
            //drones[i].Init();
            // npcList.Add(drones2[i], drones[i]);
            

        }




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
    void FixedUpdate()
    {
        if(minutecounter==50)
        {  deductbattery = true;minutecounter = 0; }
        else { deductbattery = false; }
        minutecounter++;
        

        //ROSBridgeLib.std_msgs.StringMsg ms = new ROSBridgeLib.std_msgs.StringMsg("checking_connection");
        MyDetail md = new MyDetail();
        MyDetail md2 = new MyDetail();
        ROSBridgeLib.std_msgs.StringMsg ms = new ROSBridgeLib.std_msgs.StringMsg(drone.ddzcoordinations);
            ROSBridgeLib.std_msgs.StringMsg ms5 = new ROSBridgeLib.std_msgs.StringMsg(drone.hdzcoordinations);
            ROSBridgeLib.std_msgs.StringMsg ms6 = new ROSBridgeLib.std_msgs.StringMsg( drone.dccoordinations);
            ROSBridgeLib.std_msgs.StringMsg ms7 = new ROSBridgeLib.std_msgs.StringMsg( drone.hccoordinations);
        if (deductbattery == true)


        {


            for (int i = 0; i < number_of_Drones; ++i)
            {
               
                if (RSB3.hovering.Contains(i))
                {
                   // Debug.Log(i);
                   // Debug.Log(drones[i].battery_value);
                   // Debug.Log(Math.Floor(drones[i].battery_value));
                    md.player_id = Convert.ToInt32(playerid);
                    md.drone_id = i;
                    if (drones[i].check == "ndd")
                    { if (drones[i].battery_value - 0.2 >= 0) { drones[i].battery_value = drones[i].battery_value - 0.2; }
                        else { drones[i].battery_value = 0;  }
                    }
                    else{ if (drones[i].battery_value - 1 >= 0) { drones[i].battery_value = drones[i].battery_value - 1; }
                        else { drones[i].battery_value = 0;  }
                    }
                    // md.topic= "/w_battery_value";
                    md.battery_value = Convert.ToInt32(drones[i].battery_value);
                    StringBuilder sb = new StringBuilder();
                    using (StringWriter sw = new StringWriter(sb))
                    using (JsonTextWriter writer = new JsonTextWriter(sw))
                    {
                        writer.QuoteChar = '\'';

                        JsonSerializer ser = new JsonSerializer();
                        ser.Serialize(writer, md);
                    }
                    // total_battery_value = JsonConvert.SerializeObject(md);
                    total_battery_value = sb.ToString();
                    ROSBridgeLib.std_msgs.StringMsg ms2 = new ROSBridgeLib.std_msgs.StringMsg(total_battery_value);
                    ros.Publish(publisher2.GetMessageTopic(), ms2);
                    //Thread.Sleep(20);
                }
                else
                {
                    if (drones[i].battery_value + 0.2 < 100) { drones[i].battery_value = drones[i].battery_value + 0.2; }
                    md2.player_id = Convert.ToInt32(playerid);
                    md2.drone_id = i;
                    md2.battery_value = Convert.ToInt32(drones[i].battery_value);
                    StringBuilder sb2 = new StringBuilder();
                    using (StringWriter sw2 = new StringWriter(sb2))
                    using (JsonTextWriter writer2 = new JsonTextWriter(sw2))
                    {
                        writer2.QuoteChar = '\'';

                        JsonSerializer ser = new JsonSerializer();
                        ser.Serialize(writer2, md2);
                    }
                    // total_battery_value = JsonConvert.SerializeObject(md);
                    total_battery_value = sb2.ToString();
                    ROSBridgeLib.std_msgs.StringMsg ms2 = new ROSBridgeLib.std_msgs.StringMsg(total_battery_value);
                    ros.Publish(publisher2.GetMessageTopic(), ms2);
                }

            }
        }

            ROSBridgeLib.std_msgs.StringMsg ms3 = new ROSBridgeLib.std_msgs.StringMsg(drone.building_numbers);
           // ROSBridgeLib.std_msgs.StringMsg ms4 = new ROSBridgeLib.std_msgs.StringMsg(drone.drone_clue_value);

        if (drone.ddzpublisher == true)
        {
            ros.Publish(publisher.GetMessageTopic(), ms);
            
        }
        
            
        if (!(builds.Contains(drone.buildcheck)))
        {
            
            ros.Publish(publisher3.GetMessageTopic(), ms3);
            Debug.Log(ms3);
            builds[buildcheck] = drone.buildcheck;
            ++buildcheck;
            


        }
        //if (drone.dcpublisher == true)
        //{
        //    ros.Publish(publisher4.GetMessageTopic(), ms4);
        //}
        if (drone.hdzpublisher == true)
        {
            ros.Publish(publisher5.GetMessageTopic(), ms5);
           
        }
        if (drone.dcpublisher == true)
        {
            ros.Publish(publisher6.GetMessageTopic(), ms6);
            
        }
        if (drone.hcpublisher == true)
        {
            ros.Publish(publisher7.GetMessageTopic(), ms7);
            
        }
        
        //Debug.Log(ms.ToString());




        ros.Render();



    }




}
	
	
