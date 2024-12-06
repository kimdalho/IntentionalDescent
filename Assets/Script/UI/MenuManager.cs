using UnityEngine;
using Mirror;
using UnityEngine.UI;
public class MenuManager : MonoBehaviour
{
    static public MenuManager instance;

    [SerializeField]
    private Button Btn_CreateRoom;
    [SerializeField]
    private Button Btn_Join;
    [SerializeField]
    private Button Btn_Quit;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Btn_CreateRoom.onClick.AddListener(OnClickButtonCreateRoom);
        Btn_Join.onClick.AddListener(OnClickButtonJoinRoom);
        Btn_Quit.onClick.AddListener(OnClickQuit);
    }

    private void OnClickButtonCreateRoom()
    {
        
    }
    private void OnClickButtonJoinRoom()
    {

    }
    private void OnClickQuit()
    {

    }
}
