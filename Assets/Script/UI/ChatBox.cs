using UnityEngine;
using TMPro;
using Mirror;
using UnityEngine.UI;
public class ChatBox : NetworkBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textMeshPro;

    public void SetText(string text)
    {
        textMeshPro.text = text;
    }

}
