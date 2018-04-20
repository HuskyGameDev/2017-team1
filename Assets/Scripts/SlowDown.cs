using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownSurround : MonoBehaviour
{
	public static float speed = 50f;
  private void Start()
  {
	    float timeStart = time;
      ArrowMover.setSpeed(0.5*speed);
      LogMover.setSpeed(0.5*speed);
      TerrainMover.setSpeed(0.5*speed);
      if(time-timeStart > 10)
	      resetSpeed();
   }
	
	public static void resetSpeed()
	{
      ArrowMover.setSpeed(speed);
      LogMover.setSpeed(speed);
      TerrainMover.setSpeed(speed);
	}
}
