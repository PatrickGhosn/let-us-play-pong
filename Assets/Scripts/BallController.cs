using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody2D rigid;
    public float speed = 3f;
    public float maxSpeed = 10f;
    public float minSpeed = 3f;
    public float speedIncrement = 0.5f;
    public float turnRate = 2f;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.collider.tag)
        {
            case "Wall":
                HandleEnterWallCollision(collision);
                break;
            case "Pong":
                HandleEnterPongCollision(collision);
                break;
        }
    }
    
    public void ResetBall()
    {        
        StopBall();
        ReturnToStartingPosition();
    }

    public void ReturnToStartingPosition()
    {
        transform.position = Vector3.zero;
    }

    public void StopBall()
    {
        ChangeBallSpeed(0.0f, 0.0f, false);
    }
    
    public void LaunchBall(bool isPlayerTurn)
    {
        ChangeBallSpeed((isPlayerTurn ? 1 : -1) * minSpeed, 0.0f, false);
    }

    public void ChangeBallSpeed(float horizontalSpeed, float verticalSpeed)
    {
        ChangeBallSpeed(horizontalSpeed, verticalSpeed, true);
    }

    public void ChangeBallSpeed(float horizontalSpeed, float verticalSpeed, bool rectify)
    {
        Vector2 newBallSpeed = new Vector2(horizontalSpeed, verticalSpeed);

        if ( rectify && ( (Mathf.Abs(horizontalSpeed) < minSpeed && Mathf.Abs(verticalSpeed) < minSpeed)
                        ||(Mathf.Abs(horizontalSpeed) > maxSpeed || Mathf.Abs(verticalSpeed) > maxSpeed) )
           )
        {
            newBallSpeed = RectifyBallSpeed(horizontalSpeed, verticalSpeed);
        }

        rigid.velocity = newBallSpeed;
    }

    private Vector2 RectifyBallSpeed (float horizontalSpeed, float verticalSpeed)
    {
        return RectifyBallSpeed(new Vector2(horizontalSpeed, verticalSpeed));
    }

    private Vector2 RectifyBallSpeed(Vector2 newBallSpeed)
    {
        float absHorizontalSpeed = Mathf.Abs(newBallSpeed.x);
        float absVerticalSpeed = Mathf.Abs(newBallSpeed.y);
        //Make sure to speed up only the slowest speed between horizontal and vertical so that the ball doesn't change trajectory.
        if (absHorizontalSpeed > absVerticalSpeed && absHorizontalSpeed < minSpeed)
        {
            newBallSpeed.x = minSpeed * GetHorizontalDirection();
        }
        else if (absHorizontalSpeed < absVerticalSpeed && absVerticalSpeed < minSpeed)
        {
            newBallSpeed.y = minSpeed * GetVerticalDirection();
        }

        if (absHorizontalSpeed > absVerticalSpeed && absHorizontalSpeed > maxSpeed)
        {
            newBallSpeed.x = maxSpeed * GetHorizontalDirection();
        }
        else if (absHorizontalSpeed < absVerticalSpeed && absVerticalSpeed > maxSpeed)
        {
            newBallSpeed.y = maxSpeed * GetVerticalDirection();
        }

        return newBallSpeed;
    }

    private void HandleEnterWallCollision(Collision2D collision)
    {
        BounceBallAgainstWall();
    }

    private void HandleEnterPongCollision(Collision2D collision)
    {
        BounceBallAgainstPong(collision);
    }

    private void BounceBallAgainstWall()
    {
        InverseVerticalMovement();        
    }

    private void BounceBallAgainstPong(Collision2D collision)
    {
        InverseHorizontalMovement(-collision.transform.position.y + transform.position.y);
        SpeedUpBall(true, false);
    }
    
    public void InverseAllMovement()
    {
        ChangeBallSpeed(-rigid.velocity.x, -rigid.velocity.y);
    }

    public void InverseVerticalMovement()
    {
        ChangeBallSpeed(rigid.velocity.x, -rigid.velocity.y);
    }

    public void InverseHorizontalMovement()
    {
        InverseHorizontalMovement(0);
    }

    public void InverseHorizontalMovement(float angle)
    {
        ChangeBallSpeed(-rigid.velocity.x, GetVerticalDirection() * Mathf.Abs(rigid.velocity.y) + angle * turnRate);
    }

    public float GetHorizontalDirection()
    {
        return Mathf.Sign(rigid.velocity.x);
    }

    public float GetVerticalDirection()
    {
        return Mathf.Sign(rigid.velocity.y);
    }

    public void SlowDownBall(bool slowDownHorizontally, bool slowDownVeritcally)
    {
        ChangeBallSpeedByIncrement(-speedIncrement, slowDownHorizontally, slowDownVeritcally);
    }
    
    public void SpeedUpBall(bool speedUpHorizontally, bool speedUpVertically)
    {
        ChangeBallSpeedByIncrement(speedIncrement, speedUpHorizontally, speedUpVertically);
    }

    public void ChangeBallSpeedByIncrement(float increment, bool speedUpHorizontally, bool speedUpVertically)
    {        
        float horizontalSpeed = (speedUpHorizontally) ? (Mathf.Abs(rigid.velocity.x) + increment) * GetHorizontalDirection() : rigid.velocity.x;
        float verticalSpeed = (speedUpVertically) ? (Mathf.Abs(rigid.velocity.y) + increment) * GetVerticalDirection() : rigid.velocity.y;

        ChangeBallSpeed(horizontalSpeed, verticalSpeed);
    }    
}