using UnityEngine;

/// <summary>
/// Handle the projectile launched by the player to fix the robots.
/// </summary>
public class Projectile : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    private int damage = 1;
    
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //destroy the projectile when it reach a distance of 1000.0f from the origin
        if(transform.position.magnitude > 100.0f)
            Destroy(gameObject);
    }

    //called by the player controller after it instantiate a new projectile to launch it.
    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
            EnemyController e = other.collider.GetComponent<EnemyController>();
        PlayerMovement p = other.collider.GetComponent<PlayerMovement>();
        EnemyNpc n = other.collider.GetComponent<EnemyNpc>();

        if (p != null)
        {
            Destroy(gameObject);
            p.TookDamage(damage);
        }
        if (n != null)
        {
            Destroy(gameObject);
            n.TookDamage(damage);
        }
        //if the object we touched wasn't an enemy, just destroy the projectile.
        if (e != null)
            {
                Destroy(gameObject);
                e.TookDamage(damage);
            }

            Destroy(gameObject);
        
    }
}
