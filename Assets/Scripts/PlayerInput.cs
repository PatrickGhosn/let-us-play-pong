using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PongController))]
public class PlayerInput : MonoBehaviour
{
    private PongController pongController;
    public GameLogic gameLogic;
    
    private void Awake()
    {
        pongController = GetComponent<PongController>();
    }

    void Start (){}

	void Update ()
    {
        pongController.MovePong(Input.GetAxis("Vertical"));
        if(gameLogic.IsPlayerTurn() && Input.GetKeyDown(KeyCode.Space))
        {
            gameLogic.StartTurn();
        }
        else if(Input.GetKeyDown(KeyCode.Escape)) {
            gameLogic.PauseGame();
        }
    }    
}
