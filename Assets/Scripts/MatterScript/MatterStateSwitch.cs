using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatterStateSwitch : MonoBehaviour
{
    private Rigidbody2D rd;             //刚体

    private void Start()
    {
        rd = GetComponent<Rigidbody2D>();
    }


    /// <summary>
    /// 反应物物态切换接口
    /// </summary>
    /// <param name="state">要切换成的物态</param>
    public void StateSwitch(MatterState state)
    {
        switch (state)
        {
            case MatterState.gas:
                MatterIsGas();
                break;
            case MatterState.liquid:
                MatterIsLiquid();
                break;
            case MatterState.solid:
                MatterIsSolid();
                break;
        }
    }


    private void MatterIsGas()
    {

    }

    private void MatterIsLiquid()
    {

    }
    private void MatterIsSolid()
    {
        rd.gravityScale = 1;
    }
}
