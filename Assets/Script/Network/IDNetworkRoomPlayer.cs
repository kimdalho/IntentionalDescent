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

    public GameObject playerCard;

    [SyncVar(hook = nameof(ReadyTextChanged))]
    public string ReadyText;

    //hook ������Ƽ ����
    //��ũ�ٷ� ����ȭ�� ������ �������� ����Ǿ�����
    //Ŭ���̾�Ʈ���� ȣ��ǰ��ϴ� �Լ��Դϴ�.

    [SyncVar(hook = nameof(ChatTextChanged))]
    public string chatText;

    private IDNetworkRoomManager roomManager;

    public override void Start()
    {
        base.Start();
        CreatePlayer();
    }

    
    public void CreatePlayer()
    {
        roomManager = IDNetworkRoomManager.singleton as IDNetworkRoomManager;
        playerCard = Instantiate(roomManager.spawnPrefabs[0]);

        foreach (NetworkRoomPlayer player in roomManager.roomSlots)
        {
            if (player != null)
            {
                var panel_RoomTransfrom = Panel_Room.Instance.panel_PlayerHolder.GetTransformByIndex(player.index);
                playerCard.transform.SetParent(panel_RoomTransfrom, false);
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


    //ȣ��Ʈ������ ȣ��ȴ�.(Cmd)
    //public override void OnClientExitRoom()
    //{
    //    base.OnClientEnterRoom();
    //}


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


        foreach (NetworkRoomPlayer player in roomManager.roomSlots)
        {
            if (player != null && player.isLocalPlayer == false)
            {
                var idPlayer = player as IDNetworkRoomPlayer;
                var OtherPlayerView = idPlayer.playerCard.GetComponent<Panel_PlayerView>();
                OtherPlayerView.Text_NickName.text = idPlayer.playerDisplayName;
            }
        }
    }

    public void ReadyTextChanged(string oldText, string newText) 
    {
       var localPlayerView =  playerCard.GetComponent<Panel_PlayerView>();
       localPlayerView.Text_ReadyState.text = newText;
    }

}
