using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hint : MonoBehaviour
{
    private GameObject hint;

    public GameObject player;

    private void Start()
    {
        hint = GameObject.Find("UI/Hint").gameObject;
        hint.gameObject.SetActive(false);
    }

    private void Update()
    {
        
    }
}
