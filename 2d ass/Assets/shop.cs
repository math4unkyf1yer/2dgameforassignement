using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class shop : MonoBehaviour
{
    private bool nearShop;
    public GameObject myText;
    public GameObject shopPage;
    public AudioSource shopsound;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "PlayerCharacter")
        {
            nearShop = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.name == "PlayerCharacter")
        {
            nearShop = false;
        }
    }
    private void Update()
    {
        if (nearShop)
        {
            if (!myText.activeSelf)
            {
                myText.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                shopsound.Play();
                shopPage.gameObject.SetActive(true);
            }
        }
        else
        {
            if (myText.activeSelf)
            {
                myText.SetActive(false);
            }
            if (shopPage.activeSelf)
            {
                shopPage.gameObject.SetActive(false);
            }
        }
    }

    public void Buy()
    {
        PlayerMovement controller = GameObject.Find("PlayerCharacter").GetComponent<PlayerMovement>();


        if (controller != null && controller.health < controller.maxHealth && controller.cointotal <= 30)
        {
            controller.HelthPickUpEffect();
            controller.ChangeHealth(1);
        }
        else
        {
            Debug.Log("full health or not enough money ");
        }
    }
}
