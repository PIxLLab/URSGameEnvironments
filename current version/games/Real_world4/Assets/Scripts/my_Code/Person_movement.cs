﻿using Assets.Scripts.my_Code;
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
    float speed =10;
    public GameObject[] hcs;
    public GameObject[] hdcs;
    int[] foundclues;
    int foundcluescounter;
	public static int score = 0;
	// public  Vector3 move = new Vector3(0, 0, 0);
	Vector3 prevoiusCheckSendMessage = new Vector3(0,0,0);
	Vector3 prevoiusCheckSendMessagedz = new Vector3(0, 0, 0);
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
	public string[] visitedBuildings = new string[50];
	public int buildingPos = 0;
	public bool newBuilding = false;

	public static bool hdzpublisher = false;
	public static string hdzcoordinations;

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
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            //transform.position += move * speed * Time.deltaTime;
			transform.position = new Vector3((float) ros2.humanXpos, 0.2f, (float) ros2.humanYpos);

			//Debug.Log ("Player current position from GPS");
			//Debug.Log ((float) ros2.humanXpos);
			//Debug.Log((float) ros2.humanYpos);
            // p_loc_x = (rb.transform.position.x / game_mec.p_x_scale) + game_mec.oriiginx;
            // p_loc_y = (rb.transform.position.z / game_mec.y_scale) + game_mec.oriiginy;

   //         MyDetail md3 = new MyDetail();
   //         md3.longitude = p_loc_x;
   //         md3.latitude = p_loc_y;
			//md3.player_id = Convert.ToInt32(ros2.playerid);

            //StringBuilder sb = new StringBuilder();
            //using (StringWriter sw = new StringWriter(sb))
            //using (JsonTextWriter writer = new JsonTextWriter(sw))
            //{
            //    writer.QuoteChar = '\'';

            //    JsonSerializer ser = new JsonSerializer();
            //    ser.Serialize(writer, md3);
            //}
            // Debug.Log(sb.ToString());

            // p_location = sb.ToString();

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

  //  void OnCollisionEnter(Collision collision)
  //  {

  //      if (collision.gameObject.tag=="building") 
  //      {
  //          clue = false;
		//	newBuilding = true;

  //          ++checkcollision;
            
  //          //rb.velocity = Vector3.zero;
  //          //rb.angularVelocity = Vector3.zero;
           
  //          //move = Vector3.zero;
  //          bound_x1 = (collision.gameObject.GetComponent<MeshFilter>().mesh.bounds.center.x - collision.gameObject.GetComponent<MeshFilter>().mesh.bounds.extents.x)*-1;
  //          bound_x2 = (collision.gameObject.GetComponent<MeshFilter>().mesh.bounds.center.x + collision.gameObject.GetComponent<MeshFilter>().mesh.bounds.extents.x)*-1;
  //          bound_z1 = (collision.gameObject.GetComponent<MeshFilter>().mesh.bounds.center.z - collision.gameObject.GetComponent<MeshFilter>().mesh.bounds.extents.z)*-1;
  //          bound_z2 = (collision.gameObject.GetComponent<MeshFilter>().mesh.bounds.center.z + collision.gameObject.GetComponent<MeshFilter>().mesh.bounds.extents.z)*-1;

		//	// Debug.Log(collision.gameObject.GetComponent<Text> ().text);

		//	for (int i = 0; i < (buildingPos + 1); i++) 
		//	{
		//		if (visitedBuildings [i] == collision.gameObject.GetComponent<Text> ().text) 
		//		{
		//			newBuilding = false;
		//		}
		//	}

  //          foreach (GameObject hc in hcs)
  //          {

  //              // Debug.Log(bound_x1);
  //              // Debug.Log(bound_x2);
  //              // Debug.Log(bound_z1);
  //              // Debug.Log(bound_z2);
  //              // Debug.Log(hc.transform.localPosition);
  //              // Debug.Log(hc.transform.position.x);
  //              // Debug.Log(hc.transform.position.y);
  //              // Debug.Log(hc.transform.position.z);

  //              if (bound_x2 < hc.transform.position.x && hc.transform.position.x < bound_x1 && bound_z2 < hc.transform.position.z && hc.transform.position.z < bound_z1)
  //              {
  //                  if (hc.GetComponent<Text>().text == "Treasure")
		//			{
  //                      clue = true;
  //                      GameObject.Find("gamestatus").GetComponent<Text>().text = "You found the treasure!!!";
  //                      ++treasurefound;
  //                      if (treasurefound == 1)
  //                      {
  //                          MyDetail md3 = new MyDetail();

		//					md3.player_id = Convert.ToInt32(ros2.playerid);
  //                          md3.point = 20;
  //                          //   md.topic = "/w_ddzcoordinates";

  //                          StringBuilder sb = new StringBuilder();
  //                          using (StringWriter sw = new StringWriter(sb))
  //                          using (JsonTextWriter writer = new JsonTextWriter(sw))
  //                          {
  //                              writer.QuoteChar = '\'';

  //                              JsonSerializer ser = new JsonSerializer();
  //                              ser.Serialize(writer, md3);
  //                          }

  //                          playerpointaddition = sb.ToString();
  //                          playerpointadditionbool = true;

		//					visitedBuildings [buildingPos] = collision.gameObject.GetComponent<Text> ().text;
		//					buildingPos++;
  //                      }

  //                  }
  //                  else
		//			{

		//				//if ((foundclues.Count(x => x == Convert.ToInt32(getBetween(hc.GetComponent<Text>().text, "\'", "\'"))) == 0) && newBuilding == true)

		//				if ((foundclues.Count(x => x == Convert.ToInt32(getBetween(hc.GetComponent<Text>().text, "\'", "\'"))) == 0))
  //                      {
  //                          clue = true;
  //                          foundclues[foundcluescounter] = Convert.ToInt32(getBetween(hc.GetComponent<Text>().text, "\'", "\'"));
  //                          ++foundcluescounter;
  //                          if (GameObject.Find("Clues").GetComponent<Text>().text == "Clues Placeholder")
  //                          {
  //                              GameObject.Find("Clues").GetComponent<Text>().text = hc.GetComponent<Text>().text.Substring(hc.GetComponent<Text>().text.LastIndexOf(':') + 1);
  //                              MyDetail md3 = new MyDetail();

		//						md3.player_id = Convert.ToInt32(ros2.playerid);
                                
		//						if (newBuilding == true) {
									
		//							md3.point = 10;

		//							visitedBuildings [buildingPos] = collision.gameObject.GetComponent<Text> ().text;
		//							buildingPos++;

		//						} else {

		//							md3.point = 0;

		//						}

		//						md3.drone_clue_id = Convert.ToInt32(getBetween(hc.GetComponent<Text>().text, "\'", "\'")); 

		//						score = score + 10; // for local debugging

  //                              StringBuilder sb = new StringBuilder();
  //                              using (StringWriter sw = new StringWriter(sb))
  //                              using (JsonTextWriter writer = new JsonTextWriter(sw))
  //                              {
  //                                  writer.QuoteChar = '\'';

  //                                  JsonSerializer ser = new JsonSerializer();
  //                                  ser.Serialize(writer, md3);
  //                              }

  //                              playerpointaddition = sb.ToString();
  //                              playerpointadditionbool = true;

  //                          }
		//					else 
  //                          {
  //                              clue = true;
  //                              GameObject.Find("Clues").GetComponent<Text>().text = GameObject.Find("Clues").GetComponent<Text>().text + ", " + hc.GetComponent<Text>().text.Substring(hc.GetComponent<Text>().text.LastIndexOf(':') + 1);
  //                              MyDetail md3 = new MyDetail();
		//						md3.player_id = Convert.ToInt32(ros2.playerid);

		//						if (newBuilding == true) {

		//							md3.point = 10;
		//							score = score + 10; // for local debugging

		//							visitedBuildings [buildingPos] = collision.gameObject.GetComponent<Text> ().text;
		//							buildingPos++;

		//						} else {

		//							md3.point = 0;

		//						}
                                
		//						md3.drone_clue_id = Convert.ToInt32(getBetween(hc.GetComponent<Text>().text, "\'", "\'")); 

  //                              StringBuilder sb = new StringBuilder();
  //                              using (StringWriter sw = new StringWriter(sb))
  //                              using (JsonTextWriter writer = new JsonTextWriter(sw))
  //                              {
  //                                  writer.QuoteChar = '\'';

  //                                  JsonSerializer ser = new JsonSerializer();
  //                                  ser.Serialize(writer, md3);
  //                              }

  //                              playerpointaddition = sb.ToString();
  //                              playerpointadditionbool = true;
  //                          }
  //                      }
  //                  }

  //              }
  //          }
                
		//	if (checkcollision == 1 && clue==false && newBuilding == true)
  //          {
  //              // Debug.Log("Entered");
  //              playerpointadditionbool = true;
  //              MyDetail md3 = new MyDetail();

		//        md3.player_id = Convert.ToInt32(ros2.playerid);
  //              md3.point = -2;
  //              //   md.topic = "/w_ddzcoordinates";

		//        score = score - 2;

  //              StringBuilder sb = new StringBuilder();
  //              using (StringWriter sw = new StringWriter(sb))
  //              using (JsonTextWriter writer = new JsonTextWriter(sw))
  //              {
  //                  writer.QuoteChar = '\'';

  //                  JsonSerializer ser = new JsonSerializer();
  //                  ser.Serialize(writer, md3);
  //              }

  //              playerpointaddition = sb.ToString();
		//		visitedBuildings [buildingPos] = collision.gameObject.GetComponent<Text> ().text;
		//		buildingPos++;

  //          }
				
  //      }

		//Debug.Log ("A collision, the score is: " + score);
                
  // }


           

  //  void OnCollisionExit(Collision collision)
  //  {
  //      playerpointadditionbool =false;
  //      checkcollision=0;
  
  //  }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("srd"))
            other = other.GetComponentInParent<MeshCollider>();
        else if (!other.tag.Equals("building"))
            return;
		// Testing the switch to triggers from colliders

		clue = false;
		newBuilding = true;

	

		bound_x1 = (other.gameObject.GetComponent<MeshFilter>().mesh.bounds.center.x - other.gameObject.GetComponent<MeshFilter>().mesh.bounds.extents.x)*-1;
		bound_x2 = (other.gameObject.GetComponent<MeshFilter>().mesh.bounds.center.x + other.gameObject.GetComponent<MeshFilter>().mesh.bounds.extents.x)*-1;
		bound_z1 = (other.gameObject.GetComponent<MeshFilter>().mesh.bounds.center.z - other.gameObject.GetComponent<MeshFilter>().mesh.bounds.extents.z)*-1;
		bound_z2 = (other.gameObject.GetComponent<MeshFilter>().mesh.bounds.center.z + other.gameObject.GetComponent<MeshFilter>().mesh.bounds.extents.z)*-1;

		for (int i = 0; i < (buildingPos + 1); i++) 
		{
			if (visitedBuildings [i] == other.gameObject.GetComponent<Text> ().text) 
			{
				newBuilding = false;
			}
		}
			
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

						visitedBuildings [buildingPos] = other.gameObject.GetComponent<Text> ().text;
						buildingPos++;
					}

				}
				else
				{

					if ((foundclues.Count(x => x == Convert.ToInt32(getBetween(hc.GetComponent<Text>().text, "\'", "\'"))) == 0) && newBuilding == true)
					{
						clue = true;
						foundclues[foundcluescounter] = Convert.ToInt32(getBetween(hc.GetComponent<Text>().text, "\'", "\'"));
						++foundcluescounter;
						if (GameObject.Find("Clues").GetComponent<Text>().text == "Clues Placeholder")
						{
							GameObject.Find("Clues").GetComponent<Text>().text = hc.GetComponent<Text>().text.Substring(hc.GetComponent<Text>().text.LastIndexOf(':') + 1);
							MyDetail md3 = new MyDetail();

							md3.player_id = Convert.ToInt32(ros2.playerid);
							md3.point = 10;

                            md3.longitude = ros2.convertX;
                            md3.latitude = ros2.convertY;

                            md3.drone_clue_id = Convert.ToInt32(getBetween(hc.GetComponent<Text>().text, "\'", "\'")); 

							score = score + 10; // for local debugging

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
                            Debug.Log("Sent JSON string: " + playerpointaddition);
                            Debug.Log("Found Clues");

							visitedBuildings [buildingPos] = other.gameObject.GetComponent<Text> ().text;
							buildingPos++;

						}
						else if(newBuilding == true)
						{
							clue = true;
							GameObject.Find("Clues").GetComponent<Text>().text = GameObject.Find("Clues").GetComponent<Text>().text + ", " + hc.GetComponent<Text>().text.Substring(hc.GetComponent<Text>().text.LastIndexOf(':') + 1);
							MyDetail md3 = new MyDetail();
							md3.player_id = Convert.ToInt32(ros2.playerid);
							md3.point = 10;

                            md3.longitude = ros2.convertX;
                            md3.latitude = ros2.convertY;

                            md3.drone_clue_id = Convert.ToInt32(getBetween(hc.GetComponent<Text>().text, "\'", "\'")); 

							score = score + 10; // for local debugging

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

                            Debug.Log("Sent JSON string: " + playerpointaddition);
                            Debug.Log("Found Clues");

                            visitedBuildings [buildingPos] = other.gameObject.GetComponent<Text> ().text;
							buildingPos++;
						}
					}
				}

			}
		}

        //Debug.Log("checkCollision: " + checkcollision);
        //Debug.Log("Clue: " + clue);
        //Debug.Log("newBuilding: " + newBuilding);

        if (clue==false && newBuilding == true)
		{
			// Debug.Log("Entered");
			playerpointadditionbool = true;
			MyDetail md3 = new MyDetail();

			md3.player_id = Convert.ToInt32(ros2.playerid);
			md3.point = -2;
			

			score = score - 2;

            Debug.Log("Not a clue");

			StringBuilder sb = new StringBuilder();
			using (StringWriter sw = new StringWriter(sb))
			using (JsonTextWriter writer = new JsonTextWriter(sw))
			{
				writer.QuoteChar = '\'';

				JsonSerializer ser = new JsonSerializer();
				ser.Serialize(writer, md3);
			}

			playerpointaddition = sb.ToString();
			visitedBuildings [buildingPos] = other.gameObject.GetComponent<Text> ().text;
			buildingPos++;

		}

        Debug.Log(other.name);
        Debug.Log("A collisoin, score is: " + score);


        if (other.gameObject.tag == "hdz")
        {

			Debug.Log ("Human danger zone");

			if (Math.Sqrt (Math.Pow ((rb.transform.position.x - prevoiusCheckSendMessage.x), 2) + Math.Pow ((rb.transform.position.z - prevoiusCheckSendMessage.z), 2)) > 10) {
				
				prevoiusCheckSendMessage.x = rb.transform.position.x;
				prevoiusCheckSendMessage.z = rb.transform.position.z;

				Debug.Log ("HDZ new positon");


				MyDetail md3 = new MyDetail ();

				p_loc_x = (rb.transform.position.x / game_mec.p_x_scale) + game_mec.oriiginx;
				p_loc_y = (rb.transform.position.z / game_mec.y_scale) + game_mec.oriiginy;

				md3.longitude = p_loc_x;
				md3.latitude = p_loc_y;

				md3.player_id = Convert.ToInt32 (ros2.playerid);
				md3.point = -5;
				//   md.topic = "/w_ddzcoordinates";

				StringBuilder sb = new StringBuilder ();
				using (StringWriter sw = new StringWriter (sb))
				using (JsonTextWriter writer = new JsonTextWriter (sw)) {
					writer.QuoteChar = '\'';

					JsonSerializer ser = new JsonSerializer ();
					ser.Serialize (writer, md3);
				}

				playerpointdeductdz = sb.ToString ();
				hdzcoordinations = sb.ToString ();
				playerpointdeductionbool = true;
				hdzpublisher = true;

			}

			/*
			if (Math.Sqrt (Math.Pow ((rb.transform.position.x - prevoiusCheckSendMessage.x), 2) + Math.Pow ((rb.transform.position.z - prevoiusCheckSendMessage.z), 2)) > 10) {
				
				prevoiusCheckSendMessage.x = rb.transform.position.x;
				prevoiusCheckSendMessage.z = rb.transform.position.z;

				MyDetail md3 = new MyDetail ();

				p_loc_x = (rb.transform.position.x / game_mec.p_x_scale) + game_mec.oriiginx;
				p_loc_y = (rb.transform.position.z / game_mec.y_scale) + game_mec.oriiginy;

				md3.longitude = p_loc_x;
				md3.latitude = p_loc_y;

				md3.player_id = Convert.ToInt32 (ros2.playerid);

				StringBuilder sb = new StringBuilder ();
				using (StringWriter sw = new StringWriter (sb))
				using (JsonTextWriter writer = new JsonTextWriter (sw)) {
					writer.QuoteChar = '\'';

					JsonSerializer ser = new JsonSerializer ();
					ser.Serialize (writer, md3);
				}

				hdzcoordinations = sb.ToString();
				hdzpublisher = true;

			}
			*/

       	}
       
    }



	private void OnTriggerStay(Collider other)
	{
        if (other.tag.Equals("srd"))
            other = other.GetComponentInParent<MeshCollider>();
        else if (!other.tag.Equals("building"))
            return;

        clue = false;
		newBuilding = true;

		++checkcollision;

		bound_x1 = (other.gameObject.GetComponent<MeshFilter>().mesh.bounds.center.x - other.gameObject.GetComponent<MeshFilter>().mesh.bounds.extents.x)*-1;
		bound_x2 = (other.gameObject.GetComponent<MeshFilter>().mesh.bounds.center.x + other.gameObject.GetComponent<MeshFilter>().mesh.bounds.extents.x)*-1;
		bound_z1 = (other.gameObject.GetComponent<MeshFilter>().mesh.bounds.center.z - other.gameObject.GetComponent<MeshFilter>().mesh.bounds.extents.z)*-1;
		bound_z2 = (other.gameObject.GetComponent<MeshFilter>().mesh.bounds.center.z + other.gameObject.GetComponent<MeshFilter>().mesh.bounds.extents.z)*-1;

		for (int i = 0; i < (buildingPos + 1); i++) 
		{
			if (visitedBuildings [i] == other.gameObject.GetComponent<Text> ().text) 
			{
				newBuilding = false;
			}
		}

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

						visitedBuildings [buildingPos] = other.gameObject.GetComponent<Text> ().text;
						buildingPos++;
					}

				}
				else
				{

					if ((foundclues.Count(x => x == Convert.ToInt32(getBetween(hc.GetComponent<Text>().text, "\'", "\'"))) == 0) && newBuilding == true)
					{
						clue = true;
						foundclues[foundcluescounter] = Convert.ToInt32(getBetween(hc.GetComponent<Text>().text, "\'", "\'"));
						++foundcluescounter;
						if (GameObject.Find("Clues").GetComponent<Text>().text == "Clues Placeholder")
						{
							GameObject.Find("Clues").GetComponent<Text>().text = hc.GetComponent<Text>().text.Substring(hc.GetComponent<Text>().text.LastIndexOf(':') + 1);
							MyDetail md3 = new MyDetail();

							md3.player_id = Convert.ToInt32(ros2.playerid);
							md3.point = 10;

                            md3.longitude = ros2.convertX;
                            md3.latitude = ros2.convertY;

							md3.drone_clue_id = Convert.ToInt32(getBetween(hc.GetComponent<Text>().text, "\'", "\'")); 

							score = score + 10; // for local debugging

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

							visitedBuildings [buildingPos] = other.gameObject.GetComponent<Text> ().text;
							buildingPos++;

						}
						else if(newBuilding == true)
						{
							clue = true;
							GameObject.Find("Clues").GetComponent<Text>().text = GameObject.Find("Clues").GetComponent<Text>().text + ", " + hc.GetComponent<Text>().text.Substring(hc.GetComponent<Text>().text.LastIndexOf(':') + 1);
							MyDetail md3 = new MyDetail();
							md3.player_id = Convert.ToInt32(ros2.playerid);
							md3.point = 10;

                            md3.longitude = ros2.convertX;
                            md3.latitude = ros2.convertY;

                            md3.drone_clue_id = Convert.ToInt32(getBetween(hc.GetComponent<Text>().text, "\'", "\'")); 

							score = score + 10; // for local debugging

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

							visitedBuildings [buildingPos] = other.gameObject.GetComponent<Text> ().text;
							buildingPos++;
						}
					}
				}

			}
		}

       
		if (checkcollision == 1 && clue==false && newBuilding == true)
		{
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
			visitedBuildings [buildingPos] = other.gameObject.GetComponent<Text> ().text;
			buildingPos++;

		}

		Debug.Log ("A collision, the score is: " + score);


		if (other.gameObject.CompareTag("hdz"))
		{

			if (Math.Sqrt (Math.Pow ((rb.transform.position.x - prevoiusCheckSendMessage.x), 2) + Math.Pow ((rb.transform.position.z - prevoiusCheckSendMessage.z), 2)) > 10) {

				prevoiusCheckSendMessage.x = rb.transform.position.x;
				prevoiusCheckSendMessage.z = rb.transform.position.z;

				Debug.Log ("HDZ new positon");


				MyDetail md3 = new MyDetail ();

				p_loc_x = (rb.transform.position.x / game_mec.p_x_scale) + game_mec.oriiginx;
				p_loc_y = (rb.transform.position.z / game_mec.y_scale) + game_mec.oriiginy;

				md3.longitude = p_loc_x;
				md3.latitude = p_loc_y;

				md3.player_id = Convert.ToInt32 (ros2.playerid);
				md3.point = -5;
				//   md.topic = "/w_ddzcoordinates";

				StringBuilder sb = new StringBuilder ();
				using (StringWriter sw = new StringWriter (sb))
				using (JsonTextWriter writer = new JsonTextWriter (sw)) {
					writer.QuoteChar = '\'';

					JsonSerializer ser = new JsonSerializer ();
					ser.Serialize (writer, md3);
				}

				hdzcoordinations = sb.ToString ();
				hdzpublisher = true;

			}
		}
	}

   

  //  private void OnTriggerExit(Collider other)
  //  {
  //      playerpointdeductionbool = false; checkhdz =0;

		//hdzpublisher = false;

  //      //   Debug.Log("exit");
  //      //  speed = 5;
  //      //  hdz = false;
  //  }
}
