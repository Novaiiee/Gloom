using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public new BoxCollider2D collider2D;
    [HideInInspector] public LayerMask groundMask;

    [Header("Jumping")]
    public Transform groundCheck;
    public float obstacleJumpingDistance = 5f;
    public float groundRadius;
    public float jumpForce;

    [Header("Movement")]
    public float speed = 10f;
    public float activationDistance = 30f;
    public float stoppingDistance = 2f;

    private Transform target;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<BoxCollider2D>();
        groundMask = LayerMask.GetMask("Ground");

        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (!target) return;
        if (!TargetInDistance(activationDistance)) return;

        DetectObstacle();

        if (Mathf.Abs(transform.position.x - target.position.x) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }

        FlipEnemy();
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void FlipEnemy()
    {
        if (target.position.x < transform.position.x)
        {
            Debug.Log("Is infront");
            transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            Debug.Log("Is behind");
            transform.localEulerAngles = new Vector3(0, 180, 0);
        }
    }

    private void DetectObstacle()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.right, obstacleJumpingDistance, groundMask);

        if (hit && IsGrounded())
        {
            Jump();
        }
    }

    private bool TargetInDistance(float distance)
    {
        return (transform.position.x - target.position.x) < distance;
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundMask);
    }
}
