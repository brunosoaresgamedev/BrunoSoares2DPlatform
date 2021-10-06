using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mushromAnim : MonoBehaviour
{

    Animator Anim;

    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            Anim.SetTrigger("Jump");
    }
}
