using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject target;
    public float xOffset = 0;
    public float yOffset = -500;
    public float zOffset = 0;

    void LateUpdate()
    {
        this.transform.position = new Vector3(target.transform.position.x + xOffset,
                                              target.transform.position.y + yOffset,
                                              target.transform.position.z + zOffset);
    }
}
