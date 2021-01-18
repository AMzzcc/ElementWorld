using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraButton : MonoBehaviour
{
    public GameObject ima;
    private bool isFree;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isFree)
        {
            ima.SetActive(false);
        }
        else
        {
            ima.SetActive(true);
        }
    }

    public void free()
    {
        if(isFree)
        {
            isFree = false;
        }
        else
        {
            isFree = true;
        }
        
    }
}
