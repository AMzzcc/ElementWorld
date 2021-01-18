using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorReagent : MonoBehaviour
{
    public GameObject ReagentParticle;
    public GameObject DyeingArea;

    public List<string> AcidList;   //酸
    public List<string> AlkaliList; //碱

    public DyeingColor Acid;    //酸要染成的颜色
    public DyeingColor Alkali;  //碱要染成的颜色

    public Color AcidColor;     //酸要染成的颜色
    public Color AlkaliColor;   //碱要染成的颜色

    private bool isDyeing;

    private void Start()
    {
        isDyeing = false;
    }
    /// <summary>
    /// 染色模式
    /// </summary>
    public void StartDyeing()
    {
        ReagentParticle.SetActive(true);
        DyeingArea.SetActive(true);
    }
    /// <summary>
    /// 关闭染色模式
    /// </summary>
    public void StopDyeing()
    {
        ReagentParticle.SetActive(false);
        DyeingArea.SetActive(false);
    }
    /// <summary>
    /// 对按钮接口
    /// </summary>
    public void ButtonAction()
    {
        isDyeing = !isDyeing;
        //ReagentParticle.SetActive(isDyeing);
        //DyeingArea.SetActive(isDyeing);

        ReagentParticle.SetActive(true);
        DyeingArea.SetActive(true);
    }


}
