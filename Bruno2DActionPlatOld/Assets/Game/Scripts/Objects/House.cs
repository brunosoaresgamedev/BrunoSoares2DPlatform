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

   
    public GameObject DoorButton;


    private bool Inside;
  
    
    // Start is called before the first frame update
    void Start()
    {
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
            DoorButton.SetActive(true);
            EnterCollider = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            DoorButton.SetActive(false);
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
