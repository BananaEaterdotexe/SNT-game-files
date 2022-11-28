using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using UnityEngine;

public class kickattack : MonoBehaviour
{
    public Collider2D kickCollider;

    public float damage = 3;

    Vector2 attackOffset;
   
    
    private void Start()
    {
    
        attackOffset = transform.position;
    }


    public void AttackRight()
    {
        print ("attack right");
        kickCollider.enabled = true;
        transform.localPosition = attackOffset;
    }
    public void AttackLeft()
    {
        print("attack left");
        kickCollider.enabled = true;
        transform.localPosition = new Vector2(attackOffset.x * -1, attackOffset.y);
    }
    public void Stopattack()
    {
        kickCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();

            if(enemy != null)
            {
                enemy.Health -= damage;
            }
        }
    }
}
