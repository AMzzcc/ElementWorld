using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CreateManagerVarsContainer")]
public class ManagerVars : ScriptableObject
{
    public static ManagerVars GetManagerVars()
    {
        return Resources.Load<ManagerVars>("ManagerVarsContainer");
    }
    //选择按钮图片
    public List<Sprite> materialSpriteList = new List<Sprite>();
    //水关图片
    public List<Sprite> waterSpriteList = new List<Sprite>();
    //钠关图片
    public List<Sprite> naSpriteList = new List<Sprite>();
    //铝关图片
    public List<Sprite> alSpriteList = new List<Sprite>();
    //铁关图片
    public List<Sprite> feSpriteList = new List<Sprite>();
    //铜关图片
    public List<Sprite> cuSpriteList = new List<Sprite>();

    //加载详细知识过程图片
    public Sprite loadBook;

    //所有书本图片
    public List<Sprite> allSriteList = new List<Sprite>();



    //选择按钮文本
    public List<string> choiceButtonTextList = new List<string>();

    public GameObject choiceItemPre;

    //书页宽度
    public int PageWidth = 328;
    //书页长度
    public int PageHeight = 490;
    //书页X起始坐标
    public float PageX = 343;
    //书页间隔
    public float PageInterval = 328.5f;
    //书页Y恒定坐标
    public float PageY = 7.6f;

    //小书页边长
    public int SmallPageLength = 60;
    //小书页x起始坐标
    public int SmallPageX = 30;
    //小书页y恒定坐标
    public int SmallPageY = 0;
    //小书页间隔
    public int SmallPageInterval = 100;


    //书页预制体
    public GameObject PagePre;
    //小书页预制体
    public GameObject SmallPagePre;

}
