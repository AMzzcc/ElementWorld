using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoicePanel : MonoBehaviour
{
    private ManagerVars vars;
    private List<GameObject> choiceItemList = new List<GameObject>();
    private int[] choiceNum = new int[4];
    private GameObject parent;
    private GameObject buttonUp;
    private GameObject buttonDown;
    private Button buttonMenu;
    private int levelIndex;

   private void Awake()
    {
        vars = ManagerVars.GetManagerVars();
        Init();
        EventCenter.AddListener(EventDefine.InitChoiceButton, InitChoiceItenButton);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.InitChoiceButton, InitChoiceItenButton);
    }

    private void Update()
    {
        /*book2.0修改*/
        ////水
        //if(BookController.Instance.GetLevelType() == BookController.LevelType.Water)
        //{
        //    if(Book.Instance.currentPage == vars.waterSpriteList.Count || Book.Instance.currentPage == 0)
        //    {
        //        BookController.Instance.RightHotSpot.SetActive(false);
        //    }
        //    else
        //    {
        //        BookController.Instance.RightHotSpot.SetActive(true);
        //    }
        //}

        ////钠
        //if (BookController.Instance.GetLevelType() == BookController.LevelType.Na)
        //{
        //    if (Book.Instance.currentPage == vars.naSpriteList.Count || Book.Instance.currentPage == 0)
        //    {
        //        BookController.Instance.RightHotSpot.SetActive(false);
        //    }
        //    else
        //    {
        //        BookController.Instance.RightHotSpot.SetActive(true);
        //    }
        //}

        ////铝
        //if (BookController.Instance.GetLevelType() == BookController.LevelType.Al)
        //{
        //    if (Book.Instance.currentPage == vars.alSpriteList.Count || Book.Instance.currentPage == 0)
        //    {
        //        BookController.Instance.RightHotSpot.SetActive(false);
        //    }
        //    else
        //    {
        //        BookController.Instance.RightHotSpot.SetActive(true);
        //    }
        //}

        ////铁
        //if (BookController.Instance.GetLevelType() == BookController.LevelType.Fe)
        //{
        //    if (Book.Instance.currentPage == vars.feSpriteList.Count || Book.Instance.currentPage == 0)
        //    {
        //        BookController.Instance.RightHotSpot.SetActive(false);
        //    }
        //    else
        //    {
        //        BookController.Instance.RightHotSpot.SetActive(true);
        //    }
        //}

        ////铜
        //if (BookController.Instance.GetLevelType() == BookController.LevelType.Cu)
        //{
        //    if (Book.Instance.currentPage == vars.cuSpriteList.Count || Book.Instance.currentPage == 0)
        //    {
        //        BookController.Instance.RightHotSpot.SetActive(false);
        //    }
        //    else
        //    {
        //        BookController.Instance.RightHotSpot.SetActive(true);
        //    }
        //}

        if(Book.Instance.currentPage == 52)
        {
            BookController.Instance.RightHotSpot.SetActive(false);
        }
        else
        {
            if(!BookController.Instance.RightHotSpot.activeInHierarchy)
            {
                BookController.Instance.RightHotSpot.SetActive(true);
            }
        }
    }

    private void Init()
    {
        levelIndex = 0;

        parent = transform.Find("ScrollRect/Parent").gameObject;
        buttonDown = transform.Find("Button/ChoiceDown").gameObject;
        buttonDown.GetComponent<Button>().onClick.AddListener(OnButtonChoiceDown);
        buttonUp = transform.Find("Button/ChoiceUp").gameObject;
        buttonUp.GetComponent<Button>().onClick.AddListener(OnButtonChoiceUp);
        buttonMenu = transform.Find("Button/Menu").GetComponent<Button>();
        buttonMenu.onClick.AddListener(MenuOnButton);

        //初始化滑动内容
        for(int i=0;i<vars.materialSpriteList.Count;i++)
        {
            //设置图片和选择按钮位置
            GameObject temp = Instantiate(vars.choiceItemPre, parent.transform);
            temp.GetComponentInChildren<Image>().sprite = vars.materialSpriteList[i];
            temp.transform.Find("LevelChoice/Text").GetComponent<Text>().text = vars.choiceButtonTextList[i];
            temp.transform.localPosition = new Vector3(0, (i + 1) * (-100), 0);
            if(i > 3)
            {
                temp.SetActive(false);
            }
            choiceItemList.Add(temp);
        }
        //初始化可显示按钮序号
        for(int i=0;i<choiceNum.Length;i++)
        {
            choiceNum[i] = i;
        }

        //为按钮添加事件
        choiceItemList[0].GetComponentInChildren<Button>().onClick.AddListener(FirstLevel);
        choiceItemList[1].GetComponentInChildren<Button>().onClick.AddListener(SecondLevel);
        choiceItemList[2].GetComponentInChildren<Button>().onClick.AddListener(ThirdLevel);
        choiceItemList[3].GetComponentInChildren<Button>().onClick.AddListener(FourthLevel);
        choiceItemList[4].GetComponentInChildren<Button>().onClick.AddListener(FifthLevel);
    }

    /// <summary>
    /// 初始化选择按钮
    /// </summary>
    private void InitChoiceItenButton()
    {
        foreach (GameObject go in choiceItemList)
        {
            go.GetComponentInChildren<Button>().interactable = true;
        }
        if (levelIndex != 0)
        {
            choiceItemList[levelIndex - 1].transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f, 0.7f);
            levelIndex = 0;
        }

        BookController.Instance.isUpOrDown = true;
    }

    /// <summary>
    /// 向上滑动按钮
    /// </summary>
    private void OnButtonChoiceUp()
    {
        /*book2.0修改*/
        //if(choiceNum[0]==0 || BookController.Instance.isUpOrDown == false)
        //{
        //    return;
        //}
        if(choiceNum[0] == 0)
        {
            return;
        }
        Vector3 position = parent.transform.localPosition;
        parent.transform.localPosition = position + new Vector3(0, -100, 0);

        for (int i = 0; i < choiceNum.Length; i++)
        {
            if (i == choiceNum.Length - 1)
            {
                choiceItemList[choiceNum[i]].SetActive(false);
            }
            choiceNum[i]--;
            if (i == 0)
            {
                choiceItemList[choiceNum[i]].SetActive(true);
            }

        }
    }

    /// <summary>
    /// 向下滑动按钮
    /// </summary>
    private void OnButtonChoiceDown()
    {
        if(choiceNum[choiceNum.Length - 1]== choiceItemList.Count - 1)
        {
            return;
        }

        /*book2.0修改*/
        //if(BookController.Instance.isUpOrDown == false)
        //{
        //    return;
        //}

        Vector3 position = parent.transform.localPosition;
        parent.transform.localPosition = position + new Vector3(0, 100, 0);

        for(int i = 0;i<choiceNum.Length;i++)
        {
            if(i == 0)
            {
                choiceItemList[choiceNum[i]].SetActive(false);
            }
            choiceNum[i]++;
            if(i == choiceNum.Length - 1)
            {
                choiceItemList[choiceNum[i]].SetActive(true);
            }

        }
        
    }

    /// <summary>
    /// 返回菜单按钮
    /// </summary>
    private void MenuOnButton()
    {
        //初始化按钮
        InitChoiceItenButton();

        //初始化书本内容
        if (Book.Instance.currentPage == 0)
        {
            return;
        }
        if (Book.Instance.currentPage == 2)
        {
            EventCenter.Broadcast(EventDefine.FlipLeft);
            //关闭自动翻页功能
            BookController.Instance.isAutoFlip = false;

            //关闭翻页功能
            BookController.Instance.RightHotSpot.SetActive(false);
            BookController.Instance.LeftHotSpot.SetActive(false);
            return;
        }
        int index = Book.Instance.currentPage - 3;
        Book.Instance.bookPages[index] = Book.Instance.background;
        Book.Instance.bookPages[index + 1] = Book.Instance.bookPages[0];
        EventCenter.Broadcast(EventDefine.FlipLeft);
        Book.Instance.currentPage = 2;

        /*book2.0修改*/
        ////关闭翻页功能
        //BookController.Instance.RightHotSpot.SetActive(false);
        //BookController.Instance.LeftHotSpot.SetActive(false);

        //关闭自动翻页功能
        //BookController.Instance.isAutoFlip = false;
    }

    /// <summary>
    /// 用户选择关卡
    /// </summary>
    /// <param name="type"></param>
    private void SetLevel(BookController.LevelType type,int choiceIndex)
    {
        /*book2.0修改*/
        ////根据用户的选择改变关卡的内容
        //BookController.Instance.SetLevelType(type);
        //EventCenter.Broadcast(EventDefine.UpdatePage, type);
        //EventCenter.Broadcast(EventDefine.FlipRight);

        /*book2.0修改*/
        BookController.Instance.SetLevelType(type);
        EventCenter.Broadcast(EventDefine.UpdatePage, type);
        //BookController.Instance.lastPageCount = Book.Instance.currentPage;

        if(type == BookController.LevelType.Water)
        {
            Book.Instance.currentPage = 0;
        }
        if (type == BookController.LevelType.Na)
        {
            Book.Instance.currentPage = 10;
        }
        if (type == BookController.LevelType.Al)
        {
            Book.Instance.currentPage = 22;
        }
        if (type == BookController.LevelType.Fe)
        {
            Book.Instance.currentPage = 30;
        }
        if (type == BookController.LevelType.Cu)
        {
            Book.Instance.currentPage = 46;
        }

        //BookController.Instance.isHandFlip = false;
        EventCenter.Broadcast(EventDefine.FlipRight);
        
        

        /*book2.0修改*/
        if (levelIndex != 0)
        {
            choiceItemList[levelIndex - 1].transform.GetChild(0).localScale = new Vector3(0.7f, 0.7f, 0.7f);
            levelIndex = 0;
        }

        //放大按钮
        choiceItemList[choiceIndex].transform.GetChild(0).localScale = new Vector3(1.0f, 1.0f, 1.0f);
        levelIndex = choiceIndex + 1;

        /*book2.0修改*/
        ////将选关按钮禁用
        //foreach(GameObject go in choiceItemList)
        //{
        //    go.GetComponentInChildren<Button>().interactable = false;
        //}
        

        //将上下选择按钮禁用
        BookController.Instance.isUpOrDown = false;

        /*book2.0修改*/
        ////打开翻页功能
        //BookController.Instance.RightHotSpot.SetActive(true);
        //BookController.Instance.LeftHotSpot.SetActive(true);

        //关闭自动翻页功能
        BookController.Instance.isAutoFlip = false;
    }

    /// <summary>
    /// 第一关
    /// </summary>
    private void FirstLevel()
    {
        SetLevel(BookController.LevelType.Water,0);
    }

    /// <summary>
    /// 第二关
    /// </summary>
    private void SecondLevel()
    {
        SetLevel(BookController.LevelType.Na,1);
    }

    /// <summary>
    /// 第三关
    /// </summary>
    private void ThirdLevel()
    {
        SetLevel(BookController.LevelType.Al, 2);
    }

    /// <summary>
    /// 第四关
    /// </summary>
    private void FourthLevel()
    {
        SetLevel(BookController.LevelType.Fe, 3);
    }

    /// <summary>
    /// 第五关
    /// </summary>
    private void FifthLevel()
    {
        SetLevel(BookController.LevelType.Cu, 4);
    }
}
