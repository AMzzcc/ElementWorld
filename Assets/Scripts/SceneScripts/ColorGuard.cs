using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorGuard : MonoBehaviour
{
    public DyeingColor GuardColor;  //守卫想要的颜色, Red, Blue, Purple, Yellow,Green,Normal
    public Sprite[] GuardSprites;
    private SpriteRenderer SpR;
    private Animator Amt;

    private void Awake()
    {
        SpR = this.GetComponent<SpriteRenderer>();
        Amt = this.GetComponent<Animator>();
    }
    private void Start()
    {
        switch (GuardColor)
        {
            case DyeingColor.Red:
                SpR.sprite = GuardSprites[0];
                break;
            case DyeingColor.Blue:
                SpR.sprite = GuardSprites[1];
                break;
            case DyeingColor.Purple:
                SpR.sprite = GuardSprites[2];
                break;
            case DyeingColor.Yellow:
                SpR.sprite = GuardSprites[3];
                break;
            case DyeingColor.Green:
                SpR.sprite = GuardSprites[4];
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<MatterName>())
        {
            MatterName mn = collision.gameObject.GetComponent<MatterName>();
            if (mn.dyeingColor == GuardColor)
            {
                switch(GuardColor)
                {
                    case DyeingColor.Red:
                        TextUI.GetInstance().ShowText("守门人:原来是红色派系之人，这就让你过去");
                        break;
                    case DyeingColor.Purple:
                        TextUI.GetInstance().ShowText("守门人:原来是紫色派系之人，这就让你过去");
                        break;
                    case DyeingColor.Blue:
                        TextUI.GetInstance().ShowText("守门人:原来是蓝色派系之人，这就让你过去");
                        break;
                    case DyeingColor.Yellow:
                        TextUI.GetInstance().ShowText("守门人:原来是黄色派系之人，这就让你过去");
                        break;
                    case DyeingColor.Green:
                        TextUI.GetInstance().ShowText("守门人:原来是绿色派系之人，这就让你过去");
                        break;
                    case DyeingColor.Normal:
                        TextUI.GetInstance().ShowText("守门人:原来是正常色派系之人，这就让你过去");
                        break;
                }
                Amt.SetTrigger("Hide");
                Invoke("Pass",2.0f);
            }
            else
            {
                switch (GuardColor)
                {
                    case DyeingColor.Red:
                        TextUI.GetInstance().ShowText("守门人:我是红色派系守门人，向来只让同派系之人通过");
                        break;
                    case DyeingColor.Purple:
                        TextUI.GetInstance().ShowText("守门人:我是紫色派系守门人，向来只让同派系之人通过");
                        break;
                    case DyeingColor.Blue:
                        TextUI.GetInstance().ShowText("守门人:我是蓝色派系守门人，向来只让同派系之人通过");
                        break;
                    case DyeingColor.Yellow:
                        TextUI.GetInstance().ShowText("守门人:我是黄色派系守门人，向来只让同派系之人通过");
                        break;
                    case DyeingColor.Green:
                        TextUI.GetInstance().ShowText("守门人:我是绿色派系守门人，向来只让同派系之人通过");
                        break;
                    case DyeingColor.Normal:
                        TextUI.GetInstance().ShowText("守门人:我是中立派系守门人，向来可让各派系之人通过");
                        break;
                }
            }
        }
    }

    /// <summary>
    /// 放行
    /// </summary>
    private void Pass()
    {
        Destroy(this.gameObject);
    }

}
