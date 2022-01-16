using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Chat;

public class IngameChat : MonoBehaviour{
    ChatClient chatClient;

    void Start(){
        
    }
}


/*
public class IngameChat : NetworkBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject chatUI = null;
    [SerializeField] private TMP_Text chatText = null;
    [SerializeField] private TMP_InputField inputtext = null;

    private static event Action<string> OnMessage; 

    public override void OnStartAuthority()
    {
        chatUI.SetActive(true);
        OnMessage += HandleNewMessage; 
    }

    // Update is called once per frame
    private void OnDestroy()
    {
        OnMessage-= HandleNewMessage ;
    }
    private void HandleNewMessage(string message){
        chatText.text += message;
    }

    [Client]
    public void SendMessage(string message){
        if(!inputtext.GetKeyDown(KeyCode.Return)) {return;}
        if(message==""){return;};
        CmdSendMessage(inputField.text);
        inputField.text = string.Empty; 
    }

    [Command]
    private void CmdSendMessage(string message){
        RpcHandleMessage($"[User: {message}");
    }

    [ClientRpc]
    private void RpcHandleMessage(string message){
        OnMessage?.Invoke($"\n{message}");
    }

}

*/