using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 物质状态 气体 液体 固体
/// </summary>
public enum MatterState { gas, liquid, solid}

/// <summary>
/// 反应条件 常态 点燃 加热/高温 通电 冷凝 蒸发
/// </summary>
public enum ReactionCondition { normal, light, heat, electrify, condense, evaporation }

/// <summary>
/// 物质结构体 名称 状态
/// </summary>
public struct ChemistyMatter
{
    public string name;                     //物质名称
    public MatterState state;               //物态
}

/// <summary>
/// 化学方程式 反应物 条件 生成物
/// </summary>
public struct ChemicalEqu
{
    public List<ChemistyMatter> input;
    public ReactionCondition conditon;
    public List<ChemistyMatter> output;
}

public class ReactionManager : MonoBehaviour
{
    public TextAsset EquationText;

    private string realText;
    private List<ChemicalEqu> cmtEqu;

    private static ReactionManager _Instance;           //单例类

    private void Awake()
    {
        _Instance = this;
    }


    private void CharCopy(char[] a, char[] b, int len)
    {
        for (int i = 0; i < len; i++)
        {
            a[i] = b[i];
        }
    }

    private void Start()
    {
        realText = EquationText.text;                   //提取文件数据
        cmtEqu = new List<ChemicalEqu>();               //储存方程式
        char[] t = new char[50];                        //提取句子容器

        //遍历数据
        for (int i = 0; i < realText.Length; i++)
        {
            int j = 0;      //句子容器累加
            int k = 0;
            //提取一行
            while (i < realText.Length && realText[i] != '\n')
            {
                t[j++] = realText[i++];
            }
            char[] m = new char[50];                        //提取物质容器
            ChemistyMatter cm = new ChemistyMatter();       //储存物质中介
            ChemicalEqu equ = new ChemicalEqu
            {
                input = new List<ChemistyMatter>(),
                output = new List<ChemistyMatter>()
            };            //储存方程中介

            bool isInput = true;                            //判断反应物生成物
            int index = 0;                                  //用于物质容器
            for (; k < j; k++)
            {
                char[] mm;
                switch (t[k])
                {
                    case '+':
                        m[index - 1] = '\0';
                        mm = new char[index - 1];
                        CharCopy(mm, m, index - 1);
                        cm.name = new string(mm);
                        m.Initialize();
                        index = 0;
                        switch(t[k - 1])
                        {
                            case 'g':
                                cm.state = MatterState.gas;
                                break;
                            case 'l':
                                cm.state = MatterState.liquid;
                                break;
                            case 's':
                                cm.state = MatterState.solid;
                                break;
                        }
                        
                        if(isInput)
                        {
                            equ.input.Add(cm);
                        }
                        else
                        {
                            equ.output.Add(cm);
                        }
                        break;

                    case '[':
                        m[index - 1] = '\0';
                        mm = new char[index - 1];
                        CharCopy(mm, m, index - 1);
                        cm.name = new string(mm);
                        m.Initialize();
                        index = 0;
                        switch (t[k - 1])
                        {
                            case 'g':
                                cm.state = MatterState.gas;
                                break;
                            case 'l':
                                cm.state = MatterState.liquid;
                                break;
                            case 's':
                                cm.state = MatterState.solid;
                                break;
                        }
                        equ.input.Add(cm);
                        isInput = false;
                        switch (t[k + 1])
                        {
                            case 'N':
                                equ.conditon = ReactionCondition.normal;
                                break;
                            case 'L':
                                equ.conditon = ReactionCondition.light;
                                break;
                            case 'H':
                                equ.conditon = ReactionCondition.heat;
                                break;
                            case 'E':
                                equ.conditon = ReactionCondition.electrify;
                                break;
                            case 'C':
                                equ.conditon = ReactionCondition.condense;
                                break;
                            case 'V':
                                equ.conditon = ReactionCondition.evaporation;
                                break;
                        }
                        k += 2;
                        break;

                    default:
                        m[index++] = t[k];
                        break;
                }
            }
            switch (m[index - 2])
            {
                case 'g':
                    cm.state = MatterState.gas;
                    break;
                case 'l':
                    cm.state = MatterState.liquid;
                    break;
                case 's':
                    cm.state = MatterState.solid;
                    break;
            }
            m[index - 2] = '\0';
            char[] cmm = new char[index - 2];
            CharCopy(cmm, m, index - 2);
            cm.name = new string(cmm);
            equ.output.Add(cm);
            cmtEqu.Add(equ);
        }


        ////输出实验
        //foreach (ChemicalEqu equ in cmtEqu)
        //{
        //    foreach (ChemistyMatter mt in equ.input)
        //    {
        //        Debug.Log(mt.name);
        //        Debug.Log(mt.name.Length);
        //        switch (mt.state)
        //        {
        //            case MatterState.gas:
        //                Debug.Log("气体");
        //                break;
        //            case MatterState.liquid:
        //                Debug.Log("液体");
        //                break;
        //            case MatterState.solid:
        //                Debug.Log("固体");
        //                break;
        //        }
        //    }
        //    switch (equ.conditon)
        //    {
        //        case ReactionCondition.heat:
        //            Debug.Log("加热");
        //            break;
        //        case ReactionCondition.light:
        //            Debug.Log("点燃");
        //            break;
        //        case ReactionCondition.normal:
        //            Debug.Log("常态");
        //            break;
        //    }
        //    foreach (ChemistyMatter mt in equ.output)
        //    {
        //        Debug.Log(mt.name);
        //        Debug.Log(mt.name.Length);
        //        switch (mt.state)
        //        {
        //            case MatterState.gas:
        //                Debug.Log("气体");
        //                break;
        //            case MatterState.liquid:
        //                Debug.Log("液体");
        //                break;
        //            case MatterState.solid:
        //                Debug.Log("固体");
        //                break;
        //        }
        //    }
        //}
        //Debug.Log(cmtEqu.Count);
        //Debug.Log(cmtEqu[1].input[0].name + cmtEqu[1].input[1].name);
    }

