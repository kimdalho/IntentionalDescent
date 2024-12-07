using UnityEngine;

public class Panel_Room : MonoBehaviour
{
    public static Panel_Room Instance;
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
