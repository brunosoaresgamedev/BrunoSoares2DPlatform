 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerLT : MonoBehaviour
{
   

    // Start is called before the first frame update
    void Start()
    {
        LeanTween.moveLocalY(gameObject, 1,0f);
        LeanTween.moveLocalY(gameObject, 0,1f);
    }


// Update is called once per frame
    void Update()
    {   

    }
}