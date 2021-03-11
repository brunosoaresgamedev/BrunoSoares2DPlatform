using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuturial1 : MonoBehaviour { 


private bool EnterCollider;
    public GameObject TuturialCrouch;

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
            TuturialCrouch.SetActive(true);
        EnterCollider = true;
    }
}

private void OnTriggerExit2D(Collider2D other)
{
    if (other.gameObject.CompareTag("Player"))
    {
            TuturialCrouch.SetActive(false);
        EnterCollider = false;
            //StartCoroutine(WaitAnim());
    }
    
}
    /*
    IEnumerator WaitAnim()
    {

        yield return new WaitForSeconds(20f);
        Destroy(gameObject);
    
        
    }
    */

}

