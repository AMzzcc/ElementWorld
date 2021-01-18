using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public float DiamondNum;
    //public GameObject[] DarkStar;  //星槽
    //public GameObject[] FlashStar;  //获取的星星
    public GameObject DarkDiamond; //钻石
    public GameObject FlashDiamond;

    public GameObject LevelOver;
    public GameObject WinTexture;
    public GameObject LoseTexture;
    public GameObject DiaNum0;
    public GameObject DiaNum1;

    public int Level;//当前关卡,需要每个关卡场景在Inspector设置
    
    void Start()
    {
        DiamondNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //public void AddStar()
    //{
    //    ++StarNum;
    //    if(StarNum==1)
    //    {
    //        DarkStar[0].SetActive(false);
    //        FlashStar[0].SetActive(true);
    //    }
    //    else if (StarNum == 2)
    //    {
    //        DarkStar[1].SetActive(false);
    //        FlashStar[1].SetActive(true);
    //    }
    //    else if (StarNum == 3)
    //    {
    //        DarkStar[2].SetActive(false);
    //        FlashStar[2].SetActive(true);
    //    }
    //    else if (StarNum == 4)
    //    {
    //        DarkStar[3].SetActive(false);
    //        FlashStar[3].SetActive(true);
    //    }
    //}

    public void AddDiamond()
    {
        DarkDiamond.SetActive(false);
        FlashDiamond.SetActive(true);
        ++DiamondNum;
    }

    public void Win()
    {
        //更新存档通关数
        int maxlv = Mathf.Max(Level+1,PlayerPrefs.GetInt("MaxLevel",1));
        PlayerPrefs.SetInt("MaxLevel", maxlv);
        //Time.timeScale = 0;
        LevelOver.SetActive(true);
        WinTexture.SetActive(true);
        if (DiamondNum == 1)
        {
            DiaNum1.SetActive(true);
            PlayerPrefs.SetInt("Level" + Level.ToString(), 1);
            //Debug.Log("钻石存档 " + PlayerPrefs.GetInt("Level" + Level.ToString()));
        }
        else
        {
            DiaNum0.SetActive(true);
        }
    }

    public void Lose()
    {
        //Time.timeScale = 0;
        LevelOver.SetActive(true);
        LoseTexture.SetActive(true);
        if (DiamondNum == 1)
        {
            DiaNum1.SetActive(true);
            PlayerPrefs.SetInt("Level" + Level.ToString(), 1);
            //Debug.Log("钻石存档 " + PlayerPrefs.GetInt("Level" + Level.ToString()));
        }
        else
        {
            DiaNum0.SetActive(true);
        }
    }


}
