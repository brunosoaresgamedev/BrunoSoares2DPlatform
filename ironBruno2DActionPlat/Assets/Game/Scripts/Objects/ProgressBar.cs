using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProgressBar : MonoBehaviour
{
    private Slider slider;
    public float FillSpeed = 0.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        

    }
    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncrementProgress(float newProgress)
    {
        
    }
}
