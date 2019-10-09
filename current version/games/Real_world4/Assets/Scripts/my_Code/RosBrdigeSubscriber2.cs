using System.Collections;
using ROSBridgeLib.geometry_msgs;
using SimpleJSON;
using System;
using UnityEngine;
using ROSBridgeLib;


/**
 * This defines a subscriber. Subscribers listen to publishers in ROS. Now if we could have inheritance
 * on static classes then we could do this differently. But basically, you have to make up one of these
 * for every subscriber you need.
 * 
 * Subscribers require a ROSBridgePacket to subscribe to (its type). They need the name of
 * the message, and they need something to draw it. 
 * 
 * Version History
 * 3.1 - changed methods to start with an upper case letter to be more consistent with c#
 * style.
 * 3.0 - modification from hand crafted version 2.0
 * 
 * @author Michael Jenkin, Robert Codd-Downey and Andrew Speers
 * @version 3.1
 */


public class ROSBrdigeSubscriber2: ROSBridgeSubscriber
    {

    public string identity="test";
        public string topic;
        // public string Coords { get { return coords; } set { coords = value; } }
        //static ROSBridgeSubscriber ros = new ROSBridgeSubscriber();

        /*  public string Topic

             { get { return topic; } set { topic = value; } }*/


        public string GetMessageTopic()
        {
            return topic;
        }

        public static string GetMessageType()
        {
            return "geometry_msgs/PoseStamped";
        }

        public static string ParseMessage(JSONNode msg)
        {
            //Debug.Log("msg:"+ msg.ToString());
            //ROSBridgeMsg mm = new PoseMsg(msg);
            //Debug.Log(mm.ToString());
            // CallBack(mm); 
            return msg.ToString();

        }



        public void CallBack(string msg)
        {

            identity = msg;
            //  Debug.Log(topic);
            //Debug.Log("CallBack():      "+ coords);
            //return msg;

        }


    }
