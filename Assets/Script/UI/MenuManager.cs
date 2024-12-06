using UnityEngine;
using Mirror;
using UnityEngine.UI;
using TMPro;
public class MenuManager : MonoBehaviour
{
    static public MenuManager instance;

    [Header("���� �޴�")]
    [SerializeField]
    private Image Panel_Main;
    [SerializeField]
    private Button Btn_CreateRoom;
    [SerializeField]
    private Button Btn_Join;
    [SerializeField]
    private Button Btn_Quit;
    [Header("����")]
    [SerializeField]
    private Image Panel_Join;
    [SerializeField]
    private TMP_InputField inputField_NickName;
    [SerializeField]
    private TMP_InputField inputField_RoomName;
    [SerializeField]
    private Button Btn_RoomJoin;
    [SerializeField]
    private Button Btn_Back;



    //ĳ��
    private NetworkManager roomManager;

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

        //���θ޴�
        Btn_CreateRoom.onClick.AddListener(OnClickButtonCreateRoom);
        Btn_Join.onClick.AddListener(OnClickButtonJoinMenu);
        Btn_Quit.onClick.AddListener(OnClickQuit);

        //���θ޴�
        Btn_RoomJoin.onClick.AddListener(OnClickButtonRoomJoin);
        Btn_Back.onClick.AddListener(OnClickButtonBack);
    }

    private void Start()
    {
        roomManager = IDNetworkRoomManager.singleton;
    }

    private void OnClickButtonCreateRoom()
    {
        
        if (roomManager != null)
        {
            roomManager.StartHost();
        }
        else
        {
            Debug.LogError($"Not found NetworkManager");
        }
        
    }

    private void OnClickButtonJoinMenu()
    {
        Panel_Join.gameObject.SetActive(true);
        Panel_Main.gameObject.SetActive(false);
    }

    private void OnClickButtonRoomJoin()
    {
        if (inputField_NickName.text != "")
        {
            if (roomManager != null)
            {
                roomManager.StartClient();
            }
            else
            {
                Debug.LogError($"Not found NetworkManager");
            }
        }
    }

    private void OnClickButtonBack()
    {
        Panel_Main.gameObject.SetActive(true);
        Panel_Join.gameObject.SetActive(false);
        inputField_RoomName.text = "";
        inputField_NickName.text = "";

    }


    private void OnClickQuit()
    {
        Application.Quit();
    }
}
