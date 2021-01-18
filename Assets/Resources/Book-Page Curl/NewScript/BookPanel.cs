using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BookPanel : MonoBehaviour
{
    private ManagerVars vars;
    private GameObject parent;
    private GameObject smallParent;

    private Button btn_CloseBook;
    private Button btn_Return;

    private bool isLoading = false;
    private int pageCount = 10;
    private int allPageCount = 50;
    private int currentPage = 0;

    private List<GameObject> catalogue = new List<GameObject>();
    private List<GameObject> smallCatalogue = new List<GameObject>();
    private List<GameObject> pageList = new List<GameObject>();
    private List<GameObject> smallPageList = new List<GameObject>();

    private void Awake()
    {
        vars = ManagerVars.GetManagerVars();
        EventCenter.AddListener<BookController.LevelType>(EventDefine.UpdatePage, UpdateBookContent);
        EventCenter.AddListener(EventDefine.ShowBookPanel,Show);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener<BookController.LevelType>(EventDefine.UpdatePage, UpdateBookContent);
        EventCenter.RemoveListener(EventDefine.ShowBookPanel, Show);
    }

    /// <summary>
    /// 更新书本内容
    /// </summary>
    /// <param name="type"></param>
    private void UpdateBookContent(BookController.LevelType type)
    {
        //for(int i = 1;i<vars.allSriteList.Count + 1;i++)
        //{
        //    Book.Instance.bookPages[i] = vars.allSriteList[i - 1];
        //}
        //Book.Instance.SetBookLength(1 + vars.allSriteList.Count);
    }

    private void Start()
    {
        Init();
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if(!isLoading)
        {
            currentPage = -(int)Mathf.Round(parent.transform.localPosition.x / vars.PageWidth);
            if (Input.GetMouseButtonUp(0))
            {
                parent.transform.DOLocalMoveX(-currentPage * vars.PageWidth, 0.2f);
            }
            if(Input.GetMouseButton(0))
            {
                SetPageSize(currentPage);
            }
        }
        
    }

    /// <summary>
    /// 更新书页大小
    /// </summary>
    private void SetPageSize(int currentIndex)
    {
        for(int i = 0;i < catalogue.Count;i++)
        {
            if(i == currentIndex)
            {
                catalogue[i].transform.Find("Image").DOScale(new Vector3(1.0f, 1.0f, 1.0f),0.2f);
            }
            else
            {
                catalogue[i].transform.Find("Image").DOScale(new Vector3(0.3f, 0.3f, 1.0f),0.2f);
            }
        }
    }

    /// <summary>
    /// 初始化数据
    /// </summary>
    private void Init()
    {
        parent = gameObject.transform.Find("Catalogue/Parent").gameObject;
        smallParent = gameObject.transform.Find("smallCatalogue/smallParent").gameObject;
        btn_CloseBook = gameObject.transform.Find("Button/btn_CloseBook").GetComponent<Button>();
        btn_CloseBook.onClick.AddListener(OnCloseBookButtonClick);
        btn_Return = gameObject.transform.Find("Button/btn_Return").GetComponent<Button>();
        btn_Return.onClick.AddListener(OnReturnButtonClick);
        btn_Return.gameObject.SetActive(false);

        for (int i = 0; i < allPageCount; i++)
        {
            GameObject go = Instantiate(vars.PagePre, parent.transform);
            go.SetActive(false);
            pageList.Add(go);

            GameObject go1 = Instantiate(vars.SmallPagePre, smallParent.transform);
            go1.SetActive(false);
            smallPageList.Add(go1);
        }

        pageCount = vars.materialSpriteList.Count;
        UpdateCatalogueContent(pageCount, vars.materialSpriteList);

        //小书页
        for(int i=0;i<pageCount;i++)
        {
            GameObject go = smallPageList[i];
            go.transform.localPosition = new Vector3(
                vars.SmallPageX + (vars.SmallPageInterval + vars.SmallPageLength) * (i + 2),
                vars.SmallPageY, 0);
            go.SetActive(true);
            if(i == 0)
            {
                go.GetComponent<Image>().sprite = vars.waterSpriteList[0];
            }
            smallCatalogue.Add(go);
        }
        smallParent.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(
            (pageCount + 4) * vars.SmallPageLength + (pageCount + 3) * vars.SmallPageInterval + 2 * vars.SmallPageX,
            vars.PageHeight);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 关闭书本按钮点击事件
    /// </summary>
    private void OnCloseBookButtonClick()
    {
        gameObject.SetActive(false);
        OnReturnButtonClick();
        EventCenter.Broadcast(EventDefine.ShowMainPanel);
    }

    /// <summary>
    /// 目录按钮点击事件
    /// </summary>
    private void OnCatalogueButtonClick()
    {
        SetIsLoading();
        for(int i = 0;i< catalogue.Count;i++)
        {
            catalogue[i].transform.GetChild(0).GetComponent<Image>().sprite = vars.loadBook;
            catalogue[i].transform.GetChild(0).GetComponent<Button>().enabled = false;
        }

        catalogue[currentPage].transform.GetChild(0).DOScale(new Vector3(0, 0, 1.0f), 0.3f).OnComplete(() =>
        {
            if(currentPage == 0)
            {
                ResetBookPanel(vars.waterSpriteList);
            }
            if (currentPage == 1)
            {
                ResetBookPanel(vars.naSpriteList);
            }
            if (currentPage == 2)
            {
                ResetBookPanel(vars.alSpriteList);
            }
            if (currentPage == 3)
            {
                ResetBookPanel(vars.feSpriteList);
            }
            if (currentPage == 4)
            {
                ResetBookPanel(vars.cuSpriteList);
            }
            //ResetBookPanel(vars.naSpriteList);
            btn_Return.gameObject.SetActive(true);
        });

        Invoke("SetIsLoading", 0.8f);
    }

    /// <summary>
    /// 返回目录按钮点击事件
    /// </summary>
    private void OnReturnButtonClick()
    {
        SetIsLoading();
        for (int i = 0; i < catalogue.Count; i++)
        {
            catalogue[i].transform.GetChild(0).GetComponent<Image>().sprite = vars.loadBook;
            catalogue[i].transform.GetChild(0).GetComponent<Button>().enabled = true;
        }
        catalogue[currentPage].transform.GetChild(0).DOScale(new Vector3(0, 0, 1.0f), 0.3f).OnComplete(() =>
        {
            ResetBookPanel(vars.materialSpriteList);
        });
        btn_Return.gameObject.SetActive(false);
        Invoke("SetIsLoading", 0.8f);
    }

    /// <summary>
    /// 设置isLoading的值
    /// </summary>
    private void SetIsLoading()
    {
        isLoading = !isLoading;
    }

    /// <summary>
    /// 初始化书本界面
    /// </summary>
    private void ResetBookPanel(List<Sprite> sprites)
    {
        for (int i = 0; i < allPageCount; i++)
        {
            pageList[i].gameObject.SetActive(false);
        }

        parent.transform.localPosition = new Vector3(0, 0, 0);
        pageCount = sprites.Count;
        catalogue.Clear();
        UpdateCatalogueContent(pageCount, sprites);

        for (int i = 0; i < pageCount; i++)
        {
            catalogue[i].gameObject.SetActive(true);
        }

    }

    /// <summary>
    /// 更新书本内容
    /// </summary>
    /// <param name="count"></param>
    /// <param name="sprites"></param>
    private void UpdateCatalogueContent(int count,List<Sprite> sprites)
    {
        for (int i = 0; i < count; i++)
        {
            //更新书页
            GameObject go = pageList[i];
            go.transform.GetChild(0).GetComponent<Image>().sprite = sprites[i];
            go.transform.localPosition = new Vector3(vars.PageX + vars.PageWidth * i, 0, 0);
            go.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(OnCatalogueButtonClick);
            if (i != 0)
            {
                go.transform.Find("Image").localScale = new Vector3(0.3f, 0.3f, 1);
            }
            else
            {
                go.transform.Find("Image").localScale = Vector3.one;
            }
            go.SetActive(true);
            catalogue.Add(go);
        }

        parent.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(count * vars.PageInterval + vars.PageX * 2,
            vars.PageHeight);
    }

}
