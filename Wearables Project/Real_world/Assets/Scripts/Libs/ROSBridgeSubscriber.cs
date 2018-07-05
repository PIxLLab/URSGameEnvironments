using System.Collections;
using ROSBridgeLib.geometry_msgs;
using SimpleJSON;
using System;
using UnityEngine;


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

namespace ROSBridgeLib {
	public class ROSBridgeSubscriber {

		public static string GetMessageTopic() {
            return "/uav0/ground_truth_to_tf/pose";
        }  

		public static string GetMessageType() {
            return "geometry_msgs/PoseStamped";
        }

		public static ROSBridgeMsg ParseMessage(JSONNode msg) {
            Debug.Log("msg:"+ msg);
           
            return new PoseMsg(msg);

        }

		public static void CallBack(ROSBridgeMsg msg) {
            Debug.Log("CallBack()");

            Debug.Log(msg.ToString());
        }
	}
}

