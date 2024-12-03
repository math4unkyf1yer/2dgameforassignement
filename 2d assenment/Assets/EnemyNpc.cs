using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNpc : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject projectilePrefab;
    public float shootInterval = 2.0f; // Time between shots
    public Transform child;
    private int health = 5;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(ShootProjectile());
    }

    // Coroutine to shoot projectiles at intervals
    private IEnumerator ShootProjectile()
    {
        while (true) // Loop indefinitely
        {
            Debug.Log("100");
            // Calculate the shoot direction based on the character's current orientation
            Vector2 shootDirection = transform.up; // Change this if your character's forward direction is different

            // Instantiate the projectile
            GameObject projectileObject = Instantiate(projectilePrefab, rb.position + shootDirection * 0.5f, Quaternion.identity);
            Projectile projectile = projectileObject.GetComponent<Projectile>();
            projectile.Launch(shootDirection, 300); // Launch the projectile

            // Wait for the specified shoot interval before the next shot
            yield return new WaitForSeconds(shootInterval);
        }
    }
    public void TookDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            PlayerMovement playerScript = GameObject.Find("PlayerCharacter").GetComponent<PlayerMovement>();
            playerScript.cointotal += 10;
            Destroy(gameObject);
        }
    }
}
