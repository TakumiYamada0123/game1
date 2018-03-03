﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostController : MonoBehaviour {

	public GameObject controlObject;	// 憑依対象を保持するオブジェクト
	public CameraSetter cameraSetter;	// カメラ設定スクリプト

	public bool isPossessing{ get; set; }

	// Use this for initialization
	void Start () {
		isPossessing = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// 憑依(操作対象を切り替える)
	public void Possession (GameObject GObj) {
		
		// 現在のControlの子オブジェクト(Controllable)を解除
		foreach (Transform child in controlObject.transform) {
			
			if (child.gameObject.tag == "Controllable") {
				child.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
				child.gameObject.GetComponent<BoxCollider> ().enabled = true;	// BoxColliderを再起動
				child.parent = null;	// 子オブジェクトを解除

				// Ghost(Player)をサーチ
			} else if (child.gameObject.tag == "Player") {
				child.GetComponent<ActorGhost> ().RendererOFF ();	// Ghostの身体をレンダリングしない
			}
		}

		// Controlの子オブジェクトとして登録
		GObj.transform.parent = controlObject.transform;

		// 憑依対象の位置に自身を転送
		this.transform.position = GObj.transform.position;

		// 憑依対象のBoxColliderを取得
		BoxCollider boxcol = GObj.GetComponent<BoxCollider> ();

		// 憑依対象の大きさを調べて位置(憑依後の高さ)やcolliderの大きさを反映
		float GObj_Height = GObj.transform.localScale.y * boxcol.size.y;
		float GObj_Radius = GObj.transform.localScale.x * boxcol.size.x * 0.5f;

		this.GetComponent<CapsuleCollider> ().radius = GObj_Radius;

		// 高さの反映とローカル位置の固定
		GObj.transform.localPosition = new Vector3 (0, GObj_Height * 0.5f, 0);

		// 憑依対象のBoxColliderを一時停止
		boxcol.enabled = false;

		// rigidbodyに移動制限を掛ける(ローカル位置の固定)
		GObj.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezePosition |
														RigidbodyConstraints.FreezeRotationX |
														RigidbodyConstraints.FreezeRotationZ;

		cameraSetter.CameraRebuilding (GObj);		// 憑依対象ごとのカメラ設定
		this.gameObject.layer = 9;					// Layerを"Physics"に変更
		isPossessing = true;						// 憑依中状態に遷移
		Debug.Log ("Possession!!");
	}

	// 離脱(操作対象から離れる)
	public void Disengagement(){
		foreach (Transform child in controlObject.transform) {

			if (child.gameObject.tag == "Controllable") {
				child.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
				child.gameObject.GetComponent<BoxCollider> ().enabled = true;	// BoxColliderを再起動
				child.parent = null;	// 子オブジェクトを解除

				// Ghost(Player)をサーチ
			} else if (child.gameObject.tag == "Player") {
				child.GetComponent<ActorGhost> ().RendererON ();	// Ghostの身体をレンダリングする
				cameraSetter.CameraRebuilding (child.gameObject);	// Ghostのカメラ設定
			}
		}
		this.gameObject.layer = 8;		// Layerを"Psychic"に変更
		isPossessing = false;			// 憑依中状態に遷移
		Debug.Log ("Disengagement");
	}
}
