using UnityEngine;
using Mirror;

/// <summary>
/// 
/// </summary>
public class IDNetworkRoomManager : NetworkRoomManager
{

    bool showStartButton;
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

    //서버에서 새로 접속한 클라이언트를 감지
    public override void OnRoomServerConnect(NetworkConnectionToClient conn)
    {
        base.OnRoomServerConnect(conn);
    }
    public override void OnRoomClientSceneChanged()
    {
        base.OnRoomClientSceneChanged();


    }


    //public override void OnHost

    //GUI 스타트 버튼 테스트
    //public override void OnGUI()
    //{
    //    base.OnGUI();

    //    if (allPlayersReady && showStartButton && GUI.Button(new Rect(150, 300, 120, 20), "START GAME"))
    //    {
    //        // set to false to hide it in the game scene
    //        showStartButton = false;

    //        ServerChangeScene(GameplayScene);
    //    }
    //}
    
    public void GameStart()
    {
        if(allPlayersReady)
        {
            showStartButton = false;
            ServerChangeScene(GameplayScene);
        }
        
    }
}
