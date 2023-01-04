using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    [SerializeField]
    GameObject light1, light2;

    [SerializeField]
    GameObject SpotLight;

    public GameObject Ape;
    public GameObject Shiba;
    public GameObject Kishu;
    public GameObject WhiteWitch;
    public GameObject Volt;
    
    private string ape = "ape", shiba = "shiba", kishu = "kishu", whiteWitch = "whiteWitch", volt = "volt";
    
    Vector3 Apepos, Shibapos, Kishupos, WhiteWitchpos, Voltpos;
    private void Awake()
    {
        Apepos = Ape.transform.position;
        Shibapos = Shiba.transform.position;
        Kishupos = Kishu.transform.position;
        WhiteWitchpos = WhiteWitch.transform.position;
        Voltpos = Volt.transform.position;

        Apepos.y += 20f;
        Shibapos.y += 20f;
        Kishupos.y += 20f;
        WhiteWitchpos.y += 20f;
        WhiteWitchpos.z += 2f;
        Voltpos.y += 20f;

        unchange();
    }
    public void change(string name)
    {
        light1.SetActive(false);
        light2.SetActive(false);
        SpotLight.SetActive(true);
        if (name == ape)
        {
            SpotLight.transform.position = Apepos;
        }
        else
        {
            if (name == shiba)
            {
                SpotLight.transform.position = Shibapos;
            }
            else
            {
                if (name == kishu)
                {
                    SpotLight.transform.position = Kishupos;
                }
                else
                {
                    if (name == volt)
                    {
                        SpotLight.transform.position = Voltpos;
                    }
                    else
                    {
                        if (name == whiteWitch)
                        {
                            SpotLight.transform.position = WhiteWitchpos;
                        }
                        else
                        {
                            unchange();
                        }
                    }
                }
            }
        }
    }

    public void unchange()
    {
        light1.SetActive(true);
        light2.SetActive(true);
        SpotLight.SetActive(false);
    }
}

