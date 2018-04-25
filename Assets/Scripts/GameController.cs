using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

//Author: Vincent McClintock
//Date: 10-18-2017

public class GameController : MonoBehaviour
{

    public GameObject obstacle;

    public GameObject restartButton;
    public GameObject mainMenuButton;

	public GameObject controls;

//    public int obstacleCount;

    public float spawnWaitMin;
	public float spawnWaitMax;
	public float spawnWait;

    public float startWait;
//    public float waveWait;

    public float tSpawnWaitMin;
    public float tSpawnWaitMax;
    public float tSpawnWait;

	public float pSpawnWaitMin;
	public float pSpawnWaitMax;
	public float pSpawnWait;

    public Text gameOverText;
	public Text scoreText;
	public Text HSText;
    public Text healthText;

	public long score;
	public long highScore = 0;
	public float timer;

	public int diffUpScore;

    public static bool gameOver;
    private bool restart;

    private string s = "_Scenes/mainmenu1";

	private bool scoreCheck;

    private GameObject terrain;
    private GameObject logBlock;

	private Vector3 arrowSpawnValues = new Vector3(3, 1.5f, 300);
//	private Vector3 powUpSpawnValues = new Vector3(3, 0.0f, 315);
    private Vector3 treeSpawnValues = new Vector3(8, -.75f, 250);
    private Vector3 rockSpawnValues = new Vector3(8, .60f, 250);
    private Vector3 logSpawnValues = new Vector3(0, .67f, 250);

    public int diffCount;
    public int difficulty;

    public int playerHealthmax= 3;
    public int playerHealth;

    private void Start()
    {
		Load ();
		UpdateHighScore();
        gameOver = false;
        restart = false;
		controls.SetActive (false);
        playerHealth = playerHealthmax;

        gameOverText.text = "";

        Button rbtn = restartButton.GetComponent<Button>();
        rbtn.onClick.AddListener(Restart);

        Button mbtn = mainMenuButton.GetComponent<Button>();
        mbtn.onClick.AddListener(MainMenu);

        StartCoroutine(SpawnArrows());
		StartCoroutine(SpawnTerrain());
        StartCoroutine(Spawn2LaneLog());
		StartCoroutine (SpawnPowerup ());

        score = 0;
        diffCount = 1;
        difficulty = 0;
        UpdateScore();
        UpdateHealth();
    }

    private void Update()
    {
		//updates the score when not currently checking the score
		if (scoreCheck == false && gameOver == false)
		{
			StartCoroutine (ScoreDelay ());
			UpdateScore ();
		}
			
        if (diffCount % diffUpScore == 0 && !(spawnWaitMax <= spawnWaitMin))
        {
            diffCount = 1;
            difficulty++;
            if (score % 2 == 1) //Keeps score even after it starts changing difficulty. 
                score++;

			spawnWaitMax -= 0.05f;
            ArrowMover.setSpeed(ArrowMover.getSpeed() + 20.0f);
        }


        //allows the player to restart by pressing the 'R' key after game over
        if (restart)
        {
			restartButton.SetActive(true);
			mainMenuButton.SetActive(true);
			controls.SetActive(true);
        }


    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		ArrowMover.setSpeed (50f);
		Debug.Log ("Speed: " + ArrowMover.getSpeed());
    }


    public void MainMenu()
    {
    	SceneManager.LoadScene(s, LoadSceneMode.Single);
    }

    //called when the player is destroyed, ending the game
    public void GameOver()
    {
        gameOverText.text = "Game Over";
		if (score > highScore) {
			highScore = score;
			UpdateHighScore ();
		}
		Save ();	
        gameOver = true;

    }

	//called to save the highscore value between sessions
	public void Save()
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Open (Application.persistentDataPath + "/highScore.dat", FileMode.OpenOrCreate);

		PlayerData data = new PlayerData ();
		data.highScore = highScore;

