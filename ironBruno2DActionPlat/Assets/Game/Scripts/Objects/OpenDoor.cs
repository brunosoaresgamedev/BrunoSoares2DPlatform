using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;
    public GameObject Door;


    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerStay2D()
    {
        DoorOpenning();
    }

    public void DoorOpenning()
    {
        if (Input.GetKey(KeyCode.E))
        {
            Door.SetActive(false);
            spriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);

        }
    }
}
