using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OnPlay : MonoBehaviour
{
    [SerializeField]
    public Sprite Selected;
    [SerializeField]
    public Sprite UnSelected;
    [SerializeField]
    public AudioSource TapSound;


    public void Play()
    {
        string objname = PlayerPrefs.GetString("Chosen-Character");
        TapSound.Play();
        if (objname != null && objname != "")
        {
            SceneManager.LoadScene("walking");
        }
    }
}
