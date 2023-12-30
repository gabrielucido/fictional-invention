using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CineMachineManager : MonoBehaviour
{
    public static CineMachineManager Instance;

    public CinemachineVirtualCamera virtualCamera;
    public Transform playerTransform;

    private void Awake()
    {
        // Singleton pattern to ensure only one instance of this script exists
        if (Instance == null)
        {
            Instance = this;
            
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (playerTransform == null)
        {
            // Find the player by tag (you can adjust this based on your setup)
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        if (virtualCamera != null && playerTransform != null)
        {
            // Set the virtual camera's Follow target to the player
            virtualCamera.Follow = playerTransform;
            virtualCamera.LookAt = playerTransform;
        }
    }
}
