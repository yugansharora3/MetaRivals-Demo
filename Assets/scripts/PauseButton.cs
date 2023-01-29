using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    [SerializeField]
    public GameObject panel;
    public TextMeshProUGUI status;

    public void Start()
    {
    }
    public void OnClick()
    {
        panel.SetActive(!panel.activeSelf);
        if(PlayerPrefs.HasKey("Account"))
        {
            status.text = PlayerPrefs.GetString("Account");
        }
    }
}
