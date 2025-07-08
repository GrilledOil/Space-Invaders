using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;

    public Animator animator;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    private IEnumerator Death()
    {
        Destroy(gameObject);
        yield break;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            coll.gameObject.GetComponent<EnemyShooting>().ShootingChance(40);
        }
        StartCoroutine(Death());
    }
}

public class PlayerShooting : MonoBehaviour
{
    public bool isMoving = false;
    public bool canShoot = true;
    public GameObject Player;

    public GameObject BulletPrefab;
    public float bulletSpeed;
    public float bulletCooldown;

    void Update()
    {
        isMoving = Player.GetComponent<PlayerMovement>().isMoving;

        if (isMoving == false && canShoot)
        {
            StartCoroutine(Shoot(bulletCooldown));
        }
    }

    private IEnumerator Shoot(float waitTime)
    {
        canShoot = false;

        GameObject clone = Instantiate(BulletPrefab, transform.position, transform.rotation);
        Rigidbody2D bulletRB = clone.GetComponent<Rigidbody2D>();
        bulletRB.velocity = transform.TransformDirection(Vector2.up * bulletSpeed);

        Bullet bullet = clone.AddComponent<Bullet>();

        bullet.bulletSpeed = bulletSpeed;

        yield return new WaitForSeconds(waitTime);
        canShoot = true;
    } 
}