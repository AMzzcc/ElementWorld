using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheEnd : MonoBehaviour
{
    private static TheEnd instance;
    public static TheEnd Instance
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        instance = this;
    }

    public bool isEnd;
    private LevelManager levelManager;
    private AudioSource audioSource;
    private AudioSource storage;
    void Start()
    {
        isEnd = false;
        storage = GameObject.Find("storage").GetComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //collision.gameObject.SendMessage("ChangeMove");
            //Debug.Log("...");
            storage.Stop();//停止BGM播放
            audioSource.Play();//播放音效
            levelManager.Win();
            collision.GetComponent<PlayerController>().enabled = false;
            isEnd = true;
        }
    }
}
