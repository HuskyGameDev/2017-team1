using System;
using System.Collections;

	public class Spawn : Monobehavior
	{
	public Transform spawnLocation;
	public GameObject spawnPrefab;
	public GameObject whatToSpawnClone;

    void Start()
	{
	   spawnObject();
	}

	void spawnObject()
	{
		whatToSpawnClone = Instantiate (spawnPrefab, spawnLocation, transform.position, Quaternion.Euler (0, 0, 0)) as GameObject;
	}
}

