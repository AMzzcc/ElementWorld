using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public bool isOnGround = true;
    private static Jump _Instance = null;
    private Jump() { }
    public static Jump GetInstance()
    {
        if (_Instance == null)
        {
            _Instance = FindObjectOfType<Jump>();
            if (_Instance == null)
            {
                GameObject go = new GameObject("Jump");
                _Instance = go.AddComponent<Jump>();
            }

        }
        return _Instance;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Floor")
        {
            isOnGround = true;
            Debug.Log("开始接触");
        }

    }

    public void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.tag == "Floor")
        {
            isOnGround = false;
            Debug.Log("leave");
        }
        

    }

   
}
