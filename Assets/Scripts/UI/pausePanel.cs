using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class pausePanel : MonoBehaviour
{
    public GameObject button;
    public GameObject PauseChoice;
    public GameObject GameExplain;
    [SerializeField]
    private GameObject storage;
    private bool isPause=false;
    // Start is called before the first frame update
    void Start()
    {
        PauseChoice.SetActive(false);
        storage = GameObject.Find("storage");
    }

    // Update is called once per frame
    void Update()
    {
        //Music(); 
    }

    public void Explain()
    {
        GameExplain.SetActive(true);
        //PauseChoice.SetActive(false);
    }

    public void CloseExplain()
    {
        GameExplain.SetActive(false);
        PauseChoice.SetActive(true);
    }

    public void Retry()
    {
        TheEnd.Instance.isEnd = false;
        storage.GetComponent<AudioSource>().Play();//重放音乐
        SceneManager.LoadScene(Storage.GetInstance().Level+2);
        EventCenter.Broadcast(EventDefine.SetOpenBookButtonShow);
        Time.timeScale = 1;
    }

    public void Pause()
    {
        if (isPause == true)
        {
            Continue();
        }
        else
        {
            storage.GetComponent<AudioSource>().Pause();//暂停音乐
            PauseChoice.SetActive(true);
            Time.timeScale = 0;
            TextUI.GetInstance().ShowText("");
            isPause = true;
        }
        
    }

    public void Continue()
    {
        storage.GetComponent<AudioSource>().UnPause();//恢复音乐
        PauseChoice.SetActive(false);
        EventCenter.Broadcast(EventDefine.SetOpenBookButtonShow);
        Time.timeScale = 1;
        isPause = false;
    }


    public void Home()
    {
        TheEnd.Instance.isEnd = false;
        Destroy(storage);
        EventCenter.Broadcast(EventDefine.SetOpenBookButtonShow);
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
       
    }

    //private void Music()
    //{
        
    //    if (IsPlayBgm.isOn)
    //    {
    //        storage.SendMessage("OpenMusic");
    //    }
    //    else if(!IsPlayBgm.isOn)
    //    {
    //        storage.SendMessage("CloseMusic");
    //    }
    //}
}
