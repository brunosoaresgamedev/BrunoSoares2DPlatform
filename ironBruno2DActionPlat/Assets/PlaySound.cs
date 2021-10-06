using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public AudioSource ButtonSound;
    
    public void SoundButton()
    {
        ButtonSound.Play();
    }
    
}
