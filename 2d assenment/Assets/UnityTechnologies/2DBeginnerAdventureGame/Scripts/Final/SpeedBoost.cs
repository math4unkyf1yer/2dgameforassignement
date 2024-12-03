using UnityEngine;

/// <summary>
/// speedup!!!
/// </summary>
public class SpeedBoostCollectible : MonoBehaviour 
{
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerMovement controller = other.GetComponent<PlayerMovement>();

        if (controller != null)
        {
            controller.moveSpeed = 7f;

            // 播放加速音效
            AudioClip speedupSound = Resources.Load<AudioClip>("Speedup");
            if (speedupSound != null)
            {
                AudioSource.PlayClipAtPoint(speedupSound, transform.position);
            }
            else
            {
                Debug.LogError("not found");
            }

            Destroy(gameObject);
        }
    }
}
