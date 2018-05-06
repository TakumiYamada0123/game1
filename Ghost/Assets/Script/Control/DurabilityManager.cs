using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DurabilityManager : MonoBehaviour {

    public int durabilityMaximum;
    public GameObject particle;

    private ParticleSystem effect;

    protected int durability = 1;         // 耐久力
    
    // Use this for initialization
    void Start() {
        effect = particle.GetComponent<ParticleSystem>();
        effect.Stop();
        durability = durabilityMaximum;
    }

    // Update is called once per frame
    void Update() {
        // Death
        if (durability <= 0)
        {
            Death();
        }
    }

    public void AddDamege(int attack) {
        durability -= attack;
        effect.Play();
    }

    public virtual void Death() {
        Destroy(this.gameObject);
    }
}
