using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorGhost : ActorOption {

	public Renderer renderer;	// Own "Renderer"

	// Method : Initializing
	public ActorGhost():base(ActorOption.TypeList.Ghost){	// ActorTypeを定義
	}

	// Method : Setting of what the Camera can see
	public override void SetViewable (Camera camera){
		base.SetViewable (camera);		// "Psychic", "Physics"両方を視認可能な状態にする
	}

	// Method : To Render
	public void RendererON (){
		renderer.enabled = true;
	}

	// 1method : Do not Render
	public void RendererOFF (){
		renderer.enabled = false;
	}

	// Method : Action-Attack
	public override void Attack(){
		Debug.Log ("Attack-Ghost");
	}

	// Method : Action-Special
	public override void Special(Camera camera){
		Debug.Log ("Special-Ghost");
	}
}
