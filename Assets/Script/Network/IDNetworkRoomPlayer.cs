using UnityEngine;
using Mirror;
using TMPro;
using NUnit.Framework;
using System.Collections.Generic;
public class IDNetworkRoomPlayer : NetworkRoomPlayer
{
    //������Ƽ ���� ��Ʈ��ũ�� ���� ����ȭ�� ������ �����Ѵ�.
    [SyncVar(hook = nameof(PlayerDisplayNameChanged))]
    public string playerDisplayName;

    public GameObject RoomPlayerHud;

    [SyncVar(hook = nameof(ReadyTextChanged))]
    public string ReadyText;


    //hook ������Ƽ ����
    //��ũ�ٷ� ����ȭ�� ������ �������� ����Ǿ�����
    //Ŭ���̾�Ʈ���� ȣ��ǰ��ϴ� �Լ��Դϴ�.

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

    //Command ������Ƽ ����
    //Ŭ���̾�Ʈ���� Ŀ�ǵ� ������Ƽ�� ���� �Լ��� ȣ���ϸ�
    //ȣ��Ʈ���� Ŀ�ǵ� ������Ƽ�� ���� �Լ��� ó���Ѵ�.
    //�߿��Ѱ� Ŭ���̾�Ʈ�� ȣ��Ʈ���� ��û�� ������ �Ķ���ͷ� �˷��ش�.

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
