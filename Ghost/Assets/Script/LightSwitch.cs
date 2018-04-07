using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour {

	public GameObject LightObj;

	// ライトをつける
	public void TurnOn(){
		LightObj.GetComponent<Light> ().enabled = true;
	}

	// ライトを消す
	public void TurnOff(){
		LightObj.GetComponent<Light> ().enabled = false;
	}
}
