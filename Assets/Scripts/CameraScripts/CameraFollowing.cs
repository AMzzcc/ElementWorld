using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 相机逐渐跟随
/// </summary>
public class CameraFollowing : MonoBehaviour
{
    private static CameraFollowing instance;
    public static CameraFollowing Instance
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        instance = this;
    }

    //虚拟按键
    public bool MoveW;
    public bool MoveS;
    public bool MoveA;
    public bool MoveD;

    public float MoveRare;
    public float MinDistance;
    public float Left;
    public float Right;
    public float High;
    public float Low;
    public bool IsFree;
    private Vector3 NeedToMove;
    private float MoveSpeed=8.0f;
    //private GameObject FollowingTarget;

    private Transform TargetTr;
    private Transform CameraTr;
    private Rigidbody2D rd;

    /// <summary>
    /// 对外切换目标接口
    /// </summary>
    /// <param name="newTarget">新的目标</param>
    /// <param name="isImmediately">是否立刻将摄像机转移到目标位置</param>
    public void ChangeTarget(GameObject newTarget, bool isImmediately)
    {
        //FollowingTarget = newTarget;
        TargetTr = newTarget.GetComponent<Transform>();
        if (isImmediately)
        {
            CameraTr.position = TargetTr.position;
        }
        
    }

    public void Start()
    {
        MoveA = false;
        MoveD = false;
        MoveS = false;
        MoveW = false;

        CameraTr = GetComponent<Transform>();
        rd = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        
        if (IsFree)
        {
            //if (Input.mousePosition.x >= Right)  //用鼠标控制摄像机
            //{
            //    if (Input.mousePosition.y >= High)
            //    {
            //        NeedToMove = new Vector3(Right - CameraTr.position.x, High - CameraTr.position.y, 0.0f);
            //    }
            //    else if (Input.mousePosition.y <= Low)
            //    {
            //        NeedToMove = new Vector3(Right - CameraTr.position.x, Low - CameraTr.position.y, 0.0f);
            //    }
            //    else if (Input.mousePosition.y > Low && Input.mousePosition.y <High)
            //    {
            //        NeedToMove = new Vector3(Right - CameraTr.position.x, Input.mousePosition.y - CameraTr.position.y, 0.0f);
            //    }
            //}
            //else if (Input.mousePosition.x <= Left)
            //{
            //    if (Input.mousePosition.y >= High)
            //    {
            //        NeedToMove = new Vector3(Left - CameraTr.position.x, High - CameraTr.position.y, 0.0f);
            //    }
            //    else if (Input.mousePosition.y <= Low)
            //    {
            //        NeedToMove = new Vector3(Left - CameraTr.position.x, Low - CameraTr.position.y, 0.0f);
            //    }
            //    else if (Input.mousePosition.y > Low && Input.mousePosition.y <High)
            //    {
            //        NeedToMove = new Vector3(Left - CameraTr.position.x, Input.mousePosition.y - CameraTr.position.y, 0.0f);
            //    }
            //}
            //else
            //{
            //    NeedToMove = new Vector3(Input.mousePosition.x - CameraTr.position.x, Input.mousePosition.y - CameraTr.position.y, 0.0f);
            //}
            //rd.velocity = (NeedToMove.magnitude >= MinDistance ? /*MoveRare * */NeedToMove : Vector3.zero);


            if (MoveD)
            {
                if(transform.position.x<Right)
                {
                    rd.velocity = new Vector2(MoveSpeed, rd.velocity.y);//移动
                }
                else
                {
                    rd.velocity = new Vector2(0, rd.velocity.y);//停止
                }
            }
            else if (MoveA)
            {
                if (transform.position.x >Left)
                {
                    rd.velocity = new Vector2(-MoveSpeed, rd.velocity.y);//移动
                }
                else
                {
                    rd.velocity = new Vector2(0, rd.velocity.y);//停止
                }
            }
            else
            {
                rd.velocity = new Vector2(0, rd.velocity.y);//停止
            }
            if (MoveW)
            {
                if (transform.position.y <High)
                {
                    rd.velocity = new Vector2(rd.velocity.x,MoveSpeed);//移动
                }
                else
                {
                    rd.velocity = new Vector2(rd.velocity.x, 0);//停止
                }
            }
            else if (MoveS)
            {
                if (transform.position.y >Low)
                {
                    rd.velocity = new Vector2(rd.velocity.x, -MoveSpeed);//移动
                }
                else
                {
                    rd.velocity = new Vector2(rd.velocity.x, 0);//停止
                }
            }
            else
            {
                rd.velocity = new Vector2(rd.velocity.x, 0);//停止
            }
        }


        if (TargetTr != null&&!IsFree)
        {
            //if (transform.position.x >= Right)  //用鼠标控制摄像机
            //{
            //    if (transform.position.y >= High)
            //    {
            //        NeedToMove = new Vector3(Right - CameraTr.position.x, High - CameraTr.position.y, 0.0f);
            //    }
            //    else if (transform.position.y <= Low)
            //    {
            //        NeedToMove = new Vector3(Right - CameraTr.position.x, Low - CameraTr.position.y, 0.0f);
            //    }
            //    else if (transform.position.y > Low && transform.position.y < High)
            //    {
            //        NeedToMove = new Vector3(Right - CameraTr.position.x, TargetTr.position.y - CameraTr.position.y, 0.0f);
            //    }
            //}
            //else if (transform.position.x <= Left)
            //{
            //    if (transform.position.y >= High)
            //    {
            //        NeedToMove = new Vector3(Left - CameraTr.position.x, High - CameraTr.position.y, 0.0f);
            //    }
            //    else if (transform.position.y <= Low)
            //    {
            //        NeedToMove = new Vector3(Left - CameraTr.position.x, Low - CameraTr.position.y, 0.0f);
            //    }
            //    else if (transform.position.y > Low && transform.position.y < High)
            //    {
            //        NeedToMove = new Vector3(Left - CameraTr.position.x, TargetTr.position.y - CameraTr.position.y, 0.0f);
            //    }
            //}
            //else
            //{
            //    NeedToMove = new Vector3(TargetTr.position.x - CameraTr.position.x, TargetTr.position.y - CameraTr.position.y, 0.0f);
            //}
            NeedToMove = new Vector3(TargetTr.position.x - CameraTr.position.x, TargetTr.position.y - CameraTr.position.y, 0.0f);
            rd.velocity = (NeedToMove.magnitude >= MinDistance ? MoveRare * NeedToMove : Vector3.zero);
        }
    }

    public void ChangeCameraPattern()
    {
        if(IsFree)
        {
            IsFree = false;
        }
        else
        {
            IsFree = true;
        }
        //GameObject.FindGameObjectWithTag("Player").SendMessage("ChangeMove");
        PlayerController.GetInstance().ChangeMove();
    }
}
