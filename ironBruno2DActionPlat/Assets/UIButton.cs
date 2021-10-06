using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButton : MonoBehaviour
{
    void Start()
    {
        transform.localScale = Vector2.one;
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
