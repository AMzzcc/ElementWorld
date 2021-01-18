using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool isOpen;
    private bool CanOpen;
    private Vector3 position;
    public GameObject C;
    private void Start()
    {
        position = gameObject.transform.position;
        CanOpen = false;
        isOpen = false;
    }

    private void ButtonAction()
    {
        Debug.Log("w");
    }

    private void Update()
    {
        //触发条件
        if (Input.GetKeyDown(KeyCode.Q)&&CanOpen==true)
        {
            isOpen = true;
            
        }
        //if (Input.GetKeyDown(KeyCode.J)&&isOpen)
        //{
        //    isOpen = false;

        //}
        if (isOpen)
        {
            //Debug.Log("isOpen");
            float speed = 3.0f;
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, new Vector3(position.x, position.y + gameObject.transform.GetComponent<BoxCollider2D>().size.y * gameObject.transform.localScale.y, position.z),
                speed * Time.deltaTime);
        }
        //if (!isOpen)
        //{
        //    float speed = 3.0f;
        //    gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, position,
        //        speed * Time.deltaTime);
        //}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            TextUI.GetInstance().ShowText("门上锁了，若有钥匙可按Q开门");
            MatterName mn = collision.gameObject.GetComponent<MatterName>();
            //Debug.Log(mn.matterName);
            if (mn.matterName=="C")
            {
                CanOpen = true;
                //Debug.Log("CanOpen");
            }
        }
        
    }
}
