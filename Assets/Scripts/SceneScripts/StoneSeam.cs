using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneSeam : MonoBehaviour
{
    public GameObject left;
    public GameObject right;

    private Transform tr;

    private BoxCollider2D lb;
    private BoxCollider2D rb;

    private void Start()
    {
        tr = GetComponent<Transform>();
        lb = left.GetComponent<BoxCollider2D>();
        rb = right.GetComponent<BoxCollider2D>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        MatterName mn = collision.gameObject.GetComponent<MatterName>();
        if (mn)
        {
            Transform gtr = collision.gameObject.GetComponent<Transform>();
            if ((mn.matterState == MatterState.gas || mn.matterState == MatterState.liquid))
            {
                
                if (tr.position.x > gtr.position.x)
                {
                    lb.enabled = false;
                    rb.enabled = true;
                }
                else
                {
                    rb.enabled = false;
                    lb.enabled = true;
                }
            }
            else
            {
                if (tr.position.x > gtr.position.x)
                {
                    rb.enabled = true;
                }
                else
                {
                    lb.enabled = true;
                }
            }
        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        lb.enabled = true;
        rb.enabled = true;
    }
}


