using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterFight : MonoBehaviour
{
    public void EnterFightScene()
    {
        SceneManager.LoadScene("Fight"); 
    }
}
