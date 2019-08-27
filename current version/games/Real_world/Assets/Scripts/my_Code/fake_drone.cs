/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ROSBridgeLib;
using UnityEngine.UI;

public class fake_drone : MonoBehaviour
{

    public float speed = 10;

    private Rigidbody rb;
   
    float x, y, z;
    int battery = 20;
    public Text txt1;
    private float distanceTravelled;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.position = new Vector3(x, y, z);
        
    }
    
    void FixedUpdate()
    {

        
       

        if (Input.GetKey(KeyCode.RightArrow))
                {
           
            rb.AddForce( Vector3.right * speed * Time.deltaTime);
           
            distanceTravelled += speed * Time.deltaTime;
            Debug.Log("This much the drone moved:");
            Debug.Log(distanceTravelled);
           
            if (distanceTravelled > 10)
            { --battery; txt1.text = battery.ToString(); Debug.Log("drone is moving"); }
            Debug.Log("battery is:");
            Debug.Log(battery);
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
                }
    } 
}*/
