using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionParticle : MonoBehaviour
{
    private bool play;

    private void Start()
    {
        play = false;
    }

    public void FinalParticle()
    {
        if (GetComponent<BoxCollider2D>())
            Destroy(GetComponent<BoxCollider2D>());
        if (GetComponent<CircleCollider2D>())
            Destroy(GetComponent<CircleCollider2D>());
        if (GetComponent<CapsuleCollider2D>())
            Destroy(GetComponent<CapsuleCollider2D>());
        if (GetComponent<PlayerReactionChecker>())
            Destroy(GetComponent<PlayerReactionChecker>());
        if (GetComponent<MatterReactionChecker>())
            Destroy(GetComponent<MatterReactionChecker>());
        if (GetComponent<PlayerController>())
            Destroy(GetComponent<PlayerController>());
        if (GetComponent<SpriteRenderer>())
            Destroy(GetComponent<SpriteRenderer>());
        if (GetComponent<Rigidbody2D>())
            Destroy(GetComponent<Rigidbody2D>());
        if (GetComponent<Animator>())
            Destroy(GetComponent<Animator>());
        if (this.gameObject.GetComponentInChildren<SpriteRenderer>())
            Destroy(this.gameObject.GetComponentInChildren<SpriteRenderer>());
        play = true;
        GetComponent<ParticleSystem>().Play();
    }

    private void Update()
    {
        if (play && GetComponent<ParticleSystem>().isStopped)
        {
            Destroy(this.gameObject);
        }

    }
}
