using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MainPanel : MonoBehaviour
{
    private Button btn_openBook;
    private Image backBg;

    private void Awake()
    {
        EventCenter.AddListener(EventDefine.ShowMainPanel, Show);
        EventCenter.AddListener(EventDefine.SetOpenBookButtonShow, SetOpenBookButtonShow);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.ShowMainPanel, Show);
        EventCenter.RemoveListener(EventDefine.SetOpenBookButtonShow, SetOpenBookButtonShow);
    }

    private void Start()
    {
        btn_openBook = gameObject.transform.Find("btn_OpenBook").GetComponent<Button>();
        btn_openBook.onClick.AddListener(OnOpenBookButtonClick);
        backBg = transform.Find("backBg").transform.GetComponent<Image>();
        backBg.gameObject.SetActive(false);
    }

    private void Update()
    {
        //if(SceneManager.GetActiveScene.GetInstanceID)
        if((SceneManager.GetActiveScene().buildIndex != 0&&SceneManager.GetActiveScene().buildIndex != 1
            &&SceneManager.GetActiveScene().buildIndex!=2))
        {
            if (TheEnd.Instance.isEnd ||Time.timeScale == 0)
            {
                btn_openBook.gameObject.SetActive(false);
            }
            else
            {
                btn_openBook.gameObject.SetActive(true);
            }
        }
    }

    /// <summary>
    /// 打开书本按钮点击事件
    /// </summary>
    private void OnOpenBookButtonClick()
    {
        backBg.gameObject.SetActive(true);
        backBg.DOColor(new Color(backBg.color.r, backBg.color.g, backBg.color.b, 0.5f), 0.5f).OnComplete(() =>
        {
            EventCenter.Broadcast(EventDefine.ShowBookPanel);
        });
    }

    private void Show()
    {
        backBg.color = new Color(backBg.color.r, backBg.color.g, backBg.color.b, 0);
        backBg.gameObject.SetActive(false);
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 对打开书本按钮当前状态取反方法
    /// </summary>
    private void SetOpenBookButtonShow()
    {
        btn_openBook.gameObject.SetActive(false);
    }
}
