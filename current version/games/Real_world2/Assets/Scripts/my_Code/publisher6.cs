using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ROSBridgeLib;
using SimpleJSON;
using ROSBridgeLib.geometry_msgs;

public class publisher6 : ROSBridgePublisher
{

    public static string GetMessageTopic()
    {
        return "/w_dccoordinates";
    }


    public static string GetMessageType()
    {
        return "std_msgs/String";
    }

    public static string ToYAMLString(ROSBridgeLib.std_msgs.StringMsg msg)
    {
        return msg.ToYAMLString();
    }

    /*   public new static ROSBridgeMsg ParseMessage(JSONNode msg)
       {
           return new ROSBridgeLib.std_msgs.StringMsg(msg);
       }*/
}


