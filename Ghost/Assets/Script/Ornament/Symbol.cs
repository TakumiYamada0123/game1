using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Symbol : MonoBehaviour {

    public bool fake;               // 偽物フラグ
    public Material material;       // 通常マテリアル
    public Material fakeMaterial;   // 偽物マテリアル

    public void ToFake() {
        if (fake) {
            this.GetComponent<Renderer>().material = fakeMaterial;
            this.GetComponent<Light>().enabled = false;
        }
    }

    public void ReleaseFake() {
        if (fake) {
            this.GetComponent<Renderer>().material = material;
            this.GetComponent<Light>().enabled = true;
        }
    }
}
