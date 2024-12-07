using UnityEngine;
using Mirror;
public class IDNetworkRoomPlayer : NetworkRoomPlayer
{
    //������Ƽ ���� ��Ʈ��ũ�� ���� ����ȭ�� ������ �����Ѵ�.
    [SyncVar]
    public string playerDisplayName;

    public GameObject playerCard;

    public override void Start()
    {
        base.Start();

        var roomManager = IDNetworkRoomManager.singleton;
        playerCard = Instantiate(roomManager.spawnPrefabs[0]);
        var panel_RoomTransfrom = Panel_Room.Instance.transform;

        playerCard.transform.SetParent(panel_RoomTransfrom, false);
        NetworkServer.Spawn(playerCard);
    }


    //hook
    //��ũ�ٷ� ����ȭ�� ������ �������� ����Ǿ�����
    //Ŭ���̾�Ʈ���� ȣ��ǰ��ϴ� �Լ��Դϴ�.
    [SyncVar(hook = nameof(SetPlayerNickName_Hook))]
    public string playerNickName;

    private void SetPlayerNickName_Hook(string oldNickName, string newNickname)
    {
        playerDisplayName = newNickname;
    }



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
}
