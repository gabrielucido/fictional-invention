using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CheckpointSystem : MonoBehaviour
{
    // Store the positions of all checkpoints
    private Vector3[] checkpoints;

    // Reference to the player GameObject
    public GameObject player;

    private static CheckpointSystem instance;

    private void Awake()
    {
        if (instance == null)
        {
            // Keep this GameObject alive between scene changes
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            // Destroy duplicate instances in subsequent scenes
            Destroy(gameObject);
        }

        // Initialize the array of checkpoints
        checkpoints = new Vector3[0];
    }

    // Call this method to set a new checkpoint
    public void SetCheckpoint(Vector3 checkpointPosition)
    {
        // Add the new checkpoint position to the array
        ArrayUtility.Add(ref checkpoints, checkpointPosition);
    }

    // Call this method to teleport the player to the last checkpoint
    public void TeleportToLastCheckpoint()
    {
        // Find the player GameObject in the scene
        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null && checkpoints.Length > 0)
        {
            // Teleport the player to the last checkpoint
            player.transform.position = checkpoints[checkpoints.Length - 1];
        }
        else
        {
            Debug.LogWarning("Player not found in the scene or no checkpoints set.");
        }
    }
}
