using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OnClick : MonoBehaviour
{
    [SerializeField]
    public AudioSource TapSound;
    static string objname = null;
    private string Ape = "ape", Shiba = "shiba", Kishu = "kishu", WhiteWitch = "whiteWitch",volt = "volt";
    public void OnSelect()
    {
        if(TapSound != null)
            TapSound.Play();
        setObjectName(this.gameObject);
        GameObject.Find("LightsManager").GetComponent<LightManager>().change(objname);
    }
    public void OnDeselect()
    {
        GameObject.Find("LightsManager").GetComponent<LightManager>().unchange();
    }
    void setObjectName(GameObject obj)
    {
        GameObject playButton = GameObject.Find("Canvas1").transform.GetChild(5).gameObject;
        if (obj.name != "BackGround-Button")
        {
            Image playButtonImage = playButton.transform.GetChild(0).gameObject.GetComponent<Image>();
            playButtonImage.sprite = playButton.GetComponent<OnPlay>().Selected;
            if (obj.name == "Ape-Button")
            {
                 objname = Ape;
            }
            else
            {
                if (obj.name == "Shiba-Button")
                {
                    objname = Shiba;
                }
                else
                {
                    if (obj.name == "Kishu-Button")
                    {
                        objname = Kishu;
                    }
                    else
                    {
                        if (obj.name == "volt-Button")
                        {
                            objname = volt;
                        }
                        else
                        {
                            //its white witch
                            objname = WhiteWitch;
                        }
                    }
                }
            }
        }
        else
        {
            objname = null;
            Image playButtonImage = playButton.transform.GetChild(0).gameObject.GetComponent<Image>();
            playButtonImage.sprite = playButton.GetComponent<OnPlay>().UnSelected;
        }
    }
    public void Play()
    {
        TapSound.Play();
        if (objname != null)
        {
            PlayerPrefs.SetString("Chosen-Character", objname);
            SceneManager.LoadScene("walking");
        }
    }
}
