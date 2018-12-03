using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float movementSpeed = 10.0f;
    public float mouseSensitivity = 2.0f;

    float verticalRotation = 0;
    public float upDownRange = 30.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Mouse rotation
        float rotLeftRight = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, rotLeftRight, 0);

        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);

        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);



        //FB-side Movement
        float forwardSpeed = Input.GetAxis("Vertical") * movementSpeed;
        float sidedSpeed = Input.GetAxis("Horizontal") * movementSpeed;

        Vector3 speed = new Vector3(sidedSpeed, Physics.gravity.y, forwardSpeed);
        speed = transform.rotation * speed;
        CharacterController cc = GetComponent<CharacterController>();
        cc.Move(speed * Time.deltaTime);
		
	}
}
