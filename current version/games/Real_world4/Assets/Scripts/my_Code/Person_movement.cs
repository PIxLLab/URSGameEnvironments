using Assets.Scripts.my_Code;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Person_movement : MonoBehaviour {

    public Rigidbody rb;
    float speed =5;
    public GameObject[] hcs;
    public GameObject[] hdcs;
    int[] foundclues;
    int foundcluescounter;
	public static int score = 0;
	public  Vector3 move = new Vector3(0, 0, 0);
    float bound_x1, bound_z1;
    float bound_x2, bound_z2;
    public static string p_location;
    double p_loc_x; double p_loc_y;
    public static bool hdz=false;
    public static string playerpointdeductdz;
    public static string playerpointaddition;
    public static bool playerpointadditionbool;
    public static bool playerpointdeductionbool;
    int stop = 0;int treasurefound; int checkcollision; int checkhdz; bool clue;

    void Start () {

        rb = GetComponent<Rigidbody>();
       
        rb.freezeRotation = true;
        p_loc_x= (rb.transform.position.x / game_mec.p_x_scale) + game_mec.oriiginx;
        p_loc_y = (rb.transform.position.z / game_mec.y_scale) + game_mec.oriiginy;
        MyDetail md3 = new MyDetail();
        md3.longitude = p_loc_x;
        md3.latitude = p_loc_y;
		md3.player_id = Convert.ToInt32(ros2.playerid);
        // md.topic ="/w_personcoordinates";

        StringBuilder sb = new StringBuilder();
        using (StringWriter sw = new StringWriter(sb))
        using (JsonTextWriter writer = new JsonTextWriter(sw))
        {
            writer.QuoteChar = '\'';

            JsonSerializer ser = new JsonSerializer();
            ser.Serialize(writer, md3);
        }
        // Debug.Log(sb.ToString());

        p_location = sb.ToString();
    }
	
	// Update is called once per frame
	void Update () {
        if (stop == 0)
        {
            hcs = GameObject.FindGameObjectsWithTag("hc");
            hdcs = GameObject.FindGameObjectsWithTag("hdz");
            foundclues = new int[game_mec.counter2];
            stop++;
           // Debug.Log(hcs.Length);
           // Debug.Log(hdcs.Length);
        }
        if (counter.timeLeft > 0)
        {

			// This section reads user input to determine direction
            move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            transform.position += move * speed * Time.deltaTime;

            p_loc_x = (rb.transform.position.x / game_mec.p_x_scale) + game_mec.oriiginx;
            p_loc_y = (rb.transform.position.z / game_mec.y_scale) + game_mec.oriiginy;

            MyDetail md3 = new MyDetail();
            md3.longitude = p_loc_x;
            md3.latitude = p_loc_y;
			md3.player_id = Convert.ToInt32(ros2.playerid);

            StringBuilder sb = new StringBuilder();
            using (StringWriter sw = new StringWriter(sb))
            using (JsonTextWriter writer = new JsonTextWriter(sw))
            {
                writer.QuoteChar = '\'';

                JsonSerializer ser = new JsonSerializer();
                ser.Serialize(writer, md3);
            }
            // Debug.Log(sb.ToString());

            p_location = sb.ToString();

            // Debug.Log(p_location);
            // p_location = "Longitude: " + p_loc_x.ToString() + " Latitude: " + p_loc_y.ToString();
            
        }

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

    void OnCollisionEnter(Collision collision)
    {
       

        if (collision.gameObject.tag=="building")  // or if(gameObject.CompareTag("YourWallTag"))
        {
            clue = false;
            ++checkcollision;
            
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
           
            move = Vector3.zero;
            bound_x1 = (collision.gameObject.GetComponent<MeshFilter>().mesh.bounds.center.x - collision.gameObject.GetComponent<MeshFilter>().mesh.bounds.extents.x)*-1;
            bound_x2 = (collision.gameObject.GetComponent<MeshFilter>().mesh.bounds.center.x + collision.gameObject.GetComponent<MeshFilter>().mesh.bounds.extents.x)*-1;
            bound_z1 = (collision.gameObject.GetComponent<MeshFilter>().mesh.bounds.center.z - collision.gameObject.GetComponent<MeshFilter>().mesh.bounds.extents.z)*-1;
            bound_z2 = (collision.gameObject.GetComponent<MeshFilter>().mesh.bounds.center.z + collision.gameObject.GetComponent<MeshFilter>().mesh.bounds.extents.z)*-1;

            foreach (GameObject hc in hcs)
            {

                // Debug.Log(bound_x1);
                // Debug.Log(bound_x2);
                // Debug.Log(bound_z1);
                // Debug.Log(bound_z2);
                // Debug.Log(hc.transform.localPosition);
                // Debug.Log(hc.transform.position.x);
                // Debug.Log(hc.transform.position.y);
                // Debug.Log(hc.transform.position.z);
                if (bound_x2 < hc.transform.position.x && hc.transform.position.x < bound_x1 && bound_z2 < hc.transform.position.z && hc.transform.position.z < bound_z1)
                {
                    if (hc.GetComponent<Text>().text == "Treasure")
                    {
                        clue = true;
                        GameObject.Find("gamestatus").GetComponent<Text>().text = "You found the treasure!!!";
                        ++treasurefound;
                        if (treasurefound == 1)
                        {
                            MyDetail md3 = new MyDetail();

							md3.player_id = Convert.ToInt32(ros2.playerid);
                            md3.point = 20;
                            //   md.topic = "/w_ddzcoordinates";

                            StringBuilder sb = new StringBuilder();
                            using (StringWriter sw = new StringWriter(sb))
                            using (JsonTextWriter writer = new JsonTextWriter(sw))
                            {
                                writer.QuoteChar = '\'';

                                JsonSerializer ser = new JsonSerializer();
                                ser.Serialize(writer, md3);
                            }

                            playerpointaddition = sb.ToString();
                            playerpointadditionbool = true;
                        }

                    }
                    else
                    {
                        if (foundclues.Count(x => x == Convert.ToInt32(getBetween(hc.GetComponent<Text>().text, "\'", "\'"))) == 0)
                        {
                            clue = true;
                            // Debug.Log("here in the clue");
                            foundclues[foundcluescounter] = Convert.ToInt32(getBetween(hc.GetComponent<Text>().text, "\'", "\'"));
                            ++foundcluescounter;
                            if (GameObject.Find("Clues").GetComponent<Text>().text == "Clues Placeholder")
                            {
                                GameObject.Find("Clues").GetComponent<Text>().text = hc.GetComponent<Text>().text.Substring(hc.GetComponent<Text>().text.LastIndexOf(':') + 1);
                                MyDetail md3 = new MyDetail();

								md3.player_id = Convert.ToInt32(ros2.playerid);
                                md3.point = 10;
                                //   md.topic = "/w_ddzcoordinates";

								score = score + 10;

                                StringBuilder sb = new StringBuilder();
                                using (StringWriter sw = new StringWriter(sb))
                                using (JsonTextWriter writer = new JsonTextWriter(sw))
                                {
                                    writer.QuoteChar = '\'';

                                    JsonSerializer ser = new JsonSerializer();
                                    ser.Serialize(writer, md3);
                                }

                                playerpointaddition = sb.ToString();
                                playerpointadditionbool = true;

                            }

                            else
                            {
                                clue = true;
                                GameObject.Find("Clues").GetComponent<Text>().text = GameObject.Find("Clues").GetComponent<Text>().text + ", " + hc.GetComponent<Text>().text.Substring(hc.GetComponent<Text>().text.LastIndexOf(':') + 1);
                                MyDetail md3 = new MyDetail();
								md3.player_id = Convert.ToInt32(ros2.playerid);
                                md3.point = 10;
                                //   md.topic = "/w_ddzcoordinates";

								score = score + 10;

                                StringBuilder sb = new StringBuilder();
                                using (StringWriter sw = new StringWriter(sb))
                                using (JsonTextWriter writer = new JsonTextWriter(sw))
                                {
                                    writer.QuoteChar = '\'';

                                    JsonSerializer ser = new JsonSerializer();
                                    ser.Serialize(writer, md3);
                                }

                                playerpointaddition = sb.ToString();
                                playerpointadditionbool = true;
                            }
                        }
                    }

                }
            }
                
                    if (checkcollision == 1 && clue==false)
                    {
                       // Debug.Log("here out the clue");
                        // Debug.Log("Entered");
                        playerpointadditionbool = true;
                        MyDetail md3 = new MyDetail();

				        md3.player_id = Convert.ToInt32(ros2.playerid);
                        md3.point = -2;
                        //   md.topic = "/w_ddzcoordinates";

				        score = score - 2;

                        StringBuilder sb = new StringBuilder();
                        using (StringWriter sw = new StringWriter(sb))
                        using (JsonTextWriter writer = new JsonTextWriter(sw))
                        {
                            writer.QuoteChar = '\'';

                            JsonSerializer ser = new JsonSerializer();
                            ser.Serialize(writer, md3);
                        }

                        playerpointaddition = sb.ToString();

                    }
                }

		Debug.Log ("A collision, the score is: " + score);
                
   }


           

    void OnCollisionExit(Collision collision)
    {
        playerpointadditionbool =false;
        checkcollision=0;
       


    }

    private void OnTriggerEnter(Collider other)
    {

        //  Debug.Log("entered");
        //speed = 1;
        // counter.timeLeft = counter.timeLeft-100;
        //hdz = true;
        ++checkhdz;
        if (checkhdz == 1)
        {
            MyDetail md3 = new MyDetail();

			md3.player_id = Convert.ToInt32(ros2.playerid);
            md3.point = -5;
            //   md.topic = "/w_ddzcoordinates";

            StringBuilder sb = new StringBuilder();
            using (StringWriter sw = new StringWriter(sb))
            using (JsonTextWriter writer = new JsonTextWriter(sw))
            {
                writer.QuoteChar = '\'';

                JsonSerializer ser = new JsonSerializer();
                ser.Serialize(writer, md3);
            }

            playerpointdeductdz = sb.ToString();
            playerpointdeductionbool = true;
        }
       
    }

   

        private void OnTriggerExit(Collider other)
    {
        playerpointdeductionbool = false; checkhdz =0;
        //   Debug.Log("exit");
        //  speed = 5;
        //  hdz = false;
    }
}
