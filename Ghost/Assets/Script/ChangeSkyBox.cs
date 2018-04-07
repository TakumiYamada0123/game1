using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkyBox : MonoBehaviour {

	public Material DefaultSkybox;
	public Material Darkness;

	public void ReleaseSkybox(){
		RenderSettings.skybox = DefaultSkybox;
	}

	public void ChangeDarkness(){
		RenderSettings.skybox = Darkness;
	}
}