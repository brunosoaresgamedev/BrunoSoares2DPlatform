using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{

    public AudioMixer mixer;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    float GetVol(float vol)
    {
        float newVol = 0;
        newVol = 20 * Mathf.Log10(vol);
        if (vol <= 0)
        {
            newVol = -80;
        }

        return newVol;
    }


    public void SetMasterVol(float vol)
    {
        
        mixer.SetFloat("Master", GetVol(vol));

    }


    public void SetMusicVol(float vol)
    {
        mixer.SetFloat("Music", GetVol(vol));
    }
    

    public void Loading()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
