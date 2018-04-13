using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltrasonicControl : MonoBehaviour {

    public float rangeMax = 120.0f;
    public float UltrasonicDuration = 120;  // 持続時間(フレーム)

    private int countFrame = 0;
    private new Light light = null;
    private float toDark = 0.99f;

    // Use this for initialization
    void Start () {
        light = this.GetComponent<Light> ();
        Debug.Log("Born Ultrasonic!");
	}
	
	// Update is called once per frame
	void Update () {
        // 持続時間の上限に到達
        if (countFrame >= UltrasonicDuration)
        {
            // ライト破棄
            Remove();
            Debug.Log("Death Ultrasonic");
        }
        // フレーム毎変位
        light.range = countFrame * (rangeMax / UltrasonicDuration);                 // 範囲
        light.intensity = light.intensity * toDark;   // 明るさ
        ++countFrame;   // カウントアップ
    }

    // Destroy
    public void Remove() {
        // GameObjectごと破棄
        Destroy(this.gameObject);
    }

    // Light On
    public void TurnOn() {
        this.GetComponent<Light> ().enabled = true;
    }

    // Light Off
    public void TurnOff() {
        this.GetComponent<Light>().enabled = false;
    }
}
