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

    //�������� ���� ������ Ŭ���̾�Ʈ�� ����
    public override void OnRoomServerConnect(NetworkConnectionToClient conn)
    {
        base.OnRoomServerConnect(conn);

        //���� ���ö����� ���������鿡�� ����
        //var player = Instantiate(spawnPrefabs[0]);
        //NetworkServer.Spawn(player);
        
    }

    //GUI ��ŸƮ ��ư
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
