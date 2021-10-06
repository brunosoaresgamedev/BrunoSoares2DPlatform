using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CodeMonkey.Utils;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Portal : MonoBehaviour
{
    [SerializeField]
    int sceneToLoad = 01;
    [SerializeField]
    Transform SpawnPoint;
    GameObject spawnpoint;
    [SerializeField] float fadeOutTime = 1f;
    [SerializeField] float fadeInTime = 2f;
    [SerializeField] float fadeWaitTime = 0.5f;
   
    [SerializeField]
    LvlMenuLeanTwen leanTwen;
    Button PortalButton;


    bool IsUsingPortal;
    enum DestinationIdentifier
    {
        A,B,C,D,E
    }

    [SerializeField] DestinationIdentifier destination;


    private void Awake()
    {
        SpawnPoint = gameObject.transform;

    }


    public void ButtonClicked()
    {

        
        USePortalButton();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {

            GameObject playerobjct = other.gameObject;

            Transform ButtonGameobject = playerobjct.transform.Find("PortalCanvas");

            leanTwen = ButtonGameobject.transform.GetChild(0).GetComponent<LvlMenuLeanTwen>();

            if (leanTwen != null)
                leanTwen.Open();

            var button = ButtonGameobject.transform.GetChild(0).GetComponent<Button>();
            button.onClick.AddListener(delegate () { this.ButtonClicked(); });


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
        StartCoroutine(Transition());
    }

    private IEnumerator Transition()
    {
        if (!IsUsingPortal)
        {
            if (sceneToLoad < 0)
            {
                Debug.LogError("HasNotAssignPortal");
                yield break;
            }
            IsUsingPortal = true;

            Fader fader = FindObjectOfType<Fader>();

            DontDestroyOnLoad(gameObject);

            yield return fader.FadeOut(fadeOutTime);

            Portal otherPortal = getOtherPortal();
            yield return SceneManager.LoadSceneAsync(sceneToLoad);
            otherPortal = getOtherPortal();
            UpdatePlayer(otherPortal);

            yield return new WaitForSeconds(fadeWaitTime);

            yield return fader.FadeIn(fadeInTime);
            Destroy(gameObject);
            yield return fader.FadeIn(fadeInTime);

            IsUsingPortal = false;
        }
      

    }
    private void UpdatePlayer(Portal otherPortal)
    {
       
        GameObject player = GameObject.FindWithTag("Player");
        player.transform.position = otherPortal.SpawnPoint.position;
        player.transform.rotation = otherPortal.SpawnPoint.rotation;
    }

    private Portal getOtherPortal()
    {
        foreach  (Portal portal in FindObjectsOfType<Portal>())
        {
            if (portal == this) continue;
            if (portal.destination != destination) continue;

            return portal;

        }

        return null;
    }
}
