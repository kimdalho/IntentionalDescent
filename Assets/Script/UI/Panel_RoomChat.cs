using Mirror;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering.Universal;

public interface IRoomChatService
{
    public void SendChatMessage(string message);
}

public class Panel_RoomChat : MonoBehaviour , IRoomChatService
{
    [SerializeField]
    private TMP_InputField input_chat;
    [SerializeField]
    private GameObject chatBox;
    [SerializeField]
    private Transform chatContent;


    private void Update()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.Return))
        {
            var roomManager = IDNetworkRoomManager.singleton as NetworkRoomManager;

            foreach (NetworkRoomPlayer player in roomManager.roomSlots)
            {
                if (player != null && player.isLocalPlayer)
                {
                    var idPlayer = player as IDNetworkRoomPlayer;
                    var localPlayerView = idPlayer.RoomPlayerHud.GetComponent<Panel_RoomPlayerHud>();

                    idPlayer.PostChat(idPlayer.playerDisplayName, input_chat.text);
                    input_chat.text = string.Empty;
                }
            }
        }
    }

    public void SendChatMessage(string Text)
    {
        GameObject newBox = Instantiate(chatBox);
        newBox.transform.SetParent(chatContent);
        newBox.GetComponent<ChatBox>().SetText(Text);
    }
}
