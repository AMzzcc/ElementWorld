using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LoadSceneTipsUI : MonoBehaviour
{
    public string fileAddress;
    //public Text text0;
    //tips字符串
    public string[] tips = new string[0];
    private Text text;
    private void Awake()
    {
        //text0 = Resources.Load<Text>("Txt/tips");
        text = GetComponent<Text>();
        if(tips.Length == 0)
        {
            LoadFile();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        text.text = tips[Random.Range(0, tips.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadFile()
    {
        //读取文件的所有行，并将数据读取到定义好的字符数组strs中，一行存一个单元
        //tips = File.ReadAllLines(fileAddress);
#if UNITY_EDITOR
        tips = File.ReadAllLines(fileAddress);
#else
	tips = File.ReadAllLines(Application.dataPath+"/tips.txt");
#endif

    }



}

