using UnityEngine;

public class SceneSetting : MonoBehaviour
{
    void Awake() => Screen.SetResolution(600, 400, false);
}
