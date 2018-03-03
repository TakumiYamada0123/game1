using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class someGaze : MonoBehaviour {

	public Camera camera;
	public float rayDist = 10.0f;
	public float rayRadius = 2.0f;
	public ghostController GhostCtrl;

	// Use this for initialization
	void Start () {
		GhostCtrl = GetComponent<ghostController> ();
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		Ray ray = camera.ViewportPointToRay (new Vector3(0.5f, 0.5f, 0));
		int layerMask = (1 << LayerMask.NameToLayer ("Psychic")) | (1 << LayerMask.NameToLayer ("Physics"));

		Debug.DrawRay (ray.origin, ray.direction * rayDist, Color.black);		// cameraの向き

		// マウス離上時
		if (Input.GetMouseButtonDown (1)) {
			// cameraが憑依可能な対象を注視している
			if (Physics.SphereCast (ray, rayRadius, out hit, rayDist, layerMask)) {
				Debug.Log (hit.collider.gameObject.name);		// 注視先を表示

				// 対象のTagがControllableのとき
				if (hit.collider.tag == "Controllable") {

					// 憑依
					GhostCtrl.Possession (hit.collider.gameObject);
				}
			// 憑依可能な対象を見ていない
			} else {
				// 憑依中のとき
				if(GhostCtrl.isPossessing){
					// 離脱
					GhostCtrl.Disengagement();
				}
			}
		}
	}
}
