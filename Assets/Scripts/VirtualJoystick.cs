using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class VirtualJoystick : MonoBehaviour,IDragHandler,IPointerUpHandler,IPointerDownHandler {

    private Image bgImg;
    private Image Joystick;

    public Vector3 InputDirection { set; get; }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos = Vector2.zero;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImg.rectTransform, eventData.position, eventData.pressEventCamera, out pos))
        {
            pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
            pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);

          

            float x = (bgImg.rectTransform.pivot.x == 1) ? pos.x * 2 : pos.x * 2 ;
            float y = (bgImg.rectTransform.pivot.y == 1) ? pos.y * 2 : pos.y * 2 ;
            ;
            InputDirection = new Vector3(x, 0, y);
            //不能超出範圍
            InputDirection = (InputDirection.magnitude > 1) ? InputDirection.normalized : InputDirection;

            ///move image
            ///
            Joystick.rectTransform.anchoredPosition = new Vector3(InputDirection.x * (bgImg.rectTransform.sizeDelta.x / 3), InputDirection.z * (bgImg.rectTransform.sizeDelta.y / 3));

        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        InputDirection = Vector3.zero;
        Joystick.rectTransform.anchoredPosition = Vector3.zero;
    }



    // Use this for initialization
    void Start () {
        bgImg = GetComponent<Image>();
        Joystick = transform.GetChild(0).GetComponent<Image>();
        InputDirection = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
