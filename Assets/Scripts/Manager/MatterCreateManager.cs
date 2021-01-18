using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CreatMatterType { Player, Matter};

public class MatterCreateManager : MonoBehaviour
{
    //此为辣鸡使用版
    public List<GameObject> MatterList;

    public GameObject rctMng;
    public GameObject equUI;
    public GameObject cmr;              //相机

    //移动按键
    public KeyCode MoveLeft;
    public KeyCode MoveRight;
    public KeyCode Jump;

    //选择按键
    public KeyCode ChooseKey1;
    public KeyCode ChooseKey2;
    public KeyCode ChooseKey3;
    public KeyCode ChangeKey;

    public PhysicsMaterial2D HasF;
    public PhysicsMaterial2D NotF;

    //public RuntimeAnimatorController[] ThreeAniController;  //存储三态跳跃动画控制器，气液固（蓄力跳）

    /// <summary>
    /// 生成物质的接口
    /// </summary>
    /// <param name="matter">物质</param>
    /// <param name="tr">要生成的位置</param>
    /// <param name="type">是玩家还是其他的物质</param>
    public void CreateMatterObject(ChemistyMatter matter, Transform tr, CreatMatterType type)
    {
        //寻找要生成的prefab
        GameObject m = MatterList.Find(x => x.GetComponent<MatterName>().matterName == matter.name &&
                                        x.GetComponent<MatterName>().matterState == matter.state);
        //生成
        GameObject t = Instantiate(m, tr.position, tr.rotation);
        //无效化没必要的组件
        switch (type)
        {
            case CreatMatterType.Player:
                Destroy(t.GetComponent<MatterReactionChecker>());

                //选择反应绑定
                PlayerReactionChecker pr = t.GetComponent<PlayerReactionChecker>();
                pr.reactionManager = rctMng;
                pr.equUI = equUI;
                pr.createManager = this.gameObject;
                //pr.ChooseKey1 = ChooseKey1;
                //pr.ChooseKey2 = ChooseKey2;
                //pr.ChooseKey3 = ChooseKey3;
                //pr.changeEquKey = ChangeKey;

                //移动与物理材质绑定
                PlayerController p = t.GetComponent<PlayerController>();
                //p.MoveLeftButton = MoveLeft;
                //p.MoveRightButton = MoveRight;
                //p.JumpButton = Jump;
                p.p1 = HasF;
                p.p2 = NotF;

                //t.AddComponent<Animator>();//添加动画组件并由Player状态选择添加动画控制器     //蓄力跳
                //Animator ani = t.GetComponent<Animator>();
                //MatterName pn = t.GetComponent<MatterName>();
                //switch(pn.matterState)
                //{
                //    case MatterState.gas:
                //        ani.runtimeAnimatorController = ThreeAniController[0];
                //        if(pn.matterName=="CO2"|| pn.matterName == "O2")
                //        {
                //            p.IsOpposite = false;
                //        }else
                //        {
                //            p.IsOpposite = true;
                //        }
                        
                //        break;
                //    case MatterState.liquid:
                //        ani.runtimeAnimatorController = ThreeAniController[1];
                //        p.IsOpposite = false;
                //        break;
                //    case MatterState.solid:
                //        ani.runtimeAnimatorController = ThreeAniController[2];
                //        p.IsOpposite = false;
                //        break;
                //}

                t.tag = "Player";

                cmr.GetComponent<CameraFollowing>().ChangeTarget(t, false);
                break;
            case CreatMatterType.Matter:
                Destroy(t.GetComponent<PlayerController>());
                Destroy(t.GetComponent<PlayerReactionChecker>());
                t.GetComponent<MatterReactionChecker>().reactionManager = rctMng;
                t.GetComponent<MatterReactionChecker>().createManager = this.gameObject;
                t.tag = "Matter";
                break;
        }
    }
}
