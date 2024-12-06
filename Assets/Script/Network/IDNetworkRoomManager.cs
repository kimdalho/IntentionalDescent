using UnityEngine;
using Mirror;

/// <summary>
/// 
/// </summary>
public class IDNetworkRoomManager : NetworkRoomManager
{

    bool showStartButton;
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

    //서버에서 새로 접속한 클라이언트를 감지
    public override void OnRoomServerConnect(NetworkConnectionToClient conn)
    {
        base.OnRoomServerConnect(conn);

        //새로 들어올때마다 스폰프리펩에서 생성
        //var player = Instantiate(spawnPrefabs[0]);
        //NetworkServer.Spawn(player);
        
    }

    //GUI 스타트 버튼
    public override void OnGUI()
    {
        base.OnGUI();

        if (allPlayersReady && showStartButton && GUI.Button(new Rect(150, 300, 120, 20), "START GAME"))
        {
            // set to false to hide it in the game scene
            showStartButton = false;

            ServerChangeScene(GameplayScene);
        }
    }
}
