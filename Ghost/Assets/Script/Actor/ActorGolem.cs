using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorGolem : ActorOption {

    public CameraRay camRay;
    public int attack = 100;

    public float golemEyeHeight = -0.5f;
	public int OrthoSize = 5;
	public GameObject fpCamera;

    public new Renderer renderer;

    // Method : Initializing
    public ActorGolem():base(ActorOption.TypeList.Golem){	// ActorTypeを定義
	}

	// Update is called once per frame
	void Update () {

		// ゴーレム専用カメラと基本のカメラの角度を合わせる
		foreach (Transform child in this.transform) {

			if (child.gameObject.name == "GolemCamera") {
				child.gameObject.GetComponent<Camera> ().transform.rotation = fpCamera.GetComponent<Camera> ().transform.rotation;
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

		CameraObj.transform.localPosition = new Vector3( 0, golemEyeHeight, 0 );

		// ゴーレム専用カメラの追加(Orthographic)
		Camera camera2 = CameraObj.AddComponent<Camera> () as Camera;
		base.SetViewable (camera2);		// "Psychic", "Physics"両方を視認可能な状態にする
		camera2.cullingMask &= ~(1 << LayerMask.NameToLayer ("Psychic"));	// "Psychic"を視認不能にする
		camera2.cullingMask &= ~(1 << LayerMask.NameToLayer ("Psy_snag"));	// "Psy_snag"を視認不能にする

		camera2.orthographic = true;			// 平行投影
		camera2.orthographicSize = OrthoSize;
		camera2.depth = -1;
        
        // ゴーレム専用の高さを設定
        camera2.cullingMask &= ~(1 << LayerMask.NameToLayer("Psychic"));    // "Psychic"を視認不能にする
        camera2.cullingMask &= ~(1 << LayerMask.NameToLayer("Psy_snag"));   // "Psy_snag"を視認不能にする

        camera2.orthographic = true;            // 平行投影
        camera2.orthographicSize = OrthoSize;

        renderer.enabled = false;
    }

	// Method : Canceling camera settings
	public override void ReleaseViewable (Camera camera){

        /*camera.orthographic = false;           // 平行投影解除

        fpCamera.transform.localPosition = new Vector3(0, 0, 0);   // 高さをリセット*/
        camera.enabled = true;

        Destroy(this.transform.Find ("GolemCamera").gameObject);

        renderer.enabled = true;
    }

	// Method : Action-Attack
	public override void Attack(){
        RaycastHit hit;
        Ray _ray = camRay.someGaze();

        // Rayが衝突するLayerを設定
        int layerMask = //(1 << LayerMask.NameToLayer("Psychic")) |           // 幽霊
                        (1 << LayerMask.NameToLayer("Physics")) |           // 物理
                                                                            // (1 << LayerMask.NameToLayer ("Psy_snag")) |		// 結界(結界越しに憑依できるようにコメントアウト(結界の見えないキャラクターの弊害となるため))
                        (1 << LayerMask.NameToLayer("Phy_snag")) |			// 壁
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
        Debug.Log ("Attack-Golem");
	}

	// Method : Action-Special
	public override void Special(Camera camera){
		Debug.Log ("Special-Golem");
	}
}
