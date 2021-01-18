using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticCube : MonoBehaviour
{
    public Transform trRight;
    public Transform trLeft;
    private Transform tr;
    public List<string> MagneticChemistryList;
    public float MoveSpeed;
    private Rigidbody2D rd;

    //private GameObject MagneticObject;

    private void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<MatterName>() != null)
        {
            if (MagneticChemistryList.Exists(x => x.CompareTo(collision.gameObject.GetComponent<MatterName>().matterName) == 0))
            {
                GameObject MagneticObject = collision.gameObject;
                Transform objTr = MagneticObject.GetComponent<Transform>();
                if (objTr.position.x > tr.position.x)
                {
                    if (tr.position.x < trRight.position.x)
                    {
                        rd.velocity = new Vector2(trRight.position.x - tr.position.x, trRight.position.y - tr.position.y).normalized * MoveSpeed;
                    }
                    else
                    {
                        rd.velocity = Vector2.zero;
                        tr.position = trRight.position;
                    }
                }
                else if (objTr.position.x < tr.position.x)
                {
                    if (tr.position.x > trLeft.position.x)
                    {
                        rd.velocity = new Vector2(trLeft.position.x - tr.position.x, trLeft.position.y - tr.position.y).normalized * MoveSpeed;
                    }
                    else
                    {
                        rd.velocity = Vector2.zero;
                        tr.position = trLeft.position;
                    }
                }
            }
        }
        
    }



}
