using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*同时接触到两个或以上的可反应物可能会有bug，暂时没管*/

public class PlayerReactionChecker : MonoBehaviour
{
    public GameObject reactionManager;
    private ReactionManager rctMng;                     //组件，包含搜索反应接口

    public GameObject createManager;
    private MatterCreateManager mctMng;                 //组件，包含生成物质的接口

    public GameObject equUI;                            //显示用UI
    private ReactionEquUI ui;                            //组件，包含显示用接口

    private MatterName thisMatter;                      //Player自身的物质属性
    private ReactionCondition thisCondition_old;        //旧的自己matter

    //private ChemicalEqu? includeEqu;                  //正在用的方程式
    //private GameObject includeObj;

    //public int waitFrame;                               //条件反应等待帧
    //private int nowWaitFrame;                           //当前计数帧

    //选择生成物按钮
    public KeyCode ChooseKey1;
    public KeyCode ChooseKey2;
    public KeyCode ChooseKey3;

    //虚拟选择生成物按钮
    public bool Choose1 = false;
    public bool Choose2 = false;
    public bool Choose3 = false;


    public KeyCode changeEquKey;                           //切换反应按钮

    private int? activeEquIndex;                         //正在使用的方程式下标
    public GameObject sprite;

    private List<GameObject> objList;                   //碰触OBJ表
    private List<MatterName> objMatterList;             //OBJ表的Matter，方便获取状态
    private List<ReactionCondition> objMatterList_old;  //旧的Matter状态，用于比较
    private List<ChemicalEqu> cmtEqus;                  //当前碰触可反应方程式

    [SerializeField]
    private bool abnormalChecked;                       //非normal状态时是否检查过，提高效率



    private void Start()
    {
        sprite = gameObject.transform.GetChild(2).gameObject;
        

        //初始化，获得各种组件
        equUI = GameObject.Find("UI/ReactionEquUI").gameObject;
        rctMng = reactionManager.GetComponent<ReactionManager>();
        mctMng = createManager.GetComponent<MatterCreateManager>();
        ui = equUI.GetComponent<ReactionEquUI>();
        thisMatter = this.GetComponent<MatterName>();
        thisCondition_old = thisMatter.rctCondition;
        activeEquIndex = null;
        //includeEqu = null;
        //includeObj = null;
        objList = new List<GameObject>();
        objMatterList = new List<MatterName>();
        objMatterList_old = new List<ReactionCondition>();
        cmtEqus = new List<ChemicalEqu>();
        abnormalChecked = true;
        //nowWaitFrame = waitFrame;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //加入碰撞列表，将材质加入列表
        if (collision.gameObject.tag == "Matter")
        {
            objList.Add(collision.gameObject);
            MatterName mn = collision.gameObject.GetComponent<MatterName>();
            objMatterList.Add(mn);
            objMatterList_old.Add(mn.rctCondition);
            MatterCheck(mn);
        }
    }



    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Matter")
        {
            //获取OBJ所在的序号
            int objIndex = objList.LastIndexOf(collision.gameObject);
            //Debug.Log(objIndex);
            MatterName mn = collision.gameObject.GetComponent<MatterName>();

            //删除列表中的该OBJ要素
            objMatterList_old.RemoveAt(objIndex);
            objMatterList.RemoveAt(objIndex);
            objList.RemoveAt(objIndex);

            //删除所有该OBJ参与的反应
            cmtEqus.RemoveAll(x => x.input.Count >= 2 && x.input[1].name == mn.matterName);
            activeEquIndex = 0;
        }
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    //加入碰撞列表，将材质加入列表
    //    if (collision.gameObject.tag == "Matter")
    //    {
    //        objList.Add(collision.gameObject);
    //        MatterName mn = collision.gameObject.GetComponent<MatterName>();
    //        objMatterList.Add(mn);
    //        objMatterList_old.Add(mn.rctCondition);
    //        MatterCheck(mn);
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Matter")
    //    {
    //        //获取OBJ所在的序号
    //        int objIndex = objList.LastIndexOf(collision.gameObject);
    //        Debug.Log(objIndex);
    //        MatterName mn = collision.gameObject.GetComponent<MatterName>();

    //        //删除列表中的该OBJ要素
    //        objMatterList_old.RemoveAt(objIndex);
    //        objMatterList.RemoveAt(objIndex);
    //        objList.RemoveAt(objIndex);

    //        //删除所有该OBJ参与的反应
    //        cmtEqus.RemoveAll(x => x.input.Count >= 2 && x.input[1].name == mn.matterName);
    //        activeEquIndex = 0;
    //    }
    //}

