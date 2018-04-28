using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameramotor : MonoBehaviour {

    public Transform lookat;
    private Vector3 desireposition;

    private Vector2 touchPosition;
    private float swipeResistanc=200.0f;

    private Vector3 offset;
    private float distance = 5.0f;
    private float yOffset = 3.5f;
    private float smoothspeed = 5f;
	// Use this for initialization
	void Start () {
        offset = new Vector3(0, yOffset, -1f * distance);
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SlideCamera(true);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SlideCamera(false);
        }
       
        if(Input.GetMouseButtonDown(0)||Input.GetMouseButtonDown(1))
        {
            touchPosition = Input.mousePosition;

        }

        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        {
            float swipeForce = touchPosition.x - Input.mousePosition.x;
            if(Mathf.Abs(swipeForce)>swipeResistanc)
            {
                if (swipeForce < 0) SlideCamera(true);
                else SlideCamera(false);
            }
        }

    }
    private void FixedUpdate()
    {
        //因為RIDBODY更新為60FRAMRATE 
        desireposition = lookat.position + offset;
        transform.position = Vector3.Lerp(transform.position, desireposition, smoothspeed * Time.deltaTime);
        transform.LookAt(lookat.position + Vector3.up);

    }
    public void SlideCamera(bool left)
    {
        if (left) offset = Quaternion.Euler(0, 90, 0) * offset;
        else offset = Quaternion.Euler(0, -90, 0) * offset;
    }
}
