using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour {

    public int attack;
    public int survivalTime = 1;
    public ParticleSystem attackEffect;

    private int frameCounter = 0;

	// Use this for initialization
	void Start () {
        attackEffect = GetComponent<ParticleSystem>();
        attackEffect.Stop();
    }
	
	// Update is called once per frame
	void Update () {

        if(frameCounter >= survivalTime) {
            Death();
        }

        ++frameCounter;
	}

    // Destroy this GameObject
    public void Death() {
        Debug.Log("Death!!!!");
        Destroy(this);
    }

    private void OnCollisionEnter(Collision collision) {
        attackEffect.transform.position = transform.position;
        attackEffect.Play();                                 // パーティクル出す
        if (collision.gameObject.tag == "Controllable" ||
            collision.gameObject.tag == "Player" ||
            collision.gameObject.tag == "Symbol")
        {
            //collision.gameObject.GetComponent<DurabilityManager>().AddDamege(attack);
            Debug.Log("collision!!!!!!!!!!!!!!!!!!!!");
        }
        Death();
    }
}
