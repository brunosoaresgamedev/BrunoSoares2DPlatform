using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTitle : MonoBehaviour
{
    public Animator anim;
    public bool EnterCollider;
    public Collider2D trigger;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        
            StartCoroutine(WaitAnim());
        
    }
    IEnumerator WaitAnim()
    {
        
        yield return new WaitForSeconds(5f);
        anim.SetTrigger("TitleEnd");

        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