    /// <summary>
    /// 表的对比
    /// </summary>
    /// <param name="a">方程式List的input</param>
    /// <param name="b">接口的input</param>
    /// <returns></returns>
    public static bool InputEqual(List<ChemistyMatter> a, List<ChemistyMatter> b)
    {
        if (a.Count != b.Count)
        {
            return false;
        }
        //Debug.Log("Begin");
        int count = 0;
        int count2 = 0;
        foreach (ChemistyMatter m in b)
        {
            for (int i = 0; i < a.Count; i++)
            {
                if (a[i].name == m.name && a[i].state == m.state)
                {
                    count = count + 1;
                    break;
                }
                //Debug.Log(a[i].name);
                //Debug.Log(a[i].name.Length);
            }
        }
        foreach (ChemistyMatter m in a)
        {
            for (int i = 0; i < a.Count; i++)
            {
                if (b[i].name == m.name && b[i].state == m.state)
                {
                    count2 = count2 + 1;
                    break;
                }
                //Debug.Log(a[i].name);
                //Debug.Log(a[i].name.Length);
            }
        }
        if (count != b.Count || count2 != b.Count)
        {
            //Debug.Log("CNNN");
            //Debug.Log(count);
            return false;
        }
        //Debug.Log("FFT");
        return true; 
    }
    /// <summary>
    /// 检测方程式成立接口
    /// </summary>
    /// <param name="input">反应物List</param>
    /// <param name="condition">反应条件</param>
    /// <param name="output">接收生成物List</param>
    /// <returns>若存在这样的反应，返回true</returns>
    public bool CheckReaction( List<ChemistyMatter> input, ReactionCondition condition, out List<ChemistyMatter> output)
    {
        output = new List<ChemistyMatter>();

        ChemicalEqu equ = new ChemicalEqu();
        int i;
        for (i = 0; i < cmtEqu.Count; i++)
        {
            //Debug.Log(cmtEqu[i].input[0].name);
            if (InputEqual(cmtEqu[i].input, input) && cmtEqu[i].conditon == condition)
            {
                
                equ = cmtEqu[i];
                break;
            }    
        }


        if (i == cmtEqu.Count)
        {
            //Debug.Log("CNM");
            return false;
        }
        output = new List<ChemistyMatter>(equ.output);
        return true;
    }

    /// <summary>
    /// 检测方程式成立接口
    /// </summary>
    /// <param name="input">反应物List</param>
    /// <param name="condition">反应条件</param>
    /// <param name="output">接收生成物List</param>
    /// <param name="index">序号</param>
    /// <returns>若存在这样的反应，返回true</returns>
    public bool CheckReaction(List<ChemistyMatter> input, ReactionCondition condition, out List<ChemistyMatter> output, out int index)
    {
        output = new List<ChemistyMatter>();

        ChemicalEqu equ = new ChemicalEqu();
        int i;
        for (i = 0; i < cmtEqu.Count; i++)
        {
            if (InputEqual(cmtEqu[i].input, input) && cmtEqu[i].conditon == condition)
            {
                equ = cmtEqu[i];
                break;
            }
        }

        index = 0;
        if (i == cmtEqu.Count)
        {
            //Debug.Log("CNM");
            return false;
        }
        output = new List<ChemistyMatter>(equ.output);
        index = i;
        //Debug.Log("CCCCC");
        return true;
    }
    //private void Update()
    //{
    //    List<ChemistyMatter> input = new List<ChemistyMatter>
    //    {
    //        new ChemistyMatter { name = "O2" , state = MatterState.gas},
    //        new ChemistyMatter { name = "H2" , state = MatterState.gas}
    //    };
    //    //foreach (ChemistyMatter mt in input)
    //    //{
    //    //    Debug.Log(mt.name);
    //    //    switch (mt.state)
    //    //    {
    //    //        case MatterState.gas:
    //    //            Debug.Log("气体");
    //    //            break;
    //    //        case MatterState.liquid:
    //    //            Debug.Log("液体");
    //    //            break;
    //    //        case MatterState.solid:
    //    //            Debug.Log("固体");
    //    //            break;
    //    //    }
    //    //}
    //    List<ChemistyMatter> outp;
    //    if (CheckReaction(input, ReactionCondition.light, out outp))
    //    {
    //        Debug.Log("CMB");
    //    }
    //}
}
