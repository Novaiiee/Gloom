using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public new BoxCollider2D collider2D;
    [HideInInspector] public LayerMask groundMask;
    [HideInInspector] public Transform target;

    [Header("Jumping")]
    public Transform groundCheck;
    public float obstacleJumpingDistance = 5f;
    public float groundRadius;
    public float jumpForce;

    [Header("Movement")]
    public float speed = 10f;
    public float activationDistance = 30f;
    public float stoppingDistance = 2f;

    public EnemyBaseState currentState;
    public EnemyFollowState followState;
    public EnemyPatrolState patrolState;
    public EnemyAttackingState attackingState;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<BoxCollider2D>();

        followState = new EnemyFollowState(this);
        patrolState = new EnemyPatrolState(this);
        attackingState = new EnemyAttackingState(this);

        groundMask = LayerMask.GetMask("Ground");
        currentState = patrolState;
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (!target) return;
        currentState.Update();
    }

    public void ChangeState(EnemyBaseState state)
    {
        currentState = state;
        currentState.Initialize();
    }
}
