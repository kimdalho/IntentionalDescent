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
    /// FixedUpdateNetwork�� ��� �ùķ��̼� ƽ�� ȣ��˴ϴ�. Fusion�� ������ Ȯ�ε� ��Ʈ��ũ ���¸� ������ ���� �ش� ƽ���� ����(������) ���� ƽ���� ��ùķ��̼��ϱ� ������ ������ �����Ӵ� ���� �� �߻��� �� �ֽ��ϴ�.
    ///FixedUpdateNetwork�� �Է��� �Է��ϸ� �� ƽ�� �´� �Է��� �Ǵ��� Ȯ���� �� �ֽ��ϴ�. Fusion�� �ش� ƽ�� ���� �Է��� ���� ���� �� �ִ� GetInput()�̶�� �̸��� ������ �޼ҵ带 �����մϴ�. �Է��� �Ϸ�Ǹ� NetworkCharacterController�� ȣ���Ͽ� ���� �������� �ƹ�Ÿ ��ȯ�� �����մϴ�.
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
