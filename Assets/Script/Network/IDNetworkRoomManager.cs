using UnityEngine;
using Mirror;
/// <summary>
/// 
/// </summary>
public class IDNetworkRoomManager : NetworkRoomManager
{
    bool showStartButton;

    //이렇게 써도 괜찮은가..
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

        Debug.Log($"지금 몇명있냐 {roomSlots.Count}");
    }


    /// <summary>
    /// 서버에서 호출되어 처리되고 클라와 동기화한다.
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
