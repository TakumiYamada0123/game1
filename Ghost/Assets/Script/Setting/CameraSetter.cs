using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetter : MonoBehaviour {

	public Camera camera;		// Targets of setting
	public GameObject camPiv;	// camera Pivot

	// Method : Reconstruct camera settings
	public void Rebuilding (GameObject GObj) {
		GObj.GetComponent<ActorOption> ().SetViewable (camera);		// Set visibility
	}

	// Method : Reset camera settings
	public void Reset (GameObject GObj) {
		GObj.GetComponent<ActorOption> ().ReleaseViewable (camera);
	}

	// Method : position settings
	public void Position(Vector3 relativePosition){
		camPiv.transform.localPosition = relativePosition;
	}
}
