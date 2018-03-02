using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetter : MonoBehaviour {

	public Camera camera;	// Targets of setting

	// Method : Reconstruct camera settings
	public void CameraRebuilding (GameObject GObj) {
		GObj.GetComponent<ActorOption> ().SetViewable (camera);		// Set visibility
	}
}
