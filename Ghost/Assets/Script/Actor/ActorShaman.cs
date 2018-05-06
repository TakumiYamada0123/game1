using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorShaman : ActorOption {

    public CameraRay camRay;
    public int attack = 30;

    // Method : Initializing
    public ActorShaman() : base(ActorOption.TypeList.Shaman) {      // ActorTypeを定義
    }

    // Method : Setting of what the Camera can see
    public override void SetViewable(Camera camera) {
        base.SetViewable(camera);                                   // "Psychic", "Physics"両方を視認可能な状態にする
    }

    // Method : Canceling camera settings
    public override void ReleaseViewable(Camera camera) {
    }

    // Method : Action-Attack
    public override void Attack() {
        RaycastHit hit;
        Ray _ray = camRay.someGaze();

        // Rayが衝突するLayerを設定
        int layerMask = (1 << LayerMask.NameToLayer("Psychic")) |           // 幽霊
                        (1 << LayerMask.NameToLayer("Physics")) |           // 物理
                                                                            // (1 << LayerMask.NameToLayer ("Psy_snag")) |		// 結界(結界越しに憑依できるようにコメントアウト(結界の見えないキャラクターの弊害となるため))
                        //(1 << LayerMask.NameToLayer("Phy_snag")) |			// 壁
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
        Debug.Log("Attack-Shaman");
    }

    // Method : Action-Special
    public override void Special(Camera camera) {
        Debug.Log("Special-Shaman");
    }
}
