using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class QuitScript : MonoBehaviour
{

	public Button b;

	void Start()
	{
		Button btn = b.GetComponent<Button>();
		btn.onClick.AddListener(Quit);
	}

	public void Quit ()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
