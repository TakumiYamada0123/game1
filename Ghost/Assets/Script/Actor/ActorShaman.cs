using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorShaman : ActorOption {

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
        Debug.Log("Attack-Shaman");
    }

    // Method : Action-Special
    public override void Special(Camera camera) {
        Debug.Log("Special-Shaman");
    }
}
