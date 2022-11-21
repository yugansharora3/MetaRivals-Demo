using System.IO;
using UnityEngine;
using Cinemachine;

public class ManagerScript : Singleton<ManagerScript>
{
    public GameObject Ape;
    public GameObject Shiba;
    public GameObject Kishu;
    public GameObject WhiteWitch;
    public GameObject Volt;

    void Awake()
    {
        string objectName = PlayerPrefs.GetString("Chosen-Character");
        if (objectName != "ape")
        {
            GameObject Object = Ape;
            Object.SetActive(false);
        }
        else
        {
            SetCharacter(Ape);
        }
        if (objectName != "volt")
        {
            GameObject Object = Volt;
            Object.SetActive(false);
        }
        else
        {
            SetCharacter(Volt);
        }
        if (objectName != "shiba")
        {
            GameObject Object = Shiba;
            Object.SetActive(false);
        }
        else
        {
            SetCharacter(Shiba);
        }
        if (objectName != "kishu")
        {
            GameObject Object = Kishu;
            Object.SetActive(false);
        }
        else
        {
            SetCharacter(Kishu);
        }
        if (objectName != "whiteWitch")
        {
            GameObject Object = WhiteWitch;
            Object.SetActive(false);
        }
        else
        {
            SetCharacter(WhiteWitch);
        }

    }

    void SetCharacter(GameObject obj)
    {
        GameObject cinemachine = GameObject.Find("CM FreeLook");
        cinemachine.GetComponent<CinemachineFreeLook>().Follow = obj.transform;
        cinemachine.GetComponent<CinemachineFreeLook>().LookAt = obj.transform;
    }
}
