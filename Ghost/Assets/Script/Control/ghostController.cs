using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostController : MonoBehaviour {

	public GameObject controlObject;	// 憑依対象を保持するオブジェクト
	public CameraSetter cameraSetter;	// カメラ設定スクリプト

	public float ghostRadius = 0.5f;			// Ghostの基本のRadius
	public float EyePosition = 0.85f;			// 目線の高さ(高さに対する倍率)

	public float isPossessDistance = 0.1f;		// 憑依時の移動で「憑依している」と"みなす"距離

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

			// Controllable(現在憑依中の身体)を検索
			if (child.gameObject.tag == "Controllable") {
				child.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
				child.gameObject.GetComponent<BoxCollider> ().enabled = true;	// BoxColliderを再起動
				cameraSetter.Reset(child.gameObject);							// 現在憑依中の身体の状態からカメラ設定のリセット
				child.parent = null;	// 子オブジェクトを解除

			// Ghost(Player)を検索
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

		// 自身のRotationを憑依対象のRotationに合わせる　←　FPControllerの関係で変更がなかったことになってる
		// Vector3 rotateVector = GObj.transform.rotation.eulerAngles;
		// this.transform.rotation = Quaternion.Euler(rotateVector);

		/*/ 自身のRadiusを憑依対象のRadiusに合わせる
		this.GetComponent<CapsuleCollider> ().radius = GObj_Radius;
		this.GetComponent<CapsuleCollider> ().height = GObj_Height;
		*/

		// 高さの反映とローカル位置の固定
		GObj.transform.localPosition = new Vector3 (0, GObj_Height * 0.5f, 0);

		// 憑依対象のBoxColliderを一時停止
		boxcol.enabled = false;

		// rigidbodyに移動・回転制限を掛ける(ローカル位置の固定)
		GObj.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezePosition |
														RigidbodyConstraints.FreezeRotationX |
														RigidbodyConstraints.FreezeRotationZ;
		// 憑依対象ごとのカメラ設定
		cameraSetter.Rebuilding (GObj); 	// 視認対象
		Vector3 position = new Vector3( 0, GObj.transform.localScale.y * EyePosition, 0 );
		cameraSetter.Position(position);	// 位置

		this.gameObject.layer = 9;					// Layerを"Physics"に変更
		isPossessing = true;						// 憑依中状態にする
		Debug.Log ("Possession!!" + " Height : " + GObj.transform.localScale.y * EyePosition);
	}

	// 離脱(操作対象から離れる)
	public void Disengagement(){
		foreach (Transform child in controlObject.transform) {
			
			// Controllable(現在憑依中の身体)を検索
			if (child.gameObject.tag == "Controllable") {
				child.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;	// rigidbodyの移動・回転制限を解除
				child.gameObject.GetComponent<BoxCollider> ().enabled = true;				// BoxColliderを再起動
				cameraSetter.Reset(child.gameObject);										// 現在憑依中の身体の状態からカメラ設定のリセット
				child.parent = null;	// 子オブジェクトを解除

			// Ghost(Player)を検索
			} else if (child.gameObject.tag == "Player") {
				// child.GetComponent<ActorGhost> ().RendererON ();			// Ghostの身体をレンダリングする
				cameraSetter.Rebuilding (child.gameObject);					// Ghostのカメラ視認設定
				cameraSetter.Position(new Vector3( 0, EyePosition, 0 ));	// Ghostのカメラ位置設定
			}
		}
		// 自身のRadiusを基本のRadiusに戻す
		this.GetComponent<CapsuleCollider> ().radius = ghostRadius;

		this.gameObject.layer = 8;		// Layerを"Psychic"に変更
		isPossessing = false;			// 未憑依状態にする
		Debug.Log ("Disengagement");
	}

	/*/ 憑依時の移動 ( 操作系をAssetじゃなく自作しないとできない……(このメソッドが動いている間は入力を受け付けないように) )
	public void MovePossess(Vector3 target){

		// 憑依対象との距離が「憑依している」と"みなす"距離以下である
		if(Vector3.Distance( this.transform.position, target ) <= isPossessDistance){

			this.transform.position = target;	// 座標を合わせる
			isPossessing = true;				// 憑依中状態にする

		// 憑依対象との距離が「憑依している」と"みなす"距離以下でない
		}else{
			// 憑依対象との距離を等速で近づける
			this.transform.position = Vector3.Lerp( this.transform.position, target, Time.deltaTime);
		}
	}*/
}
