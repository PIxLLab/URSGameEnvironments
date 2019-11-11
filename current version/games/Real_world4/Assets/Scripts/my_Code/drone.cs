using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ROSBridgeLib;
using UnityEngine.UI;
using System.Globalization;
using System;
using System.Linq;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Assets.Scripts;
using System.Text;
using System.IO;

public class drone : MonoBehaviour {

    public float speed=10;
    float preTotalSampleDistance;
    public Rigidbody rb;
    public int id;
    public ROSBridgeSubscriber drone_subscriber;
    public publisher2 drone_batterypublisher;
    float x, y, z;
    double movx, movy;
    bool[] building_flags = new bool[35] ;
    string[] bui = new string[35] ;
    int buicheck=0;
   // int battery = 100;
    public Text txt1;
    public Text txt2;
    public Text txt3;
    public GameObject[] dz;
    public static string ddzcoordinations;
    public static string hdzcoordinations;
    public static string dccoordinations;
    public static string hccoordinations;
    public  double battery_value=100;
    public static string drone_clue_value;
    public static string building_numbers;

	bool[] found_clues = new bool[10];
	int clue_id;
    

    public static string buildcheck;
    public string check = "ndd";
    int NumberOfSamples = 10;
    float[] distances;
    int lastSampleSlot = 0;
    Vector3 newPosition;
    Vector3 dir;
    Vector3 prevoiusCheckSendMessage=new Vector3(0,0,0);
    Vector3 prevoiusCheckSendMessagedz = new Vector3(0, 0, 0);
    Vector3 previousPosition;
    UnityEngine.Camera cam;
    string s;
    public static bool ddzpublisher = false;
    public static bool hdzpublisher = false;
    public static bool dcpublisher = false;
    public static bool hcpublisher = false;
    public static bool buipublisher = false;
    
    Renderer[] renderers3;



    public static string coordsToJason;

    void Start() {

		for (int i = 0; i < 10; i++) {
			found_clues [i] = false;
		}
        

        distances = new float[NumberOfSamples];
      //  battery_value =  battery;
        
        GameObject[] bun = GameObject.FindGameObjectsWithTag("bun");

        
        renderers3 = new Renderer[bun.Length];
       

        for (int i = 0; i < bun.Length; ++i)
        {

            renderers3[i] = bun[i].GetComponent<Renderer>();


        }

        cam = GameObject.FindWithTag("fpc").GetComponent<Camera>();
        rb = GetComponent<Rigidbody>();
        //  s= getBetween(drone_subscriber.Coords, "topic", ",");
        // s=s.Substring(s.LastIndexOf(':') + 1);
        // s= getBetween(s, "\"", "\"");
        //Debug.Log(s);
      
        if (drone_subscriber.coords != null )
        {
            string datay = getBetween(drone_subscriber.coords, "y", ",");
            string datax = getBetween(drone_subscriber.coords, "x", ",");
            string dataz = getBetween(drone_subscriber.coords, "z", ",");

            datay = datay.Substring(datay.LastIndexOf(':') + 1);
            datax = datax.Substring(datax.LastIndexOf(':') + 1);
            dataz = dataz.Substring(dataz.LastIndexOf(':') + 1);
            dataz = dataz.TrimEnd('}');

            datay = getBetween(datay, "\"", "\"");
            datax = getBetween(datax, "\"", "\"");
            dataz = getBetween(dataz, "\"", "\"");

          
            var format = new CultureInfo("en-US").NumberFormat;
            format.NegativeSign = "-";
            z = float.Parse(datay, format);
            x = float.Parse(datax, format);
            y = float.Parse(dataz, format);



          
            //change y with z and divided by ten
            newPosition = new Vector3(x, y, z);
            rb.position = newPosition;

        }
        else
        {
            rb.velocity = new Vector3(0, 0, 0);
        }

        

    }

    





  /*  private bool IsVisible(Renderer renderer)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(cam);

        if (GeometryUtility.TestPlanesAABB(planes, renderer.bounds))
            return true;
        else
            return false;
    }

    private bool IsVisible2(Renderer renderer2)
    {
        Plane[] planes2 = GeometryUtility.CalculateFrustumPlanes(cam);

        if (GeometryUtility.TestPlanesAABB(planes2, renderer2.bounds))
            return true;
        else
            return false;
    }*/

