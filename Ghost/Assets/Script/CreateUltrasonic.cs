using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateUltrasonic : MonoBehaviour {

	public void Generate(){
		GameObject lightGameObject = new GameObject ("The Light");
		lightGameObject.transform.parent = this.transform;
		Light light = lightGameObject.AddComponent<Light> ();
		light.color = Color.white;
		lightGameObject.transform.localPosition = new Vector3 (0, 0, 0);
	}
}
