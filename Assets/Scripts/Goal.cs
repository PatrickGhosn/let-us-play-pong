using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

    public GameLogic gameLogic;
    public BallController ballController;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.collider.tag)
        {
            case "Ball":
                ballController.ResetBall();
                gameLogic.HandleGoal(tag);
                break;
        }
    }    
}
