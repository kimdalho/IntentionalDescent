using UnityEngine;
using Mirror;
public class IDNetworkRoomPlayer : NetworkRoomPlayer
{
    //������Ƽ ���� ��Ʈ��ũ�� ���� ����ȭ�� ������ �����Ѵ�.
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
    //��ũ�ٷ� ����ȭ�� ������ �������� ����Ǿ�����
    //Ŭ���̾�Ʈ���� ȣ��ǰ��ϴ� �Լ��Դϴ�.
    
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
