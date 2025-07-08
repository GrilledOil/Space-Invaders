using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public Rigidbody2D rb;

    public float bulletSpeed;
    public float deathTimer = 0.5f;
    public Animator animator;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    }

    private IEnumerator Death(float deathTime)
    {
        Destroy(gameObject.GetComponent<BoxCollider2D>());
        Destroy(gameObject.GetComponent<Rigidbody2D>());

        animator.SetTrigger("Impact");

        yield return new WaitForSeconds(deathTime);
        
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.GetComponent<PlayerHealth>().TakeDamage();
        }
        StartCoroutine(Death(deathTimer));
    }
}

public class EnemyShooting : MonoBehaviour
{
    public GameObject BulletPrefab;

    private bool canShoot = true;

    public float bulletCooldown = 2f;
    public int chanceToShoot;

    public float bulletSpeed;

    void Update()
    {
        if (canShoot)
        {
            StartCoroutine(ShootingChance(chanceToShoot));
        }
    }


    // The actual shooting
    private IEnumerator Shoot(float waitTime)
    {
        canShoot = false;

        GameObject clone = Instantiate(BulletPrefab, new Vector3(transform.position.x, transform.position.y-0.5f), transform.rotation); // Spawns enemy bullet
        Rigidbody2D bulletRB = clone.GetComponent<Rigidbody2D>(); // Gets bullets rigidbody
        bulletRB.velocity = transform.TransformDirection(Vector2.down * bulletSpeed); // Gives bullet velocity
        EnemyBullet bullet = clone.AddComponent<EnemyBullet>();

        bullet.bulletSpeed = bulletSpeed;

        yield return new WaitForSeconds(waitTime);
        canShoot = true;
    }

    public IEnumerator ShootingChance(float chanceShoot) // Decides whether or not the enemy will shoot every x amount of seconds
    {
        canShoot = false;
        if (Random.Range(0, 100) <= chanceShoot)
        {
            StartCoroutine(Shoot(bulletCooldown));
        }
        else
        {
            yield return new WaitForSeconds(bulletCooldown);
            canShoot = true;
        }
    }
}
