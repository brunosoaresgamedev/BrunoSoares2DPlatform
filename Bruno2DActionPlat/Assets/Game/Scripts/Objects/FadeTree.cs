using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeTree : MonoBehaviour
{

    public bool EnterCollider;
    private Renderer rend;


    [SerializeField]
    private Color colorToTurnTo = Color.white;

    // Start is called before the first frame update
    void Start()
    {
        EnterCollider = false;
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EnterCollider == true)
        {
            rend.material.color = colorToTurnTo;
        }
        if(EnterCollider == false)
        {
            rend.material.color = Color.white;
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
