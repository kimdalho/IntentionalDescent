using UnityEngine;
using TMPro;
using UnityEngine.UI;

public interface IRoomPlayerHudService
{
    public void SetTMPNickNameText(string str);

    public void SetTMPReadyText(string str);

    public void SetActiveHostRedDot(bool active);
}

public class Panel_RoomPlayerHud : MonoBehaviour , IRoomPlayerHudService
{
    [SerializeField]
    private Image Img_RedDot;
    [SerializeField]
    private TextMeshProUGUI Text_NickName;
    [SerializeField]
    private TextMeshProUGUI Text_ReadyState;

    public void SetTMPNickNameText(string str)
    {
        Text_NickName.text = str;
    }

    public void SetTMPReadyText(string str)
    {
        Text_ReadyState.text = str;
    }

    public void SetActiveHostRedDot(bool active)
    {
        Img_RedDot.gameObject.SetActive(active);
    }
}
