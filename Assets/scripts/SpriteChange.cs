using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpriteChange : MonoBehaviour
{
    GameObject canvas;
    [SerializeField]
    Sprite DullButton;
    [SerializeField]
    Sprite GlowingButton;
    [SerializeField]
    Sprite Default;
    [SerializeField]
    Sprite First;
    [SerializeField]
    Sprite Second;
    [SerializeField]
    Sprite Third;
    [SerializeField]
    Sprite Fourth;
    [SerializeField]
    AudioSource TapSound;

    public string objectName, Ape = "ape", Shiba = "shiba", Kushi = "kushi", WhiteWitch = "whiteWitch";

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas1");
    }

    public void OnClick(Sprite sprite)
    {
        TapSound.Play();
        Image image = canvas.transform.GetChild(1).GetComponent<Image>();
        Image PlayButtonImage = GameObject.Find("PlayButton").GetComponent<Image>();
        image.sprite = sprite;
        if(sprite == First)
        {
            objectName = Ape;
            PlayButtonImage.sprite = GlowingButton;
        }
        else
        {
            if(sprite == Second)
            {
                objectName = Shiba;
                PlayButtonImage.sprite = GlowingButton;
            }
            else
            {
                if (sprite == Third)
                {
                    objectName = Kushi;
                    PlayButtonImage.sprite = GlowingButton;
                }
                else if (sprite == Fourth)
                {
                    objectName = WhiteWitch;
                    PlayButtonImage.sprite = GlowingButton;
                }
                else
                {
                    objectName = null;
                    PlayButtonImage.sprite = DullButton;
                    TapSound.Stop();
                }
                    
            }
        }
    }

    
    public void Play()
    {
        TapSound.Play();
        if(objectName != null)
        {
            PlayerPrefs.SetString("Chosen-Character", objectName);
            SceneManager.LoadScene("walking");
            
        }
    }
    
}
