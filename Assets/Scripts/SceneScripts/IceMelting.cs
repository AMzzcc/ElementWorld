using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceMelting : MonoBehaviour
{
    //private Dictionary<string, string> HeatReactionPairs;

    //辣鸡试用版
    public string HeatMatter;
    public int MeltingFrame;    //溶解计数帧
    public Material DissolveMaterial;
    public GameObject WaterParticle;
    private int NowMeltingFrame;
    //public int WaitFrame;       //生成物出现等待帧
    //private int NowWaitFrame;
    private bool isMelting;
    private SpriteRenderer sr;

    private string WaitMatter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MatterName mn;
        if ((mn = collision.gameObject.GetComponent<MatterName>()) != null)
        {
            if (/*HeatReactionPairs.ContainsKey(mn.matterName)*/HeatMatter == mn.matterName)
            {
                //HeatReactionPairs.TryGetValue(mn.matterName,out WaitMatter);
                //NowWaitFrame = WaitFrame;
                DissolveMaterial.SetFloat("_BurnAmount", 0);
                sr.material = DissolveMaterial;
                
                WaterParticle.GetComponent<ParticleSystem>().Play();
                isMelting = true;

            }
            //else if (mn.matterName == WaitMatter && NowWaitFrame > 0)
            //{
                
            //}
        }
    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    MatterName mn;
    //    if (mn = collision.gameObject.GetComponent<MatterName>())
    //    {
    //        if (HeatReactionPairs.ContainsKey(mn.matterName))
    //        {
    //            HeatReactionPairs.TryGetValue(mn.matterName, out WaitMatter);
    //        }

    //    }
    //}


    private void Start()
    {
        //ActiveMatter = new List<string>();
        //NowWaitFrame = 0;
        NowMeltingFrame = MeltingFrame;
        //WaitMatter = null;
        isMelting = false;
        //NowMeltingFrame = MeltingFrame;
        sr = GetComponent<SpriteRenderer>();
        //HeatReactionPairs = new Dictionary<string, string>();
        //HeatReactionPairs.Add("CaO", "CaOH");
    }

    
    

    private void Update()
    {
        //if (NowWaitFrame > 0)
        //{
        //    NowWaitFrame--;
        //}
        if (isMelting)
        {
            if (NowMeltingFrame > 0)
            {
                NowMeltingFrame--;
                
                DissolveMaterial.SetFloat("_BurnAmount", DissolveMaterial.GetFloat("_BurnAmount") + 1.0f / MeltingFrame);
                //sr.color -= new Color(0.0f, 0.0f, 0.0f, 1.0f / MeltingFrame);
            }
            else
            {
                Destroy(GetComponent<BoxCollider2D>());
                Destroy(this.gameObject);
            }
        }
    }

   



}
