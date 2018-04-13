using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltrasonicGenerater : MonoBehaviour {

    // Generate Ultrasonic
    public void Generate() {

        GameObject lightObj = new GameObject();
        lightObj.transform.parent = this.transform;
        Light light = lightObj.AddComponent<Light>();
        light.color = Color.white;
        light.range = 0.0f;
        light.intensity = 1.0f;
        lightObj.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        lightObj.AddComponent<UltrasonicControl> ();

        Debug.Log("Generate Ultrasonic!!!");
    }
}
