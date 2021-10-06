using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlMenuLeanTwen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = Vector2.zero;
    }
    public void Open()
    {
        transform.LeanScale(Vector2.one, 1f);
    }
    public void Close()
    {
        transform.LeanScale(Vector2.zero, 1f).setEaseInBack();
    }
}
