using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongController : MonoBehaviour
{
    private Rigidbody2D rigid;
    public float speed = 1f;
    private bool canMoveUp = true;
    private bool canMoveDown = true;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(!CanMoveInDirection(Mathf.Sign(rigid.velocity.y))) //safety check
        {
            StopPong();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.collider.tag)
        {
            case "Wall":
                HandleEnterWallCollision(collision);
                break;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        switch (collision.collider.tag)
        {
            case "Wall":
                HandleExitWallCollision(collision);
                break;
        }
    }

    public void MovePongToPosition(Vector2 targetPosition, float targetDirection)
    {
        if (CanMoveInDirection(targetDirection))
        {
            rigid.MovePosition(targetPosition);
        }
    }

    public void MovePong(float movementSpeed)
    {
        if (!CanMoveInDirection(Mathf.Sign(movementSpeed)))
        {
            movementSpeed = 0;
        }

        rigid.velocity = new Vector2(0, movementSpeed * speed);
    }

    private void StopPong()
    {
        MovePong(0);
    }

    private void HandleEnterWallCollision(Collision2D collision)
    {
        if (collision.transform.position.y > transform.position.y) //wall is above player
        {
            canMoveUp = false;
        }
        else if (collision.transform.position.y < transform.position.y) //wall is below player
        {
            canMoveDown = false;
        }
    }

    private void HandleExitWallCollision(Collision2D collision)
    {
        canMoveUp = true;
        canMoveDown = true;
    }

    public bool CanMoveUp()
    {
        return canMoveUp;
    }

    public bool CanMoveDown()
    {
        return canMoveDown;
    }

    public bool CanMoveInDirection(float direction)
    {
        return (direction >= 1 && canMoveUp) || (direction <= -1 && canMoveDown);
    }
}
