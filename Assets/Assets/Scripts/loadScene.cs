using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class loadScene : MonoBehaviour
{
    // Start is called before the first frame update
    public string editorSceneName;
    public TMP_InputField inputField;


    public void LoadSceneByEditorName(){
        LoadScene(editorSceneName);
    }

    public void LoadSceneByInputField()
    {
        LoadScene(inputField.text);
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