    ///// <summary>
    ///// 材质比较函数
    ///// </summary>
    //private bool MatterEqu(MatterName a, MatterName b)
    //{
    //    if (a.matterName == b.matterName && a.matterState == b.matterState && a.rctCondition == b.rctCondition)
    //        return true;
    //    return false;
    //}

    private void Update()
    {
        //遍历材质，找到变化的材质
        for (int i = 0; i < objList.Count; i++)
        {
            if (objMatterList[i].rctCondition != objMatterList_old[i])
            {
                //若之前有，删除，重新检查
                cmtEqus.RemoveAll(x => x.input.Exists(m => m.name == objMatterList[i].matterName));
                MatterCheck(objMatterList[i]);
                //更新对应oldlist
                objMatterList_old[i] = objMatterList[i].rctCondition;
            }
        }

        //检查状态是否变化
        if (thisCondition_old != thisMatter.rctCondition)
        {
            thisCondition_old = thisMatter.rctCondition;
            cmtEqus.RemoveAll(x => x.input.Count == 1);
            //nowWaitFrame = waitFrame;
            abnormalChecked = false;
        }

        //自身状态变化重新检测
        if (abnormalChecked == false)
        {
            //清空
            cmtEqus.Clear();
            //重新检查
            for (int i = 0; i < objList.Count; i++)
            {
                MatterCheck(objMatterList[i]);
            }
        }



        //检查针对自身的分解反应
        if (thisMatter.rctCondition != ReactionCondition.normal && abnormalChecked == false)
        {
            
            activeEquIndex = 0;
            //Debug.Log("wtf");
            abnormalChecked = true;

            List<ChemistyMatter> input = new List<ChemistyMatter>                   //用于存储自己
                        {
                            new ChemistyMatter { name = thisMatter.matterName, state = thisMatter.matterState },
                        };

            List<ChemistyMatter> output;                                            //用于接受返回的生成物

            //匹配成功，更新反应列表
            int index;
            if (rctMng.CheckReaction(input, thisMatter.rctCondition, out output, out index))
            {
                //Debug.Log("OOOOKKKK");
                ChemicalEqu equ = new ChemicalEqu { input = input, conditon = thisMatter.rctCondition, output = output };
                //更新
                cmtEqus.Add(equ);
                //if (thisMatter.rctCondition == ReactionCondition.condense ||
                //    (thisMatter.rctCondition == ReactionCondition.heat && input.Count == 1 && output.Count == 1))
                //{
                    
                //    activeEquIndex = cmtEqus.FindIndex(x => ReactionManager.InputEqual(x.input, equ.input) && x.conditon == equ.conditon);
                //    MakeReaction(0);
                //    return;
                //}
                if (thisMatter.rctCondition == ReactionCondition.condense || thisMatter.rctCondition == ReactionCondition.evaporation)
                {
                    activeEquIndex = cmtEqus.Count - 1;
                    MakeReaction(0);
                }
                else
                {
                    ui.AddEqu(index, this.transform);
                }
                
            }
        }
        //切换使用的反应方程式
        abnormalChecked = true;
        if (cmtEqus.Count == 0)
        {
            activeEquIndex = null;
            ui.ClosePrintOutEqu();
        }

        ////切换方程式
        //if (Input.GetKeyDown(changeEquKey) && activeEquIndex != null)
        //{
        //    activeEquIndex = (activeEquIndex + 1) > (cmtEqus.Count - 1) ? 0 : (activeEquIndex + 1);
        //    ui.AddEqu(cmtEqus[activeEquIndex.Value], this.transform);
        //}

        if (activeEquIndex != null)
        {
            if (Choose1)
            {
                Choose1 = false;
                ui.ClosePrintOutEqu();
                MakeReaction(0);
            }
            else if (Choose2)
            {
                Choose2 = false;
                ui.ClosePrintOutEqu();
                MakeReaction(1);
            }
            else if (Choose3)
            {
                Choose3 = false;
                ui.ClosePrintOutEqu();
                MakeReaction(2);
            }
        }
    }

