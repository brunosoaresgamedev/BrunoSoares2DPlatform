using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
public class EnemyAI : MonoBehaviour
{
   
    private Vector3 startingPosition;
    private Vector3 roamPosition;

    private void Awake()
    {
        
    }
    private void Start()
    {
        startingPosition = transform.position;
        roamPosition = GetRoamingPosition();
    }

    private void Update()
    {
        
    }
    private Vector3 GetRoamingPosition()
    {
        return startingPosition + UtilsClass.GetRandomDir() * Random.Range(10f, 70f);
    }
}
