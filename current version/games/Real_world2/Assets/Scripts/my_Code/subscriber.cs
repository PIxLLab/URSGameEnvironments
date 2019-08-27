using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ROSBridgeLib;
using SimpleJSON;
using ROSBridgeLib.geometry_msgs;


public class subscriber : MonoBehaviour {

    public class RealsenseImageSubscriber : ROSBridgeSubscriber
    {
        static GameObject ball;

        void Start() { ball = GameObject.FindWithTag("drone"); Debug.Log("inside subscriber"); }

        public new static string GetMessageTopic()
        {
            
            return "/uav3/ground_truth_to_tf/pose";
        }

        public new static string GetMessageType()
        {
            
            return "";
        }

        // Important function (I think, converting json to PoseMsg)
        public new static ROSBridgeMsg ParseMessage(JSONNode msg)
        {
            Debug.Log("msg");
            return new PoseMsg(msg);
        }

        // This function should fire on each ros message
        public new static void CallBack(ROSBridgeMsg msg)
        {

            // Update ball position, or whatever
            //ball.transform.position = new Vector3(msg.x,msg.y,msg.z); // Check msg definition in rosbridgelib
            Debug.Log(msg.ToString());
        }
    }

    
}
