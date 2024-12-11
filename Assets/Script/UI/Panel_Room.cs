using TMPro;
using UnityEngine;

public class Panel_Room : MonoBehaviour
{
    public static Panel_Room Instance;

    public TMP_InputField input;
    public GameObject panel_PlayerView;
    public GameObject chatBox;
    public GameObject viewport;



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
    }
}
