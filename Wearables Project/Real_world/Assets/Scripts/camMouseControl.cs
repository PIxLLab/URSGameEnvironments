using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camMouseControl : MonoBehaviour {

    Vector2 mouseControl;
    Vector2 smoothV;
    public float sensitivity = 2.0f;
    public float smoothing = 1.0f;

    GameObject character;
    // Use this for initialization
	void Start () {
        character = this.transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
        mouseControl += smoothV;
        mouseControl.y = Mathf.Clamp(mouseControl.y, -80f, 80f);

        transform.localRotation = Quaternion.AngleAxis(-mouseControl.y, Vector3.right);
        character.transform.localRotation = Quaternion.AngleAxis(mouseControl.x, character.transform.up);
 	}
}
