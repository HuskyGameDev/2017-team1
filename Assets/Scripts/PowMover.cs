using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Vincent McClintock
//Date: 10-18-2017

public class PowMover : MonoBehaviour
{

	public static float speed = 50f;
	private void Start()
	{
		// If this is a clone of the powerup prefab, set it to move at the terrain speed. 
		if (gameObject.tag == "Untagged") {
			GetComponent<Rigidbody> ().velocity = -transform.forward * (ArrowMover.getSpeed () - 20f);
		}
	}

	public static float getSpeed(){
		return speed;
	}

	public static void setSpeed(float newSpeed){
		speed = newSpeed;
	}

}