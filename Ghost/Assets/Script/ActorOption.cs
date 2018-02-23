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
	public void Initialize(TypeList SetType){
		Type = SetType;		// Defining ActorType
	}

	// Method : Setting of what the Camera can see
	public virtual void SetViewable (Camera camera){
		camera.cullingMask = (1 << LayerMask.NameToLayer ("Psychic"));
		camera.cullingMask = (1 << LayerMask.NameToLayer ("Physics"));
	}
}
