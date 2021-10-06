using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatInIsland : MonoBehaviour
{

    //PlayerController playerController;
    GameObject Boat;
    GameObject boatColliders;
    LvlMenuLeanTwen leanTwen;
    private void Awake()
    {
        Boat = GameObject.Find("SpawnPoint");

        boatColliders = GameObject.Find("boatColliders");


        GameManager.instance.Boat.transform.position = GameManager.instance.SpawnPosition.position;
        GameManager.instance.playerTransform.position = GameManager.instance.SpawnPosition.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
          //  leanTwen.Open();
            //   SceneManager.LoadScene(0);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
         //   leanTwen.Close();
            //   SceneManager.LoadScene(0);
        }
    }
}
