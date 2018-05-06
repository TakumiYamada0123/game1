using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorBat : ActorOption {

    public CameraRay camRay;
    public int attack = 10;

	private bool isSoundLocationMode = false;

	// Method : Initializing
	public ActorBat():base(ActorOption.TypeList.Bat) {		// ActorTypeを定義
	}

	// Method : Setting of what the Camera can see
	public override void SetViewable (Camera camera) {
		base.SetViewable (camera);											// "Psychic", "Physics"両方を視認可能な状態にする
		camera.cullingMask &= ~(1 << LayerMask.NameToLayer ("Psychic"));	// "Psychic"を視認不能にする
		camera.cullingMask &= ~(1 << LayerMask.NameToLayer ("Psy_snag"));	// "Psy_snag"を視認不能にする

		camera.GetComponent<ContrastEnhance> ().enabled = true;
	}
		
	// Method : Canceling camera settings
	public override void ReleaseViewable (Camera camera) {
		camera.GetComponent<ContrastEnhance> ().enabled = false;

        // 音像定位時
		if (isSoundLocationMode) {
			camera.GetComponent<ChangeSkyBox> ().ReleaseSkybox ();
			camera.GetComponent<LightSwitch> ().TurnOn ();
			camera.GetComponent<Monochrome> ().enabled = false;
			isSoundLocationMode = false;
		}
	}

	// Method : Action-Attack
	public override void Attack() {

		// 通常視界時
		if (!isSoundLocationMode) {
            RaycastHit hit;
            Ray _ray = camRay.someGaze();

            // Rayが衝突するLayerを設定
            int layerMask = //(1 << LayerMask.NameToLayer("Psychic")) |           // 幽霊
                            (1 << LayerMask.NameToLayer("Physics")) |           // 物理
                                                                                // (1 << LayerMask.NameToLayer ("Psy_snag")) |		// 結界(結界越しに憑依できるようにコメントアウト(結界の見えないキャラクターの弊害となるため))
                            //(1 << LayerMask.NameToLayer("Phy_snag")) |          // 壁
                            (1 << LayerMask.NameToLayer("Psy_Phy"));           // Shaman等、霊的且つ物理的なもの

            Debug.DrawRay(_ray.origin, _ray.direction * camRay.rayDist, Color.black);       // cameraの向き

            // cameraが攻撃可能な対象を注視している
            if (Physics.Raycast(_ray, out hit, camRay.rayDist, layerMask))
            {
                if (hit.collider.gameObject.GetComponent<DurabilityManager>())
                {
                    hit.collider.gameObject.GetComponent<DurabilityManager>().AddDamege(attack);
                }
            }
            Debug.Log ("Attack-Bat-Kick");

		// 音像定位時
		} else {
            GetComponent<UltrasonicGenerater>().Generate();
			Debug.Log ("Attack-Bat-Ultrasonic");
		}
	}

	// Method : Action-Special
	public override void Special(Camera camera) {
		// 通常視界と音像定位モードの切換

		// 音像定位モードへの切換
		if (!isSoundLocationMode) {
			camera.GetComponent<ChangeSkyBox> ().ChangeDarkness ();
			camera.GetComponent<LightSwitch> ().TurnOff ();
			camera.GetComponent<Monochrome> ().enabled = true;
			isSoundLocationMode = true;
			Debug.Log ("Special-Bat-toSoundLocation");

		// 通常視界への切換
		} else {
			camera.GetComponent<ChangeSkyBox> ().ReleaseSkybox ();
			camera.GetComponent<LightSwitch> ().TurnOn ();
			camera.GetComponent<Monochrome> ().enabled = false;
			isSoundLocationMode = false;
			Debug.Log ("Special-Bat-toNormalView");
		}
	}
}
