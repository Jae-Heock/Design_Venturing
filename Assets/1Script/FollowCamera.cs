using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;     // 따라갈 대상 (플레이어)
    public Vector3 offset = new Vector3(0f, 20f, -30f);  // 카메라 위치 오프셋
    public float followSpeed = 5f; // 따라가는 속도
    public float mouseSensitivity = 3f; // 마우스 감도
    public float minYAngle = -89f; // 카메라가 아래로 볼 수 있는 최소 각도
    public float maxYAngle = 89f;  // 카메라가 위로 볼 수 있는 최대 각도

    private float currentYaw = 0f; // 좌우 회전 각도
    private float currentPitch = 20f; // 위아래 회전 각도 (초기값은 offset.y에 맞춤)

    private void LateUpdate()
    {
        if (target == null) return;

        // 좌클릭을 누르고 있을 때만 회전
        if (Input.GetMouseButton(0))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            currentYaw += mouseX * mouseSensitivity;
            currentPitch -= mouseY * mouseSensitivity;
            currentPitch = Mathf.Clamp(currentPitch, minYAngle, maxYAngle);
        }

        // 회전값을 쿼터니언으로 변환
        Quaternion rotation = Quaternion.Euler(currentPitch, currentYaw, 0);
        Vector3 desiredPosition = target.position + rotation * offset;

        // 부드럽게 이동
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
        // 항상 타겟을 바라보게
        transform.LookAt(target.position);
    }
}
