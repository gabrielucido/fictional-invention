using System.Collections;
using UnityEngine;

public enum PlatformsState
{
    PATROL,
    IDLE
}

public enum PatrolsType
{
    SEQUENTIAL,
    RANDOM
}

public class MovingPlatformVertical : MonoBehaviour
{
    public Transform[] waypoints;
    public int currentWayPointIndex;
    public float speed;
    public Transform target;
    public Vector2 boxSize;
    public PlatformsState platformsState;
    public PatrolsType patrolsType;
    private Animator _animator;
    private Rigidbody2D _rb;
    public PlayerInteractions playerInteractions;

    void Start()
    {
        currentWayPointIndex = 0;
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        platformsState = PlatformsState.IDLE;
        
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.E) && playerInteractions.hasElectricCharm == true)
        {
            StartCoroutine(PlayAnimation());
        }
        if(platformsState == PlatformsState.PATROL)
        {
            if(Vector3.Distance(transform.position, waypoints[currentWayPointIndex].position) <= 1)
            {
                if(patrolsType == PatrolsType.SEQUENTIAL)
                {
                    currentWayPointIndex++;
                    currentWayPointIndex %= waypoints.Length;
                }
                else if (patrolsType == PatrolsType.RANDOM)
                {
                    currentWayPointIndex = Random.Range(0, waypoints.Length);
                }
            }
        }
    }

    public void FixedUpdate()
    {
        if (platformsState == PlatformsState.PATROL)
        {
            Vector2 targetVelocity = Vector2.zero;
            if (transform.position.y < waypoints[currentWayPointIndex].position.y)
            {
                targetVelocity = new Vector2(0f, speed);
            }
            else
            {
                targetVelocity = new Vector2(0f, -speed);
            }
            _rb.velocity = targetVelocity;
        }
    }

     IEnumerator PlayAnimation()
    {
        _animator.SetBool("Charging",true);
        yield return new WaitForSeconds(1);
        // Start the patrol after the animation is done
        StartMoving();

    }

    public void StartMoving()
    {
        platformsState = PlatformsState.PATROL;
    }
}
