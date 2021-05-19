using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.UI;

public class UI_Connect : MonoBehaviour
{
    NetworkManager manager;
    [SerializeField]
    GameObject Panel;
    [SerializeField]
    TMP_InputField IP_address;
    [SerializeField]
    GameObject BTN_Stop;
    [SerializeField]
    TMP_Text TXT_Message;


    void Awake()
    {
        manager = GetComponent<NetworkManager>();
    }

    private void Start()
    {
        if (Application.isBatchMode)
        {
            manager.StartServer();
        }
        else
        {
            Panel.SetActive(true);
            IP_address.text = "45.32.126.93";
        }
    }

    public void BTN_OnHostClick()
    {
        manager.StartHost();
        Panel.SetActive(false);
        BTN_Stop.SetActive(true);
    }

    public void BTN_OnClientClick()
    {
        manager.networkAddress = IP_address.text.Trim();
        manager.StartClient();

        OnConnecting();
    }

    public void BTN_OnStopClick()
    {
        if (NetworkServer.active && NetworkClient.isConnected)
        {
            manager.StopHost();
        }
        else if (NetworkClient.isConnected)
        {
            manager.StopClient();
        }

        OnDisconnect();
    }

    public void OnConnecting()
    {
        Panel.SetActive(false);
        TXT_Message.text = "Connecting to: " + IP_address.text;
    }

    public void OnConnected()
    {
        BTN_Stop.SetActive(true);
        TXT_Message.text = "";
    }

    public void OnDisconnect()
    {
        TXT_Message.text = "";
        Panel.SetActive(true);
        BTN_Stop.SetActive(false);
        UI_KnifeSelect.HidePanel();
    }
}
