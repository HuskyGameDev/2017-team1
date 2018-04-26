using UnityEngine;
using System.Collections;

public class Spin : MonoBehaviour
{
    public float degreesPerSecondx = 10f;
	public float degreesPerSecondy = 10f;
	public float degreesPerSecondz = 10f;
    
    
    void Update ()
    {
		if (!(GameController.gameOver)) {
			transform.Rotate (new Vector3 (Time.deltaTime * degreesPerSecondx, Time.deltaTime * degreesPerSecondy, Time.deltaTime * degreesPerSecondz), Space.World);
		}
    }
}