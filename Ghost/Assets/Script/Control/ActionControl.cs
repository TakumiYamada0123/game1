using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionControl : MonoBehaviour {

	public GameObject bodyObject;
	public Camera camera;

	private ActorOption Actor;

	// Use this for initialization
	void Start () {
		this.KnowMyBody ();
	}

	// Know My Body
	public void KnowMyBody () {
		ActorOption Ghost = null;
		bool haveBody = false;		// 身体の所持フラグ

		foreach (Transform child in bodyObject.transform) {
			
			// Controllable(現在憑依中の身体)を検索
			if (child.gameObject.tag == "Controllable") {
				Actor = child.GetComponent<ActorOption> ();
				Debug.Log("Get " + child.gameObject.name + "'s Action★");
				haveBody = true;
			}
			else if (child.gameObject.tag == "Player") {
				Ghost = child.GetComponent<ActorGhost> ();
			}
		}
		// 身体がない時、幽霊とする
		if (!haveBody)
			Actor = Ghost;
	}
	
	// Update is called once per frame
	void Update () {

		// Attack-Action
		if (Input.GetMouseButtonDown (0)) {
			Actor.GetComponent<ActorOption> ().Attack ();
			Debug.Log ("Attack!!");
		}

		// Special-Action
		if (Input.GetKeyDown (KeyCode.R)) {
			Actor.GetComponent<ActorOption> ().Special (camera);
			Debug.Log ("Special!!!");
		}
	}
}
