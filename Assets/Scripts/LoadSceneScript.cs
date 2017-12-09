using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneScript : MonoBehaviour
{
	public void LoadIndex (int index)
    {
        SceneManager.LoadScene(index);
	}
}
