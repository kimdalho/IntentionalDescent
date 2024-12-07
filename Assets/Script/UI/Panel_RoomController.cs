using System.Collections.Generic;
using System.Linq;
using Mirror;
using UnityEngine;
using UnityEngine.UI;
public class Panel_RoomController : MonoBehaviour
{
    [SerializeField]
    private Button Btn_Ready;

    public Button btn_RoomExit;

    private void Start()
    {
        Btn_Ready.onClick.AddListener(OnClickReady);
        btn_RoomExit.onClick.AddListener(OnClickRoomExit);
    }


    private void OnClickReady()
    {
        var roomManager = IDNetworkRoomManager.singleton as NetworkRoomManager;

        foreach (NetworkRoomPlayer player in roomManager.roomSlots)
        {
            if (player != null && player.isLocalPlayer)
            {
                var idPlayer = player as IDNetworkRoomPlayer;
                var localPlayerView = idPlayer.playerCard.GetComponent<Panel_PlayerView>();
                

                if (player.readyToBegin)
                {
                    Debug.Log("레디 취소");
                    player.CmdChangeReadyState(false);
                    localPlayerView.Text_ReadyState.text = "Cancel";

                }
                else
                {
                    Debug.Log("레디!");
                    player.CmdChangeReadyState(true);
                    localPlayerView.Text_ReadyState.text = "Ready!";
                }
            }
        }   
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
