using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ROSBridgeLib;
using SimpleJSON;
using ROSBridgeLib.geometry_msgs;

public class publisher4 : ROSBridgePublisher
{
    public static string GetMessageTopic()
    {
        return "/w_clue_numbers";
    }

    public static string GetMessageType()
    {
        return "std_msgs/String";
    }

    public static string ToYAMLString(ROSBridgeLib.std_msgs.StringMsg msg)
    {
        return msg.ToYAMLString();
    }
}