using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    public string previousScene = "";

    public void backToPreviousScene()
    {
        SceneManager.LoadScene(previousScene);
    }
}
