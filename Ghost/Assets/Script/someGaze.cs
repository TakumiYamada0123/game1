using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class someGaze : MonoBehaviour {

	public Camera camera;
	public float rayDist = 10.0f;
	public float rayRadius = 2.0f;
	public ghostController GhostCtrl;

	private int ButtonDownCounter = 0;				// 押下中フレームのカウンタ
	private int DisengagementThreshold = 30;		// 離脱みなし用の閾値

	// Use this for initialization
	void Start () {
		GhostCtrl = GetComponent<ghostController> ();
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		Ray ray = camera.ViewportPointToRay (new Vector3(0.5f, 0.5f, 0));
		int layerMask = (1 << LayerMask.NameToLayer ("Psychic")) |			// 幽霊
						(1 << LayerMask.NameToLayer ("Physics")) |			// 物理
						// (1 << LayerMask.NameToLayer ("Psy_snag")) |		// 結界(結界越しに憑依できるようにコメントアウト(結界の見えないキャラクターの弊害となるため))
						(1 << LayerMask.NameToLayer ("Phy_snag"));			// 壁

		Debug.DrawRay (ray.origin, ray.direction * rayDist, Color.black);		// cameraの向き

		// マウス離上時
		if (Input.GetMouseButtonUp (1)) {

			// マウス押下が「離脱とみなす」閾値に達していない
			if ( ButtonDownCounter < DisengagementThreshold ) {

				// cameraが憑依可能な対象を注視している
				if (Physics.SphereCast (ray, rayRadius, out hit, rayDist, layerMask)) {
					
					Debug.Log (hit.collider.gameObject.name);		// 注視先を表示

					// 対象のTagがControllableのとき
					if (hit.collider.tag == "Controllable") {
						// 憑依
						GhostCtrl.Possession (hit.collider.gameObject);
					}

				}
			}
			ButtonDownCounter = 0;	// 押下中フレームのカウンタをリセット
		}

		// マウス押下中
		if (Input.GetMouseButton (1)) {
			
			// マウス押下が「離脱とみなす」閾値に達している
			if (ButtonDownCounter >= DisengagementThreshold) {
				// 憑依中である
				if(GhostCtrl.isPossessing){
					// 離脱
					GhostCtrl.Disengagement();
				}
			}

			++ButtonDownCounter;		// 押下中フレームのカウント
		}
	}
}
