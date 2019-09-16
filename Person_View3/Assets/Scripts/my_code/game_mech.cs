using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using System.Xml;
using System.Globalization;
using UnityEngine.UI;

public class game_mech : MonoBehaviour {

   
    
    string hdzvectors;
    string dcvectors;
    
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
    int counter = 0; public static int counter2 = 0;
    static int count = 0;
    string[] clues;
    public static double oriiginx;
    //= (-106.7520998723799);
    public static double oriiginy;
    //= (32.28106550836159);
    
    void Start()
    {
        string filepath2 = @"C:\Users\zahra\Desktop\configurations\config3.xml";
        string filepath = @"C:\Users\zahra\Desktop\configurations\new_mat3.kml";
        var format2 = new CultureInfo("en-US").NumberFormat;
        format2.NegativeSign = "-";
        XmlDocument xmlDoc2 = new XmlDocument();
        if (File.Exists(filepath2))
        {
            xmlDoc2.Load(filepath2);



            XmlNodeList transformList6 = xmlDoc2.GetElementsByTagName("originx");
            XmlNodeList transformList7 = xmlDoc2.GetElementsByTagName("Hclue");
           
            clues = new string[transformList7.Count];
            
            foreach (XmlNode xn7 in transformList7)
            {

                clues[counter] = xn7.InnerText.ToString();
                counter++;


            }
            foreach (XmlNode xn6 in transformList6)
            {

                oriiginx = double.Parse(xn6.InnerText.ToString(), format2);
                //Debug.Log(oriiginx);


            }
            XmlNodeList transformList2 = xmlDoc2.GetElementsByTagName("originy");
            foreach (XmlNode xn2 in transformList2)
            {
                oriiginy = double.Parse(xn2.InnerText.ToString(), format2);
                // Debug.Log(xn2.InnerText.ToString());
                // Debug.Log(oriiginy);


            }
            XmlNodeList transformList3 = xmlDoc2.GetElementsByTagName("n_x_scale");
            foreach (XmlNode xn3 in transformList3)
            {
                n_x_scale = double.Parse(xn3.InnerText.ToString(), format2);
                //  Debug.Log(n_x_scale);


            }
            XmlNodeList transformList4 = xmlDoc2.GetElementsByTagName("p_x_scale");
            foreach (XmlNode xn4 in transformList4)
            {
                p_x_scale = double.Parse(xn4.InnerText.ToString(), format2);
                //   Debug.Log(p_x_scale);


            }
            XmlNodeList transformList5 = xmlDoc2.GetElementsByTagName("y_scale");
            foreach (XmlNode xn5 in transformList5)
            {
                y_scale = double.Parse(xn5.InnerText.ToString(), format2);
                //  Debug.Log(y_scale);


            }

        }

        //string filepath = @"C:\Users\User\Desktop\newdz.kml";


        /*   for(int i=0;i<2;++i)
            { Instantiate(Resources.Load("drone"), result, Quaternion.Euler(-90, 0, 90)) ; }*/



        XmlDocument xmlDoc = new XmlDocument();

        if (File.Exists(filepath) & count == 0)
        {

            xmlDoc.Load(filepath);
            XmlNodeList transformList = xmlDoc.GetElementsByTagName("Placemark");



            foreach (XmlNode xn in transformList)
            {
                if (xn["name"].InnerText.StartsWith("treasure"))
                {
                    dcvectors = xn.InnerText.ToString();

                    singlevector = dcvectors.Split(null);


                    array = singlevector[8].Split(',');
                    var format = new CultureInfo("en-US").NumberFormat;
                    format.NegativeSign = "-";

                    longi = float.Parse(array[0], format);


                    lat = float.Parse(array[1], format);

                    double longid = longi;
                    double latd = lat;

                    longid = (longid - oriiginx) * n_x_scale;
                    latd = (latd - oriiginy) * y_scale;


                    result = new Vector3((float)longid, (float)11, (float)latd);
                    GameObject dc1 = Instantiate(Resources.Load("HC"), result, Quaternion.Euler(90, 0, 0)) as GameObject;

                    dc1.AddComponent<Text>();
                    dc1.GetComponent<Text>().text = "Treasure";

                    dc1.tag = "hhc";

                }

                if (xn["name"].InnerText.StartsWith("hc"))
                {
                    dcvectors = xn.InnerText.ToString();

                    singlevector = dcvectors.Split(null);


                    array = singlevector[8].Split(',');
                    var format = new CultureInfo("en-US").NumberFormat;
                    format.NegativeSign = "-";

                    longi = float.Parse(array[0], format);


                    lat = float.Parse(array[1], format);

                    double longid = longi;
                    double latd = lat;

                    longid = (longid - oriiginx) * n_x_scale;
                    latd = (latd - oriiginy) * y_scale;


                    result = new Vector3((float)longid, (float)11, (float)latd);
                    GameObject dc = Instantiate(Resources.Load("HC"), result, Quaternion.Euler(90, 0, 0)) as GameObject;

                    dc.AddComponent<Text>();


                    dc.tag = "hhc";

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



               

            }



          
            GameObject.Find("Hu_DnZ").SetActive(false);
            GameObject.Find("HC").SetActive(false);
           
            droneclues = GameObject.FindGameObjectsWithTag("hhc");
            
            foreach (GameObject c in droneclues)
            {
                if (c.GetComponent<Text>().text != "Treasure")
                {
                    c.GetComponent<Text>().text = clues[counter2];

                    counter2++;
                }
            }
            //Debug.Log(counter2);
            //Debug.Log(count);
            //Debug.Log(droneclues.Length);
            ++count;
        }
    }
            
	
	// Update is called once per frame
	void Update () {
		
	}
}
