using UnityEngine;
using TMPro;
using Mirror;
using UnityEngine.UI;
public class ChatBox : NetworkBehaviour
{

    public void Setup()
    {
        this.gameObject.transform.SetParent(Panel_Room.Instance.viewport.transform);
    }
    [SerializeField]
    public TextMeshProUGUI textMeshPro;

    public void SetText(string name, string text)
    {
        textMeshPro.text = $"{name} : {text}";
    }

}
