using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    public  int Level=1;
    private static Storage _instance = null;

    public AudioSource BGM;
    public AudioClip[] BGMAudio;//0.水族馆


    private Storage() { }
    public static Storage GetInstance()
    {
        if (_instance == null)
        {
            _instance = FindObjectOfType<Storage>();
            if(_instance==null)
            {
                GameObject go = new GameObject("Storage");
                _instance = go.AddComponent<Storage>();
            }
           
        }
        return _instance;
    }
    void Start()
    {
        BGM.clip = BGMAudio[0];
        OpenMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeLe(int number)
    {
        Level = number;
    }

    public void OpenMusic()
    {
        if(!BGM.isPlaying)
        {
            BGM.Play();
        }
      
    }

    public void CloseMusic()
    {
        if (BGM.isPlaying)
        {
            BGM.Stop();
        }
    }

}
