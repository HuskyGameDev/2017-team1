using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SelectScript : MonoBehaviour
{
    public EventSystem eSystem;
    public GameObject objectSelected;

    private string s = "_Scenes/Dev_Vinc";

    private bool isSelected;

    public Button b;

    void Start()
    {
        Button btn = b.GetComponent<Button>();
        btn.onClick.AddListener(LoadGame);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(s, LoadSceneMode.Single);
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetAxisRaw("Vertical") != 0 && isSelected == false)
        {
           eSystem.SetSelectedGameObject(objectSelected);
            isSelected = true;
        }
	}

    private void OnRelease()
    {
        isSelected = false;
    }
}
