using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerInteractions : MonoBehaviour
{
    public bool hasElectricCharm = false;
    public bool hasPhasingCharm = false;
    public bool hasWindCharm = false;
    public float passThroughSpeed = 5f;
    public bool isPassingThrough = false;
    public Vector3 desiredPosition;
    private Animator _animator;
        public float maxTravelDistance = 5f; // Adjust this value as needed
    public Tilemap tilemap;
    private Rigidbody2D _rb;
    public CheckpointSystem checkpointSystem;
    public bool hasCollected = false;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    /* Needs to add a functions to get the tilemap based on the scene
    so that when the player needs to use the phasing charm, the player can use it proprely*/

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && hasPhasingCharm == true)
        {

            //Set the desiredPostion to the Mouse position in the World
            desiredPosition = GetMouseWorldPosition();
            Debug.Log(desiredPosition);
            //Trigger the pass-through effect
            StartPassThrough();
            
            
        }
        if (isPassingThrough)
        {
            // Move the player towards the desired position
            transform.position = Vector3.MoveTowards(transform.position, desiredPosition, passThroughSpeed * Time.deltaTime);

            // Check if the player has reached the desired position
            if (Vector3.Distance(transform.position, desiredPosition) < 0.1f)
            {
                StopPassThrough();
                _animator.SetBool("Phasing",false);
                hasPhasingCharm = false;
            }
        }
    }
    
    void StartPassThrough()
    {
         // Check if there are any tiles at the desired position on the tilemap
        if (!AreTilesAtPosition(desiredPosition))
        {
            
            //_animator.SetBool("Phasing",true);

            // Turn off the collider so the player can pass through
            GetComponent<CapsuleCollider2D>().enabled = false;

            // Disable the Rigidbody2D
            GetComponent<Rigidbody2D>().simulated = false;

            isPassingThrough = true;
        }
        else
        {
            
            Debug.Log("Cannot pass through. There are tiles there");
        }
    }

    void StopPassThrough()
    {
        GetComponent<CapsuleCollider2D>().enabled = true;

        isPassingThrough = false;
        GetComponent<Rigidbody2D>().simulated = true;
        Time.timeScale = 1f;
        _rb.gravityScale = 10f;
        _rb.mass = 1f;
    }

     Vector3 GetMouseWorldPosition()
{
    // Get the mouse position in screen space
    Vector3 mousePosition = Input.mousePosition;

    // Set the z-coordinate to the distance from the camera to the player
    mousePosition.z = Vector3.Distance(transform.position, Camera.main.transform.position);

      // Convert the mouse position to a world position
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

    // Convert the mouse position to a world position
    return tilemap.GetCellCenterWorld(tilemap.WorldToCell(worldPosition));
}
bool AreTilesAtPosition(Vector3 position)
    {
        // Convert the world position to cell position
        Vector3Int cellPosition = tilemap.WorldToCell(position);

        // Check if there is a tile at the given cell position
        return tilemap.GetTile(cellPosition) != null;
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("ElectricCharm"))
        {
            hasElectricCharm = true;
            Destroy(other.gameObject);
        }
        else if(other.gameObject.CompareTag("PhasingCharm"))
        {
            hasPhasingCharm = true;
            Destroy(other.gameObject);
            Time.timeScale = 0.5f;
            Time.fixedDeltaTime = Time.timeScale * 0.01f;
            _rb.gravityScale = 2f;
            _rb.mass = 0.5f;
        }
        else if(other.CompareTag("WindCharm"))
        {
            hasWindCharm = true;
        }
        else if(other.CompareTag("Collectable"))
        {
            Destroy(other.gameObject);
            hasCollected = true;
        }
        else if(other.CompareTag("Spikes"))
        {
            Debug.Log("Died");
            checkpointSystem.TeleportToLastCheckpoint();
            // Need to make the player go to the start of the level
        }
    }
    
}
