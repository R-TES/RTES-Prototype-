using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
 

public class LevelControllerScript : MonoBehaviour
{
    private string mainMenuScene = "Intro"; 
    public void LeaveToMainMenu()
    {
        PhotonNetwork.Disconnect();
        LoadScene(mainMenuScene); 
    }


    private void LoadScene(string name)
    {
        if (Application.CanStreamedLevelBeLoaded(name))
        {
            SceneManager.LoadScene(name);
        }
        else
        {
            Debug.LogError("Invalid Name Given, or scene not added to Build Menu List.");
        }
    }
}
