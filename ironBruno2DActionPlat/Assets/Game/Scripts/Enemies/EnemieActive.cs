using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieActive : MonoBehaviour
{

    public GameObject Manager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == ("Player"))
        {
            Manager.SetActive(true);
        }
    }

        private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == ("Player"))
        {
            Manager.SetActive(false);
        }
    }
}
