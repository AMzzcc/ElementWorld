using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheEnd1 : MonoBehaviour
{
    private LevelManager levelManager;
    void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PlayerPrefs.GetInt("Level2-4-2")==1)
        {
            //if(PlayerPrefs.GetInt("Level2-4-2") == 1)
            //{
                //PlayerPrefs.SetInt("Dia2-4", 1);
                levelManager.DiamondNum++;
                //获得钻石
           // }
            
        }
        else
        {
            PlayerPrefs.SetInt("Level2-4-1", 1);
        }      
        levelManager.Win();
    }
}
