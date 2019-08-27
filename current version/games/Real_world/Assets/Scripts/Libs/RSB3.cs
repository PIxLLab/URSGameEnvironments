using System.Collections;
using ROSBridgeLib.geometry_msgs;
using SimpleJSON;
using System;
using UnityEngine;
using Newtonsoft.Json.Linq;

namespace ROSBridgeLib
{
    public class RSB3
    {

        public static string identity;
       // public static int[] hoveringdrones = new int[4] { 0,1,2,3};
         public static int[] hovering = new int[ros2.number_of_Drones] ;
        
        int counter;
        public string topic;
        // public string Coords { get { return coords; } set { coords = value; } }
        //static ROSBridgeSubscriber ros = new ROSBridgeSubscriber();

        /*  public string Topic

             { get { return topic; } set { topic = value; } }*/


        public string GetMessageTopic3()
        {
            return "/urs_wearable/state";
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

        public static string getBetween(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return "";
            }
        }

        public void CallBack3(string msg)
        {
            for (int i = 0; i < ros2.number_of_Drones; ++i)
            { hovering[i]= 100; }
            counter = 0;
            identity = msg;
           // Debug.Log(identity);

            var test = Newtonsoft.Json.JsonConvert.DeserializeObject(identity);
            JSONNode node = JSONNode.Parse(identity);
            
            var articles = node["predicates"].Childs;
            //Debug.Log(articles.ToString());
            string check;string flag;string drone;
            foreach (var article in articles)
            {
                check = article["type"];
                // Debug.Log(check);
                // Debug.Log(article["hovered"]);
                // Debug.Log(article["truth_value"]);
                drone = getBetween(article["hovered"].ToString(), "value", "}");

                drone = drone.Substring(drone.LastIndexOf(':') + 2);
                drone = drone.TrimEnd('\"');

                if (check == "4")
                {

                    flag = article["truth_value"];
                    if (flag == "true")

                    {
                        drone = getBetween(article["hovered"].ToString(), "value", "}");

                        drone = drone.Substring(drone.LastIndexOf(':') + 2);
                        drone = drone.TrimEnd('\"'); ;
                        hovering[counter] = Convert.ToInt32(drone);
                        ++counter;
                    }
                }
            }
                for(int i=0;i<hovering.Length;++i)
                { //Debug.Log(hovering[i]);
            }
           // Debug.Log("----------");

                       
                
               
            }
            

        }


    }

