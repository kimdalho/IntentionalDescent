using UnityEngine;
using Mirror;
using UnityEngine.Windows;
using TMPro;
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
