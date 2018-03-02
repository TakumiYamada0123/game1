using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorOption : MonoBehaviour {

	// Actor Type List
	public enum TypeList{
		None = 0,
		Ghost,
		Human,
		Bat,
		Golem,
		Shaman
	}
	// Property
	public TypeList Type{ get; set; }

	// Method : Initializing
	public ActorOption(){
		Type = TypeList.None;	// ActorTypeを定義
	}
	public ActorOption(TypeList SetType){
		Type = SetType;			// ActorTypeを定義
	}

	// Method : Setting of what the Camera can see
	public virtual void SetViewable (Camera camera){
		camera.cullingMask |= (1 << LayerMask.NameToLayer ("Psy_Phy"));
		camera.cullingMask |= (1 << LayerMask.NameToLayer ("Psychic"));
		camera.cullingMask |= (1 << LayerMask.NameToLayer ("Physics"));
		camera.cullingMask |= (1 << LayerMask.NameToLayer ("Psy_snag"));
		camera.cullingMask |= (1 << LayerMask.NameToLayer ("Phy_snag"));
		Debug.Log ("SetViewable_Parent");
	}
}
