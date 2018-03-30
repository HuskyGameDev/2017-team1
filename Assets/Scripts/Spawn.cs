using System;
using System.Collections;
using UnityEngine;

public class Spawn : MonoBehaviour
{
     public Transform[] spawnPlaces;
	 public GameObject[] spawnObject;
	 public GameObject[] spawnType;
	 
	 void Start()
	 {
        spawn();
	 }
	 
	 void spawn()
	 {
		 spawnType[0] = Instantiate(spawnObject[0], spawnPlaces[0].transform.position, Quaternion.Euler(0,0,0)) as GameObject;
		 spawnType[1] = Instantiate(spawnObject[1], spawnPlaces[1].transform.position, Quaternion.Euler(0,0,0)) as GameObject;
		 spawnType[2] = Instantiate(spawnObject[2], spawnPlaces[2].transform.position, Quaternion.Euler(0,0,0)) as GameObject;
	 }
}

