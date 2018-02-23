using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorHuman : ActorOption {

	// Method : Initializing
	public ActorHuman(){
		Type = TypeList.Human;		// ActorTypeを定義
	}

	// Method : Setting of what the Camera can see
	public override void SetViewable (Camera camera){
		base.SetViewable (camera);											// "Psychic", "Physics"両方を視認可能な状態にする
		camera.cullingMask &= ~(1 << LayerMask.NameToLayer ("Psychic"));	// "Psychic"を視認不能にする
	}
}
