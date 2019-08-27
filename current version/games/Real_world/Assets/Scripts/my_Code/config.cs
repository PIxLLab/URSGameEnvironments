using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using System.IO;

public class config : MonoBehaviour {

    public int number_of_Drones;
    public  string[] Message_Topics;
    int counter = 0;
    // Use this for initialization
    void Start()
    {

        
        string filepath = @"C:\Users\GDC-Admin\Desktop\newsetting\config.xml";

        XmlDocument xmlDoc = new XmlDocument();
        if (File.Exists(filepath))
        {
            xmlDoc.Load(filepath);
            


            XmlNodeList transformList = xmlDoc.GetElementsByTagName("Topic");
            number_of_Drones = transformList.Count;
            Message_Topics = new string[number_of_Drones];
            foreach (XmlNode xn in transformList)
            {
                Message_Topics[counter]= xn.InnerText.ToString();
                counter++;
            }

            }
    }
        
	
	// Update is called once per frame
	void Update () {
		
	}
}
