using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContrastEnhance : MonoBehaviour {

	public Shader shader;
	public Material mat;

	void Start()
	{
		this.mat = new Material (this.shader);
	}

	void OnRenderImage(RenderTexture src, RenderTexture dest )
	{
		Graphics.Blit (src, dest, this.mat);
	}
}
