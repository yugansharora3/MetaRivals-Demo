using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseButton : MonoBehaviour
{
    [SerializeField]
    public GameObject panel;

    public void OnClick()
    {
        panel.SetActive(!panel.activeSelf);
    }
}
