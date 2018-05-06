using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRay : MonoBehaviour {

    public new Camera camera;
    public float rayDist = 10.0f;

    public Ray someGaze() {
        return camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
    }
}
