using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SurroundingCollider : MonoBehaviour
{
    
    Mesh mesh;
    List<Vector3> verticies;
    bool drawn = false;
    

	    // Use this for initialization
	void Start ()
    {
        mesh = GetComponent<MeshFilter>().sharedMesh;
        Vector3[] allVerticies = mesh.vertices;
        verticies = new List<Vector3>();

        foreach (Vector3 v in allVerticies)
        {

            if (v.y > 1 && !verticies.Contains(new Vector3(-v.x, v.y, -v.z)))
            {
//                Debug.Log("Added");
                Vector3 theVector = new Vector3(-v.x, v.y, -v.z);
                verticies.Add(theVector);
            }
        }

        int i;
        for (i = 0; i < verticies.Count-1; ++i)
        {
            makeColliderBetweenTwoPoints(verticies[i], verticies[i+1]);
        }
        makeColliderBetweenTwoPoints(verticies[0], verticies[i]);

    }

    void makeColliderBetweenTwoPoints(Vector3 point1, Vector3 point2)
    {
        Vector3 middle = Vector3.Lerp(point1, point2, 0.5f);
        middle.y = 0.2f;
        
        GameObject g1 = Instantiate(Resources.Load("Collider"), middle, Quaternion.identity) as GameObject;
        g1.transform.parent = gameObject.transform;
        
        BoxCollider collider = g1.GetComponent<BoxCollider>();
        Vector3 length = point2 - point1;
        float size = Vector3.Distance(point1, point2);
        collider.size = new Vector3(9f, 4, size);


        g1.transform.Rotate(new Vector3(0, Vector3.SignedAngle(g1.transform.forward, length.normalized, Vector3.up), 0));
        // Debug.Log(length.normalized);
        // Debug.Log(Vector3.SignedAngle(Vector3.forward, length.normalized, Vector3.up));
    }

}
