using UnityEngine;
using System.IO;
using System.Xml;
using System.Globalization;
using UnityEngine.UI;

public class game_mec : MonoBehaviour
{

    Vector3[] coords = new Vector3[4];

    string ddzvectors;
    string hdzvectors;
    string dcvectors;
    string hcvectors;
    string[] singlevector = new string[1000];
    string[] array = new string[3];
    public static double n_x_scale; 
    //= -84165.03045352287;
    public static double p_x_scale;
    //= -134165.03045352287;
    public static double y_scale;
    //= -108785.2905048975;
    
    Vector3 result, result2;
    public GameObject[] droneclues;
    float longi;
    float lat;
    int hcounter = 0; int counter = 0;public static int counter2 = 0; int hcounter2 = 0;
    static int count = 0;
    string[] clues; string[] Hclues;
    public static double oriiginx;
    //= (-106.7520998723799);
    public static double oriiginy;
    //= (32.28106550836159);

    void Start()
    {

		//Debug.Log ("RED: Inside game_mec.cs");

		/*
		string filepath = @"/Users/longtran/Desktop/setting/new_mat_version2_1.kml";
		string filepath2 = @"/Users/longtran/Desktop/setting/config.xml";
		*/

		//string filepath = @"/Users/theradbarron/Desktop/PIXLgit/URSGameEnvironments/current version/newsetting/new_mat_version2_1.kml";
		//string filepath2 = @"/Users/theradbarron/Desktop/PIXLgit/URSGameEnvironments/current version/newsetting/config.xml" ;

		string filepath = @"Assets/XML-KML/new_mat_version2_1.kml";
		string filepath2 = @"Assets/XML-KML/config.xml";


        var format2 = new CultureInfo("en-US").NumberFormat;

        format2.NegativeSign = "-";

        XmlDocument xmlDoc2 = new XmlDocument();

        if (File.Exists(filepath2))
        {
            xmlDoc2.Load(filepath2);

            XmlNodeList transformList6 = xmlDoc2.GetElementsByTagName("originx");
            XmlNodeList transformList7 = xmlDoc2.GetElementsByTagName("clue");
            XmlNodeList transformList8 = xmlDoc2.GetElementsByTagName("Hclue");

            clues = new string[transformList7.Count];
            Hclues = new string[transformList8.Count];

            foreach (XmlNode xn7 in transformList7)
            {

                clues[counter] = xn7.InnerText.ToString();
				//Debug.Log("RED:/CLUES-COUNT-Inner"+xn7.InnerText.ToString());
                counter++;

            }
            foreach (XmlNode xn8 in transformList8)
            {

                Hclues[hcounter] = xn8.InnerText.ToString();
                hcounter++;

            }
            foreach (XmlNode xn6 in transformList6)
            {

                 oriiginx = double.Parse(xn6.InnerText.ToString(),format2);

            }

            XmlNodeList transformList2 = xmlDoc2.GetElementsByTagName("originy");
            foreach (XmlNode xn2 in transformList2)
            {
				
				oriiginy = double.Parse (xn2.InnerText.ToString (), format2);

            }

            XmlNodeList transformList3 = xmlDoc2.GetElementsByTagName("n_x_scale");
            foreach (XmlNode xn3 in transformList3)
            {
				
                n_x_scale = double.Parse(xn3.InnerText.ToString(), format2);

            }

            XmlNodeList transformList4 = xmlDoc2.GetElementsByTagName("p_x_scale");
            foreach (XmlNode xn4 in transformList4)
            {
				
                p_x_scale = double.Parse(xn4.InnerText.ToString(), format2);

            }

            XmlNodeList transformList5 = xmlDoc2.GetElementsByTagName("y_scale");
            foreach (XmlNode xn5 in transformList5)
            {
				
                y_scale = double.Parse(xn5.InnerText.ToString(), format2);

            }

        }
			
             
        //  Debug.Log(filepath);

        /*   for(int i=0;i<2;++i)
            { Instantiate(Resources.Load("drone"), result, Quaternion.Euler(-90, 0, 90)) ; }*/

      

        XmlDocument xmlDoc = new XmlDocument();

        if (File.Exists(filepath) & count == 0)
        {

			//Debug.Log ("Hello REDWAN!!! KML FILE found");

            xmlDoc.Load(filepath);
            XmlNodeList transformList = xmlDoc.GetElementsByTagName("Placemark");

            foreach (XmlNode xn in transformList)
            {

                if (xn["name"].InnerText.StartsWith("dc"))
                {
					
                    dcvectors = xn.InnerText.ToString();

					//Debug.Log ("dcvectors: ");
					//Debug.Log ("dcvectors: " + dcvectors);

                    singlevector = dcvectors.Split(null);

                    var format = new CultureInfo("en-US").NumberFormat;
                    format.NegativeSign = "-";

                    double longid; double latd; int dclength = singlevector.Length - 16;

					GameObject[]dc=new GameObject[dclength];int dronecluecounter=0;
                   
					for (int i = 8; i < singlevector.Length-8; ++i)
					{
						array = singlevector[i].Split(',');
						//Debug.Log ("Array[0] /long: " + array[0] + "Array[1]/ lat: " + array[1] + "Array[2]/ altitude: " + array[2] );


						longi = float.Parse(array[0], format);


						lat = float.Parse(array[1], format);

						longid = longi;
						latd = lat;

						longid = (longid - oriiginx) * n_x_scale;
						latd = (latd - oriiginy) * y_scale;

						//Debug.Log ("After Conversion /long: " + longid + "After Conversion /lat:" + latd);


						result = new Vector3((float)longid, (float)16, (float)latd);

						//Debug.Log ("result vector: " + result);

						dc[dronecluecounter] = Instantiate(Resources.Load("DC"), result, Quaternion.Euler(90, 0, 0)) as GameObject;
						dc[dronecluecounter].AddComponent<Text>();
						dc[dronecluecounter].GetComponent<Text>().text = clues[counter2];
						dc[dronecluecounter].tag = "dc";
						dronecluecounter++;
					}
				
                    ++counter2;

                }

                if (xn["name"].InnerText.StartsWith("ddz"))
                {
                    ddzvectors = xn.InnerText.ToString();

                    singlevector = ddzvectors.Split(null);

                    double lg_max = -1000;double lt_max = -1000;
                    double lg_min = 1000; double lt_min = 1000;

                    for (int i = 8; i < singlevector.Length && singlevector[i] != ""; ++i)
                    {
						
                        array = singlevector[i].Split(',');

                        var format = new CultureInfo("en-US").NumberFormat;
                        format.NegativeSign = "-";

                        longi = float.Parse(array[0], format);


                        lat = float.Parse(array[1], format);

                        double longid = longi;
                        double latd = lat;

                        if ((longid - oriiginx) < 0)
                        { longid = (longid - oriiginx) * n_x_scale; }
                        else

                        { longid = (longid - oriiginx) * p_x_scale; }
                        latd = (latd - oriiginy) * y_scale;

                        if(lg_max<longid && lt_max < latd) { lg_max = longid;lt_max = latd; }
                        if(lg_min > longid && lt_min > latd) { lg_min = longid; lt_min = latd; }

                    }

                    result = new Vector3((float)lg_max, (float)0.0001, (float)lt_max);
                    // GameObject ddz = Instantiate(Resources.Load("D_DnZ"), result, Quaternion.Euler(90, 0, 0)) as GameObject;

                    result2 = new Vector3((float)lg_min, (float)0.0001, (float)lt_min);
                    // GameObject ddz2 = Instantiate(Resources.Load("D_DnZ"), result2, Quaternion.Euler(90, 0, 0)) as GameObject;

                    Vector3 center = (result + result2) * 0.5f;
                    GameObject ddz3 = Instantiate(Resources.Load("D_DnZ"), center, Quaternion.Euler(90, 0, 0)) as GameObject;
                    Vector3 scale = result - result2;
                    scale.x = Mathf.Abs(scale.x);
                    scale.y = Mathf.Abs(scale.y);
                    scale.z = Mathf.Abs(scale.z);
                   
                    // scale = scale * 0.5f;
                    // Debug.Log(scale.y); Debug.Log(scale.z);

                    ddz3.transform.localScale = new Vector3((float)scale.x, (float)scale.z, 1000);
                   
                    ddz3.tag = "dz";
                     
                }
					
				if (xn["name"].InnerText.StartsWith("hdz"))
				{
					
					hdzvectors = xn.InnerText.ToString();

                    singlevector = hdzvectors.Split(null);
                  	double lg_max = -1000; double lt_max = -1000;
                    double lg_min = 1000; double lt_min = 1000;
                    for (int i = 8; i < singlevector.Length && singlevector[i] != ""; ++i)
                    {

						array = singlevector[i].Split(',');
                        var format = new CultureInfo("en-US").NumberFormat;
                        format.NegativeSign = "-";

                        longi = float.Parse(array[0], format);


                        lat = float.Parse(array[1], format);

                        double longid = longi;
                        double latd = lat;

                        if ((longid - oriiginx) < 0)
                        { longid = (longid - oriiginx) * n_x_scale; }
                        else

                        { longid = (longid - oriiginx) * p_x_scale; }
                        latd = (latd - oriiginy) * y_scale;

                        if (lg_max < longid && lt_max < latd) { lg_max = longid; lt_max = latd; }
                        if (lg_min > longid && lt_min > latd) { lg_min = longid; lt_min = latd; }
					
                      }

                    result = new Vector3((float)lg_max, (float)0.0001, (float)lt_max);
                    result2 = new Vector3((float)lg_min, (float)0.0001, (float)lt_min);

                    // GameObject hdz = Instantiate(Resources.Load("Hu_DnZ"), result, Quaternion.Euler(90, 0, 0)) as GameObject;
                    // GameObject hdz2 = Instantiate(Resources.Load("Hu_DnZ"), result2, Quaternion.Euler(90, 0, 0)) as GameObject;

                    Vector3 center = (result + result2) * 0.5f;
                    GameObject hdz3 = Instantiate(Resources.Load("Hu_DnZ"), center, Quaternion.Euler(90, 0, 0)) as GameObject;
                    Vector3 scale = result - result2;

                    scale.x = Mathf.Abs(scale.x);
                    scale.y = Mathf.Abs(scale.y);
                    scale.z = Mathf.Abs(scale.z);

                    //scale = scale * 0.5f;
                    //Debug.Log(scale.y); Debug.Log(scale.z);

                    hdz3.transform.localScale = new Vector3((float)scale.x, (float)scale.z, 700);
                    hdz3.tag = "hdz";

                }

				if (xn["name"].InnerText.StartsWith("hc"))
				{
					dcvectors = xn.InnerText.ToString();

					singlevector = dcvectors.Split(null);


					array = singlevector[8].Split(',');
					var format = new CultureInfo("en-US").NumberFormat;
					format.NegativeSign = "-";

					double longid; double latd; int dclength = singlevector.Length - 16;
					GameObject[]hc=new GameObject[dclength];int dronecluecounter=0;

					for (int i = 8; i < singlevector.Length-8; ++i)
					{
						array = singlevector[i].Split(',');


						longi = float.Parse(array[0], format);


						lat = float.Parse(array[1], format);

						longid = longi;
						latd = lat;

						longid = (longid - oriiginx) * n_x_scale;
						latd = (latd - oriiginy) * y_scale;


						result = new Vector3((float)longid, (float)25, (float)latd);

						hc[dronecluecounter] = Instantiate(Resources.Load("HC"), result, Quaternion.Euler(90, 0, 0)) as GameObject;


						hc[dronecluecounter].AddComponent<Text>();
						hc[dronecluecounter].GetComponent<Text>().text = Hclues[hcounter2];
						hc[dronecluecounter].tag = "hc";
						dronecluecounter++;


					}

					++hcounter2;

				}

			}



            GameObject.Find("D_DnZ").SetActive(false);
            GameObject.Find("Hu_DnZ").SetActive(false);
            GameObject.Find("HC").SetActive(false);
            GameObject.Find("DC").SetActive(false);
            //droneclues = GameObject.FindGameObjectsWithTag("dc");
            
            //foreach (GameObject c in droneclues)
            //{
            //   c.GetComponent<Text>().text= clues[counter2];
                
            //    if (clues[counter2]!=checkclue)
            //    { checkclue = clues[counter2]; counter2++; }
            //}

            ++count;
        }
    }

