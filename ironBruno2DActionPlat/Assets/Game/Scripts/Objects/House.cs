using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class House : MonoBehaviour
{

    private bool EnterCollider;
    SpriteRenderer spriteRenderer;
    
    public GameObject Colliders;
    public Sprite HouseNormal;
    public Sprite HouseInside;
   

    LvlMenuLeanTwen lvlMenuLeanTwen;


    private bool Inside;
  
    
    // Start is called before the first frame update
    void Start()
    {
         GameObject theButtonDoormanager = GameObject.Find("CanvaMenuButton");
      
         Colliders = GameObject.Find("CollidersCasa");
        Colliders.SetActive(false);
        theButtonDoormanager.transform.GetChild(1).GetComponent<Button_UI>().ClickFunc = () => {
            UsingDoor();
        };
     
        lvlMenuLeanTwen = theButtonDoormanager.transform.GetChild(1).GetComponent<LvlMenuLeanTwen>();
        Inside = false;
        EnterCollider = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            lvlMenuLeanTwen.Open();
               EnterCollider = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            lvlMenuLeanTwen.Close();
            EnterCollider = false;
        }
    }
   

    public void DoorUsedEnter()
    {
        UsingDoor();
    }

    private void UsingDoor()
    {
        if (Inside == false)
        {
            spriteRenderer.sprite = HouseInside;
            Colliders.SetActive(true);

            Inside = true;
        }
        else if(Inside == true)
        {
            spriteRenderer.sprite = HouseNormal;
            Colliders.SetActive(false);

            Inside = false;
        }
    }

    
    
}
