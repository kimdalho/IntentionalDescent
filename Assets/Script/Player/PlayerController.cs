using Fusion;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{

    private NetworkCharacterController _cc;

    [SerializeField]
    private float speed = 5f;
    private void Awake()
    {
        _cc = GetComponent<NetworkCharacterController>();
    }
    /// <summary>
    /// FixedUpdateNetwork는 모든 시뮬레이션 틱에 호출됩니다. Fusion이 오래된 확인된 네트워크 상태를 적용한 다음 해당 틱에서 현재(예측된) 로컬 틱까지 재시뮬레이션하기 때문에 렌더링 프레임당 여러 번 발생할 수 있습니다.
    ///FixedUpdateNetwork에 입력을 입력하면 각 틱에 맞는 입력이 되는지 확인할 수 있습니다. Fusion은 해당 틱에 대한 입력을 쉽게 얻을 수 있는 GetInput()이라는 이름의 간단한 메소드를 제공합니다. 입력이 완료되면 NetworkCharacterController를 호출하여 실제 움직임을 아바타 변환에 적용합니다.
    /// </summary>
    public override void FixedUpdateNetwork() 
    {
        if (GetInput(out NetworkInputData data))
        {
            data.direction.Normalize();
            _cc.Move(speed * data.direction * Runner.DeltaTime);
        }
    }
}
