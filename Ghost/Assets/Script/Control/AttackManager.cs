using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour {

    public GameObject damageCollider;   // 当たり判定オブジェクト

    public void Attack(int attack, Vector3 force, Vector3 position, int layer, int survivalTime, float radius) {
        /*/ 当たり判定オブジェクトの複製
        GameObject DamageColliders = GameObject.Instantiate(damageCollider) as GameObject;

        DamageColliders.AddComponent<DamageCollider>();

        DamageColliders.GetComponent<Rigidbody>().AddForce(force);
        DamageColliders.transform.position = position;
        DamageColliders.layer = layer;
        DamageColliders.GetComponent<DamageCollider>().attack = attack;
        DamageColliders.GetComponent<DamageCollider>().survivalTime = survivalTime;
        DamageColliders.GetComponent<CapsuleCollider>().radius = radius;
        */
    }
}
