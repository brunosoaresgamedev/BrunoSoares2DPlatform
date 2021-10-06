using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryGem : MonoBehaviour
{
   
    public bool CanDropGem;

    public static RecoveryGem instance;

    
    // Start is called before the first frame update
    void Start()
    {
        
       
        
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        GameManager.instance.coinRecovery += GameManager.instance.coin;
        GameManager.instance.coin -= GameManager.instance.coin;
        //StartCoroutine(RecoveryGemDelay());
        StartCoroutine(LayerChanged());

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    IEnumerator RecoveryGemDelay()
    {
        
        Debug.Log(GameManager.instance.coinRecovery);
        Debug.Log(GameManager.instance.coin);
        yield return new WaitForSeconds(0.2f);
        
        Debug.Log(GameManager.instance.coinRecovery);
        Debug.Log(GameManager.instance.coin);
    }
    IEnumerator LayerChanged()
    {

        gameObject.layer = 12;
        yield return new WaitForSeconds(10);
        gameObject.layer = 0;
        StopCoroutine(LayerChanged());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.coin += GameManager.instance.coinRecovery;


            Destroy(gameObject);
        }
    }
}
