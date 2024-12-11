using System.Collections.Generic;
using System.Linq;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
public class Panel_RoomController : MonoBehaviour
{
    [SerializeField]
    private Button Btn_Ready;

    [SerializeField]
    private Button btn_RoomExit;

    [SerializeField]
    private Button Btn_GameStart;



    private void Start()
    {
        Btn_Ready.onClick.AddListener(OnClickReady);
        btn_RoomExit.onClick.AddListener(OnClickRoomExit);
        Btn_GameStart.onClick.AddListener(OnClickGameStart);
        
    }


    private void OnClickReady()
    {
        var roomManager = IDNetworkRoomManager.singleton as NetworkRoomManager;

        foreach (NetworkRoomPlayer player in roomManager.roomSlots)
        {
            if (player != null && player.isLocalPlayer)
            {
                var idPlayer = player as IDNetworkRoomPlayer;
                var localPlayerView = idPlayer.RoomPlayerHud.GetComponent<Panel_RoomPlayerHud>();
                

                if (idPlayer.readyToBegin)
                {
                    player.CmdChangeReadyState(false);
                    idPlayer.CmdChangeReadyText("Cancel");

                }
                else
                {
                    player.CmdChangeReadyState(true);
                    idPlayer.CmdChangeReadyText("Ready");                   
                }
            }
        }   
    }

    private void OnClickGameStart()
    {
        IDNetworkRoomManager roomManager = IDNetworkRoomManager.singleton as IDNetworkRoomManager;
        roomManager.GameStart();
    }

    private void OnClickRoomExit()
    {
        var mode = NetworkManager.singleton.mode;
        if(mode == NetworkManagerMode.Host)
        {
            NetworkManager.singleton.StopHost();
        }
        else
        {
            NetworkManager.singleton.StopClient();
        }
        NetworkManager.singleton.StopHost();
    }
      
}
