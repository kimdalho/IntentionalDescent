using UnityEngine;
using Mirror;
public class IDNetworkRoomPlayer : NetworkRoomPlayer
{
    //프로퍼티 역할 네트워크를 통해 동기화할 변수를 지정한다.
    [SyncVar]
    public string playerDisplayName;
    

    //hook
    //싱크바로 동기화된 변수가 서버에서 변경되었을때
    //클라이언트에서 호출되게하는 함수입니다.
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
