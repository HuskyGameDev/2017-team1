using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    //loads the scene by the index value associated with the scene
	public void LoadIndex(int index)
	{
        SceneManager.LoadScene(index);
	}
}
