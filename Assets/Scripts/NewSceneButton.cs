using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewSceneButton : MonoBehaviour
{
    public string nextScene = "Level 1";
    public void NextScene()
    {
        SceneManager.LoadScene(nextScene);
    }
}
