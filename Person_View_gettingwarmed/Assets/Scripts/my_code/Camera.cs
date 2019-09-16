using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

    public GameObject player;
    private Vector3 offset;
    int velocidade = 30;
    // Use this for initialization
    void Start () {
        offset = player.transform.position;
        
    }
	
	// Update is called once per frame
	void LateUpdate () {
        transform.position = player.transform.position;
        if (Input.GetKey(KeyCode.R))
        {
      transform.Rotate(0, velocidade * Time.deltaTime,0, Space.Self);

        //    Transform par = transform.parent.transform;
        //    transform.rotation = Quaternion.identity;
        //    transform.RotateAround(transform.position, par.right, transform.rotation.x);
        }

    }
}
