using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionControl : MonoBehaviour {

	public GameObject bodyObject;
	public Camera camera;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		// Attack-Action
		if(Input.GetMouseButtonDown(0))
			bodyObject.GetComponent<ActorOption> ().Attack();

		// Special-Action
		if(Input.GetKeyDown(KeyCode.R))
			bodyObject.GetComponent<ActorOption> ().Special(camera);
	}
}
