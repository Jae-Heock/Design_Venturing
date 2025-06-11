using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 0f, -50f); // y=0, z=-거리로 변경
    public float followSpeed = 5f;
    public float mouseSensitivity = 5f;
    public float zoomSpeed = 30f;
    public float targetHeight = 2f; // 타겟 머리 위를 바라보게

    public float minZoom = 10f;   // 줌 시 더 멀리 가능
    public float maxZoom = 100f;   // 줌 시 더 가까이 가능
    public float minYAngle = -150;
    public float maxYAngle = 150f;

    private float currentYaw = 0f;
    private float currentPitch = 30f; // 초기 pitch

    private void LateUpdate()
    {
        if (target == null) return;

        // 줌 (마우스 휠)
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) > 0.01f)
        {
            float newZoom = offset.magnitude - scroll * zoomSpeed;
            newZoom = Mathf.Clamp(newZoom, minZoom, maxZoom);
            offset = offset.normalized * newZoom;
        }

        // 마우스 좌클릭 시 회전
        if (Input.GetMouseButton(0))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            currentYaw += mouseX * mouseSensitivity;
            currentPitch -= mouseY * mouseSensitivity;

            // Y축 회전 각도를 자연스럽게 (-10 ~ 80도)
            currentPitch = Mathf.Clamp(currentPitch, -80f, 80f);
        }

        Quaternion rotation = Quaternion.Euler(currentPitch, currentYaw, 0);
        Vector3 desiredPosition = target.position + rotation * offset;

        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
        // 타겟의 머리 위를 바라보게
        transform.LookAt(target.position + Vector3.up * targetHeight);
    }
}
