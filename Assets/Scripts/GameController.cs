using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Author: Vincent McClintock
//Date: 10-18-2017

public class GameController : MonoBehaviour
{

    public GameObject obstacle;
    public Vector3 spawnValues;
//    public int obstacleCount;

    public float spawnWaitMin;
	public float spawnWaitMax;
	public float spawnWait;

    public float startWait;
//    public float waveWait;

    public Text gameOverText;
    public Text restartText;
	public Text scoreText;

	public int score;
	public float timer;

	public int diffUpScore;

    private bool gameOver;
    private bool restart;

	private bool scoreCheck;
	private bool diffCheck;
     
    private void Start()
    {
        gameOver = false;
        restart = false;


        gameOverText.text = "";
        restartText.text = ""; 

        StartCoroutine(SpawnArrows());

		score = 0;
		UpdateScore ();
    }

    private void Update()
    {
		//updates the score when not currently checking the score
		if (scoreCheck == false && gameOver == false)
		{
			StartCoroutine (ScoreDelay ());
			UpdateScore ();
		}

		//Difficulty value shortens the time interval every time the score threshold is passed
		if ((score % diffUpScore) == 0 && diffCheck == false && !(spawnWaitMax <= spawnWaitMin) ) {
			diffCheck = true;
			spawnWaitMax -= 0.05f;
//			Mover.Instance.setSpeed (Mover.Instance.getSpeed() + 5.0f);
			Mover.setSpeed(Mover.getSpeed() + 20.0f);
			Debug.Log ("Speed: " + Mover.getSpeed());
		}

        //allows the player to restart by pressing the 'R' key after game over
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
				Mover.setSpeed (50f);
				Debug.Log ("Speed: " + Mover.getSpeed());
            }
        }


    }



    //called when the player is destroyed, ending the game
    public void GameOver()
    {
        gameOverText.text = "Game Over";
        gameOver = true;

    }

	//updates the score over time
	IEnumerator ScoreDelay()
	{
		scoreCheck = true;
		score++;
		yield return new WaitForSeconds (timer);
		scoreCheck = false;
		diffCheck = false;
	}



    IEnumerator SpawnArrows()
    {
        yield return new WaitForSeconds(startWait);

        //while loop runs the for loop, constantly spawning waves of arrows
        while (true)
        {
            //each for loop spawns a wave of a set number of arrows
//            for (int i = 0; i < obstacleCount; i++)
//            {
                //Random variable is used in an if, elseif statement in order to have
                //arrows spawn in one of three lanes; left, middle, and right.
                float xrand = Random.Range(0, 6);
                int xVal;
                if (xrand < 2)
                {
                    xVal = -1;
                }
                else if (xrand < 4)
                {
                    xVal = 0;
                }
                else
                {
                    xVal = 1;
                }


                //The spawn position is set as desired. The spawn rotation ensures 
                //the arrows spawn facing the player.
                Vector3 spawnPosition = new Vector3(xVal * spawnValues.x, spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                spawnRotation[0] = -1;
                Instantiate(obstacle, spawnPosition, spawnRotation);


				//Allows arrows to spawn randomly within a time interval
				spawnWait = Random.Range(spawnWaitMin, spawnWaitMax);
				yield return new WaitForSeconds(spawnWait);

//            }
            //wait after each for loop to allow time between waves of arrows. 
            //yield return new WaitForSeconds(waveWait);

            //breaks out of the wave spawning loop when gameOver is true
            if (gameOver)
            {
                restartText.text = "Press 'R' to Restart";
                restart = true;
                break;
            }
        }
    }

	void UpdateScore ()
	{
		scoreText.text = "Score: " + score;
		return;
	}
			
}