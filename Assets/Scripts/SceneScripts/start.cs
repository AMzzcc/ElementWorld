using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class start : MonoBehaviour
{
    public GameObject WorkersName;
    public AudioSource BGM;
    public AudioClip BGMAudio;
    void Start()
    {
        BGM.clip = BGMAudio;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Choice()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void UIClickMusic()
    {
        if (!BGM.isPlaying)
        {
            BGM.Play();
        }

    }
    //NOTE By Aery:这些都是不需要的，可以看看开始场景按钮直接拖GO就可以SetActive，不必再写函数
    //public void OpenWorkers()
    //{
    //    WorkersName.SetActive(true);
    //}

    //public void CloseWorkers()
    //{
    //    WorkersName.SetActive(false);
    //}
}
