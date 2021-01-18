using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private static ObjectPool instance;
    public static ObjectPool Instance
    {
        get
        {
            return instance;
        }
    }

    private ManagerVars vars;
    //书页
    private List<GameObject> PageList = new List<GameObject>();
    //书页数量
    private int pageCount;

    private void Awake()
    {
        instance = this;
        vars = ManagerVars.GetManagerVars();
        Init();
    }

    /// <summary>
    /// 初始化
    /// </summary>
    private void Init()
    {
        for(int i =0;i<pageCount;i++)
        {
            SetList(vars.PagePre, ref PageList);
        }
    }

    private GameObject SetList(GameObject pre,ref List<GameObject> list)
    {
        GameObject go = Instantiate(pre, transform);
        go.SetActive(false);
        list.Add(go);
        return go;
    }

    public GameObject GetPage()
    {
        for(int i = 0;i<pageCount;i++)
        {
            if(PageList[i].activeInHierarchy == false)
            {
                return PageList[i];
            }
        }
        return SetList(vars.PagePre,ref PageList);
    }


    


}
