using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class playercontroller : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;

    public kickattack Kickattack;

    Vector2 movementInput;

    SpriteRenderer spriteRenderer;

    Rigidbody2D rb;

    Animator animator;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void FixedUpdate()
    {
        
        if(movementInput != Vector2.zero)
        {
           bool success = TryMove(movementInput);

            if (!success)
            {
                success = TryMove(new Vector2(movementInput.x, 0));

                if(!success)
                {
                    success = TryMove(new Vector2(0, movementInput.y));
                }
            }

            animator.SetBool("isMoving", success);

        } else
        {
            animator.SetBool("isMoving", false);
        }
        if (movementInput.x < 0)
        {
            spriteRenderer.flipX = true;
         
        }
        else if (movementInput.x > 0)
        {
            spriteRenderer.flipX = false;
            
        }
    }

    private bool TryMove(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            int count = rb.Cast(
                  direction,
                  movementFilter,
                  castCollisions,
                  moveSpeed * Time.fixedDeltaTime + collisionOffset);

            if (count == 0)
            {
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            }
            else
            {
                return false;
            }
        }else
        {
            return false;
        }
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }
    void OnFire()
    {
        animator.SetTrigger("attackKick");
    }

    public void KickAttack()
    {
        LockMovement();

        if (spriteRenderer.flipX == true)
        {
            Kickattack.AttackLeft();
        }else
        {
            Kickattack.AttackRight();
        }

        
    }

    public void EndAttack()
    {
        UnlockMovement();
        Kickattack.Stopattack();
    }


    public void LockMovement()
    {
        canMove = false;
    }

    public void UnlockMovement()
    {
        canMove = true;
    }
}
