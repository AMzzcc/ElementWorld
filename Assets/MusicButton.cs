using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicButton : MonoBehaviour
{
    public GameObject ima;
    private bool isOn;
    private GameObject storage;
    // Start is called before the first frame update
    void Start()
    {
        isOn = true;
        storage = GameObject.Find("storage");
    }

    // Update is called once per frame
    void Update()
    {
        if (isOn)
        {
            ima.SetActive(false);
            storage.SendMessage("OpenMusic");
        }
        else
        {
            ima.SetActive(true);
            storage.SendMessage("CloseMusic");
        }
    }

    public void BgmOnOff()
    {
        if (isOn)
        {
            isOn = false;
        }
        else
        {
            isOn = true;
        }

    }
}
