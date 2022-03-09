using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restart : MonoBehaviour
{
    public void restartGame(){
        Debug.Log("Quiting");
        SceneManager.LoadScene("MainMenu1");
    }
}
