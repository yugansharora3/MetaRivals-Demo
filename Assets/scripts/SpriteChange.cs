using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using Cinemachine;

public class SpriteChange : MonoBehaviour
{
    GameObject canvas;
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
        
        Image image = canvas.transform.GetChild(0).GetComponent<Image>();
        
        image.sprite = sprite;
        if(sprite == First)
        {
            objectName = Ape;
        }
        else
        {
            if(sprite == Second)
            {
                objectName = Shiba;
            }
            else
            {
                if (sprite == Third)
                {
                    objectName = Kushi;
                }
                else if (sprite == Fourth)
                {
                    objectName = WhiteWitch;
                }
                else
                    objectName = null;
                    
            }
        }
    }

    
    public void Play()
    {
        if(objectName != null)
        {
            PlayerPrefs.SetString("Chosen-Character", objectName);
            SceneManager.LoadScene("walking");
            
        }
    }
    
}
