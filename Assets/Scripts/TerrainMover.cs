using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Vincent McClintock
//Date: 10-18-2017

public class TerrainMover : MonoBehaviour
{

    private void Start()
    {

		// If this is a clone of the prefab, set it to move at the terrain speed. 
		if (gameObject.tag == "Untagged") {
			GetComponent<Rigidbody> ().velocity = -transform.forward * (ArrowMover.getSpeed () - 20f);
		}
    }

    public void Update()
    {
    	if (GameController.gameOver)
    		GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

}