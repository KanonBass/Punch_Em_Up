using System.Threading;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAI : MonoBehaviour
{
    private Rigidbody body;
    public GameObject attackPrefab;

    private GameObject player;

    private string state;
    private string idleState = "idleing";
    private string movingState = "moving";
    private string attackState = "attacking";

    private float stateTimer = 0;

    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float attackTimer = .5f;
    [SerializeField] private float idleTimer = .5f;
    [SerializeField] private float moveThreashHold = .1f;
    private float moveTimer = 100000;
    [SerializeField] private float attackThreashHold = 1;

    private float currentTimer;

    private Vector3 targetLocation;

    private int direction = 1;
    [SerializeField] private float attackOffset = .6f;

    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Sprite idleSprite;
    [SerializeField] private Sprite moveSprite;
    [SerializeField] private Sprite punchSprite;

    [SerializeField] private int maxHealth = 3;
    private int health = 3;
    [SerializeField] private Collider collide;

    public UnityEvent EnemyDeath;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        state = idleState;
        currentTimer = idleTimer;
        body = GetComponent<Rigidbody>();

        EnemyDeath.AddListener(GameObject.FindWithTag("Spawner").GetComponent<spawner>().OnDeath);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (state == idleState)
        {
            if (stateTimer > currentTimer)
            {
                if (Mathf.Abs(Vector2.Distance(player.transform.position, transform.position)) < attackThreashHold)
                {
                    state = attackState;
                    stateTimer = 0;
                    currentTimer = attackTimer;
                    Instantiate(attackPrefab, transform.position + new Vector3(attackOffset * direction, 0, 0), quaternion.identity);
                    sprite.sprite = punchSprite;
                }
                else
                {
                    state = movingState;
                    stateTimer = 0;
                    currentTimer = moveTimer;
                    targetLocation = player.transform.position;
                    if (-transform.position.x +targetLocation.x < 0)
                    {
                        direction = 1;
                    }
                    else
                    {
                        direction = -1;
                    }
                    sprite.sprite = moveSprite;
                }
            }
        }
        else if (state == movingState)
        {
            body.AddForce((Vector3.Normalize(new Vector3(-transform.position.x + targetLocation.x, -transform.position.y + targetLocation.y, 0))) * moveSpeed);
            if (Vector2.Distance(player.transform.position, transform.position) < moveThreashHold)
            {
                state = idleState;
                stateTimer = 0;
                currentTimer = idleTimer;
                sprite.sprite = idleSprite;
            }
        }
        else
        {
            if (stateTimer > currentTimer)
            {
                state = idleState;
                stateTimer = 0;
                currentTimer = idleTimer;
                sprite.sprite = idleSprite;
            }
        }

        stateTimer += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerHit"))
        {
            health--;
            if (health < 0)
            {
                Destroy(gameObject);
                EnemyDeath.Invoke();
            }
        }
    }
        


}
