using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideStar : MonoBehaviour
{
    public GameObject LevelManager;
    public GameObject HideMap;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            LevelManager.SendMessage("AddStar");
            HideMap.SendMessage("StarBeGet");   //星星已经被获得
            this.gameObject.SetActive(false);
            //Debug.Log("星星");
        }
    }
}
