using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

//public class Pair<T1, T2>
//{
//    public T1 first;
//    public T2 Second;
//}

public class ReactionEquUI : MonoBehaviour
{
    public GameObject pic;              //UI背景框
    public GameObject tx;               //UI文字
    public GameObject txpic;            //UI文字图片
    public Camera mainCmr;              //主摄像机

    private GameObject show;            //按钮和方程式
    private GameObject bg;              //背景
    private bool isCloseing;            //是否处于不可反应阶段

    public Vector2 offset;              //偏移修正

    private Image img;                  //UI背景框组件
    private Image tximg;                //UI文字图片组件
    private Text text;                  //UI文字组件

    public TextAsset EquationText;      //由序号找化学式text
    public List<Sprite> TxPics;      //用图片显示       
    private List<string> equList;       //由文件提取出来的表

    private Transform activeTr;         //跟踪位置

    private int? ActiveIndex;
    //private ChemicalEqu? activeEqu;     //当前使用的方程式

    //public KeyCode SwitchKey;              //切换显示的按钮

    //private List<Pair<ChemicalEqu, Transform>> equList;   //保存待输出的方程式

    //Debug用
    //[SerializeField]
    //private int ListCount;



    ///// <summary>
    ///// List比对
    ///// </summary>
    //private bool CheckListEqu(List<ChemistyMatter> a, List<ChemistyMatter> b)
    //{
    //    if (a.Count != b.Count)
    //    {
    //        return false;
    //    }
    //    //Debug.Log("Begin");
    //    int count = 0;
    //    int count2 = 0;
    //    foreach (ChemistyMatter m in b)
    //    {
    //        for (int i = 0; i < a.Count; i++)
    //        {
    //            if (a[i].name == m.name && a[i].state == m.state)
    //            {
    //                count = count + 1;
    //                break;
    //            }
    //            //Debug.Log(a[i].name);
    //            //Debug.Log(a[i].name.Length);
    //        }
    //    }
    //    foreach (ChemistyMatter m in a)
    //    {
    //        for (int i = 0; i < a.Count; i++)
    //        {
    //            if (b[i].name == m.name && b[i].state == m.state)
    //            {
    //                count2 = count2 + 1;
    //                break;
    //            }
    //            //Debug.Log(a[i].name);
    //            //Debug.Log(a[i].name.Length);
    //        }
    //    }
    //    if (count != b.Count || count2 != b.Count)
    //    {
    //        //Debug.Log("CNNN");
    //        //Debug.Log(count);
    //        return false;
    //    }

    //    return true;
    //}
    /// <summary>
    /// 化学方程式比对
    /// </summary>
    //private bool CheckEqu(ChemicalEqu a, ChemicalEqu b)
    //{
    //    bool[] check = new bool[3];

    //    check[0] = CheckListEqu(a.input, b.input);
    //    check[1] = CheckListEqu(a.output, b.output);
    //    check[2] = a.conditon == b.conditon;

    //    if (check[0] && check[1] && check[2])
    //        return true;

    //    return false;
    //}

    

    /// <summary>
    /// 显示方程式的接口
    /// </summary>
    /// <param name="equ">要显示的方程式</param>
    /// <param name="tr">要附加在GameObject的Transform</param>
    public void AddEqu(ChemicalEqu equ, Transform tr)
    {
        activeTr = tr;
        //activeEqu = equ;
        MakeOutEqu(equ);
        OpenPrintOutEqu();
        //if (!equList.Exists(x=>CheckEqu(x.first,equ)))
        //{
        //    equList.Add(new Pair<ChemicalEqu, Transform>{ first = equ, Second = tr});
        //    if (equList.Count == 1)
        //    {
        //        activeEqu = equ;
        //        activeTr = tr;
        //        MakeOutEqu(equ);
        //        OpenPrintOutEqu();
        //    }
        //}
        //else
        //{
        //    return false;
        //}
        //return true;
    }


    public void AddEqu(int index, Transform tr)
    {

        //if (ActiveIndex != index || activeTr != tr)
        //{
        //Debug.Log(index);
        activeTr = tr;
        ActiveIndex = index;
        //文字输出模式
        //text.text = equList[index];

        //图片输出模式
        tximg.sprite = TxPics[index];

        //this.transform.position = mainCmr.WorldToScreenPoint(activeTr.position) + new Vector3(offset.x, offset.y);
        OpenPrintOutEqu();
        //StartCoroutine(FormulaUI.Instance.UnlockFormulas(index));
        //Debug.Log("addqeu");
        //}


    }

    ///// <summary>
    ///// 移除方程式的接口
    ///// </summary>
    ///// <param name="equ">要移除的方程式</param>
    //public void RemoveEqu(ChemicalEqu equ)
    //{
    //    ////移除
    //    //equList.RemoveAll(x => CheckEqu(x.first, equ));
    //    ////如果List空了，则关闭
    //    //if (equList.Count == 0)
    //    //{
    //    //    activeEqu = null;
    //    //    activeTr = null;
    //    //    ClosePrintOutEqu();
    //    //    return;
    //    //}
    //    ////更新活动中数据
    //    //activeEqu = equList[0].first;
    //    //activeTr = equList[0].Second;
    //    ////更新输出
    //    //MakeOutEqu(activeEqu.Value);
    //}