    // GameObject[] ddz; GameObject[] hdz; GameObject[] dc; GameObject[] hc; GameObject[] bun;

    void Update()
    {
     /*   ddz = GameObject.FindGameObjectsWithTag("dz");
        hdz = GameObject.FindGameObjectsWithTag("hdz");
        dc = GameObject.FindGameObjectsWithTag("dc");
        hc = GameObject.FindGameObjectsWithTag("hc");
        bun= GameObject.FindGameObjectsWithTag("bun");*/

      /*  if (drone.rb.transform.position.y > 70)
        {
            for (int i = 0; i < ddz.Length; ++i)
            {
                ddz[i].SetActive(false);
            }

        }
        else
        {
            for (int i = 0; i < ddz.Length; ++i)
            {
                ddz[i].SetActive(true);
            }
        }

        if (drone.rb.transform.position.y > 50)
        {

            for (int i = 0; i < dc.Length; ++i)
            {
                dc[i].SetActive(false);
            }
            for (int i = 0; i < hc.Length; ++i)
            {
                hc[i].SetActive(false);
            }

        }

        else
        {
            
            for (int i = 0; i < dc.Length; ++i)
            {
                dc[i].SetActive(true);
            }
            for (int i = 0; i < hc.Length; ++i)
            {
                hc[i].SetActive(true);
            }


        }

        if (drone.rb.transform.position.y > 30)
        {
            for (int i = 0; i < hdz.Length; ++i)
            {
                hdz[i].SetActive(false);
            }

            for (int i = 0; i < bun.Length; ++i)
            {
                bun[i].SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < hdz.Length; ++i)
            {
                hdz[i].SetActive(true);
            }

            for (int i = 0; i < bun.Length; ++i)
            {
                bun[i].SetActive(true);
            }
        }*/
    }
}
