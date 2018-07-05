using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ROSBridgeLib;

public class drone : MonoBehaviour {
 
    public float speed=3;

    private Rigidbody rb;
    ROSBridgeWebSocketConnection test = new ROSBridgeWebSocketConnection("ws://spitfire.cs.nmsu.edu", 9090);
    float x, y, z;
  

 //   interface Itest { ROSBridgeWebSocketConnection.}

    // Use this for initialization
    void Start () {


        rb = GetComponent<Rigidbody>();
        if (ROSBridgeWebSocketConnection.coordmessage != null)
        {
            string datay = getBetween(ROSBridgeWebSocketConnection.coordmessage, "y", ",");
            string datax = getBetween(ROSBridgeWebSocketConnection.coordmessage, "x", ",");
            string dataz = getBetween(ROSBridgeWebSocketConnection.coordmessage, "z", ",");

            datay = datay.Substring(datay.LastIndexOf(':') + 1);
            datax = datax.Substring(datax.LastIndexOf(':') + 1);
            dataz = dataz.Substring(dataz.LastIndexOf(':') + 1);
            dataz = dataz.TrimEnd('}');

            z = float.Parse(datay);
            x = float.Parse(datax);
            y = float.Parse(dataz) ;

            Debug.Log("float y is: " + y);
            Debug.Log("float x is: " + x);
            Debug.Log("float z is: " + z);

            //change y with z and divided by ten

        }
        transform.position = new Vector3(x, y, z);
        


        

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

    // Update is called once per frame
    void FixedUpdate () {
       // Debug.Log("this is from coords in drone update" + ROSBridgeWebSocketConnection.coordmessage);

        if (ROSBridgeWebSocketConnection.coordmessage != null)
        {
            string datay = getBetween(ROSBridgeWebSocketConnection.coordmessage, "y", ",");
            string datax= getBetween(ROSBridgeWebSocketConnection.coordmessage, "x", ",");
            string dataz = getBetween(ROSBridgeWebSocketConnection.coordmessage, "z", ",");
          
            datay = datay.Substring(datay.LastIndexOf(':') + 1);
            datax = datax.Substring(datax.LastIndexOf(':') + 1);
            dataz = dataz.Substring(dataz.LastIndexOf(':') + 1);
            dataz = dataz.TrimEnd('}');
           
            z = float.Parse(datay);
            x = float.Parse(datax);
            y = float.Parse(dataz);

            Debug.Log("float y is: " + y);
            Debug.Log("float x is: " + x);
            Debug.Log("float z is: " + z);

            Vector3 newPosition = new Vector3(x, y, z);
            rb.position = Vector3.Lerp(transform.position, newPosition, speed * Time.deltaTime);
            Debug.Log("here are the new vectors: ");
            Debug.Log("x: " + transform.position.x);
            Debug.Log("y: " + transform.position.y);
            Debug.Log("z: " + transform.position.z);

           

            //change y with z and divided by ten
           
        }

        
           

        // Vector3 movement = new Vector3(x, y, z);
        //   rb.AddForce(movement * speed * Time.deltaTime);

        /*  float moveHorizontal = Input.GetAxis("Horizontal");
          float moveVertical = Input.GetAxis("Vertical");
          Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
          rb.AddForce (movement * speed * Time.deltaTime);*/
        /*
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    rb.AddForce( Vector3.right * speed * Time.deltaTime);
                    //transform.position += Vector3.right * speed * Time.deltaTime;
                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    rb.AddForce(Vector3.left * speed * Time.deltaTime);
                    //transform.position += Vector3.left * speed * Time.deltaTime;
                }
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    rb.AddForce(Vector3.forward * speed * Time.deltaTime);
                    //transform.position += Vector3.forward * speed * Time.deltaTime;
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    rb.AddForce(Vector3.back * speed * Time.deltaTime);
                    //transform.position += Vector3.back * speed * Time.deltaTime;
                }
                if (Input.GetKey(KeyCode.Space))
                {
                    if (transform.position.y < 50)
                    {
                        rb.AddForce(Vector3.up * speed * Time.deltaTime);
                        //transform.position += Vector3.up * speed * Time.deltaTime;
                    }
                }

                if (Input.GetKey(KeyCode.RightShift))
                {
                    if (transform.position.y > -90)
                    {
                        rb.AddForce(Vector3.down * speed * Time.deltaTime);
                          //transform.position += Vector3.down * speed * Time.deltaTime;
                    }
                    else { Debug.Log(transform.position.y); }
                }*/

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("here");
        if (col.gameObject.CompareTag("wall") || col.gameObject.CompareTag("building") || col.gameObject.CompareTag("ground"))
        {
            rb.velocity = Vector3.zero;
            Debug.Log("Can not proceed");
            

        }
    }
}
