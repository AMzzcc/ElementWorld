using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explain2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TextUI.GetInstance().ShowText("冷凝蒸发物态变化自动进行，普通化学反应按123键选择反应物");
    }
}
