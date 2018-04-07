using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorGolem : ActorOption {

	public float golemEyeHeight = -0.5f;
	public int OrthoSize = 7;
	public GameObject fpCamera;

	// Method : Initializing
	public ActorGolem():base(ActorOption.TypeList.Golem){	// ActorTypeを定義
	}

	// Update is called once per frame
	void Update () {
		
		foreach (Transform child in this.transform) {
			
			if (child.gameObject.name == "GolemCamera") {
				child.gameObject.GetComponent<Camera> ().transform.localRotation = fpCamera.GetComponent<Camera> ().transform.localRotation;
				Debug.Log ("update");
			}
		}
	}

	// Method : Setting of what the Camera can see
	public override void SetViewable (Camera camera){

		camera.enabled = false;		// fpCamaraを切る

		// ゴーレム用のGameObjectの追加
		GameObject CameraObj = new GameObject ();
		CameraObj.name = "GolemCamera";
		CameraObj.transform.parent = this.transform;		// 上記を自身の"子"とする

		CameraObj.transform.localPosition = new Vector3( 0, golemEyeHeight, 2 );

		// ゴーレム専用カメラの追加(Orthographic)
		Camera camera2 = CameraObj.AddComponent<Camera> () as Camera;
		base.SetViewable (camera2);		// "Psychic", "Physics"両方を視認可能な状態にする
		camera2.cullingMask &= ~(1 << LayerMask.NameToLayer ("Psychic"));	// "Psychic"を視認不能にする
		camera2.cullingMask &= ~(1 << LayerMask.NameToLayer ("Psy_snag"));	// "Psy_snag"を視認不能にする

		camera2.orthographic = true;			// 平行投影
		camera2.orthographicSize = OrthoSize;
		camera2.depth = -1;
	}

	// Method : Canceling camera settings
	public override void ReleaseViewable (Camera camera){

		camera.enabled = true;

		Destroy(this.transform.Find ("GolemCamera").gameObject);
	}

	// Method : Action-Attack
	public override void Attack(){

	}

	// Method : Action-Special
	public override void Special(Camera camera){

	}
}
