using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderText : MonoBehaviour
{
    TextMeshProUGUI m_textComponent;
    public Slider slider;
    private void Awake()
    {
        m_textComponent = GetComponent<TextMeshProUGUI>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateText()
    {
        m_textComponent.text = slider.value.ToString();
    }

    // Update is called once per frame
    void Update()
    {
         
    }
}
