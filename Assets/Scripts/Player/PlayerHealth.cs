using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public AudioClip deathSoundClip;
    public AudioClip damageSoundClip;
    private GameObject camera;

    public int lives;

    public bool canTakeDamage = true;
    public float invincibilityTimer;

    public Animator anim;

    void Start()
    {
        camera = GameObject.Find("cams");
        anim = gameObject.GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (canTakeDamage == true && coll.gameObject.layer != 4)
        {
            StartCoroutine(LoseLife());
        }
    }

    public IEnumerator LoseLife()
    {
        canTakeDamage = false; // Makes a cooldown so player cannot instantly lose 3 lives
        lives--;

        if (lives == 0)
        {
            GameObject.Find("GameManager").GetComponent<GameManagement>().PermaDeath();
            anim.SetTrigger("Death");
            AudioSource.PlayClipAtPoint(deathSoundClip, camera.transform.position, 1f);
            Destroy(gameObject, 1f);
        }

        anim.SetBool("TakeDamage", true); // Makes player flash in and out of existence to show it cannot take damage
        AudioSource.PlayClipAtPoint(damageSoundClip, camera.transform.position, 1f);
        yield return new WaitForSeconds(invincibilityTimer);

        anim.SetBool("TakeDamage", false);
        canTakeDamage = true;
    }
}