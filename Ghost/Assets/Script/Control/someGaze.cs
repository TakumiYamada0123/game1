using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class someGaze : MonoBehaviour {

    public CameraRay cameraRay;
	public ghostController GhostCtrl;
    public GameObject reticleUI;
    public Color reticleColor = new Color(0.35f, 0.35f, 0.875f);

    private int ButtonDownCounter = 0;				// 押下中フレームのカウンタ
	private int DisengagementThreshold = 30;		// 離脱みなし用の閾値

	// Use this for initialization
	void Start () {
		GhostCtrl = GetComponent<ghostController> ();
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
        Ray ray = cameraRay.someGaze();

        // Rayが衝突するLayerを設定
        int layerMask = (1 << LayerMask.NameToLayer ("Psychic")) |			// 幽霊
						(1 << LayerMask.NameToLayer ("Physics")) |			// 物理
						// (1 << LayerMask.NameToLayer ("Psy_snag")) |		// 結界(結界越しに憑依できるようにコメントアウト(結界の見えないキャラクターの弊害となるため))
						(1 << LayerMask.NameToLayer ("Phy_snag")) |			// 壁
                        (1 << LayerMask.NameToLayer ("Psy_Phy"));           // Shaman等、霊的且つ物理的なもの

        Debug.DrawRay (ray.origin, ray.direction * cameraRay.rayDist, Color.black);       // cameraの向き

        // cameraが憑依可能な対象を注視している
        if (Physics.Raycast(ray, out hit, cameraRay.rayDist, layerMask)) {

            // 照準UIの色を変更
            // 対象のTagがControllableのとき
            if (hit.collider.tag == "Controllable") {
                reticleUI.GetComponent<Image>().color = reticleColor;

            // 基本色に戻す
            } else {
                reticleUI.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f);
            }
            // マウス離上時
            if (Input.GetMouseButtonUp (1)) {

			    // マウス押下が「離脱とみなす」閾値に達していない
			    if ( ButtonDownCounter < DisengagementThreshold ) {

					// 対象のTagがControllableのとき
					if (hit.collider.tag == "Controllable") {
						// 憑依
						GhostCtrl.Possession (hit.collider.gameObject);
						GetComponent<ActionControl> ().KnowMyBody ();	// 身体の再認識
					}

				}
			}
			ButtonDownCounter = 0;  // 押下中フレームのカウンタをリセット
        } else {
            // 照準UIの色を変更
            reticleUI.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f);
        }

        // マウス押下中
        if (Input.GetMouseButton (1)) {
			
			// マウス押下が「離脱とみなす」閾値に達している
			if (ButtonDownCounter >= DisengagementThreshold) {
				// 憑依中である
				if(GhostCtrl.isPossessing){
					// 離脱
					GhostCtrl.Disengagement();
					GetComponent<ActionControl> ().KnowMyBody ();	// 身体の再認識
				}
			}

			++ButtonDownCounter;		// 押下中フレームのカウント
		}
	}
}
