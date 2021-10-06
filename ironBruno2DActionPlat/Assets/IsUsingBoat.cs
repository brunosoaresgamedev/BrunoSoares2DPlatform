using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IsUsingBoat : MonoBehaviour
{
    PlayerController playerController;
  
    GameObject boatTrigger;
    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Boat"))
        {
         

            playerController.canControl = true;


        }
    }

}
