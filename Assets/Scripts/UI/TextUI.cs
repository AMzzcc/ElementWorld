using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TextUI : MonoBehaviour
{
    public Text text;
    public Animator animator;
    private static TextUI _instance = null;
    private TextUI() { }
    public static TextUI GetInstance()
    {
        if (_instance == null)
        {
            _instance = FindObjectOfType<TextUI>();
            if (_instance == null)
            {
                GameObject go = new GameObject("TextUI");
                _instance = go.AddComponent<TextUI>();
            }

        }
        return _instance;
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowText(string str)
    {

        //text.text = str;
        //animator.SetTrigger("Show");
        
    }

    public void Nothing()
    {
        text.text = "";
    }

    public void Show()
    {
        animator.SetTrigger("Show");
    }


}