		bf.Serialize (file, data);
		file.Close ();
	}

	//called to load the highscore value on new session
	public void Load()
	{
		if (File.Exists (Application.persistentDataPath + "/highScore.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/highScore.dat", FileMode.Open);
			PlayerData data = (PlayerData) bf.Deserialize (file);
			file.Close ();

			highScore = data.highScore;
		}

	}
		
    //called when the player is hit by an obstacle
    public int PlayerHit()
    {
        playerHealth--;
        UpdateHealth();
        //Debug.Log("Health =" + playerHealth);
        if (playerHealth <= 0)
            GameOver(); //------------------------------------------------


        return playerHealth;
    }

	//called when the player gains health from a powerup
	public int HealtPowUp()
	{
        if(playerHealth < 5)
        {
            playerHealth++;
            UpdateHealth();
            return playerHealth;
        }

        return playerHealth;
		
	}

	//called when the player grabs an hourglass powerup
	public void SlowTime()
	{
		Time.timeScale = 0.5f;
		float timeLeft = 5;
		while (timeLeft < 0) {
			timeLeft -= Time.deltaTime;
		}
		Time.timeScale = 1.0f;
		return;
	}

    //updates the score over time
    IEnumerator ScoreDelay()
	{
        scoreCheck = true;
        diffCount++;
        score = score + (int)Mathf.Pow(2, difficulty);
        yield return new WaitForSeconds(timer);
        scoreCheck = false;
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
                float xrand = UnityEngine.Random.Range(0, 6);
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
                Vector3 spawnPosition = new Vector3(xVal * arrowSpawnValues.x, arrowSpawnValues.y, arrowSpawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                spawnRotation[0] = 0;
                Instantiate(obstacle, spawnPosition, spawnRotation);


				//Allows arrows to spawn randomly within a time interval
				spawnWait = UnityEngine.Random.Range(spawnWaitMin, spawnWaitMax);
				yield return new WaitForSeconds(spawnWait);

//            }
            //wait after each for loop to allow time between waves of arrows. 
            //yield return new WaitForSeconds(waveWait);

            //breaks out of the wave spawning loop when gameOver is true
            if (gameOver)
            {
                restart = true;
                break;
            }
        }
    }

	IEnumerator SpawnTerrain()
	{
		Vector3 spawnPosition;

		while (true)
		{
			float xrand = UnityEngine.Random.Range(0, 3);
			int xVal;
			if (xrand < 2) {
				xVal = -1;
			}
			else {
				xVal = 1;
			}

			Quaternion spawnRotation = Quaternion.identity;
			spawnRotation[0] = -1;

			string tText = "";

			float trand = UnityEngine.Random.Range(0, 4);
			if (trand < 2) {
				tText = "Terrain";
				spawnPosition = new Vector3(xVal * treeSpawnValues.x, treeSpawnValues.y, treeSpawnValues.z);
			} else {
				tText = "Terrain1";
				spawnPosition = new Vector3(xVal * rockSpawnValues.x, rockSpawnValues.y, rockSpawnValues.z);
			}

			terrain = GameObject.FindWithTag(tText);

			Instantiate(terrain, spawnPosition, spawnRotation);

			tSpawnWait = UnityEngine.Random.Range(tSpawnWaitMin, tSpawnWaitMax);
			yield return new WaitForSeconds(tSpawnWait);


			if (gameOver)
            {
                break;
            }
		}
	}

    IEnumerator Spawn2LaneLog()
    {
        Vector3 spawnPosition;

        while (true)
        {
            float xrand = UnityEngine.Random.Range(0, 4);
            Debug.Log("xrand is " + xrand);
            float xValue;
            if (xrand < 2)
            {
                xValue = -.3f; //Right and Center Lane
            }
            else
            {
                xValue = -3.8f; //Left and Center Lane
            }

            Quaternion spawnRotation = Quaternion.identity;
            spawnRotation[1] = 1;
            spawnRotation[0] = 0;
            //spawnRotation [2] = 1;

            string tText = "Log";
            spawnPosition = new Vector3(xValue, logSpawnValues.y, logSpawnValues.z);

            //float trand = Random.Range(0, 4);
            int trand = 0;
            if (trand < 2)
            {
                //tText = "";
                //spawnPosition = new Vector3(xVal, logSpawnValues.y, logSpawnValues.z);
            }
            else
            {
                // Location for more walls and other stuff
                //tText = "Terrain1";
                //spawnPosition = new Vector3(xVal * rockSpawnValues.x, rockSpawnValues.y, rockSpawnValues.z);
            }

            logBlock = GameObject.FindWithTag(tText);

            Instantiate(logBlock, spawnPosition, spawnRotation);

            tSpawnWait = UnityEngine.Random.Range(spawnWaitMin, spawnWaitMax);
            yield return new WaitForSeconds(tSpawnWait * 4);


            if (gameOver)
            {
                break;
            }
        }
    }


	IEnumerator SpawnPowerup()
	{
		yield return new WaitForSeconds(startWait);


		while (true)
		{
			float xrand = UnityEngine.Random.Range(0, 6);
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

			GameObject[] Powerups;
			GameObject toSpawn;

			Powerups = GameObject.FindGameObjectsWithTag ("PowerUp");
			toSpawn = Powerups [UnityEngine.Random.Range (0, Powerups.Length)];

			Vector3 defaultPosition = toSpawn.transform.position;

			Vector3 spawnPosition = new Vector3(xVal * defaultPosition.x, defaultPosition.y, defaultPosition.z + 365);
			Quaternion spawnRotation = Quaternion.identity;
			GameObject spawned = Instantiate(toSpawn, spawnPosition, spawnRotation);
			spawned.tag = "Untagged";


			pSpawnWait = UnityEngine.Random.Range(pSpawnWaitMin, pSpawnWaitMax);
			yield return new WaitForSeconds(pSpawnWait);

			//breaks out of the spawning loop when gameOver is true
			if (gameOver)
			{
				break;
			}
		}
	}

	void UpdateHighScore()
	{
		HSText.text = "High Score: " + highScore;
		return;
	}

    void UpdateScore ()
	{
		scoreText.text = "Score: " + score;
		return;
	}

    void UpdateHealth()
    {
        healthText.text = "Health: " + playerHealth;
        return;
    }
			
}

//class used in storing persistant player data
[Serializable]
class PlayerData
{
	public long highScore;

}
