using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;
using Photon.Pun;
public class SceneControl : MonoBehaviour
{
  
    public string editorSceneName;
    public TMP_InputField inputField;


    public void LoadSceneByEditorName(){
        LoadScene(editorSceneName);
    }

    public void LoadSceneByInputField()
    {
        LoadScene(inputField.text);
    }

    public void LoadScene(string name)
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

    public void DisconnectPhoton()
    {
        PhotonNetwork.Disconnect();
    }
}
