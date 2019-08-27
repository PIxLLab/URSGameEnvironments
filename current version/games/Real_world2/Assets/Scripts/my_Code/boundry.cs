using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using UnityEngine.UI;

using System.IO;

using System.Xml;
using System.Xml.Linq;
using System.Globalization;

public class boundry : MonoBehaviour {


    Vector3[] coords = new Vector3[4];
    int counter = 0;
    string vectors;
    string []singlevector=new string[4];
    string[] array = new string[3];
   

    void Start()
    {
        float f= -106.7488035102381F;
       // Debug.Log(f);
        double d = -106.7488035102381;

        
        const string filepath = @"C:\Users\User\Desktop\nmsu_uni.kml";


        XmlDocument xmlDoc = new XmlDocument();

        if (File.Exists(filepath))
        {
            // Debug.Log("Here");
            xmlDoc.Load(filepath);

            XmlNodeList transformList = xmlDoc.GetElementsByTagName("coordinates");

            foreach (XmlNode transformInfo in transformList)
            {
                XmlNodeList transformcontent = transformInfo.ChildNodes;
                foreach (XmlNode transformItens in transformcontent)
                {

                    vectors = transformItens.InnerText.ToString();
                    
                    //Debug.Log(vectors);
                   singlevector = vectors.Split(null);

                    for (int i = 0; i < singlevector.Length; ++i)
                    {
                        //Debug.Log(singlevector[i]);
                        array = singlevector[i].Split(',');
                        var format = new NumberFormatInfo();
                        format.NegativeSign = "-";
                        // format.NumberDecimalSeparator = ".";
                        //Vector3 result = new Vector3(float.Parse(array[0],format),float.Parse(array[1],format),float.Parse(array[2],format));
                        // Debug.Log(result);
                        //coords[i] = result;

                      //  for (int j = 0; j < array.Length; ++j)
                       // {
                            //Debug.Log(array[j]);
                        
                        try
                        {
                            Vector3 result = new Vector3(float.Parse(array[0], format), float.Parse(array[1], format), float.Parse(array[2], format));
                            // Debug.Log(float.Parse(array[j], format));
                            try {/* coords[i] = result;*/
                               // Debug.Log(result);
                            }
                            catch (OverflowException) { }


                           
                        }
                        catch (FormatException)
                        {
                               
                                              
                        }
                   // }
                    }


                }
            }
        }
     
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
