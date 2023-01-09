using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour
{
    public Animator Popup;
    public void quit()
    {
        Debug.Log("Quit");
        Application.Quit();
        Popup.SetBool("Pop", false);
    }
}
