using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enterIsland : MonoBehaviour
{
    GameObject Boat;
    GameObject boatTriggers;
    GameObject boatColliders;
    LvlMenuLeanTwen leanTwen;

    private void Awake()
    {
      //  boatTriggers = GameObject.Find("boatTriggersEnter");
     //   boatColliders = GameObject.Find("boatColliders");
      //  Boat = GameObject.Find("boat");
        
       // leanTwen = Boat.transform.GetChild(2).transform.GetChild(0).GetComponent<LvlMenuLeanTwen>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boat"))
        {
            leanTwen.Open();
         //   SceneManager.LoadScene(0);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Boat"))
        {
            leanTwen.Close();
            //   SceneManager.LoadScene(0);
        }
    }
}
