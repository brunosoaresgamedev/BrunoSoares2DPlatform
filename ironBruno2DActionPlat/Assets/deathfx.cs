using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathfx : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(deathfxx());
    }
    
    IEnumerator deathfxx()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
