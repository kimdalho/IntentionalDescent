using Mirror;
using UnityEngine;

public class NetworkPlayerController : NetworkBehaviour
{
    [Header("ī�޶� �ʱ� ��ġ")]
    [SerializeField]
    private Vector3 CameraLocalPosition;
    [SerializeField]
    private int CameraLocaleulerAnglesX;

    public override void OnStartLocalPlayer()
    {
        Camera.main.transform.SetParent(transform);
        Camera.main.transform.localPosition = CameraLocalPosition;
        Camera.main.transform.eulerAngles = new Vector3(-CameraLocaleulerAnglesX, 0, 0); ;
    }

    [Header("Y�� ���� ����")]
    [SerializeField]
    private float minY = -45f;
    [SerializeField]
    private float maxY = 45f;

    [Header("�ӵ�")]
    private float moveSpeed = 4f;


    private float rotationX,rotationY;

    //����
    [Header("����")]
    public float sensitivityX, sensitivityY;


    void Update()
    {
        if (!isLocalPlayer) { return; }

        HandleCameraRotation();
        HandlePlayerMovement();
    }

    /// <summary>
    /// ī�޶� ȸ���� ó���մϴ�.
    /// </summary>
    private void HandleCameraRotation()
    {
        // ���콺 �Է� �޾ƿ���
        rotationX += Input.GetAxis("Mouse X") * sensitivityX * Time.deltaTime;
        rotationY += Input.GetAxis("Mouse Y") * sensitivityY * Time.deltaTime;

        //// Y�� ���� ����
        rotationY = Mathf.Clamp(rotationY, minY, maxY);

        // ī�޶� ���� ȸ�� ����
        Camera.main.transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);

        // �÷��̾�(��ü) ȸ�� ����
        transform.eulerAngles = new Vector3(0, rotationX, 0);
    }

    /// <summary>
    /// �÷��̾� �̵��� ó���մϴ�.
    /// </summary>
    private void HandlePlayerMovement()
    {
        // �̵� �Է� �ޱ�
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // �̵� ���� ���
        Vector3 moveDirection = transform.right * moveX + transform.forward * moveZ;

        // �̵� ����
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
    }

}
