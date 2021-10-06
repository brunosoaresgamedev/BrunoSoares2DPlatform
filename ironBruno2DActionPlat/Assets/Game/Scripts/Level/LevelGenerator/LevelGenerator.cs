using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{


    private const float PLAYER_DISTANCE_SPAWN_lEVEL_PART = 500F;

    [SerializeField] private Transform levelPart_Start;
    [SerializeField] private List<Transform> levelPartList;
    GameObject PlayerObject;
    [SerializeField] private Transform player;

    private Vector3 lastEndPosition;

    private void Awake()
    {
        PlayerObject = GameObject.Find("Player");
        player = PlayerObject.GetComponent<Transform>();

        lastEndPosition = levelPart_Start.Find("EndPosition").position;
        
        int startingSpawnLevelPàrts = 5;
        for(int i= 0;i< startingSpawnLevelPàrts; i++)
        {
            SpawnLevelPart();
        }
        
    }

    private void Update()
    {
     if(Vector3.Distance(player.position, lastEndPosition) < PLAYER_DISTANCE_SPAWN_lEVEL_PART)
        {
            //spawn another level part
            SpawnLevelPart();
        }   
    }


    private void SpawnLevelPart()
    {
       Transform chosenLevelPart = levelPartList[Random.Range(0, levelPartList.Count)];
       Transform lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, lastEndPosition);
       lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
    }

    private Transform SpawnLevelPart(Transform levelPart, Vector3 spawnPosition)
    {
       Transform levelPartTransform = Instantiate(levelPart, spawnPosition, Quaternion.identity);
        return levelPartTransform;
   }
}
















