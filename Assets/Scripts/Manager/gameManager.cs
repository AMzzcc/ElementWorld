using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class gameManager : MonoBehaviour
{
   
    public static gameManager instance;

    private void Awake()
    {
        instance = this;
        
    }
    //游戏数据类
    private GameData data;
    //是否开启音乐
    private bool isMusic;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Initialized()
    {
    }


    //游戏逻辑
    public void ShowStars()
    {
        StartCoroutine("show");
    }

    IEnumerator show()
    {
        yield return new WaitForSeconds(0.2f);
    }

    public void Replay()
    {
        SceneManager.LoadScene(2);
    }

    public void Home()
    {
        SceneManager.LoadScene(0);
    }

    public void Choice()
    {
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// 保存数据
    /// </summary>
    private void Sava()
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = File.Create(Application.persistentDataPath + "/GameData.data"))
            {
                data.SetIsMusic(isMusic);
                bf.Serialize(fs,data);
            }
        }
        catch(System.Exception e)
        {
            Debug.Log(e.Message);
        }
    }


    private void Read()
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = File.Open(Application.persistentDataPath + "/GameData.data", FileMode.Open))
            {
                data = (GameData)bf.Deserialize(fs);
            }
        }
        catch(System.Exception e)
        {
            Debug.Log(e.Message);
        }
    }

}
