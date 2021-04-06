using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PongController))]
public class AI : MonoBehaviour
{
    private PongController pongController;
    private BoxCollider2D col;
    
    private bool goingToCatchBall;
    private float targetDirection;

    public float speed = 7f;
    public Transform ball;

    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        pongController = GetComponent<PongController>();
    }
    
    void Update()
    {
        CatchBall();
    }

    private void CatchBall()
    {
        targetDirection = Mathf.Sign(ball.position.y - transform.position.y);
        goingToCatchBall = (transform.position.y + col.offset.y + col.size.y / 2 > ball.position.y) && (transform.position.y + col.offset.y - col.size.y / 2 < ball.position.y);

        if (!goingToCatchBall)
        {
            pongController.MovePongToPosition(new Vector2(transform.position.x, transform.position.y + targetDirection * speed * Time.deltaTime), targetDirection);
        }
    }
    
}
