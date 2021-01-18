using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*三反应物以上没整*/
//由Player代码修改而来，阉割了用按钮选择的功能而是选择最后一个化学式
//优先发生多反应物反应，再发生分解反应
//但是增加了通过计数帧数在仪器内延时反应的模块
public class MatterReactionChecker : MonoBehaviour
{
    public GameObject reactionManager;
    private ReactionManager rctMng;                     //组件，包含搜索反应接口

    public GameObject createManager;
    private MatterCreateManager mctMng;                 //组件，包含生成物质的接口

    //public GameObject equUI;                            //显示用UI
    //private ReactionEquUI ui;                            //组件，包含显示用接口

    public int waitFrame;                               //条件反应等待帧
    private int nowWaitFrame;                           //当前计数帧

    private MatterName thisMatter;                      //自身的物质属性
    private ReactionCondition thisCondition_old;        //旧的自己matter

    ////选择生成物按钮
    //public KeyCode ChooseKey1;
    //public KeyCode ChooseKey2;
    //public KeyCode ChooseKey3;


    //public KeyCode changeEquKey;                           //切换反应按钮

    //private int? activeEquIndex;                         //正在使用的方程式下标


    private List<GameObject> objList;                   //碰触OBJ表
    private List<MatterName> objMatterList;             //OBJ表的Matter，方便获取状态
    private List<ReactionCondition> objMatterList_old;  //旧的Matter状态，用于比较
    private List<ChemicalEqu> cmtEqus;                  //当前碰触可反应方程式

    private bool abnormalChecked;                       //非normal状态时是否检查过，提高效率
    private bool isAbnormalReacting;                    //是否正在反应



    private void Start()
    {
        //初始化，获得各种组件
        rctMng = reactionManager.GetComponent<ReactionManager>();
        mctMng = createManager.GetComponent<MatterCreateManager>();
        nowWaitFrame = waitFrame;
        //ui = equUI.GetComponent<ReactionEquUI>();
        thisMatter = this.GetComponent<MatterName>();
        thisCondition_old = thisMatter.rctCondition;
        //activeEquIndex = null;
        //includeEqu = null;
        //includeObj = null;
        objList = new List<GameObject>();
        objMatterList = new List<MatterName>();
        objMatterList_old = new List<ReactionCondition>();
        cmtEqus = new List<ChemicalEqu>();
        abnormalChecked = false;
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
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Matter")
        {
            //获取OBJ所在的序号
            int objIndex = objList.LastIndexOf(collision.gameObject);
            MatterName mn = collision.gameObject.GetComponent<MatterName>();

            //删除列表中的该OBJ要素
            objMatterList_old.RemoveAt(objIndex);
            objMatterList.RemoveAt(objIndex);
            objList.RemoveAt(objIndex);

            //删除所有该OBJ参与的反应
            cmtEqus.RemoveAll(x => x.input.Count >= 2 && x.input[1].name == mn.matterName);
        }
    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Matter")
    //    {
    //        //获取OBJ所在的序号
    //        int objIndex = objList.LastIndexOf(collision.gameObject);
    //        MatterName mn = collision.gameObject.GetComponent<MatterName>();

    //        //删除列表中的该OBJ要素
    //        objMatterList_old.RemoveAt(objIndex);
    //        objMatterList.RemoveAt(objIndex);
    //        objList.RemoveAt(objIndex);

    //        //删除所有该OBJ参与的反应
    //        cmtEqus.RemoveAll(x => x.input.Count >= 2 && x.input[1].name == mn.matterName);
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
            nowWaitFrame = waitFrame;                           //刷新计时
            thisCondition_old = thisMatter.rctCondition;        //更新旧的
            cmtEqus.RemoveAll(x => x.input.Count == 1);         //删除自分解反应
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
            //activeEquIndex = 0;
            abnormalChecked = true;

            List<ChemistyMatter> input = new List<ChemistyMatter>                   //用于存储自己
                        {
                            new ChemistyMatter { name = thisMatter.matterName, state = thisMatter.matterState },
                        };

            List<ChemistyMatter> output;                                            //用于接受返回的生成物

            //匹配成功，更新反应列表
            if (rctMng.CheckReaction(input, thisMatter.rctCondition, out output))
            {
                ChemicalEqu equ = new ChemicalEqu { input = input, conditon = thisMatter.rctCondition, output = output };
                //更新
                cmtEqus.Add(equ);
                //ui.AddEqu(equ, this.transform);
            }
        }

