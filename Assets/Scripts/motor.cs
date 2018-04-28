using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class motor : MonoBehaviour {

    public float movespeed = 3.0f;
    public float drag = 0.5f;
    public float rotationspeed = 25.0f;
    private Transform camtransform;
    private Rigidbody controller;
    public VirtualJoystick moveJoystick;

    public float bootSpeed = 5.0f;
    public float boostCooldown = 2.0f;
    private float lastboost;
	// Use this for initialization
	void Start () {
        lastboost = Time.time - boostCooldown;

        controller = GetComponent<Rigidbody>();
        controller.maxAngularVelocity = rotationspeed;
        controller.drag = drag;
        camtransform = Camera.main.transform;
	}
	
	// Update is called once per frame
	private void Update () {
        //用鍵盤控制
        Vector3 dir = Vector3.zero;
        dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");

        if (dir.magnitude > 1)
            dir.Normalize();
        //用joystick控制
        if(moveJoystick.InputDirection!=Vector3.zero)
        {
            dir = moveJoystick.InputDirection;
        }


        //旋轉方向根據相機
        Vector3 rotatedDir = camtransform.TransformDirection(dir);
        rotatedDir = new Vector3(rotatedDir.x, 0, rotatedDir.z);
        rotatedDir = rotatedDir.normalized * dir.magnitude;

        controller.AddForce(rotatedDir * movespeed);
       
	}

    public void Boost()
    {
        if(Time.time -lastboost >boostCooldown)
        {
            controller.AddForce(controller.velocity.normalized * bootSpeed, ForceMode.VelocityChange);
            lastboost = Time.time;
        }
        
    }
}
