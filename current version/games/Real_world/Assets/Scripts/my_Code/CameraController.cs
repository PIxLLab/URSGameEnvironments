
/* new camera code*/

using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public Camera firstPersonCamera;
    public Camera overheadCamera;

    public void ShowOverheadView()
    {
        firstPersonCamera.enabled = false;
        overheadCamera.enabled = true;
    }

    public void ShowFirstPersonView()
    {
        firstPersonCamera.enabled = true;
        overheadCamera.enabled = false;
    }
}
