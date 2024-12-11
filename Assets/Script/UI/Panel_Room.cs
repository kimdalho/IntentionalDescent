using UnityEngine;

public class Panel_Room : MonoBehaviour
{
    public static Panel_Room Instance;

    public Panel_PlayersHudHolder panel_PlayerHolder;
    
    public IRoomChatService roomChatService;

    public Transform[] roomPlayerRoot;

    [SerializeField]
    private Panel_RoomChat panel_RoomChat;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Instance = this;
        }

        roomChatService = panel_RoomChat.GetComponent<IRoomChatService>();

    }
}
