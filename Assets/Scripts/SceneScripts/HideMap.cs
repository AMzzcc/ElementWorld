using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideMap : MonoBehaviour
{
    public GameObject[] Wall;
    public GameObject Diamond;
    private bool InWall;
    private bool HasDiamond;
    void Start()
    {
        InWall = false;
        HasDiamond = true;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (!InWall)  //在墙外，则正常显示
        {
            foreach (GameObject n in Wall)
            {
                n.SetActive(true);
            }
            if(HasDiamond)
            {
                Diamond.SetActive(false);
            }
        }
        else       //在墙外，则暂时消失
        {
            foreach (GameObject n in Wall)
            {
                n.SetActive(false);
            }
            if (HasDiamond)
            {
                Diamond.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        InWall = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        InWall = false;
    }

    public void DiamondBeGet()
    {
        HasDiamond = false;
    }
}
