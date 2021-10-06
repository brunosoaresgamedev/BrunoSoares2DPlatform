using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShowFPS : MonoBehaviour
{

    public float timer, refresh, avgFramerate;
    public string display = "{0} FPS";
    public Text m_Text;

    GameObject m_TextObject;
    void Awake()
    {
        m_TextObject = GameObject.Find("CanvaMenuButton");
        m_Text = m_TextObject.transform.GetChild(0).GetComponent<Text>();

        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float timelapse = Time.smoothDeltaTime;
        timer = timer <= 0 ? refresh : timer -= timelapse;

        if (timer <= 0) avgFramerate = (int)(1f / timelapse);
        m_Text.text = string.Format(display, avgFramerate.ToString());
    }
}
