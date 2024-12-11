using UnityEngine;
using Mirror;
/// <summary>
/// 
/// </summary>
public class IDNetworkRoomManager : NetworkRoomManager
{
    bool showStartButton;

    //�̷��� �ᵵ ��������..
    public string playerName;
    public override void OnRoomServerPlayersReady()
    {
        // calling the base method calls ServerChangeScene as soon as all players are in Ready state.
        if (Utils.IsHeadless())
        {
            base.OnRoomServerPlayersReady();
        }
        else
        {
            showStartButton = true;
        }
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);

        Debug.Log($"���� ����ֳ� {roomSlots.Count}");
    }


    /// <summary>
    /// �������� ȣ��Ǿ� ó���ǰ� Ŭ��� ����ȭ�Ѵ�.
    /// </summary>
    /// <param name="conn"></param>
    public override void OnRoomServerDisconnect(NetworkConnectionToClient conn)
    {
        base.OnRoomServerDisconnect(conn);

        IDNetworkRoomPlayer roomPlayer = conn.identity.GetComponent<IDNetworkRoomPlayer>();
        Destroy(roomPlayer.RoomPlayerHud);
    }

    public void GameStart()
    {
        if(allPlayersReady)
        {
            showStartButton = false;
            ServerChangeScene(GameplayScene);
        }
        
    }
}
