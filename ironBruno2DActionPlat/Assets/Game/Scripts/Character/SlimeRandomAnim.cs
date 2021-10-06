using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeRandomAnim : MonoBehaviour
{
    public Animator animator;
    int pickAnumber;
    // Start is called before the first frame update
    void Start()
    {
        
        animator = GetComponent<Animator>();
       
    }

   

    // Update is called once per frame
    void Update()
    {
        Animation();


    }

    private void Animation()
    {
        animator.SetFloat("RandomAttack", Random.Range(0, 1));
        
    }
}
