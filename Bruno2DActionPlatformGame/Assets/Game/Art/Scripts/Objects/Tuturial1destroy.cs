using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tuturial1destroy : MonoBehaviour
{


    public Animator anim;
    public bool ButtonPressed;


    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        if(ButtonPressed == true)
        {
            StartCoroutine(WaitAnim());
        }
    }
    
   
    IEnumerator WaitAnim()
    {

        yield return new WaitForSeconds(4f);
        anim.SetTrigger("TuturialEnd");

        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