        //不为normal状态且有反应可发生，证明应有计时
        if (thisMatter.rctCondition != ReactionCondition.normal && cmtEqus.Count > 0) 
        {
            //时间到，反应
            if (nowWaitFrame <= 0)
            {
                MakeReaction();
            }
            //计时
            else
            {
                nowWaitFrame--;
            }
        }








    }

    /// <summary>
    /// 按下选择后，进行反应
    /// </summary>
    private void MakeReaction()
    {
        int index = 0;
        if (cmtEqus.Exists(x => x.input.Count > 1))
        {
            index = cmtEqus.FindLastIndex(x => x.input.Count > 1);
        }

        if (cmtEqus[index].input.Count == 1 || string.Compare(cmtEqus[index].input[1].name, thisMatter.matterName) > 0)
        {
            //遍历output来进行生成
            for (int i = 0; i < cmtEqus[index].output.Count; i++)
            {
                mctMng.CreateMatterObject(cmtEqus[index].output[i], this.transform, CreatMatterType.Matter);
            }
            //自反应不需要删除另一个反应物
            if (cmtEqus[index].input.Count != 1)
            {
                GameObject obj = objList.Find(x => x.GetComponent<MatterName>().matterName == cmtEqus[index].input[1].name);
                //Debug.Log("WDNMD");
                Destroy(obj);
            }
            GetComponent<ReactionParticle>().FinalParticle();
            //Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// 检测材质是否能反应
    /// </summary>
    /// <param name="mn">材质</param>
    /// <returns>是否反应</returns>
    private bool MatterCheck(MatterName mn)
    {
        if (mn.rctCondition == thisMatter.rctCondition)
        {

            List<ChemistyMatter> input = new List<ChemistyMatter>                   //用于存储自己和反应物
                        {
                            new ChemistyMatter { name = thisMatter.matterName, state = thisMatter.matterState },
                            new ChemistyMatter { name = mn.matterName, state = mn.matterState }
                        };

            List<ChemistyMatter> output;                                            //用于接受返回的生成物

            //匹配成功，更新反应列表
            if (rctMng.CheckReaction(input, thisMatter.rctCondition, out output))
            {
                ChemicalEqu equ = new ChemicalEqu { input = input, conditon = thisMatter.rctCondition, output = output };
                //更新
                cmtEqus.Add(equ);
                //ui.AddEqu(equ, this.transform);
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }


    //public GameObject reactionManager;
    //private ReactionManager rctMng;                     //组件，包含搜索反应接口

    //public GameObject createManager;
    //private MatterCreateManager mtcMng;                 //组件，包含生成物质的接口

    //public int waitFrame;                               //条件反应等待帧
    //private int nowWaitFrame;                           //当前计数帧

    ////public GameObject equUI;                            //显示用UI
    ////private ReactionEquUI ui;                            //组件，包含显示用接口

    //private MatterName thisMatter;                      //自身的物质属性
    ////private ChemicalEqu? includeEqu;
    ////private GameObject includeObj;
    //private MatterName objLateState;

    //private void Start()
    //{
    //    //初始化，获得各种组件
    //    rctMng = reactionManager.GetComponent<ReactionManager>();
    //    thisMatter = GetComponent<MatterName>();
    //    //ui = equUI.GetComponent<ReactionEquUI>();
    //    mtcMng = createManager.GetComponent<MatterCreateManager>();
    //    //includeEqu = null;
    //    //includeObj = null;
    //    nowWaitFrame = waitFrame;
    //}

    //private void OnTriggerStay2D(Collider2D collision)
    //{

    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{

    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{

    //}

    //public GameObject reactionManager;
    //private ReactionManager rctMng;                     //组件，包含搜索反应接口

    //public GameObject createManager;
    //private MatterCreateManager mtcMng;                 //组件，包含生成物质的接口

    //public int waitFrame;                               //条件反应等待帧
    //private int nowWaitFrame;                           //当前计数帧

    ////public GameObject equUI;                            //显示用UI
    ////private ReactionEquUI ui;                            //组件，包含显示用接口

    //private MatterName thisMatter;                      //自身的物质属性
    //private ChemicalEqu? includeEqu;
    //private GameObject includeObj;


    //private void Start()
    //{
    //    //初始化，获得各种组件
    //    rctMng = reactionManager.GetComponent<ReactionManager>();
    //    thisMatter = GetComponent<MatterName>();
    //    //ui = equUI.GetComponent<ReactionEquUI>();
    //    mtcMng = createManager.GetComponent<MatterCreateManager>();
    //    includeEqu = null;
    //    includeObj = null;
    //    nowWaitFrame = waitFrame;
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    //常态与Matter的反应在此判定，非常态在仪器判定
    //    //改为都在此判断
    //    if (collision.gameObject.tag == "Matter"/* && thisMatter.rctCondition == ReactionCondition.normal*/)
    //    {
    //        MatterName Matter = collision.gameObject.GetComponent<MatterName>();        //反应物

    //        if (Matter.rctCondition == /*ReactionCondition.normal*/thisMatter.rctCondition)  //反应物应也是常态    //改为反应物状态和本状态相对
    //        {
    //            List<ChemistyMatter> input = new List<ChemistyMatter>
    //            {
    //                new ChemistyMatter { name = thisMatter.matterName, state = thisMatter.matterState },
    //                new ChemistyMatter { name = Matter.matterName, state = Matter.matterState }
    //            };                //用于存储自己和反应物

    //            List<ChemistyMatter> output = new List<ChemistyMatter>();                //用于接受返回的生成物
    //            if (rctMng.CheckReaction(input, thisMatter.rctCondition, out output))
    //            {
    //                includeEqu = new ChemicalEqu { input = input, conditon = thisMatter.rctCondition, output = output };
    //                includeObj = collision.gameObject;
    //                CreateMatter();
    //            }
    //            else
    //            {

    //            }
    //        }
    //    }
    //}

    //private void CreateMatter()
    //{
    //    //ui.AddEqu(equ, transform);  //显示

    //    //选一个调用生成函数
    //    if (string.Compare(thisMatter.matterName, includeObj.GetComponent<MatterName>().matterName) >= 1 || includeEqu.Value.input.Count == 1)
    //    {
    //        Destroy(includeObj);
    //        //rctObj.SetActive(false);
    //        foreach (ChemistyMatter m in includeEqu.Value.output)
    //        {
    //            mtcMng.CreateMatterObject(m, GetComponent<Transform>(), CreatMatterType.Matter);
    //        }
    //        Destroy(gameObject);
    //    }
    //    return;

    //    //this.gameObject.SetActive(false);
    //    //Destroy(rctObj);
    //    //return;
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Matter")
    //    {
    //        if (includeEqu != null && collision.gameObject.GetComponent<MatterName>().matterName == includeEqu.Value.input[1].name)
    //        {
    //            //ui.RemoveEqu(includeEqu.Value);  //离开 关闭显示
    //            includeEqu = null;
    //        } 
    //    }
    //}

    //IEnumerator LaterCreateMain()
    //{
    //    while (nowWaitFrame-- > 0)
    //    {
    //        yield return null;
    //    }
    //    if (thisMatter.rctCondition != ReactionCondition.normal)
    //    {
    //        CreateMatter();
    //    }
    //    yield return null;
    //}

    //private void Update()
    //{

    //}

    //private void LaterCreate()
    //{

    //}
}


