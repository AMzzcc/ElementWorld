using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideDiamond : MonoBehaviour
{
    private GameObject LevelManager;
    public GameObject HideMap;
    void Start()
    {
        LevelManager = GameObject.Find("LevelManager");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            LevelManager.SendMessage("AddDiamond");
            HideMap.SendMessage("DiamondBeGet");   //星星已经被获得
            this.gameObject.SetActive(false);
        }
    }
}
