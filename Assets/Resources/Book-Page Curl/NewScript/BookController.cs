using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookController : MonoBehaviour
{
    private static BookController instance;
    public static BookController Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
        elementBook = GameObject.Find("storage/BookCanvas90/ElementBook/Book").gameObject;
        elementBook.SetActive(false);
    }

    public enum LevelType
    {
        Water,
        Na,
        Al,
        Fe,
        Cu,
    }

    //是否开启上下选择按钮
    public bool isUpOrDown { set; get; }
    //是否处于可自动翻页状态
    public bool isAutoFlip { set; get; }
    //书本是否打开
    public bool isOpenBooking { set; get; }

    //书本的总页数
    private int bookLength;
    //用户选择的书本类型
    private LevelType levelType;
    //化学宝典
    private GameObject elementBook;
    //打开宝典按钮
    private Button openBook;
    //关闭宝典按钮
    private Button closeBook;


    public GameObject RightHotSpot;
    public GameObject LeftHotSpot;

    private void Start()
    {
        openBook = GameObject.Find("Button/OpenBook").GetComponent<Button>();
        openBook.onClick.AddListener(OpenBookOnButton);
        closeBook = GameObject.Find("Button/CloseBook").GetComponent<Button>();
        closeBook.onClick.AddListener(CloseBookOnButton);
        closeBook.gameObject.SetActive(false);

        RightHotSpot.SetActive(false);
        LeftHotSpot.SetActive(false);
        isUpOrDown = true;
        isOpenBooking = false;
    }

    /// <summary>
    /// 打开书本
    /// </summary>
    private void OpenBookOnButton()
    {
        elementBook.SetActive(true);
        openBook.gameObject.SetActive(false);
        closeBook.gameObject.SetActive(true);
        isOpenBooking = true;
    }

    /// <summary>
    /// 关闭书本
    /// </summary>
    private void CloseBookOnButton()
    {
        if (Book.Instance.pageDragging)
        {
            return;
        }
        InitBook();
        elementBook.SetActive(false);
        openBook.gameObject.SetActive(true);
        closeBook.gameObject.SetActive(false);
        isOpenBooking = false;
    }

    /// <summary>
    /// 初始化书本
    /// </summary>
    private void InitBook()
    {
        //初始化选择按钮
        EventCenter.Broadcast(EventDefine.InitChoiceButton);

        //初始化书本内容
        if (Book.Instance.currentPage == 0)
        {
            return;
        }
        int index = Book.Instance.currentPage - 1;
        Book.Instance.RightNext.sprite = Book.Instance.bookPages[0];
        Book.Instance.LeftNext.sprite = Book.Instance.background;
        Book.Instance.currentPage = 0;

        //关闭翻页功能
        RightHotSpot.SetActive(false);
        LeftHotSpot.SetActive(false);
    }

    public int GetBookLength()
    {
        return bookLength;
    }
    public void SetBookLength(int length)
    {
        bookLength = length;
    }

    public LevelType GetLevelType()
    {
        return levelType;
    }
    public void SetLevelType(LevelType type)
    {
        levelType = type;
    }
}
