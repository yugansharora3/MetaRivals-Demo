using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System.Collections.Generic;

public class OnClick : MonoBehaviour
{
    [SerializeField]
    public AudioSource TapSound;
    private string objname = null;
    private string Ape = "ape", Shiba = "shiba", Kishu = "kishu", WhiteWitch = "whiteWitch",volt = "volt";
    public GameObject playButton;
    public GameObject[] AnimatorObjects;
    private List<Animator> animators;

    private void Start()
    {
        animators = new List<Animator>();
        foreach(GameObject item in AnimatorObjects)
        {
            animators.Add(item.GetComponent<Animator>());
        }
    }
    public void OnSelect()
    {
        if(TapSound != null)
            TapSound.Play();
        setObjectName(this.gameObject);
        GameObject.Find("LightsManager").GetComponent<LightManager>().change(objname);
        OpenSideDialogBox();
    }

    public void OpenSideDialogBox()
    {
        foreach(Animator item in animators)
        {
            item.SetBool("Contracted", true);
        }
    }
    public void OnDeselect()
    {
        GameObject.Find("LightsManager").GetComponent<LightManager>().unchange();
        CloseSideDialogBox();
    }
    public void CloseSideDialogBox()
    {
        foreach(Animator item in animators)
        {
            item.SetBool("Contracted", false);
        }
    }


    void SetName(string name)
    {
        if (name == "Ape-Button")
            objname = Ape;
        else
            if (name == "Shiba-Button")
                objname = Shiba;
            else
                if (name == "Kishu-Button")
                    objname = Kishu;
                else
                    if (name == "volt-Button")
                        objname = volt;
                    else
                        objname = WhiteWitch;  //its white witch
    }
    void setObjectName(GameObject obj)
    {
        //GameObject playButton = GameObject.Find("Canvas1").transform.GetChild(5).gameObject;
        if (obj.name != "BackGround-Button")
        {
            Image playButtonImage = playButton.transform.GetChild(0).gameObject.GetComponent<Image>();
            playButtonImage.sprite = playButton.GetComponent<OnPlay>().Selected;
            SetName(obj.name);
        }
        else
        {
            objname = null;
            Image playButtonImage = playButton.transform.GetChild(0).gameObject.GetComponent<Image>();
            playButtonImage.sprite = playButton.GetComponent<OnPlay>().UnSelected;
        }
        PlayerPrefs.SetString("Chosen-Character", objname);
    }
    
}
