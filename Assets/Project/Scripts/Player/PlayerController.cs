using UnityEngine;

/// <summary>
/// 玩家移动控制器。
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("移动设置")]
    public float moveSpeed = 2f;

    [Tooltip("重力大小，防止角色悬空")]
    public float gravity = -9.81f;

    
    private CharacterController _controller;
    private Vector3 _velocity; // 用于累积重力

    private void Awake()
    {
       
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {

        if (GameManager.Instance.CurrentState != GameManager.GameState.Playing)
            return;

        float horizontal = Input.GetAxisRaw("Horizontal"); // A/D
        float vertical = Input.GetAxisRaw("Vertical");     // W/S

        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

        if (moveDirection.magnitude >= 0.1f)
        {
            _controller.Move(moveDirection * moveSpeed * Time.deltaTime);
        }

        if (_controller.isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f; 
        }
        _velocity.y += gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }
}