    ///// <summary>
    ///// 切换显示方程式的接口
    ///// </summary>
    //public void SwitchEqu()
    //{
    //    if (equList.Count == 0)
    //    {
    //        return;
    //    }
    //    int find = equList.FindIndex(x=>CheckEqu(x.first, activeEqu.Value) && x.Second == activeTr);    //寻找当前显示
    //    //Debug.Log(find);
    //    int changeinto = ((find + 1) > (equList.Count - 1)) ? 0 : (find + 1);                           //切换到下一个
    //    //更新活动中数据
    //    activeEqu = equList[changeinto].first;
    //    activeTr = equList[changeinto].Second;
    //    //更新输出
    //    MakeOutEqu(activeEqu.Value);
    //}

    private void Start()
    {
        img = pic.GetComponent<Image>();
        text = tx.GetComponent<Text>();
        tximg = txpic.GetComponent<Image>();
        bg = transform.Find("bg").gameObject;
        bg.gameObject.GetComponent<Button>().onClick.AddListener(OnSetClosingClick);
        bg.SetActive(false);
        show = transform.Find("Show").gameObject;
        show.SetActive(false);
        show.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(OnChoose1ButtonClick);
        show.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(OnChoose2ButtonClick);
        show.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(OnChoose3ButtonClick);
        isCloseing = false;
        

        activeTr = null;
        ActiveIndex = null;
        //activeEqu = null;
        ClosePrintOutEqu();

        equList = new List<string>();
        string realtext = EquationText.text;

        string t = "";
        foreach (char ch in realtext)
        {
            if (ch == '\n')
            {
                equList.Add(t);
                //Debug.Log("OJBK");
                t = "";
                continue;
            }
            t += ch.ToString();
        }





        //equList = new List<Pair<ChemicalEqu, Transform>>();
        //ListCount = equList.Count;
    }

    /// <summary>
    /// 设置是否关闭化学反应窗口
    /// </summary>
    private void OnSetClosingClick()
    {
        isCloseing = true;
        ClosePrintOutEqu();
        bg.transform.DOScale(new Vector3(1.0f, 1.0f, 1.0f), 1.0f).OnComplete(() =>
           {
               isCloseing = false;
           });
    }

    /// <summary>
    /// 合成表达式并装配到UI
    /// </summary>
    /// <param name="equ">化学表达式</param>
    /// <param name="tr">反应位置的transform</param>
    private void MakeOutEqu(ChemicalEqu equ)
    {
        string print = null;
        pic.SetActive(true);
        tx.SetActive(true);

        //合成表达式
        for (int i = 0; i < equ.input.Count; i++)
        {
            print += equ.input[i].name;
            if (i < equ.input.Count - 1)
            {
                print += "+";
            }
        }
        print += "→";
        for (int i = 0; i < equ.output.Count; i++)
        {
            print += equ.output[i].name;
            if (i < equ.output.Count - 1)
            {
                print += "+";
            }
        }

        text.text = print;

        //this.transform.position = mainCmr.WorldToScreenPoint(activeTr.position) + new Vector3(offset.x, offset.y);
    }

    private void Update()
    {
        //ListCount = equList.Count;

        //更新位置
        if (activeTr != null)
        {
            //this.transform.position = mainCmr.WorldToScreenPoint(activeTr.position) + new Vector3(offset.x, offset.y);
        }
        else
        {
            ClosePrintOutEqu();
        }

        //if (Input.GetKeyDown(SwitchKey))
        //{
        //    SwitchEqu();
        //    //Debug.Log("CNM");
        //}
    }

    /// <summary>
    /// 开启化学式窗口
    /// </summary>
    private void OpenPrintOutEqu()
    {
        if (isCloseing)
        {
            return;
        }
        bg.SetActive(true);
        bg.transform.GetComponent<Image>().DOColor(new Color(
            bg.transform.GetComponent<Image>().color.r,
            bg.transform.GetComponent<Image>().color.g,
            bg.transform.GetComponent<Image>().color.b,
            0.5f), 0.4f).OnComplete(() =>
            {
                show.SetActive(true);
            });
        pic.SetActive(true);
        //pic.transform.position = Camera.main.transform.position + new Vector3(400, 300, 0);
        //图片输出模式
        txpic.SetActive(true);
        //文字输出模式
        //tx.SetActive(true);
    }

    /// <summary>
    /// 选择反应物1
    /// </summary>
    private void OnChoose1ButtonClick()
    {
        PlayerController.GetInstance().prc.Choose1 = true;
    }

    /// <summary>
    /// 选择反应物2
    /// </summary>
    private void OnChoose2ButtonClick()
    {
        PlayerController.GetInstance().prc.Choose2 = true;
    }

    /// <summary>
    /// 选择反应物3
    /// </summary>
    private void OnChoose3ButtonClick()
    {
        PlayerController.GetInstance().prc.Choose3 = true;
    }

    /// <summary>
    /// 关闭化学式窗口
    /// </summary>
    public void ClosePrintOutEqu()
    {
        show.SetActive(false);
        bg.SetActive(false);
        pic.SetActive(false);
        tx.SetActive(false);
    }
}
