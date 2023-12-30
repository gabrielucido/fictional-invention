using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum PlatformState
{
    PATROL,
    IDLE
}
public enum PatrolType
{
    SEQUENTIAL,
    RANDOM
}
public class MovingPlatformHorizontal : MonoBehaviour
{
    public Transform[] waypoints;
    public int currentWayPointIndex;
    public float speed;
    public Transform target;
    public Vector2 boxSize;
    public PlatformState platformState;
    public PatrolType patrolType;
    private Animator _animator;
    private Rigidbody2D _rb;
    public PlayerInteractions playerInteractions;
    void Start()
    {
        currentWayPointIndex = 0;
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        platformState = PlatformState.IDLE;

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.E) && playerInteractions.hasElectricCharm == true)
        {
            StartCoroutine(PlayAnimation());
            playerInteractions.hasElectricCharm = false;
        }
        if(platformState == PlatformState.PATROL)
        {
            if(Vector3.Distance(transform.position, waypoints[currentWayPointIndex].position) <= 1)
            {
                if(patrolType == PatrolType.SEQUENTIAL)
                {
                    currentWayPointIndex++;
                    currentWayPointIndex %= waypoints.Length;
                }
                else if (patrolType == PatrolType.RANDOM)
                {
                    currentWayPointIndex = Random.Range(0, waypoints.Length);
                }
            }
        }
    }
    void FixedUpdate()
    {
        if(platformState == PlatformState.PATROL)
        {
            Vector2 targetVelocity = Vector2.zero;
             if(transform.position.x < waypoints[currentWayPointIndex].position.x)
            {
                targetVelocity = new Vector2(speed,0f);
            }
            else
            {
                targetVelocity = new Vector2(-speed,0f);    
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
        platformState = PlatformState.PATROL;
    }  
}
