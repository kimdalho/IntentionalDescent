using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class SceneSetting : MonoBehaviour
{
    [SerializeField]
    [Header("ÇØ»óµµ")]
    private int ScreenWidth ,ScreenHegiht;
    [SerializeField]
    private bool fullScreen;

    private void Awake() => Screen.SetResolution(ScreenWidth, ScreenHegiht, fullScreen);


}
