using UnityEngine;

public class Panel_PlayersHudHolder : MonoBehaviour
{
    [SerializeField]
    private Transform[] holderTransforms;

    public Transform GetTransformByIndex(int index)
    {
        return holderTransforms[index];
    }
}
