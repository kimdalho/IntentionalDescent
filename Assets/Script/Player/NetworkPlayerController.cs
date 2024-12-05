using Mirror;
using UnityEngine;

public class NetworkPlayerController : NetworkBehaviour
{
    [Header("카메라 초기 위치")]
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

    [Header("Y축 각도 제한")]
    [SerializeField]
    private float minY = -45f;
    [SerializeField]
    private float maxY = 45f;

    [Header("속도")]
    private float moveSpeed = 4f;


    private float rotationX,rotationY;

    //감도
    [Header("감도")]
    public float sensitivityX, sensitivityY;


    void Update()
    {
        if (!isLocalPlayer) { return; }

        HandleCameraRotation();
        HandlePlayerMovement();
    }

    /// <summary>
    /// 카메라 회전을 처리합니다.
    /// </summary>
    private void HandleCameraRotation()
    {
        // 마우스 입력 받아오기
        rotationX += Input.GetAxis("Mouse X") * sensitivityX * Time.deltaTime;
        rotationY += Input.GetAxis("Mouse Y") * sensitivityY * Time.deltaTime;

        //// Y축 각도 제한
        rotationY = Mathf.Clamp(rotationY, minY, maxY);

        // 카메라 로컬 회전 설정
        Camera.main.transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);

        // 플레이어(몸체) 회전 설정
        transform.eulerAngles = new Vector3(0, rotationX, 0);
    }

    /// <summary>
    /// 플레이어 이동을 처리합니다.
    /// </summary>
    private void HandlePlayerMovement()
    {
        // 이동 입력 받기
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // 이동 방향 계산
        Vector3 moveDirection = transform.right * moveX + transform.forward * moveZ;

        // 이동 적용
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
    }

}
