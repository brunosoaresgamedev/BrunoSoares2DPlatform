using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerBruno : MonoBehaviour
{
    [SerializeField]
    PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
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
