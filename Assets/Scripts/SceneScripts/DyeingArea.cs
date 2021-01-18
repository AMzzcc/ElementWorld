using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DyeingArea : MonoBehaviour
{
    public ColorReagent colorReagent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<MatterName>())
        {
            MatterName mn = collision.gameObject.GetComponent<MatterName>();
            SpriteRenderer sr = collision.gameObject.GetComponentInChildren<SpriteRenderer>();
            if (colorReagent.AcidList.Exists(x => x == mn.matterName))
            {
                mn.dyeingColor = colorReagent.Acid;
                sr.color = colorReagent.AcidColor;
                
            }
            else if(colorReagent.AlkaliList.Exists(x => x == mn.matterName))
            {
                mn.dyeingColor = colorReagent.Alkali;
                sr.color = colorReagent.AlkaliColor;
                if (mn.matterName == "Na2O2")
                {
                    collision.SendMessage("Fade");
                }

            }
        }
    }
}