    /// <summary>
    /// 按下选择后，进行反应
    /// </summary>
    /// <param name="playerIndex">作为玩家的序号</param>
    private void MakeReaction(int playerIndex)
    {
        //序号超出范围，return
        if (cmtEqus[activeEquIndex.Value].output.Count - 1 < playerIndex)
        {

            return;
        }
        //Debug.Log("WD");
        //遍历output来进行生成
        for (int i = 0; i < cmtEqus[activeEquIndex.Value].output.Count; i++)
        {
            //判断是不是玩家
            if (i == playerIndex)
            {
                mctMng.CreateMatterObject(cmtEqus[activeEquIndex.Value].output[i], this.transform, CreatMatterType.Player);
            }
            else
            {
                mctMng.CreateMatterObject(cmtEqus[activeEquIndex.Value].output[i], this.transform, CreatMatterType.Matter);
            }
        }
        //自反应不需要删除另一个反应物
        if (cmtEqus[activeEquIndex.Value].input.Count != 1)
        {
            GameObject obj = objList.Find(x => x.GetComponent<MatterName>().matterName == cmtEqus[activeEquIndex.Value].input[1].name);
            //Debug.Log("WDNMD");
            Destroy(obj);
        }
        //Debug.Log("liziqidong");
        GetComponent<ReactionParticle>().FinalParticle();
        //Destroy(this.gameObject);
        return;
    }

    /// <summary>
    /// 检测材质是否能反应
    /// </summary>
    /// <param name="mn">材质</param>
    /// <returns>是否反应</returns>
    private bool MatterCheck(MatterName mn)
    {
        activeEquIndex = 0;
        //Debug.Log("MKMP");
        if (mn.rctCondition == thisMatter.rctCondition)
        {

            List<ChemistyMatter> input = new List<ChemistyMatter>                   //用于存储自己和反应物
                        {
                            new ChemistyMatter { name = thisMatter.matterName, state = thisMatter.matterState },
                            new ChemistyMatter { name = mn.matterName, state = mn.matterState }
                        };

            List<ChemistyMatter> output;                                            //用于接受返回的生成物

            //匹配成功，更新反应列表
            int index;
            //Debug.Log(input[0].name + input[1].name + thisMatter.rctCondition.ToString());
            if (rctMng.CheckReaction(input, thisMatter.rctCondition, out output, out index))
            {
                ChemicalEqu equ = new ChemicalEqu { input = input, conditon = thisMatter.rctCondition, output = output };
                //更新
                cmtEqus.Add(equ);
                ui.AddEqu(index, this.transform);
                return true;
            }
            else
            {
                //Debug.Log("F");
                return false;
            }
        }
        return false;
    }
    
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    //常态与Matter的反应在此判定，非常态在仪器判定
    //    if (collision.gameObject.tag == "Matter"/* && thisMatter.rctCondition == ReactionCondition.normal*/)
    //    {
    //        MatterName Matter = collision.gameObject.GetComponent<MatterName>();        //反应物

    //        if (Matter.rctCondition == /*ReactionCondition.normal*/thisMatter.rctCondition) //反应物应也是常态 //反应物和player状态一致
    //        {
    //            List<ChemistyMatter> input = new List<ChemistyMatter>                   //用于存储自己和反应物
    //            {
    //                new ChemistyMatter { name = thisMatter.matterName, state = thisMatter.matterState },
    //                new ChemistyMatter { name = Matter.matterName, state = Matter.matterState }
    //            };

    //            List<ChemistyMatter> output;                                            //用于接受返回的生成物
    //            if (rctMng.CheckReaction(input, thisMatter.rctCondition, out output))
    //            {
    //                ChemicalEqu equ = new ChemicalEqu { input = input, conditon = thisMatter.rctCondition, output = output };
    //                includeEqu = equ;
    //                includeObj = collision.gameObject;
    //                AfterCheckPass();
    //            }
    //        }
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Matter")
    //    {
    //        if (includeEqu != null && collision.gameObject.GetComponent<MatterName>().matterName == includeEqu.Value.input[1].name)
    //        {
    //            ui.RemoveEqu(includeEqu.Value);  //离开 关闭显示
    //            includeEqu = null;
    //        }
    //    }
    //}

    ///// <summary>
    ///// 成功匹配后执行
    ///// </summary>
    //private void AfterCheckPass()
    //{
    //    //includeEqu = equ;
    //    ui.AddEqu(includeEqu.Value, transform);                                     //显示

    //}

    //private void Update()
    //{
    //    if (includeEqu != null)
    //    {
    //        if (Input.GetKeyDown(ChooseKey1))
    //        {

    //        }
    //        else if (Input.GetKeyDown(ChooseKey2))
    //        {

    //        }
    //        else if (Input.GetKeyDown(ChooseKey3))
    //        {

    //        }
    //    }
    //}

}
