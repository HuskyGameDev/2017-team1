using UnityEngine;
using System.Collections;

public class QuitButton
{
	public void QuitApp()
	{
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
