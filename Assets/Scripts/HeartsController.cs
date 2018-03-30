using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartsController : MonoBehaviour
{
    public Sprite[] HeartSprites;

    public Image HeartUI;

    private GameController gameController;
    

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    void Update()
    {
        HeartUI.sprite = HeartSprites[gameController.playerHealth];
    }

}
