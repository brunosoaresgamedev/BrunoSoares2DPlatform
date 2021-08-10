using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGenerator : MonoBehaviour
{
    [SerializeField]
    GameObject[] clouds;

    [SerializeField]
    float spawnInterval;

    [SerializeField]
    GameObject endPoint;

    Vector3 startPos;


    // Start is called before the first frame update
    void Start()
    {
        
        Invoke("AttemptSpawn", spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        startPos = transform.position;
    }

    
    void SpawnCloud(Vector3 startPos)
    {
        //randomizano qual nuvem aparece
        int randomIndex = UnityEngine.Random.Range(0, 4);
        GameObject cloud = Instantiate(clouds[randomIndex]);

        //randomizando a  posicao
        float startY = UnityEngine.Random.Range(startPos.y - 1f, startPos.y + 1f);

        cloud.transform.position = new Vector3(startPos.x, startY,startPos.z);

        //randomizando a esclada
        float scale = UnityEngine.Random.Range(0.8f, 1.2f);
        cloud.transform.localScale = new Vector2(scale, scale);

        //randomizando a velocidade
        float speed = (UnityEngine.Random.Range(0.5f, 1.5f));
        cloud.GetComponent<CloudScript>().StartFloating(speed, endPoint.transform.position.x);



    }
    void AttemptSpawn()
    {
        SpawnCloud(startPos);

        Invoke("AttemptSpawn", spawnInterval);
    }

}
