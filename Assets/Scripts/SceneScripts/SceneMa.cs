using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class SceneMa : MonoBehaviour
{
    public GameObject storage;
    //public Button []Lock;  //关卡未解锁锁效果
    public Image []Gray;  //关卡未解锁灰暗
    public Image[] DiamondGray;
    public int level;
    public int LevelNum=8; 
    void Start()
    {
        level = Storage.GetInstance().Level-1;
        LevelLock();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScene(int number) // 关卡场景切换
    {
        //if(!BookController.Instance.isOpenBooking)
        {
            Storage.GetInstance().ChangeLe(number);
            DontDestroyOnLoad(storage);
            SceneManager.LoadScene(2);
        }
    }

    public void LevelLock()  //判断关卡解锁到那一关
    {
        int levelnum = PlayerPrefs.GetInt("MaxLevel", 1);
        //int levelnum = 8;

        for(int i= levelnum;i< Gray.Length; ++i)
        {
            Gray[i].gameObject.SetActive(true);           

        }
        for(int i = 1; i <=8; ++i)
        {
            if (PlayerPrefs.GetInt("Level" + i.ToString()) == 1)
            {
                DiamondGray[i-1].gameObject.SetActive(false);
                //Debug.Log(i.ToString()+"解锁钻石");
            }
            else
            {
                //Debug.Log(i.ToString()+"钻石未获得");
            }
        }
        
        //Gray[level].enabled = false;
    }

    public void Back()
    {
        SceneManager.LoadScene(0);
    }
}
