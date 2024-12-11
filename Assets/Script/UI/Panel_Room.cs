using UnityEngine;

public class Panel_Room : MonoBehaviour
{
    public static Panel_Room Instance;

    public GameObject panel_PlayerView;
    
    public IRoomChatService roomChatService;

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
