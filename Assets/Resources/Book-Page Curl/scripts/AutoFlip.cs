using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Book))]
public class AutoFlip : MonoBehaviour {


    public FlipMode Mode;
    public float PageFlipTime = 1;
    public float TimeBetweenPages = 1;
    public float DelayBeforeStarting = 0;
    public bool AutoStartFlip=true;
    public Book ControledBook;
    public int AnimationFramesCount = 40;
    bool isFlipping = false;

    private void Awake()
    {
        EventCenter.AddListener(EventDefine.FlipLeft, FlipLeftPage);
        EventCenter.AddListener(EventDefine.FlipRight, FlipRightPage);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.FlipLeft, FlipLeftPage);
        EventCenter.RemoveListener(EventDefine.FlipRight, FlipRightPage);
    }

    // Use this for initialization
    void Start () {
        
        if (!ControledBook)
            ControledBook = GetComponent<Book>();
        if (AutoStartFlip)
            StartFlipping();
        ControledBook.OnFlip.AddListener(new UnityEngine.Events.UnityAction(PageFlipped));
	}
    void PageFlipped()
    {
        isFlipping = false;
    }
	public void StartFlipping()
    {
        StartCoroutine(FlipToEnd());
    }

    public void FlipRightPage()
    {
        if (isFlipping) return;
        if (ControledBook.currentPage >= ControledBook.TotalPageCount) return;
        isFlipping = true;
        float frameTime = PageFlipTime / AnimationFramesCount;
        float xc = (ControledBook.EndBottomRight.x + ControledBook.EndBottomLeft.x) / 2;
        float xl = ((ControledBook.EndBottomRight.x - ControledBook.EndBottomLeft.x) / 2) * 0.9f;
        //float h =  ControledBook.Height * 0.5f;
        float h = Mathf.Abs(ControledBook.EndBottomRight.y) * 0.9f;
        float dx = (xl)*2 / AnimationFramesCount;
        StartCoroutine(FlipRTL(xc, xl, h, frameTime, dx));
    }


    public void FlipLeftPage()
    {
        if (isFlipping) return;
        if (ControledBook.currentPage <= 0) return;
        isFlipping = true;
        float frameTime = PageFlipTime / AnimationFramesCount;
        float xc = (ControledBook.EndBottomRight.x + ControledBook.EndBottomLeft.x) / 2;
        float xl = ((ControledBook.EndBottomRight.x - ControledBook.EndBottomLeft.x) / 2) * 0.9f;
        //float h =  ControledBook.Height * 0.5f;
        float h = Mathf.Abs(ControledBook.EndBottomRight.y) * 0.9f;
        float dx = (xl) * 2 / AnimationFramesCount;
        StartCoroutine(FlipLTR(xc, xl, h, frameTime, dx));
      
    }

    /// <summary>
    /// 快速向右翻页
    /// </summary>
    /// <param name="waitTime"></param>
    /// <returns></returns>
    public void FlipQuickRightPage()
    {
        if (!isFlipping && (ControledBook.currentPage < ControledBook.TotalPageCount))
        {
            isFlipping = true;
            //float frameTime = 0.00001f;
            float frameTime = PageFlipTime / AnimationFramesCount;
            float xc = (ControledBook.EndBottomRight.x + ControledBook.EndBottomLeft.x) / 2;
            float xl = ((ControledBook.EndBottomRight.x - ControledBook.EndBottomLeft.x) / 2) * 0.9f;
            //float h =  ControledBook.Height * 0.5f;
            float h = Mathf.Abs(ControledBook.EndBottomRight.y) * 0.9f;
            float dx = (xl) * 2 / AnimationFramesCount;
            Debug.Log(ControledBook.TotalPageCount);
            StartCoroutine(FlipRTL(xc, xl, h, frameTime, dx));
        }
        
        
    }

    /// <summary>
    /// 快速向左翻页
    /// </summary>
    /// <param name="waitTime"></param>
    /// <returns></returns>
    public void FlipQuickLeftPage()
    {
        //if (isFlipping) yield return 0;
        //if (ControledBook.currentPage <= 0) yield return 0;
        if (!isFlipping&&ControledBook.currentPage > 0)
        {
            //Book.Instance.currentPage += 2;
            isFlipping = true;
            float frameTime = PageFlipTime / AnimationFramesCount;
            float xc = (ControledBook.EndBottomRight.x + ControledBook.EndBottomLeft.x) / 2;
            float xl = ((ControledBook.EndBottomRight.x - ControledBook.EndBottomLeft.x) / 2) * 0.9f;
            //float h =  ControledBook.Height * 0.5f;
            float h = Mathf.Abs(ControledBook.EndBottomRight.y) * 0.9f;
            float dx = (xl) * 2 / AnimationFramesCount;

            StartCoroutine(FlipLTR(xc, xl, h, frameTime, dx));
        }
        
    }

    

    IEnumerator FlipToEnd()
    {
        yield return new WaitForSeconds(DelayBeforeStarting);
        float frameTime = PageFlipTime / AnimationFramesCount;
        float xc = (ControledBook.EndBottomRight.x + ControledBook.EndBottomLeft.x) / 2;
        float xl = ((ControledBook.EndBottomRight.x - ControledBook.EndBottomLeft.x) / 2)*0.9f;
        //float h =  ControledBook.Height * 0.5f;
        float h = Mathf.Abs(ControledBook.EndBottomRight.y)*0.9f;
        //y=-(h/(xl)^2)*(x-xc)^2          
        //               y         
        //               |          
        //               |          
        //               |          
        //_______________|_________________x         
        //              o|o             |
        //           o   |   o          |
        //         o     |     o        | h
        //        o      |      o       |
        //       o------xc-------o      -
        //               |<--xl-->
        //               |
        //               |
        float dx = (xl)*2 / AnimationFramesCount;
        
        switch (Mode)
        {
            case FlipMode.RightToLeft:
                while (ControledBook.currentPage < ControledBook.TotalPageCount)
                {
                    StartCoroutine(FlipRTL(xc, xl, h, frameTime, dx));
                    yield return new WaitForSeconds(TimeBetweenPages);
                }
                break;
            case FlipMode.LeftToRight:
                while (ControledBook.currentPage > 0)
                {
                    StartCoroutine(FlipLTR(xc, xl, h, frameTime, dx));
                    yield return new WaitForSeconds(TimeBetweenPages);
                }
                break;
        }
    }
    IEnumerator FlipRTL(float xc, float xl, float h, float frameTime, float dx)
    {
        float x = xc + xl;
        float y = (-h / (xl * xl)) * (x - xc) * (x - xc);
        ControledBook.DragRightPageToPoint(new Vector3(x, y, 0));
        for (int i = 0; i < AnimationFramesCount; i++)
        {
            y = (-h / (xl * xl)) * (x - xc) * (x - xc);
            ControledBook.UpdateBookRTLToPoint(new Vector3(x, y, 0));
            yield return new WaitForSeconds(0);
            x -= dx;
        }
        ControledBook.ReleasePage();

        //修改    2019年7月30日22:49:09
        BookController.Instance.isAutoFlip = true;
    }
    IEnumerator FlipLTR(float xc, float xl, float h, float frameTime, float dx)
    {
        float x = xc - xl;
        float y = (-h / (xl * xl)) * (x - xc) * (x - xc);
        ControledBook.DragLeftPageToPoint(new Vector3(x, y, 0));
        for (int i = 0; i < AnimationFramesCount; i++)
        {
            y = (-h / (xl * xl)) * (x - xc) * (x - xc);
            ControledBook.UpdateBookLTRToPoint(new Vector3(x, y, 0));
            yield return new WaitForSeconds(0);
            x += dx;
        }
        ControledBook.ReleasePage();

        //修改    2019年7月30日22:49:09
        BookController.Instance.isAutoFlip = true;
    }
}
