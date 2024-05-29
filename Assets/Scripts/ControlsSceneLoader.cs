using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlsSceneLoader : MonoBehaviour
{
    public string nextScene = "ControlsScene";
    public void NextScene()
    {
        SceneManager.LoadScene(nextScene);
    }
}