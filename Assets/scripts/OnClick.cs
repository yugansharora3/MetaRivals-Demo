using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OnClick : MonoBehaviour
{
    [SerializeField]
    public Sprite Selected;
    [SerializeField]
    public Sprite UnSelected;
    [SerializeField]
    public AudioSource TapSound;
    static string objname = null;
    private string Ape = "ape", Shiba = "shiba", Kishu = "kishu", WhiteWitch = "whiteWitch";
    public void OnSelect()
    {
        if(TapSound != null)
            TapSound.Play();
        GameObject backgroundButton = GameObject.Find("Canvas1").transform.GetChild(1).gameObject;
        Image backgroundimage = backgroundButton.transform.GetChild(0).gameObject.GetComponent<Image>();
        backgroundimage.sprite = backgroundButton.GetComponent<OnClick>().UnSelected;
        GameObject child = this.gameObject.transform.GetChild(0).gameObject;
        Image image = child.GetComponent<Image>();
        image.sprite = Selected;
        setObjectName(this.gameObject);
    }
    public void OnDeselect()
    {
        GameObject child = this.gameObject.transform.GetChild(0).gameObject;
        Image image = child.GetComponent<Image>();
        image.sprite = UnSelected;
    }
    void setObjectName(GameObject obj)
    {
        GameObject playButton = GameObject.Find("Canvas1").transform.GetChild(6).gameObject;
        if (obj.name != "BackGround-Button")
        {
            Image playButtonImage = playButton.transform.GetChild(0).gameObject.GetComponent<Image>();
            playButtonImage.sprite = playButton.GetComponent<OnClick>().Selected;
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
                        //its white witch
                        objname = WhiteWitch;
                    }
                }
            }
        }
        else
        {
            objname = null;
            Image playButtonImage = playButton.transform.GetChild(0).gameObject.GetComponent<Image>();
            playButtonImage.sprite = playButton.GetComponent<OnClick>().UnSelected;
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