    private bool IsVisible3(Renderer renderer3)
    {
        Plane[] planes3 = GeometryUtility.CalculateFrustumPlanes(cam);

        if (GeometryUtility.TestPlanesAABB(planes3, renderer3.bounds))
            return true;
        else
            return false;
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


    
    void FixedUpdate () {


        /*    foreach (var renderer in renderers)
            {
                // output only the visible renderers' name
                if (IsVisible(renderer))
                {
                    txt3.text = "Drone Clue Detected";
                    Debug.Log(renderer.name + " is detected!");
                    movx = (rb.transform.position.x / -84165) + game_mec.oriiginx;
                    movy = (rb.transform.position.y / -108785) + game_mec.oriiginy;

                    dccoordinations = "Drone Clue: longitude: " + movx.ToString() + " " + " latitude: " + " " + movy.ToString() + " altitude: " + " " + rb.transform.position.y.ToString();

                    if (rb.transform.position.y<30)
                    { txt3.text = "Drone Clue Detected " + " The Clue is: " + renderer.GetComponent<GUIText>().text;
                        drone_clue_value= "The Clue is: "+ " "+ renderer.GetComponent<GUIText>().text;
                    }


                }
            }

            foreach (var renderer2 in renderers2)
            {
                // output only the visible renderers' name
                if (IsVisible(renderer2))
                {
                    movx = (rb.transform.position.x / -84165) + game_mec.oriiginx;
                    movy = (rb.transform.position.y / -108785) + game_mec.oriiginy;

                    hccoordinations = "Human Clue: longitude: " + movx.ToString() + " " + " latitude: " + " " + movy.ToString() + " altitude: " + " " + rb.transform.position.y.ToString();

                    Debug.Log(renderer2.name + " is detected!");
                }
            }*/
            
            //foreach (var renderer3 in renderers3)
            //{
            //    // output only the visible renderers' name
            //    if (IsVisible3(renderer3))
            //    {
            //        if (rb.transform.position.y < 30)
            //        {
            //            building_numbers = "Building number is: " + " " + renderer3.GetComponent<TextMesh>().text;
            //           // Debug.Log("The Building number is: " + renderer3.GetComponent<TextMesh>().text);
            //        }



            //    }
            //}
            /*
              if (30<rb.transform.position.y  && rb.transform.position.y <= 50)
              {
                  for (int i = 0; i < game_mec.ddzxmax.Length; ++i)
                  {

                      if (game_mec.ddzxmin[i] < rb.transform.position.x && rb.transform.position.x < game_mec.ddzxmax[i] && game_mec.ddzymin[i] < rb.transform.position.z && rb.transform.position.z < game_mec.ddzymax[i])
                      {
                          txt2.text = "Drone Dangerous Zone" + game_mec.ddzxmin[i].ToString()+" < " +rb.transform.position.x.ToString()+ " < " + game_mec.ddzxmax[i].ToString()+ " | " + game_mec.ddzymin[i].ToString()+" < "+ rb.transform.position.z.ToString() +" < "+  game_mec.ddzymax[i].ToString();
                          Debug.Log("Drone Dangerous Zone" + game_mec.ddzxmin[i].ToString() + " < " + rb.transform.position.x.ToString() + " < " + game_mec.ddzxmax[i].ToString() + " | " + game_mec.ddzymin[i].ToString() + " < " + rb.transform.position.z.ToString() + " < " + game_mec.ddzymax[i].ToString());
                          if (rb.transform.position.x > 0) { movx = (rb.transform.position.x / -84165.03045352287) + game_mec.oriiginx; }
                          else { movx = (rb.transform.position.x / -134165.03045352287) + game_mec.oriiginx; }
                          movy = (rb.transform.position.y / -108785.2905048975) + game_mec.oriiginy;
                         ddzcoordinations = "Drone Dangerous Zone: longitude: " + movx.ToString() + " " + " latitude: " + " " + movy.ToString() + " altitude: " + " " + rb.transform.position.y.ToString();


                      }
                      else
                      { txt2.text = "End of Drone Dangerous Zone"; }
                  }
              }
              else { }

              if (rb.transform.position.y <= 30)
              {
                  for (int i = 0; i < game_mec.hdzxmax.Length; ++i)
                  {

                      if (game_mec.hdzxmin[i] < rb.transform.position.x && rb.transform.position.x < game_mec.hdzxmax[i] && game_mec.hdzymin[i] < rb.transform.position.z && rb.transform.position.z < game_mec.hdzymax[i])
                      {
                          if (rb.transform.position.x > 0) { movx = (rb.transform.position.x / -84165.03045352287) + game_mec.oriiginx; }
                          else { movx = (rb.transform.position.x / -134165.03045352287) + game_mec.oriiginx; }
                          movy = (rb.transform.position.y / -108785.2905048975) + game_mec.oriiginy;

                          hdzcoordinations = "Human Dangerous Zone: longitude: " + movx.ToString() + " " + " latitude: " + " " + movy.ToString() + " altitude: " + " " + rb.transform.position.y.ToString();

                      }
                      else
                      {


                      }

                      for (int j = 0; j < game_mec.ddzxmax.Length; ++j)
                      {

                          if (game_mec.ddzxmin[j] < rb.transform.position.x && rb.transform.position.x < game_mec.ddzxmax[j] && game_mec.ddzymin[j] < rb.transform.position.z && rb.transform.position.z < game_mec.ddzymax[j])
                          {
                              check = "dd";
                              if (rb.transform.position.x > 0) { movx = (rb.transform.position.x / -84165.03045352287) + game_mec.oriiginx; }
                              else { movx = (rb.transform.position.x / -134165.03045352287) + game_mec.oriiginx; }
                              movy = (rb.transform.position.y / -108785.2905048975) + game_mec.oriiginy;
                             ddzcoordinations = "Drone Dangerous Zone: longitude: " + movx.ToString() + " " + " latitude: " + " " + movy.ToString()+" altitude: "+" "+rb.transform.position.y.ToString() ;

                          }
                          else
                          { check="ndd"; }
                      }

                  }




              }
              else {   check = "ndd"; }*/


        float TotalSampleDistance = 0;
        
        if (drone_subscriber.coords != null)
        {
            //Debug.Log(drone_subscriber.topic);
            string data= getBetween(drone_subscriber.coords, "position", "}");
           // Debug.Log(data);
            string datay = getBetween(drone_subscriber.coords, "y", ",");
           // Debug.Log(datay);
            string datax = getBetween(drone_subscriber.coords, "x", ",");
           // Debug.Log(datax);
            string dataz = getBetween(drone_subscriber.coords, "z", ",");
            //Debug.Log(dataz);



            datay = datay.Substring(datay.LastIndexOf(':') + 1);
            datax = datax.Substring(datax.LastIndexOf(':') + 1);
            dataz = dataz.Substring(dataz.LastIndexOf(':') + 1);
            dataz = dataz.TrimEnd('}');

            datay = getBetween(datay, "\"", "\"");
            datax = getBetween(datax, "\"", "\"");
            dataz = getBetween(dataz, "\"", "\"");

           

            var format = new CultureInfo("en-US").NumberFormat;
            format.NegativeSign = "-";
            z = float.Parse(datay, format);
            x = float.Parse(datax,format);
            y = float.Parse(dataz, format);

            // Debug.Log("float y is: " + y);
            //   Debug.Log("float x is: " + x);
            //Debug.Log("float z is: " + z);

          //  x = (float)((x - game_mec.oriiginx) * game_mec.n_x_scale);
           // z =(float)((z - game_mec.oriiginy) * game_mec.y_scale);

            newPosition = new Vector3(x, y, z);
           // Debug.Log(newPosition);
           // Debug.Log(rb.position);
          //  rb.position = newPosition;

              dir = (newPosition - transform.position).normalized * speed;
         //  Debug.Log(newPosition);
           //Debug.Log(transform.position);
           //Debug.Log(newPosition - transform.position);
           rb.velocity = dir;
           //rb.position = newPosition;
           //Vector3.Lerp(transform.position, newPosition, speed * Time.deltaTime);
           

            // Debug.Log("float x is: " + rb.position.x);

        }
        else
        {
            rb.velocity = new Vector3(0, 0, 0);
        }


        int currentSampleSlot=(int)(Time.time) % NumberOfSamples;
        if (currentSampleSlot != lastSampleSlot)
        {
            distances[currentSampleSlot] = 0;
        }

        distances[currentSampleSlot] += Vector3.Distance(newPosition,previousPosition);
       
        previousPosition = newPosition;

        // sum all the values in the distances array to work out the distance in the last x samples secs.

       
        foreach (float value in distances)
        {
            TotalSampleDistance += value;
            
                if ((int)System.Math.Floor(TotalSampleDistance) % 5 == 0 &&  (int)System.Math.Floor(TotalSampleDistance)  != preTotalSampleDistance)
                    { //Debug.Log("The distance valueee is"+ (int)System.Math.Floor(TotalSampleDistance)+" pre distance "+ (int)System.Math.Floor(preTotalSampleDistance)/*(int)System.Math.Floor(distanceTravelled)*/);
                //if (battery > 0.1)
                //{
                //    if (check == "ndd") {
                //       // battery =battery-2;
                //    }
                //    else
                //    {
                //        //Debug.Log("WE are in dd, reduce battery by -3");
                //        //battery = battery - 15;
                //    }
                //}
                // txt1.text = "Battery: "+ battery.ToString();
                //MyDetail md = new MyDetail();
                //md.battery_value= battery.ToString();
                //md.player_id = ros2.playerid;
                //md.topic= "/w_battery_value";
                //battery_value = JsonConvert.SerializeObject(md);
                //battery_value = battery;
                preTotalSampleDistance =(int)System.Math.Floor(TotalSampleDistance);
            }
                   
           
           // preTotalSampleDistance = TotalSampleDistance;
            
        }
       
        
        
           
       
    }





    private void OnCollisionEnter(Collision col)
    {

		//  if (col.gameObject.CompareTag("wall") || col.gameObject.CompareTag("building") || col.gameObject.CompareTag("ground"))

        if (col.gameObject.CompareTag("wall") || col.gameObject.CompareTag("ground"))
        {
            rb.angularVelocity = Vector3.zero;
            rb.velocity = Vector3.zero;
            dir = Vector3.zero;
           // Debug.Log("Can not proceed");
           // txt2.text = "Can not proceed";


        }
    }

    private void OnTriggerEnter(Collider other)
    {


        
        if (other.gameObject.CompareTag("building") && !(bui.Contains(other.GetComponent<Text>().text)))
		{
           
            //buipublisher = true;

            movx = (transform.TransformPoint(other.bounds.center).x / game_mec.n_x_scale) + game_mec.oriiginx;
            movy = (transform.TransformPoint(other.bounds.center).y / game_mec.y_scale) + game_mec.oriiginy;

            // Debug.Log("Building number is: " + other.GetComponent<Text>().text + " location: longitude: " + movx.ToString() + " " + " latitude: " + " " + movy.ToString());

            bui[buicheck] = other.GetComponent<Text>().text;
            ++buicheck;
            buipublisher = true;
            buildcheck = other.GetComponent<Text>().text;
            MyDetail md = new MyDetail();
            md.building_number = Convert.ToInt32(other.GetComponent<Text>().text);
            md.longitude = movx;
            md.latitude = movy;
            md.player_id = Convert.ToInt32(ros2.playerid);
        //    md.topic = "/w_building_numbers";

            StringBuilder sb = new StringBuilder();
            using (StringWriter sw = new StringWriter(sb))
            using (JsonTextWriter writer = new JsonTextWriter(sw))
            {
                writer.QuoteChar = '\'';

                JsonSerializer ser = new JsonSerializer();
                ser.Serialize(writer, md);
            }
            building_numbers = sb.ToString();
            //building_numbers = "Building number is: " + other.GetComponent<Text>().text + " location: longitude: " + movx.ToString() + " " + " latitude: " + " " + movy.ToString();
            // Debug.Log("Building number is: " + other.GetComponent<Text>().text + " location: longitude: " + movx.ToString() + " " + " latitude: " + " " + movy.ToString());



        }

        if ( other.gameObject.CompareTag("hdz"))
        {

            if (Math.Sqrt(Math.Pow((rb.transform.position.x - prevoiusCheckSendMessage.x), 2) + Math.Pow((rb.transform.position.z - prevoiusCheckSendMessage.z), 2)) > 10)
            {
                prevoiusCheckSendMessage.x = rb.transform.position.x;
                prevoiusCheckSendMessage.z = rb.transform.position.z;
                hdzpublisher = true;
                movx = (rb.transform.position.x / game_mec.n_x_scale) + game_mec.oriiginx;
                movy = (rb.transform.position.z / game_mec.y_scale) + game_mec.oriiginy;
                // hdzcoordinations = "Human Dangerous Zone: longitude: " + movx.ToString() + " " + " latitude: " + " " + movy.ToString();
                MyDetail md = new MyDetail();
                md.longitude = movx;
                md.latitude = movy;
                md.player_id = Convert.ToInt32(ros2.playerid);
               // md.topic = "/w_hdzcoordinates";
                //hdzcoordinations = "{   'longitude': '" + movx.ToString() + "' ,  'latitude': '" + movy.ToString()+"'}";
                // string jsonData =  "{   'FirstName': 'Jignesh', 'LastName': 'Trivedi'}";

                StringBuilder sb = new StringBuilder();
                using (StringWriter sw = new StringWriter(sb))
                using (JsonTextWriter writer = new JsonTextWriter(sw))
                {
                    writer.QuoteChar = '\'';

                    JsonSerializer ser = new JsonSerializer();
                    ser.Serialize(writer, md);
                }

                hdzcoordinations = sb.ToString();
                 
                //Debug.Log(hdzcoordinations);
               
            }
            else { hdzpublisher = false; }


            // txt2.text = "Collision detected in hdz";
            // if (rb.transform.position.x > 0)
            // {
            //     movx = (rb.transform.position.x / game_mec.n_x_scale) + game_mec.oriiginx;
            //     //movx = (other.transform.position.x / game_mec.n_x_scale) + game_mec.oriiginx;
            // }
            // else
            // {
            //     movx = (rb.transform.position.x / game_mec.p_x_scale) + game_mec.oriiginx;
            //     //movx = (other.transform.position.x / game_mec.n_x_scale) + game_mec.oriiginx;
            // }
            //  movy = (rb.transform.position.z / game_mec.y_scale) + game_mec.oriiginy;
            //// movy = (other.transform.position.y / game_mec.y_scale) + game_mec.oriiginy;

            //   hdzcoordinations = "Human Dangerous Zone: longitude: " + movx.ToString() + " " + " latitude: " + " " + movy.ToString() + " altitude: " + " " + rb.transform.position.y.ToString();
            // //  hdzcoordinations = "Human Dangerous Zone: longitude: " + movx.ToString() + " " + " latitude: " + " " + movy.ToString() + " size: " + " " + other.bounds.size.ToString();


        }

        if (other.gameObject.CompareTag("dz"))
        {
           
           
            if (Math.Sqrt(Math.Pow((rb.transform.position.x - prevoiusCheckSendMessagedz.x),2)+ Math.Pow((rb.transform.position.z - prevoiusCheckSendMessagedz.z),2)) > 10)
            {
                prevoiusCheckSendMessagedz.x = rb.transform.position.x;
                prevoiusCheckSendMessagedz.z = rb.transform.position.z;
                ddzpublisher = true;
                movx = (rb.transform.position.x / game_mec.n_x_scale) + game_mec.oriiginx;
                movy = (rb.transform.position.z / game_mec.y_scale) + game_mec.oriiginy;
                // ddzcoordinations = "Drone Dangerous Zone: longitude: " + movx.ToString() + " " + " latitude: " + " " + movy.ToString();
                MyDetail md = new MyDetail();
                md.latitude = movy;
                md.longitude= movx;
                
                md.player_id = Convert.ToInt32(ros2.playerid);
             //   md.topic = "/w_ddzcoordinates";

                StringBuilder sb = new StringBuilder();
                using (StringWriter sw = new StringWriter(sb))
                using (JsonTextWriter writer = new JsonTextWriter(sw))
                {
                    writer.QuoteChar = '\'';

                    JsonSerializer ser = new JsonSerializer();
                    ser.Serialize(writer, md);
                }

                ddzcoordinations = sb.ToString();
            }
            else { ddzpublisher = false; }
            if (2 < rb.transform.position.y && rb.transform.position.y <= 20)
            { check = "dd"; }
            else { check = "ndd"; }
            //ddzpublisher = true;
            //if (rb.transform.position.x > 0) { movx = (rb.transform.position.x / game_mec.n_x_scale) + game_mec.oriiginx; }
            //else { movx = (rb.transform.position.x / game_mec.p_x_scale) + game_mec.oriiginx; }
            //movy = (rb.transform.position.z / game_mec.y_scale) + game_mec.oriiginy;
            //ddzcoordinations = "Drone Dangerous Zone: longitude: " + movx.ToString() + " " + " latitude: " + " " + movy.ToString() + " altitude: " + " " + rb.transform.position.y.ToString();


        }

        if (other.gameObject.CompareTag("dc"))
        {
            
			clue_id = Convert.ToInt32(getBetween(other.GetComponent<Text>().text, "\'", "\'"));

			Debug.Log (clue_id);
			Debug.Log (found_clues[clue_id - 1]);

            movx = (other.transform.position.x / game_mec.n_x_scale) + game_mec.oriiginx;
            movy = (other.transform.position.z / game_mec.y_scale) + game_mec.oriiginy;

			// if (rb.transform.position.y <= 20 && !found_clues[clue_id - 1])

			if (rb.transform.position.y <= 20)
            {
				dcpublisher = true;

                //txt3.text = "Drone Clue Detected " + " The Clue is: " + other.GetComponent<GUIText>().text;
                movx = (other.transform.position.x / game_mec.n_x_scale) + game_mec.oriiginx;
                movy = (other.transform.position.z / game_mec.y_scale) + game_mec.oriiginy;
                //drone_clue_value = "The Clue is: " + " " + other.GetComponent<Text>().text+ "Location is: longitude: " + movx.ToString() + " " + " latitude: " + " " + movy.ToString();
                //dccoordinations = "Drone Clue: longitude: " + movx.ToString() + " " + " latitude: " + " " + movy.ToString() + " Value: " + other.GetComponent<Text>().text ;
                MyDetail md = new MyDetail();
                md.latitude = movy;
                md.longitude = movx;
                
                md.player_id =Convert.ToInt32(ros2.playerid);
                // md.topic = "/w_dccoordinates";
                md.drone_clue_id=Convert.ToInt32(getBetween(other.GetComponent<Text>().text, "\'", "\'"));
               
                md.drone_clue_value = Convert.ToInt32(other.GetComponent<Text>().text.Substring(other.GetComponent<Text>().text.LastIndexOf(':')+1));

                StringBuilder sb = new StringBuilder();
                using (StringWriter sw = new StringWriter(sb))
                using (JsonTextWriter writer = new JsonTextWriter(sw))
                {
                    writer.QuoteChar = '\'';

                    JsonSerializer ser = new JsonSerializer();
                    ser.Serialize(writer, md);
                }

                dccoordinations = sb.ToString();
				found_clues [clue_id - 1] = true;
            }
            else
            {
				// For the purposes of testing, I'm going to comment out everything that happens if the rb.transform.position.y >= 20 -- MC

				/* 
                // dccoordinations = "Drone Clue: longitude: " + movx.ToString() + " " + " latitude: " + " " + movy.ToString() + " Value: NON ";
                MyDetail md = new MyDetail();
                md.latitude = movy;
                md.longitude = movx;
               
                md.player_id = Convert.ToInt32(ros2.playerid);
                md.drone_clue_id = Convert.ToInt32(getBetween(other.GetComponent<Text>().text, "\'", "\'"));
                //  md.topic = "/w_dccoordinates";

                StringBuilder sb = new StringBuilder();
                using (StringWriter sw = new StringWriter(sb))
                using (JsonTextWriter writer = new JsonTextWriter(sw))
                {
                    writer.QuoteChar = '\'';

                    JsonSerializer ser = new JsonSerializer();
                    ser.Serialize(writer, md);
                }

                dccoordinations = sb.ToString();
				*/
            }
        }

        if (other.gameObject.CompareTag("hc") )
        {

			clue_id = Convert.ToInt32(getBetween(other.GetComponent<Text>().text, "\'", "\'"));

			Debug.Log (clue_id);
			Debug.Log (found_clues[clue_id - 1]);
           
            //  txt2.text = "Collision detected in hc ";
            movx = (other.transform.position.x / game_mec.n_x_scale) + game_mec.oriiginx;
            movy = (other.transform.position.z / game_mec.y_scale) + game_mec.oriiginy;

			// This if-statement added to test drone-ineligibility of finding clues when rb.transform.position.y >= 20  --MC

			//if (rb.transform.position.y <= 20 && !found_clues[clue_id - 1]) 

			if (rb.transform.position.y <= 20) 
			{
				hcpublisher = true;

				movx = (other.transform.position.x / game_mec.n_x_scale) + game_mec.oriiginx;
				movy = (other.transform.position.z / game_mec.y_scale) + game_mec.oriiginy;

				//  hccoordinations = "Human Clue: longitude: " + movx.ToString() + " " + " latitude: " + " " + movy.ToString() + " altitude: " + " " + rb.transform.position.y.ToString();
				MyDetail md = new MyDetail ();
				md.latitude = movy;
				md.longitude = movx;

				md.player_id = Convert.ToInt32 (ros2.playerid);
				md.drone_clue_id = Convert.ToInt32 (getBetween (other.GetComponent<Text> ().text, "\'", "\'"));
				//   md.topic= "/w_hccoordinates";
				// md.drone_clue_value = other.GetComponent<Text>().text;

				StringBuilder sb = new StringBuilder ();
				using (StringWriter sw = new StringWriter (sb))
				using (JsonTextWriter writer = new JsonTextWriter (sw)) {
					writer.QuoteChar = '\'';

					JsonSerializer ser = new JsonSerializer ();
					ser.Serialize (writer, md);
				}

				hccoordinations = sb.ToString ();
				found_clues[clue_id - 1] = true;
			 }
         }
    }

    private void OnTriggerStay(Collider other)
    {
       // if (other.gameObject.CompareTag("building") )
       //{
       // ////    buipublisher = true;
       //     movx = (rb.transform.position.x / game_mec.n_x_scale) + game_mec.oriiginx;
       //    movy = (rb.transform.position.z / game_mec.y_scale) + game_mec.oriiginy;
       //     ////    // debug.log("building number is: " + other.getcomponent<text>().text + " location: longitude: " + movx.tostring() + " " + " latitude: " + " " + movy.tostring());
       //     // building_numbers = "building number is: " + other.getcomponent<text>().text + " location: longitude: " + movx.tostring() + " " + " latitude: " + " " + movy.tostring();
       //     buildcheck = other.GetComponent<Text>().text;
       //     building_numbers = "building number is: " + other.GetComponent<Text>().text + " location: longitude: " + movx.ToString() + " " + " latitude: " + " " + movy.ToString();
       // ////    // debug.log("building number is: " + other.getcomponent<text>().text + " location: longitude: " + movx.tostring() + " " + " latitude: " + " " + movy.tostring());
       // ////    bui[buicheck] = other.getcomponent<text>().text;
       // ////    ++buicheck;




       // }
        //else if (bui.Contains(other.GetComponent<Text>().text)) { buipublisher = false; }
        if (other.gameObject.CompareTag("hdz"))
        {

            if (Math.Sqrt(Math.Pow((rb.transform.position.x - prevoiusCheckSendMessage.x), 2) + Math.Pow((rb.transform.position.z - prevoiusCheckSendMessage.z), 2)) > 10)
            {
                prevoiusCheckSendMessage.x = rb.transform.position.x;
                prevoiusCheckSendMessage.z = rb.transform.position.z;
                hdzpublisher = true;
                movx = (rb.transform.position.x / game_mec.n_x_scale) + game_mec.oriiginx;
                movy = (rb.transform.position.z / game_mec.y_scale) + game_mec.oriiginy;
                //hdzcoordinations = "Human Dangerous Zone: longitude: " + movx.ToString() + " " + " latitude: " + " " + movy.ToString();
                MyDetail md = new MyDetail();
                md.longitude = movx;
                md.latitude = movy;
                md.player_id = Convert.ToInt32(ros2.playerid);
             //   md.topic = "/w_hdzcoordinates";
                //hdzcoordinations = "{   'longitude': '" + movx.ToString() + "' ,  'latitude': '" + movy.ToString()+"'}";
                // string jsonData =  "{   'FirstName': 'Jignesh', 'LastName': 'Trivedi'}";

                StringBuilder sb = new StringBuilder();
                using (StringWriter sw = new StringWriter(sb))
                using (JsonTextWriter writer = new JsonTextWriter(sw))
                {
                    writer.QuoteChar = '\'';

                    JsonSerializer ser = new JsonSerializer();
                    ser.Serialize(writer, md);
                }

                hdzcoordinations = sb.ToString();

               // Debug.Log(hdzcoordinations); 
            }
            else { hdzpublisher = false; }
            // hdzpublisher = true;
            // // txt2.text = "Collision detected in hdz";
            // if (rb.transform.position.x > 0)
            // {
            //     movx = (rb.transform.position.x / game_mec.n_x_scale) + game_mec.oriiginx;
            //    // movx= (other.transform.position.x / game_mec.n_x_scale) + game_mec.oriiginx;
            // }
            // else {
            //     //movx = (rb.transform.position.x / game_mec.p_x_scale) + game_mec.oriiginx;
            //     movx = (other.transform.position.x / game_mec.n_x_scale) + game_mec.oriiginx;
            // }
            //  movy = (rb.transform.position.z / game_mec.y_scale) + game_mec.oriiginy;
            //// movy = (other.transform.position.y / game_mec.y_scale) + game_mec.oriiginy;

            // //  Debug.Log(other.transform.position);
            // // Debug.Log(other.bounds.size);

            //   hdzcoordinations = "Human Dangerous Zone: longitude: " + movx.ToString() + " " + " latitude: " + " " + movy.ToString() + " altitude: " + " " + rb.transform.position.y.ToString();
            //// hdzcoordinations = "Human Dangerous Zone: longitude: " + movx.ToString() + " " + " latitude: " + " " + movy.ToString() + " size: " + " " + other.bounds.size.ToString();


        }

        if (other.gameObject.CompareTag("dz"))
        {
            
            
            if (Math.Sqrt(Math.Pow((rb.transform.position.x - prevoiusCheckSendMessagedz.x), 2) + Math.Pow((rb.transform.position.z - prevoiusCheckSendMessagedz.z), 2)) > 10)
            {
                prevoiusCheckSendMessagedz.x = rb.transform.position.x;
                prevoiusCheckSendMessagedz.z = rb.transform.position.z;
                ddzpublisher = true;
                movx = (rb.transform.position.x / game_mec.n_x_scale) + game_mec.oriiginx;
                movy = (rb.transform.position.z / game_mec.y_scale) + game_mec.oriiginy;
                // ddzcoordinations = "Drone Dangerous Zone: longitude: " + movx.ToString() + " " + " latitude: " + " " + movy.ToString();
                MyDetail md = new MyDetail();
                md.longitude = movx;
                md.latitude = movy;
                md.player_id = Convert.ToInt32(ros2.playerid);
            //    md.topic = "/w_ddzcoordinates";

                StringBuilder sb = new StringBuilder();
                using (StringWriter sw = new StringWriter(sb))
                using (JsonTextWriter writer = new JsonTextWriter(sw))
                {
                    writer.QuoteChar = '\'';

                    JsonSerializer ser = new JsonSerializer();
                    ser.Serialize(writer, md);
                }

                ddzcoordinations = sb.ToString();
            }
            else { ddzpublisher = false; }
            if (5 < rb.transform.position.y && rb.transform.position.y <= 20)
            { check = "dd"; }
            else { check = "ndd";  }
            //ddzpublisher = true;
            //if (rb.transform.position.x > 0) { movx = (rb.transform.position.x / game_mec.n_x_scale) + game_mec.oriiginx; }
            //else { movx = (rb.transform.position.x / game_mec.p_x_scale) + game_mec.oriiginx; }
            //movy = (rb.transform.position.z / game_mec.y_scale) + game_mec.oriiginy;
            //ddzcoordinations = "Drone Dangerous Zone: longitude: " + movx.ToString() + " " + " latitude: " + " " + movy.ToString() + " altitude: " + " " + rb.transform.position.y.ToString();


        }

        //if (other.gameObject.CompareTag("dc"))
        //{
        //    dcpublisher = true;
        //    movx = (other.transform.position.x / game_mec.n_x_scale) + game_mec.oriiginx;
        //    movy = (other.transform.position.z / game_mec.y_scale) + game_mec.oriiginy;

        //   // dccoordinations = "Drone Clue: longitude: " + movx.ToString() + " " + " latitude: " + " " + movy.ToString() + " altitude: " + " " + rb.transform.position.y.ToString();

        //    if (rb.transform.position.y < 30)
        //    {
        //        //txt3.text = "Drone Clue Detected " + " The Clue is: " + other.GetComponent<GUIText>().text;
        //        movx = (other.transform.position.x / game_mec.n_x_scale) + game_mec.oriiginx;
        //        movy = (other.transform.position.z / game_mec.y_scale) + game_mec.oriiginy;
        //        //drone_clue_value = "The Clue is: " + " " + other.GetComponent<Text>().text + "Location is: longitude: " + movx.ToString() + " " + " latitude: " + " " + movy.ToString();
        //        MyDetail md = new MyDetail();
        //        md.longitude = movx;
        //        md.latitude = movy;
        //        md.drone_clue_value = Convert.ToInt32(other.GetComponent<Text>().text);
        //        md.player_id = Convert.ToInt32(ros2.playerid);
        //     //   md.topic= "/w_dccoordinates";

        //        StringBuilder sb = new StringBuilder();
        //        using (StringWriter sw = new StringWriter(sb))
        //        using (JsonTextWriter writer = new JsonTextWriter(sw))
        //        {
        //            writer.QuoteChar = '\'';

        //            JsonSerializer ser = new JsonSerializer();
        //            ser.Serialize(writer, md);
        //        }

        //        dccoordinations = sb.ToString();
        //    }
        //    else
        //    {
        //        MyDetail md = new MyDetail();
        //        md.longitude = movx;
        //        md.latitude = movy;
        //        md.player_id = Convert.ToInt32(ros2.playerid);
        //     //   md.topic = "/w_dccoordinates";
        //        // md.drone_clue_value = other.GetComponent<Text>().text;

        //        StringBuilder sb = new StringBuilder();
        //        using (StringWriter sw = new StringWriter(sb))
        //        using (JsonTextWriter writer = new JsonTextWriter(sw))
        //        {
        //            writer.QuoteChar = '\'';

        //            JsonSerializer ser = new JsonSerializer();
        //            ser.Serialize(writer, md);
        //        }

        //        dccoordinations = sb.ToString();
        //    }
        //}

        //if (other.gameObject.CompareTag("hc"))
        //{
        //    hcpublisher = true;
        //    //  txt2.text = "Collision detected in hc ";
        //    movx = (other.transform.position.x / game_mec.n_x_scale) + game_mec.oriiginx;
        //    movy = (other.transform.position.z / game_mec.y_scale) + game_mec.oriiginy;

        //    // dccoordinations = "Human Clue: longitude: " + movx.ToString() + " " + " latitude: " + " " + movy.ToString() + " altitude: " + " " + rb.transform.position.y.ToString();
        //    MyDetail md = new MyDetail();
        //    md.latitude = movy;
        //    md.longitude = movx;
        //    md.player_id = Convert.ToInt32(ros2.playerid);
        ////    md.topic= "/w_hccoordinates";
        //    // md.drone_clue_value = other.GetComponent<Text>().text;

        //    StringBuilder sb = new StringBuilder();
        //    using (StringWriter sw = new StringWriter(sb))
        //    using (JsonTextWriter writer = new JsonTextWriter(sw))
        //    {
        //        writer.QuoteChar = '\'';

        //        JsonSerializer ser = new JsonSerializer();
        //        ser.Serialize(writer, md);
        //    }

        //    hccoordinations = sb.ToString();

        //}
    }


        private void OnTriggerExit(Collider other)
    {
        

        if (other.gameObject.CompareTag("dz"))
        {
            ddzpublisher = false;
            check = "ndd";

        }
        if (other.gameObject.CompareTag("hc"))
        {
            hcpublisher = false;

        }
        if (other.gameObject.CompareTag("dc"))
        {
            dcpublisher = false;

        }
        if (other.gameObject.CompareTag("building"))
        {
            buipublisher = false;

        }
        if (other.gameObject.CompareTag("hdz"))
        {
            hdzpublisher = false;

        }


    }
}
