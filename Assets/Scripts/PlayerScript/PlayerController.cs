using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //虚拟按键
    public bool MoveL;
    public bool MoveR;
    public bool JumpUp;

    //按键
    private KeyCode MoveLeftButton;
    private KeyCode MoveRightButton;
    private KeyCode JumpButton;

    private KeyCode ChooseKey1;
    private KeyCode ChooseKey2;
    private KeyCode ChooseKey3;
    private KeyCode ChangeKey;
    //移动数值
    public float MoveSpeed;
    public float JumpSpeed;
    public float JumpPressure = 0;  //蓄力值    //蓄力跳
    public float JumpPreMax = 12;    //蓄力最大值
    public float JumpPreMin = 10;    //蓄力最小值
    //public bool isJumping = false;

    private Transform tr;
    private Rigidbody2D rg;
    public PlayerReactionChecker prc;
    private SpriteRenderer sr;
    private PlayerController pc;
    private Animator amt;
    //public string strPath;   
    //public bool IsOpposite;

    [SerializeField]private bool OnGround;   //跳跃
    [SerializeField] private bool FaceToRight = false;
    private Ray2D ray;
    public Transform tf;
    public PhysicsMaterial2D p1;  //有摩擦
    public PhysicsMaterial2D p2;  //无摩擦
    private float Highest;
    private float Lowest;
    private bool CanMove;
    public GameObject LevelManager;
    private bool OpenBook;
    //private AnimatorStateInfo info;
    private static PlayerController _instance = null;
    private PlayerController() { }
    public static PlayerController GetInstance()
    {
        if (_instance == null)
        {
            _instance = FindObjectOfType<PlayerController>();
            if (_instance == null)
            {
                GameObject go = new GameObject("PlayerController");
                _instance = go.AddComponent<PlayerController>();
            }

        }
        return _instance;
    }
    private void Awake()
    {
        tr = GetComponent<Transform>();
        rg = GetComponent<Rigidbody2D>();
        prc = gameObject.GetComponent<PlayerReactionChecker>();
        pc = gameObject.GetComponent<PlayerController>();
        amt = GetComponent<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }
    private void Start()
    {
        MoveL = false;
        MoveR = false;
        JumpUp = false;

        MoveLeftButton = KeyCode.A;
        MoveRightButton = KeyCode.D;
        JumpButton = KeyCode.Space;

        ChooseKey1 = KeyCode.Alpha1;
        ChooseKey2 = KeyCode.Alpha2;
        ChooseKey3 = KeyCode.Alpha3;
        ChangeKey = KeyCode.E;

        prc.ChooseKey1 = ChooseKey1;
        prc.ChooseKey2 = ChooseKey2;
        prc.ChooseKey3 = ChooseKey3;
        prc.changeEquKey = ChangeKey;


        //pc.p1 = (PhysicsMaterial2D)Resources.Load("Assets/Resources/PhysicsMeterial/HasFriction.physicsMaterial2D");
        //pc.p2 = (PhysicsMaterial2D)Resources.Load("Assets/Resources/PhysicsMeterial/NotFriction.physicsMaterial2D");
        rg.sharedMaterial = p1;
        Highest = 28.0f;
        Lowest = -40.0f;
        CanMove = true;
        
        LevelManager = GameObject.Find("LevelManager");

        JumpPressure = JumpPreMin;

        //strPath = "Assets/AnimationController/Liquid.controller";  // 路径
        //amt.runtimeAnimatorController = runAnim; // 赋值
    }
    private void FixedUpdate()
    {
        RayCastJudge();
        //if (BookController.Instance.isOpenBooking)  //宝典打开就不可以移动
        //{
        //    CanMove = false;
        //    OpenBook = true;
        //}
        //if(OpenBook&& !BookController.Instance.isOpenBooking)
        //{
        //    CanMove = true;
        //    OpenBook = false;
        //}
        //if(CanMove)
        {
            Move();
        }     
        Over();
    //ActionControll();
    }

    /// <summary>
    /// 水平移动
    /// </summary>
    private void Move()
    {
        if (OnGround)
        {
            //按住跳跃键;
            if (JumpUp)
            {
                JumpUp = false;
                //if (JumpPressure < JumpPreMax)
                //{
                //    JumpPressure += Time.deltaTime * 15;
                //}
                //else
                //{
                //    JumpPressure = JumpPreMax;
                //}
                JumpPressure = JumpPreMax;
                amt.SetFloat("InPressing", JumpPressure);
                
            }
            //松开跳跃键
            else if (JumpPressure > JumpPreMin + 0.01f)
            {
                rg.velocity = new Vector2(rg.velocity.x, Mathf.Sign(JumpSpeed) * JumpPressure);
                JumpPressure = JumpPreMin; //升空以后把蓄力值重设为0
                rg.sharedMaterial = p2;
                amt.SetTrigger("Jump");
                amt.SetFloat("InPressing", 0);
                JumpUp = false;
            }
        }
        else
        {
            JumpUp = false;
            JumpPressure = JumpPreMin; //非地面 把蓄力值重设为0
            //JumpPressure = JumpPreMax;
            amt.SetFloat("InPressing", 0);
        }

        amt.SetBool("OnGround", OnGround);


        float amtWalkSpeed = 0.0f;

        //if (Input.GetKey(MoveRightButton))
        //{
        //    rg.velocity = new Vector2(MoveSpeed, rg.velocity.y);//移动
        //    amtWalkSpeed = MoveSpeed / 5.0f;
        //    FaceToRight = true;
        //}
        //if (Input.GetKey(MoveLeftButton))
        //{
        //    rg.velocity = new Vector2(-MoveSpeed, rg.velocity.y);//移动
        //    amtWalkSpeed = MoveSpeed / 5.0f;
        //    FaceToRight = false;
        //}

        if (MoveR)
        {
            rg.velocity = new Vector2(MoveSpeed, rg.velocity.y);//移动
            amtWalkSpeed = MoveSpeed / 5.0f;
            FaceToRight = true;
        }
        if (MoveL)
        {
            rg.velocity = new Vector2(-MoveSpeed, rg.velocity.y);//移动
            amtWalkSpeed = MoveSpeed / 5.0f;
            FaceToRight = false;
        }

        //处理转向
        Vector3 scale = rg.transform.localScale;
        scale.x = (FaceToRight ? -1 : 1) * Mathf.Abs(scale.x);
        rg.transform.localScale = scale;

        amt.SetFloat("WalkSpeed", amtWalkSpeed);


        //if (/*isMoving && */isOnGround)
        //{
        //    //amt.ResetTrigger("Stand");
        //    //amt.ResetTrigger("Jump");
        //    //amt.SetTrigger("Move");
        //    //amt.Play("Move");
        //}
    }

    private void RayCastJudge()
    {
        ray = new Ray2D(transform.position, Vector2.down);
        Vector2 direction = new Vector2(tf.position.x, tf.position.y) - ray.origin;
        Vector2 target = direction + new Vector2(transform.position.x, transform.position.y);
        Debug.DrawLine(ray.origin, target, Color.red);
        RaycastHit2D info = Physics2D.Raycast(ray.origin, direction, Mathf.Sqrt(direction.x * direction.x + direction.y * direction.y));
        if (info.collider != null)
        {
            if (info.transform.gameObject.CompareTag("Ground") || info.transform.gameObject.CompareTag("Matter"))
            {
                //Debug.Log("碰到地板");
                OnGround = true;
                rg.sharedMaterial = p1;
               
                
            }
            else
            {
                //Debug.Log("else");
                //OnGround = false;
            }
        }
        else
        {
            //Debug.Log("离开地板");
            OnGround = false;
            //JumpPressure = 0;        //蓄力跳
            rg.sharedMaterial = p2;
        }
    }

    private void Over()
    {
        if(transform.position.y>Highest||transform.position.y<Lowest)
        {
            LevelManager.SendMessage("Lose");
            GetComponent<PlayerController>().enabled = false;
        }
    }

    public void Fade() //褪色
    {
        Invoke("Bleach", 2.0f);
        //Debug.Log("fade");
    }

    private void Bleach() 
    {
        sr.color = Color.white;
        gameObject.GetComponent<MatterName>().dyeingColor = DyeingColor.Normal;
    }

    public void ChangeMove()
    {
        if(CanMove)
        {
            CanMove = false;
        }
        else
        {
            CanMove = true;
        }
    }
}
