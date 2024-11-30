using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of movement
    private Rigidbody2D rb;
    private Vector2 move;
    public GameObject healthPickUpEffect;
    public GameObject tookDamageEffect;

    public int maxHealth = 5;
    public int health { get { return currentHealth; } }
    int currentHealth;

    // Variables related to temporary invincibility
    public float timeInvincible = 2.0f;
    bool isInvincible;
    float damageCooldown;

    // Reference to the health slider
    public Slider healthSlider;

    // Variables related to animation
    Animator animator;
    Vector2 moveDirection = new Vector2(1, 0);
    public GameObject projectilePrefab;
    public int robotRepair;
    public GameObject winPage;
    public GameObject losePage;
    public TextMeshProUGUI myText;
    public GameObject extraCamera;
    public AudioSource playerSound;

    //coins 
    public int cointotal; /// here 

    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rb.position + Vector2.up * 0.5f, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(moveDirection, 300);
        animator.SetTrigger("Launch");
    }
    public void TookDamage(int damage)
    {
        playerSound.Play();
        currentHealth -= damage;
        tookDamageEffect.SetActive(true);
        StartCoroutine(resetDamageEffect());
        if (isInvincible)
        {
            return;
        }
        isInvincible = true;
        damageCooldown = timeInvincible;
        animator.SetTrigger("Hit");
        // Update the slider value
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }
        if (currentHealth <= 0)
        {
            extraCamera.SetActive(true);
            Debug.Log("dead");
            Destroy(gameObject);
            losePage.SetActive(true);
            this.enabled = false;
        }

    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        // Initialize the slider
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
        animator.SetTrigger("Hit");
    }

    void Update()
    {
        myText.text = "Repair all Robots" + robotRepair + "/3";
        if(robotRepair == 3)
        {
            winPage.SetActive(true);
            this.enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Launch();
        }
        // Get input from WASD or arrow keys
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");
        move = move.normalized; // Normalize to avoid faster diagonal movement
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            moveDirection.Set(move.x, move.y);
            moveDirection.Normalize();
        }
        animator.SetFloat("Look X", moveDirection.x);
        animator.SetFloat("Look Y", moveDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (isInvincible)
        {
            damageCooldown -= Time.deltaTime;
            if (damageCooldown < 0)
            {
                isInvincible = false;
            }
        }
    }

    void FixedUpdate()
    {
        // Move the Rigidbody
        rb.MovePosition(rb.position + move * moveSpeed * Time.fixedDeltaTime);
    }

    public void ChangeHealth(int amount)
    {
       
        if (amount < 0)
        {
            playerSound.Play();
            tookDamageEffect.SetActive(true);
            StartCoroutine(resetDamageEffect());
            if (isInvincible)
            {
                return;
            }
            isInvincible = true;
            damageCooldown = timeInvincible;
            animator.SetTrigger("Hit");
            if (currentHealth <= 0)
            {
                extraCamera.SetActive(true);
                Debug.Log("dead");
                Destroy(gameObject);
                losePage.SetActive(true);
                this.enabled = false;
            }
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

        // Update the slider value
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        Debug.Log(currentHealth + "/" + maxHealth);
    }

    public void HelthPickUpEffect()
    {
        healthPickUpEffect.SetActive(true);
        StartCoroutine(resetEffecthealth());
    }
    public IEnumerator resetEffecthealth()
    {
        yield return new WaitForSeconds(0.8f);
        healthPickUpEffect.SetActive(false);
    }
    private IEnumerator resetDamageEffect()
    {
        yield return new WaitForSeconds(0.8f);
        tookDamageEffect.SetActive(false);
    }
}
