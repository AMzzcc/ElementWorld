using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Button_Handle :  MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IBeginDragHandler
{
	//UI_Button 控制
	public GameObject button_Img;   //手柄圆
	public GameObject button_Point; //触摸圆


    bool isonCon = false;//是否正在控制

	public bool isLock = false;//是否是【锁定式】
	bool isStartInHandlePoint = false;//触摸点一开始是否在【手柄圆】中
//接口
	public void OnBeginDrag(PointerEventData eventData)//滑动开始
	{
        
	}

	public void OnDrag(PointerEventData eventData)//滑动中
	{
        //Debug.Log("x = " + button_Point.transform.localPosition.x);
        //Debug.Log(button_Point.transform.localPosition.y);
        if(!CameraFollowing.Instance.IsFree)
        {
            if (button_Point.transform.localPosition.x < -30.0f)
            {
                PlayerController.GetInstance().MoveL = true;
                PlayerController.GetInstance().MoveR = false;
            }
            if (button_Point.transform.localPosition.x > 30.0f)
            {
                PlayerController.GetInstance().MoveR = true;
                PlayerController.GetInstance().MoveL = false;
            }
            if(button_Point.transform.localPosition.x < 20.0f && button_Point.transform.localPosition.x > -20.0f)
            {
                PlayerController.GetInstance().MoveR = false;
                PlayerController.GetInstance().MoveL = false;
            }
        }
        else
        {
            PlayerController.GetInstance().MoveR = false;
            PlayerController.GetInstance().MoveL = false;
        }


        //w
        if(button_Point.transform.localPosition.x < 40.0f &&
            button_Point.transform.localPosition.x > -40.0f && button_Point.transform.localPosition.y > 30.0f)
        {
            CameraFollowing.Instance.MoveW = true;
            CameraFollowing.Instance.MoveS = false;
            CameraFollowing.Instance.MoveD = false;
            CameraFollowing.Instance.MoveA = false;
        }
        //s
        if (button_Point.transform.localPosition.x < 40.0f &&
            button_Point.transform.localPosition.x > -40.0f && button_Point.transform.localPosition.y < -30.0f)
        {
            CameraFollowing.Instance.MoveW = false;
            CameraFollowing.Instance.MoveS = true;
            CameraFollowing.Instance.MoveD = false;
            CameraFollowing.Instance.MoveA = false;
        }
        //a
        if (button_Point.transform.localPosition.y < 30.0f &&
            button_Point.transform.localPosition.y > -30.0f && button_Point.transform.localPosition.x < -40.0f)
        {
            CameraFollowing.Instance.MoveW = false;
            CameraFollowing.Instance.MoveS = false;
            CameraFollowing.Instance.MoveD = false;
            CameraFollowing.Instance.MoveA = true;
        }
        //d
        if (button_Point.transform.localPosition.y < 30.0f &&
            button_Point.transform.localPosition.y > -30.0f && button_Point.transform.localPosition.x >40.0f)
        {
            CameraFollowing.Instance.MoveW = false;
            CameraFollowing.Instance.MoveS = false;
            CameraFollowing.Instance.MoveD = true;
            CameraFollowing.Instance.MoveA = false;
        }


        if (isLock&&!isStartInHandlePoint)//【锁定式】且初始触摸点不在【手柄圆】中，不进行onDrag
		{
			return;
		}
		//获取ViewPoint
		Vector3 viewPoint = Camera.main.ScreenToViewportPoint(new Vector3(eventData.position.x, eventData.position.y, 0));
		Vector2 panelViewPoint = changeInBase(viewPoint, gameObject);
		//
		Vector2 button_base_ViewPoint = changeInBase(panelViewPoint, button_Img.gameObject);

		Vector2 toMiddlePoint = new Vector2(button_base_ViewPoint.x - 0.5f, button_base_ViewPoint.y - 0.5f);
		float disToMiddlePoint = toMiddlePoint.magnitude;
		if (disToMiddlePoint - 0.5 < 0.001)//在范围内
		{
			//按键设置
			setToViewPoint(panelViewPoint, button_Img.gameObject, button_Point.gameObject, true);
			
		}
		else//不在范围内
		{
			//按键设置
			Vector2 goToViewPoint = toMiddlePoint.normalized * 0.5f;
			setToViewPoint(new Vector2(goToViewPoint.x + 0.5f, goToViewPoint.y + 0.5f), button_Img.gameObject, button_Point.gameObject, false);
			
		}
	}

	public void OnPointerDown(PointerEventData eventData)//触摸开始
	{
		if (!isLock)//【非锁定式】
		{
			button_Img.SetActive(true);//显示

			//根据初始点击点 设置【手柄圆】的位置
			Vector3 viewPoint = Camera.main.ScreenToViewportPoint(new Vector3(eventData.pressPosition.x, eventData.pressPosition.y, 0));
			setToViewPoint(viewPoint, gameObject, button_Img, true);
			//根据初始点击点 设置【触点圆】的位置
			setToViewPoint(new Vector2(0.5f, 0.5f), button_Img, button_Point, false);
		}
		else
		//【锁定式】需要根据初始触摸点是否在【手柄圆】中设置flag告知onDrag是否需要触发，以及在范围内时立刻完成对于【触摸点】位置的设置
		{
			Vector3 viewPoint = Camera.main.ScreenToViewportPoint(new Vector3(eventData.position.x, eventData.position.y, 0));
			Vector2 panelViewPoint = changeInBase(viewPoint, gameObject);
			//
			Vector2 button_base_ViewPoint = changeInBase(panelViewPoint, button_Img.gameObject);

			Vector2 toMiddlePoint = new Vector2(button_base_ViewPoint.x - 0.5f, button_base_ViewPoint.y - 0.5f);
			float disToMiddlePoint = toMiddlePoint.magnitude;
			if (disToMiddlePoint - 0.5 < 0.001)//在范围内
			{
				isStartInHandlePoint = true;
				//按键设置
				setToViewPoint(panelViewPoint, button_Img.gameObject, button_Point.gameObject, true);

			}
			else
			{
				isStartInHandlePoint = false;
			}
		}

	}

	public void OnPointerUp(PointerEventData eventData)//触摸结束
	{
		if (isLock)//【锁定式】抬起时【触点圆】回归中心，而不是取消显示
		{
            PlayerController.GetInstance().MoveL = false;
            PlayerController.GetInstance().MoveR = false;
            CameraFollowing.Instance.MoveW = false;
            CameraFollowing.Instance.MoveS = false;
            CameraFollowing.Instance.MoveD = false;
            CameraFollowing.Instance.MoveA = false;
            setToViewPoint(new Vector2(0.5f, 0.5f), button_Img, button_Point, false);
			return;
		}
		button_Img.gameObject.SetActive(false);//取消显示
        
	}
//内部封装模块
	//UI_Button控制
	
	void setToViewPoint(Vector2 viewPoint, GameObject objbase, GameObject objset, bool model)
//根据base中view点 设置set RectTrans位置 model true:由点击触发考虑baseView偏移值(进行ViewPoint转入) false:不考虑baseView偏移值(硬性设置）
	{
		//获取RectTransform
		RectTransform objset_rt = objset.GetComponent<RectTransform>();
		//获取Img Anchor占比距离 Panel Anchor占比距离
		float objset_x = objset_rt.anchorMax.x - objset_rt.anchorMin.x;
		float objset_y = objset_rt.anchorMax.y - objset_rt.anchorMin.y;
		Vector2 topanel_Point;//最终需要的坐标位置
		if (model)
		{
			topanel_Point = changeInBase(viewPoint, objbase);
		}
		else
		{
			topanel_Point = viewPoint;
		}
		objset_rt.anchorMin = new Vector2(topanel_Point.x - objset_x / 2, topanel_Point.y - objset_y / 2);
		objset_rt.anchorMax = new Vector2(topanel_Point.x + objset_x / 2, topanel_Point.y + objset_y / 2);
	}
	Vector2 changeInBase(Vector2 viewPoint, GameObject objbase)//将高一级的ViewPoint 转入低一级的ViewPoint 
	{
		//获取RectTransform
		RectTransform objbase_rt = objbase.GetComponent<RectTransform>();

		float objbase_x = objbase_rt.anchorMax.x - objbase_rt.anchorMin.x;
		float objbase_y = objbase_rt.anchorMax.y - objbase_rt.anchorMin.y;
		//获取ViewPoint转换至objbase中的对应的View点

		return new Vector2((viewPoint.x - objbase_rt.anchorMin.x) / objbase_x, (viewPoint.y - objbase_rt.anchorMin.y) / objbase_y);

	}
	//Base
	// Use this for initialization
	void Start () {

        if (isLock)//【锁定式】一开始就需要被设置在中心
		{
			setToViewPoint(new Vector2(0.5f, 0.5f), button_Img, button_Point, false);
		}
		else//【非锁定式】一开始保证不现实【手柄圆】
		{
			button_Img.SetActive(false);
		}
	}

    public void OnJumpButtonClick()
    {
        PlayerController.GetInstance().JumpUp = true;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
