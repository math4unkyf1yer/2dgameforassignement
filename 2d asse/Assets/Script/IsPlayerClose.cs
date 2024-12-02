using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IsPlayerClose : MonoBehaviour
{
    public Transform player; // player location
    public float detectionRange = 3f; // trigger range
    public Animator RepairAnimation;
    public GameObject robotEffect;
    public TextMeshProUGUI myText;
    public PlayerMovement playerScript;
    bool alreadyPressed;

    void Update()
    {
        if(player != null)
        {
            // calculate distance between player and enemy
            float distanceToPlayer = Vector3.Distance(player.position, transform.position);

            // detect player in range
            if (distanceToPlayer <= detectionRange)
            {
                // show prompt
                myText.text = "Press R To Repair";

                if (Input.GetKeyDown(KeyCode.R) && !alreadyPressed)
                {
                    alreadyPressed = true;
                    playerScript.robotRepair++;
                    // change animation
                    RepairAnimation.SetBool("Repair", true);
                    // close effect
                    robotEffect.SetActive(false);

                    // play audio
                    AudioClip repairSound = Resources.Load<AudioClip>("Repair");
                    if (repairSound != null)
                    {
                        AudioSource.PlayClipAtPoint(repairSound, transform.position);
                    }
                    else
                    {
                        Debug.LogError("not found Resources  'Repair' 。");
                    }
                }

            }
            else
            {
                myText.text = "";
            }
        }
    }
}
