using UnityEngine;

public class PlayerMovementDebug : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null) Debug.LogError("Rigidbody2D не найден на объекте!");
    }

    void Update()
    {
        // Получаем ввод
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        
        moveInput = new Vector2(moveX, moveY).normalized;
        
        // ОТЛАДКА - смотрим, есть ли ввод
        if (moveX != 0 || moveY != 0)
        {
            Debug.Log($"Движение: X={moveX}, Y={moveY}");
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * moveSpeed;
        
        // ОТЛАДКА - смотрим скорость
        if (moveInput.magnitude > 0)
        {
            Debug.Log($"Скорость: {rb.linearVelocity}");
        }
    }
}
