using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OreScript : MonoBehaviour
{

    [SerializeField]
    Transform MinerPos1, MinerPos2;

    [SerializeField]
    GameObject Miner;

    [SerializeField]
    LvlMenuLeanTwen leanTwen;

    [SerializeField]
    MinerScript minerScriptM1,minerScriptM2;
    [SerializeField]
    bool HasMinerIn;

    bool isEven;

    public bool IsMining;

    bool MinerDestroy;
    private void Awake()
    {
        Transform ButtonGameobject = gameObject.transform.Find("MinerCanvas");

        leanTwen = ButtonGameobject.transform.GetChild(0).GetComponent<LvlMenuLeanTwen>();

        var button = ButtonGameobject.transform.GetChild(0).GetComponent<Button>();
        button.onClick.AddListener(delegate () { this.ButtonClicked(); });

    }

   
    public void ButtonClicked()
    {
        if(!HasMinerIn)
        USePortalButton();
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {

            GameObject playerobjct = other.gameObject;

          

            if (leanTwen != null)
                leanTwen.Open();

           

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (leanTwen != null)
                leanTwen.Close();

        }
    }

    public void USePortalButton()
    {
        if(GameManager.instance.CurrentMining < GameManager.instance.maxMining)
        {
            HasMinerIn = true;


                GameManager.instance.CurrentMining++;
                Instantiate(Miner, MinerPos1.transform.position, MinerPos1.transform.rotation);
            
            
               
                GameManager.instance.CurrentMining++;
                Instantiate(Miner, MinerPos2.transform.position, MinerPos2.transform.rotation);
            
           
        }
        
    }
    private void Update()
    {
        if (GameManager.instance.CurrentMining % 2 == 0)
         isEven = true;
        else
        {
            isEven = false;
        }

        if (GameManager.instance.IsMinerDead < 0)
        {
            HasMinerIn = false;
            StartCoroutine(hasminerDied());
            //  
        }

       
    }
    IEnumerator hasminerDied()
    {
        yield return new WaitForSeconds(0.2f);
        GameManager.instance.IsMinerDead = 0;
    }
    /*
    void MinerDestroyed()
    {
       
       
    }
    */
}
