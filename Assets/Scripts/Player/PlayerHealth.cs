using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int lives;

    public bool canTakeDamage = true;
    public float invincibilityTimer;

    public Animator anim;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    void OnTriggerEnter2D()
    {
        TakeDamage();
    }

    private IEnumerator LoseLife()
    {
        canTakeDamage = false; // Makes a cooldown so player cannot instantly lose 3 lives
        lives--; // Reduces life count

        if (lives == 0)
        {
            GameObject.Find("GameManager").GetComponent<GameManagement>().PermaDeath(); // Starts the death sequence
        }

        anim.SetTrigger("TakeDamage"); // Makes player flash in and out of existence to show it cannot take damage

        yield return new WaitForSeconds(invincibilityTimer); // Starts the invincibility timer

        canTakeDamage = true; // Allows damage to be taken again
    }

    public void TakeDamage()
    {
        if (canTakeDamage == true)
        {
            StartCoroutine(LoseLife()); // Damages player if collides with anything
        }
    }
}