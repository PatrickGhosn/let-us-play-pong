using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameLogic : MonoBehaviour
{
    public BallController ballController;
    public Text playerScoreText;
    public Text aiScoreText;
    public GameObject pauseMenu;
    public GameObject endMenu;
    public Text endTitle;
    public Text endText;

    public int winningScore = 10;    
    private int playerScoreCount = 0;
    private int aiScoreCount = 0;
    private bool isPlayerTurn = true;
    private bool turnStarted = false;
    private bool gamePaused = false;

    public void Awake()
    {
        Time.timeScale = 1f;
    }
    public bool IsPlayerTurn()
    {
        return isPlayerTurn && !turnStarted;
    }

    public void StartTurn()
    {
        turnStarted = true;
        ballController.LaunchBall(isPlayerTurn);
    }

    public void HandleGoal(string goalTag)
    { 
        switch (goalTag)
        {
            case "PlayerGoal":
                HandleAIScoring();
                break;
            case "AIGoal":
                HandlePlayerScoring();
                break;
        }

        CheckWinCondition();
    }

    private void HandleAIScoring()
    {
        aiScoreCount++;
        aiScoreText.text = aiScoreCount.ToString();
        isPlayerTurn = true;
        turnStarted = false;
    }

    private void HandlePlayerScoring()
    {
        playerScoreCount++;
        playerScoreText.text = playerScoreCount.ToString();
        isPlayerTurn = false;
        ballController.LaunchBall(isPlayerTurn);
    }

    private void CheckWinCondition()
    {
        if(playerScoreCount >= winningScore)
        {
            endTitle.text = "Congratulations!";
            endText.text = "You won the game with a score of " + playerScoreCount + " to " + aiScoreCount + "!";
            EndGame();
        }
        else if(aiScoreCount >= winningScore)
        {
            endTitle.text = "Game over!";
            endText.text = "You lost the game with a score of " + playerScoreCount + " to " + aiScoreCount + "!";
            EndGame();
        }        
    }

    private void EndGame()
    {
        Time.timeScale = 0.0f;
        endMenu.SetActive(true);
    }

    public void PauseGame()
    {        
        Time.timeScale = 0.0f;
        gamePaused = true;
        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        gamePaused = false;
        pauseMenu.SetActive(false);
    }

    public bool IsGamePaused()
    {
        return gamePaused;
    }
}
