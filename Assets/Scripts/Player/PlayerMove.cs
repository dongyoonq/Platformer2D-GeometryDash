using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f;        // ���������� �̵��ϴ� �ӵ�
    public float jumpForce = 6f;        // �����ϴ� ��
    public float rotationSpeed = 360f;  // �� ���� ȸ���ϴ� �ӵ�

    private Rigidbody2D player;
    private bool isJumping = false;
    private float currentRotation = 0f;
    private bool isRotating = false;

    void Awake()
    {
        player = GetComponent<Rigidbody2D>();
        player.gravityScale = 4f; // �߷��� Ȱ��ȭ
    }

    void Update()
    {
        // ���������� �̵�
        player.velocity = new Vector2(moveSpeed, player.velocity.y);

        // ȸ��
        if (isRotating)
        {
            float rotationAmount = rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.forward, -rotationAmount);
            currentRotation += -rotationAmount;

            // 360�� ȸ�� �� ���� ���� �ʱ�ȭ
            if (currentRotation <= -180f)
            {
                isRotating = false;
                currentRotation = 0f;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // �ٴڿ� ������ ���� ���� �ʱ�ȭ
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") ||
            collision.gameObject.layer == LayerMask.NameToLayer("BoundaryRange"))
        {
            isJumping = false;
            isRotating = false;
            currentRotation = 0f;
            transform.rotation = Quaternion.identity;
        }
    }

    void OnJump(InputValue value)
    {
        player.velocity = Vector2.right * moveSpeed;
        if (value.isPressed && !isJumping)
        {
            isJumping = true;
            isRotating = true;
            player.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Force);
        }
    }
}