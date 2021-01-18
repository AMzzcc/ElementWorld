using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DyeingColor { Red, Blue, Purple, Yellow,Green,Normal };

public class MatterName : MonoBehaviour
{
    public string matterName;
    public MatterState matterState;
    public ReactionCondition rctCondition;

    public DyeingColor dyeingColor;
    

    private void Start()
    {
        rctCondition = ReactionCondition.normal;
        //dyeingColor = DyeingColor.Normal;
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ReactionCondition")
        {
            rctCondition = collision.gameObject.GetComponent<ReactionConditionEntity>().Condition;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ReactionCondition")
        {
            rctCondition = ReactionCondition.normal;
        }
    }




}
