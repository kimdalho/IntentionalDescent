using UnityEngine;
using Mirror;
public class IDNetworkRoomPlayer : NetworkRoomPlayer
{
    //프로퍼티 역할 네트워크를 통해 동기화할 변수를 지정한다.
    [SyncVar(hook = nameof(PlayerDisplayNameChanged))]
    public string playerDisplayName;

    public GameObject playerCard;

    [SyncVar(hook = nameof(ReadyTextChanged))]
    public string ReadyText;

    public override void Start()
    {
        base.Start();

        IDNetworkRoomManager roomManager = IDNetworkRoomManager.singleton as IDNetworkRoomManager;
        playerCard = Instantiate(roomManager.spawnPrefabs[0]);
        var panel_RoomTransfrom = Panel_Room.Instance.transform;

        playerCard.transform.SetParent(panel_RoomTransfrom, false);
        NetworkServer.Spawn(playerCard);

        CmdChangeNickNameText(roomManager.playerName);


        foreach (NetworkRoomPlayer player in roomManager.roomSlots)
        {
            if (player != null && player.isLocalPlayer == false)
            {
                var idPlayer = player as IDNetworkRoomPlayer;
                var localPlayerView = idPlayer.playerCard.GetComponent<Panel_PlayerView>();
                localPlayerView.Text_NickName.text = idPlayer.playerDisplayName;
            }
        }

    }


    //hook
    //싱크바로 동기화된 변수가 서버에서 변경되었을때
    //클라이언트에서 호출되게하는 함수입니다.
    
    [Command]
    public void CmdSendName(string playerName)
    {
        playerDisplayName = playerName;
    }

    public override void OnClientExitRoom()
    {
        base.OnClientEnterRoom();
        Destroy(playerCard);
    }
    [Command]
    public void CmdChangeNickNameText(string Text)
    {
        playerDisplayName = Text;
    }


    [Command]
    public void CmdChangeReadyText(string Text)
    {
        ReadyText = Text;
    }

    //Hook

    public void PlayerDisplayNameChanged(string oldText, string newText)
    {
        var localPlayerView = playerCard.GetComponent<Panel_PlayerView>();
        localPlayerView.Text_NickName.text = newText;
    }

    public void ReadyTextChanged(string oldText, string newText) 
    {
       var localPlayerView =  playerCard.GetComponent<Panel_PlayerView>();
       localPlayerView.Text_ReadyState.text = newText;
    }

}
