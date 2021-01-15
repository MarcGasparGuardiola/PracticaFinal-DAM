﻿
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    

    public void goToMainMenu()
    {
        SceneManager.LoadScene(sceneBuildIndex: 2);
    }

    public void goToFollowRoute()
    {
        SceneManager.LoadScene(sceneBuildIndex: 9);
    }
}