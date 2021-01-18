using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionConditionEntity : MonoBehaviour
{
    public ReactionCondition Condition;                 //该仪器负责的条件

    //public GameObject reactionManager;
    //private ReactionManager rctMng;                     //组件，包含搜索反应的接口

    ////[SerializeField]
    //private List<ChemistyMatter> cmatterList;           //用于存放反应物

    //public GameObject equUI;                            //显示用UI
    //private ReactionEquUI ui;                            //组件，包含显示接口
    //public int MaxInput;                                //默认最大输入值，超过则不搜索，加快时间

    //private ChemicalEqu? includeEqu;                    //正在使用的方程式

    //private void Start()
    //{
    //    //初始化各种组件
    //    rctMng = reactionManager.GetComponent<ReactionManager>();
    //    cmatterList = new List<ChemistyMatter>();
    //    ui = equUI.GetComponent<ReactionEquUI>();
    //    includeEqu = null;
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if ((collision.gameObject.tag == "Matter" || collision.gameObject.tag == "Player") && cmatterList.Count + 1 <= MaxInput)
    //    {
    //        MatterName Matter = collision.gameObject.GetComponent<MatterName>();                                    //反应物
    //        if (!cmatterList.Exists(x => x.name == Matter.matterName && x.state == Matter.matterState))
    //        {
    //            cmatterList.Add(new ChemistyMatter { name = Matter.matterName, state = Matter.matterState });       //将反应物加入进入仪器List
    //        }


    //        List<ChemistyMatter> output;                                                                            //接收返回生成物
    //        if (rctMng.CheckReaction(cmatterList, Condition, out output))
    //        {
    //            ChemicalEqu equ = new ChemicalEqu { input = cmatterList, conditon = Condition, output = output };
    //            AfterCheckPass(equ);
    //        }
    //        else
    //        {
    //            AfterCheckLost();
    //        }
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if ((collision.gameObject.tag == "Matter" || collision.gameObject.tag == "Player"))
    //    {
    //        MatterName Matter = collision.gameObject.GetComponent<MatterName>();                                    //反应物
    //        cmatterList.Remove(new ChemistyMatter { name = Matter.matterName, state = Matter.matterState });        //因为已经离开，所以从List移除

    //        List<ChemistyMatter> output;
    //        if (cmatterList.Count > 0 && rctMng.CheckReaction(cmatterList, Condition, out output))                  //重新搜索，若List直接跳过
    //        {
    //            ChemicalEqu equ = new ChemicalEqu { input = cmatterList, conditon = Condition, output = output };
    //            AfterCheckPass(equ);
    //        }
    //        else
    //        {
    //            AfterCheckLost();
    //        }
    //    }
    //}
    ///// <summary>
    ///// 成功匹配后执行
    ///// </summary>
    ///// <param name="equ">匹配成功的化学方程式</param>
    //private void AfterCheckPass(ChemicalEqu equ)
    //{
    //    includeEqu = equ;
    //    ui.AddEqu(equ, transform);         //输出
    //}

    //private void AfterCheckLost()
    //{
    //    if (includeEqu != null)
    //    {
    //        ui.RemoveEqu(includeEqu.Value);
    //    }
    //    includeEqu = null;
    //}
}
