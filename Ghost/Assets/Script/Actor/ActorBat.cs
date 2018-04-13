using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorBat : ActorOption {

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
