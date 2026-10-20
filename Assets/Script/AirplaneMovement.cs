using UnityEngine;

public class PlaneMove : MonoBehaviour
{
    public float speed = 10f;       
    public float takeOffZ = 12f;    
    public float climbSpeed = 2f;   
    public float maxHeight = 4.5f;  

    private bool isFlying = true;
    private GameManager gameManager;

    void Start()
    {
        // Tự động tìm GameManager trên Main Camera
        gameManager = FindFirstObjectByType<GameManager>();
    }

    void Update()
    {
        if (!isFlying) return;

        // Bay lùi / tiến tùy theo setup của bạn hiện tại
        transform.Translate(Vector3.back * speed * Time.deltaTime, Space.Self);

        if (transform.position.z > takeOffZ)
        {
            if (transform.position.y < maxHeight)
            {
                Quaternion targetRotation = Quaternion.Euler(15f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * climbSpeed);
            }
            else
            {
                Quaternion levelRotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
                transform.rotation = Quaternion.Lerp(transform.rotation, levelRotation, Time.deltaTime * climbSpeed);
            }
        }
    }

    // Bắt sự kiện máy bay va chạm với tường
    private void OnCollisionEnter(Collision collision)
    {
        // Kiểm tra nếu chạm vào Wall
        if (collision.gameObject.name == "Wall" || collision.gameObject.CompareTag("Wall"))
        {
            isFlying = false;

            if (gameManager != null)
            {
                gameManager.GameOver(); // Hiển thị menu hỏi Yes/No và dừng game
            }
        }
    }
}