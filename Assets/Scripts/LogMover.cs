using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogMover : MonoBehaviour {

	private void Start()
	{

		//moves the arrow towards the player at a desired speed
		GetComponent<Rigidbody>().velocity = transform.right * (ArrowMover.getSpeed() - 20f) ;
	}

	public void Update()
	{
		if (GameController.gameOver)
			GetComponent<Rigidbody>().velocity = Vector3.zero;
	}
}
