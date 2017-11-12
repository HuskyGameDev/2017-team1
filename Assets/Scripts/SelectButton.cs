using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class SelectButton : MonoBehaviour
{
    private bool isSelected;
	public EventSystem eSystem;
    public GameObject chosenObj;

    //initializes
    void Start()
    { }

    //sets the game object to a selected state and sets the boolean flag to true as the button was just selected
    void SwitchON()
    {
        if(Input.GetAxisRaw("Vertical") != 0 && isSelected == false)
        {
            eSystem.SetSelectedGameObject(chosenObj);
            isSelected = true;
        }
    }

    //sets the boolean flag to false now that the button is deselected
    private void Deselect()
    {
        isSelected = false;
    }
    
}
