using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsSceneLoader : MonoBehaviour
{
    public string nextScene = "CreditsScene";
    public void NextScene()
    {
        SceneManager.LoadScene(nextScene);
    }
}
