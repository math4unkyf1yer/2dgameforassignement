using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IsPlayerClose : MonoBehaviour
{
    public Transform player; // Reference to the player's Transform
    public float detectionRange = 3f; // Distance within which the action will trigger
    public Animator RepairAnimation;
    public GameObject robotEffect;
    public TextMeshProUGUI myText;
    public PlayerMovement playerScript;
    bool alreadyPressed;

    void Update()
    {
        if(player != null)
        {
            // Calculate the distance between the player and this object
            float distanceToPlayer = Vector3.Distance(player.position, transform.position);

            // Check if the player is within the detection range
            if (distanceToPlayer <= detectionRange)
            {
                // Call a function to trigger the event or action
                myText.text = "Pressed R to repair";
                if (Input.GetKeyDown(KeyCode.R) && !alreadyPressed)
                {
                    alreadyPressed = true;
                    playerScript.robotRepair++;
                    //change animation 
                    RepairAnimation.SetBool("Repair", true);
                    //take off effect
                    robotEffect.SetActive(false);
                }

            }
            else
            {
                myText.text = "";
            }
        }
    }

    // This function is called when the player is close enough
}
