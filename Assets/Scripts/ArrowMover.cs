using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Vincent McClintock
//Date: 10-18-2017

public class ArrowMover : MonoBehaviour
{

	public static float speed = 50f;

    private void Start()
    {

        //moves the arrow towards the player at a desired speed
		GetComponent<Rigidbody>().velocity = transform.up * speed;
    }

	public void Update()
	{
		if (GameController.gameOver)
			GetComponent<Rigidbody>().velocity = Vector3.zero;
	}

	public static float getSpeed(){
		return speed;
	}

	public static void setSpeed(float newSpeed){
		speed = newSpeed;
	}

}