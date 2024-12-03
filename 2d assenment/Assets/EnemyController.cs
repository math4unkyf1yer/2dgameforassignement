using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public bool vertical;
    public float changeTime = 3.0f;
    private int health = 3;

    // Private variables
    Rigidbody2D rigidbody2d;
    float timer;
    int direction = 1;
    public Animator enemyAnimation;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        timer = changeTime;
    }

    // Update is called every frame
    void Update()
    {
        enemyAnimation.SetFloat("speed", speed);
        timer -= Time.deltaTime;


        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }

    }

    // FixedUpdate has the same call rate as the physics system
    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;

        if (vertical)
        {
            position.y = position.y + speed * direction * Time.deltaTime;
            enemyAnimation.SetFloat("ForwardX", 0);
            enemyAnimation.SetFloat("ForwardY", direction); // 1 for up, -1 for down
        }
        else
        {
            position.x = position.x + speed * direction * Time.deltaTime;
            Debug.Log(direction);
            enemyAnimation.SetFloat("ForwardX", direction); // 1 for right, -1 for left
            enemyAnimation.SetFloat("ForwardY", 0);
        }


        rigidbody2d.MovePosition(position);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();


        if (player != null)
        {
            player.ChangeHealth(-1);
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
