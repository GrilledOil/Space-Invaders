using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private void Death()
    {
        Destroy(gameObject.GetComponent<BoxCollider2D>());
        Destroy(gameObject.GetComponent<Rigidbody2D>());

        gameObject.GetComponent<Animator>().SetTrigger("Impact");

        Destroy(gameObject, 0.5f);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.GetComponent<PlayerHealth>().LoseLife();
        }
        Death();
    }
}

public class EnemyShooting : MonoBehaviour
{
    public GameObject BulletPrefab;

    private bool canShoot = true;

    private float bulletCooldown = 2f;
    private int chanceToShoot = 9;

    public float bulletSpeed;

    void Update()
    {
        if (canShoot)
        {
            StartCoroutine(ShootingChance(chanceToShoot));
        }
    }


    // The actual shooting
    private void Shoot()
    {
        GameObject clone = Instantiate(BulletPrefab, new Vector3(transform.position.x, transform.position.y-0.5f), transform.rotation);
        Rigidbody2D bulletRB = clone.GetComponent<Rigidbody2D>();
        bulletRB.velocity = transform.TransformDirection(Vector2.down * bulletSpeed);

        EnemyBullet bullet = clone.AddComponent<EnemyBullet>();
    }

    public IEnumerator ShootingChance(int chanceShoot) // Decides whether or not the enemy will shoot every x amount of seconds
    {
        canShoot = false;
        if (Random.Range(0, 100) <= chanceShoot)
        {
            Shoot();
        }

        yield return new WaitForSeconds(bulletCooldown);
        canShoot = true;
    }
}
