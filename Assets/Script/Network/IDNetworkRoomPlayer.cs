using UnityEngine;
using Mirror;
using UnityEngine.Windows;
using TMPro;
public class IDNetworkRoomPlayer : NetworkRoomPlayer
{
    //프로퍼티 역할 네트워크를 통해 동기화할 변수를 지정한다.
    [SyncVar(hook = nameof(PlayerDisplayNameChanged))]
    public string playerDisplayName;

    public GameObject playerCard;

    [SyncVar(hook = nameof(ReadyTextChanged))]
    public string ReadyText;

    //hook 프로퍼티 설명
    //싱크바로 동기화된 변수가 서버에서 변경되었을때
    //클라이언트에서 호출되게하는 함수입니다.

    [SyncVar(hook = nameof(ChatTextChanged))]
    public string chatText;


    public override void Start()
    {
        base.Start();

        IDNetworkRoomManager roomManager = IDNetworkRoomManager.singleton as IDNetworkRoomManager;
        playerCard = Instantiate(roomManager.spawnPrefabs[0]);
        var panel_RoomTransfrom = Panel_Room.Instance.panel_PlayerView.transform;

        playerCard.transform.SetParent(panel_RoomTransfrom, false);
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
    //Command 프로퍼티 설명
    //클라이언트에서 커맨드 프로퍼티가 붙은 함수를 호출하면
    //호스트에만 커맨드 프로퍼티가 붙은 함수를 처리한다.
    //중요한건 클라이언트는 호스트에게 요청할 정보를 파라미터로 알려준다.

    [Command]
    public void PostChat(string chatPlayerName, string Text)
    {
        chatText =  $"{chatPlayerName} : {Text}";
    }

    private void ChatTextChanged(string oldText, string newText)
    {
        //GameObject newBox = Instantiate(Panel_Room.Instance.chatBox);
        //newBox.GetComponent<ChatBox>().Setup();
        //newBox.GetComponent<ChatBox>().textMeshPro.text = newText;
        Panel_Room.Instance.roomChatService.SendChatMessage(newText);
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
