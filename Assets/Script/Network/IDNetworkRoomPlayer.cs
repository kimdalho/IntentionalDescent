using UnityEngine;
using Mirror;
public class IDNetworkRoomPlayer : NetworkRoomPlayer
{
    //������Ƽ ���� ��Ʈ��ũ�� ���� ����ȭ�� ������ �����Ѵ�.
    [SyncVar]
    public string playerDisplayName;
    

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
}
