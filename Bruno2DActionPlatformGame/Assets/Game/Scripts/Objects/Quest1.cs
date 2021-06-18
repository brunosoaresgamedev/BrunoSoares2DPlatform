using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest1 : MonoBehaviour
{
    private bool EnterCollider;

    public GameObject QuestText;
    // Start is called before the first frame update
    void Start()
    {
        EnterCollider = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            QuestText.SetActive(true);
            EnterCollider = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            QuestText.SetActive(false);
            EnterCollider = false;
        }
    }
}
