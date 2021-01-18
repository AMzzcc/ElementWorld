using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectJumpWater : MonoBehaviour
{
    public GameObject jumpwaterEffect;
    public Transform jumpwaterEffectLocator;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3 position = collision.transform.position;
        position.y = jumpwaterEffectLocator.position.y;
        var go = Instantiate(jumpwaterEffect, position, jumpwaterEffect.transform.rotation);
        go.GetComponent<ParticleSystem>().Play();
        StartCoroutine(DeleteEffectLater(go));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Vector3 position = collision.transform.position;
        position.y = jumpwaterEffectLocator.position.y;
        var go = Instantiate(jumpwaterEffect, position, jumpwaterEffect.transform.rotation);
        go.GetComponent<ParticleSystem>().Play();
        StartCoroutine(DeleteEffectLater(go));
    }

    IEnumerator DeleteEffectLater (GameObject go)
    {
        yield return new WaitForSeconds(2.0f);

        Destroy(go);

    }
}
