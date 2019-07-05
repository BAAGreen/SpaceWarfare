using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalMove : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private MobileControllers mControllers;
    private Vector2 moveForce;

    public float moveSpeed = 2;

	void Start ()
    {
        Initialization();
	}
	
	void FixedUpdate ()
    {
        ForceCalculate();
        Moving();
	}

    private void Initialization()
    {
        rb2d = GetComponent<Rigidbody2D>();
        mControllers = GameObject.FindGameObjectWithTag("Joystick").GetComponent<MobileControllers>();
    }

    private void Moving()
    {
        rb2d.AddForce(moveForce);
    }

    private void ForceCalculate()
    {
        moveForce.x = mControllers.Horizontal() * moveSpeed;
        moveForce.y = mControllers.Vertical() * moveSpeed;
    }
}
