using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Sprite idleSprite;
    [SerializeField] private Sprite moveSprite;
    [SerializeField] private Sprite punchSprite;
    [SerializeField] private Sprite kickSprite;

    public UnityEvent<int> DirectionChange;

    private int direction = 1;

    private Vector2 movement;
    private Rigidbody playerBody;

    UnityEvent GameOver;

    private void Awake()
    {
        playerBody = GetComponent<Rigidbody>();
    }

    private void OnMovement(InputValue value)
    {
        movement = value.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        playerBody.AddForce(movement*speed);

        if(playerBody.linearVelocity.x > 0 && direction<0)
        {
            direction = 1;
            DirectionChange.Invoke(1);
            
        }
        else if (playerBody.linearVelocity.x < 0 && direction > 0)
        {
            direction = -1;
            DirectionChange.Invoke(-1);
        }

        if (playerBody.linearVelocity.magnitude > .5f)
        {
            sprite.sprite = moveSprite;
        }
        else
        {
            sprite.sprite = idleSprite;
        }
    }

    public void ChangeSprite(int attack)
    {
        if (attack == 1)
        {
            sprite.sprite = punchSprite;
        }
        else
        {
            sprite.sprite = kickSprite;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyHit"))
        {
            SceneManager.LoadScene("Main Menu");
        }

        Debug.Log("triggered");
    }
}
