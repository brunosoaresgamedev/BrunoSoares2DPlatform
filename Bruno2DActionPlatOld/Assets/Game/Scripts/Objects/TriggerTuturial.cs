using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTuturial : MonoBehaviour
{
    Tuturial1destroy tuturial1Destroy;
    public bool EnterCollider;
    // Start is called before the first frame update
    void Start()
    {
        tuturial1Destroy = GetComponent<Tuturial1destroy>();
    }

    // Update is called once per frame
    void Update()
    {
        if(EnterCollider == true)
        {
            tuturial1Destroy.ButtonPressed = true;
        }
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            EnterCollider = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            EnterCollider = false;
        }
    }
}
