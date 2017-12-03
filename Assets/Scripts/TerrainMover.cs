using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Vincent McClintock
//Date: 10-18-2017

public class TerrainMover : MonoBehaviour
{

    private void Start()
    {

        //moves the arrow towards the player at a desired speed
        GetComponent<Rigidbody>().velocity = transform.up * (Mover.getSpeed() - 20f) ;
    } 

}