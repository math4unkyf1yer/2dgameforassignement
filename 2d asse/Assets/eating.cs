using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eating : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerMovement controller = other.GetComponent<PlayerMovement>();

        if (controller != null && controller.health < controller.maxHealth)
        {
            controller.HelthPickUpEffect();
            controller.ChangeHealth(1);

            // play audio when pickup
            AudioClip collectSound = Resources.Load<AudioClip>("Pickup_health");
            AudioSource.PlayClipAtPoint(collectSound, transform.position);

            Destroy(gameObject);
        }
    }
}
