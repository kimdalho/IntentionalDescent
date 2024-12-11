using UnityEngine;
using Mirror;
using TMPro;
using NUnit.Framework;
using System.Collections.Generic;
public class IDNetworkRoomPlayer : NetworkRoomPlayer
{
    //프로퍼티 역할 네트워크를 통해 동기화할 변수를 지정한다.
    [SyncVar(hook = nameof(PlayerDisplayNameChanged))]
    public string playerDisplayName;

    public GameObject RoomPlayerHud;

    [SyncVar(hook = nameof(ReadyTextChanged))]
    public string ReadyText;


    //hook 프로퍼티 설명
    //싱크바로 동기화된 변수가 서버에서 변경되었을때
    //클라이언트에서 호출되게하는 함수입니다.

    [SyncVar(hook = nameof(ChatTextChanged))]
    public string chatText;

    private IDNetworkRoomManager roomManager;

    private void Awake()
    {
        roomManager = IDNetworkRoomManager.singleton as IDNetworkRoomManager;
    }

    public override void Start()
    {
        base.Start();
        CreatePlayer();
    }

    public void CreatePlayer()
    {
        RoomPlayerHud = Instantiate(roomManager.spawnPrefabs[0]);

        foreach (IDNetworkRoomPlayer player in roomManager.roomSlots)
        {
            if (player != null)
            {
                player.transform.position = Panel_Room.Instance.roomPlayerRoot[player.index].position;
                
                var panel_RoomTransfrom = Panel_Room.Instance.panel_PlayerHolder.GetTransformByIndex(player.index);
                RoomPlayerHud.transform.SetParent(panel_RoomTransfrom, false);
                RoomPlayerHud.GetComponent<IRoomPlayerHudService>().SetActiveHostRedDot(false);    

                if(player.index == 0)
                {
                    player.RoomPlayerHud.GetComponent<IRoomPlayerHudService>().SetActiveHostRedDot(true);
                }
            }
        }
        CmdChangeNickNameText(roomManager.playerName);
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
        Panel_Room.Instance.roomChatService.SendChatMessage(newText);
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
        foreach (IDNetworkRoomPlayer idPlayer in roomManager.roomSlots)
        {
            if (idPlayer != null && idPlayer.isLocalPlayer == false)
            {
                var OtherRoomPlayerHud = idPlayer.RoomPlayerHud.GetComponent<IRoomPlayerHudService>();
                OtherRoomPlayerHud.SetTMPNickNameText(idPlayer.playerDisplayName);
            }
            else if(idPlayer != null && idPlayer.isLocalPlayer == true)
            {
                var localRoomPlayerHud = this.RoomPlayerHud.GetComponent<IRoomPlayerHudService>();
                localRoomPlayerHud.SetTMPNickNameText(newText);
            }
        }
    }

    public void ReadyTextChanged(string oldText, string newText) 
    {
       var localPlayerView =  RoomPlayerHud.GetComponent<IRoomPlayerHudService>();
       localPlayerView.SetTMPReadyText(newText);
    }

}
