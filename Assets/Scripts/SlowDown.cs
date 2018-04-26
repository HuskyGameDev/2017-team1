using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDown : MonoBehaviour
{
  public static float speed = 50f;
  private void Start()
  {
      float timeStart = Time.time;
      ArrowMover.setSpeed(0.5*speed);
      LogMover.setSpeed(0.5*speed);
      TerrainMover.setSpeed(0.5*speed);
      if(Time.time-timeStart > 10)
      resetSpeed();
   }
	

   public static void resetSpeed()
   {
      ArrowMover.setSpeed(speed);
      LogMover.setSpeed(speed);
      TerrainMover.setSpeed(speed);
   }
}
