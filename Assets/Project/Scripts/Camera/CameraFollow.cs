using UnityEngine;

/// <summary>
/// 摄像机跟随脚本 - 45° 固定俯视角跟随玩家。
/// </summary>
public class CameraFollow : MonoBehaviour
{
    [Header("跟随目标")]
    public Transform target;

    [Header("摄像机设置")]
    public Vector3 offset = new Vector3(0f, 10f, -10f);

    [Tooltip("摄像机跟随的平滑速度，越大越灵敏")]
    [Range(1f, 20f)]
    public float smoothSpeed = 8f;

    private void Start()
    {
        // 如果没有手动设置 offset，就自动计算一个 45° 俯视的偏移
        // 45° 意味着 Y 和 Z 的绝对值相等（tan45° = 1）
        if (offset == Vector3.zero)
        {
            offset = new Vector3(0f, 10f, -10f);
        }

        // 设置摄像机的旋转角度为 45° 俯视
        transform.rotation = Quaternion.Euler(45f, 0f, 0f);
    }

   
    private void LateUpdate()
    {
        if (target == null) return;

        // 目标位置
        Vector3 desiredPosition = target.position + offset;

        Vector3 smoothedPosition = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime
        );

        transform.position = smoothedPosition;
    }
}
