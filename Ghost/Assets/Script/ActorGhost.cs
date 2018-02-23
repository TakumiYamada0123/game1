using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorGhost : ActorOption {

	// Method : Initializing
	public ActorGhost(){
		Type = TypeList.Ghost;			// ActorTypeを定義
	}

	// Method : Setting of what the Camera can see
	public override void SetViewable (Camera camera){
		base.SetViewable (camera);		// "Psychic", "Physics"両方を視認可能な状態にする
	}
}
