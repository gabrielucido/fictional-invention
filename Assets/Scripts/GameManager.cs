using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
  // Singleton instance to make GameManager accessible from any script
    public static GameManager Instance { get; private set; }

    // Enum to define the type of portal (Next or Previous)
    public enum PortalType
    {
        NextLevel,
        PreviousLevel
    }

    // Store the player's position when entering a portal
    private Vector3 playerPositionOnEnter;

    // Awake is called before Start
    private void Awake()
    {
        // Implement Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Method to handle player entering a portal
    public void EnterPortal(PortalType portalType)
    {
        // Store the player's position when entering a portal
        playerPositionOnEnter = GameObject.FindGameObjectWithTag("Player").transform.position;

        // Determine the target level index based on the portal type
        int targetLevelIndex = (portalType == PortalType.NextLevel) ? SceneManager.GetActiveScene().buildIndex + 1 :
            SceneManager.GetActiveScene().buildIndex - 1;

        // Load the target scene if it exists
        if (targetLevelIndex >= 0 && targetLevelIndex < SceneManager.sceneCountInBuildSettings)
        {
            LoadLevel(targetLevelIndex);
        }
        else
        {
            Debug.LogWarning("Invalid target level index.");
        }
    }

    // Method to load a level by index
    private void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }

    // Callback method for the sceneLoaded event
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Set the player's position when loading a level
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        // Find the spawn point by name
        string spawnPointName = $"SpawnPoint_{scene.name}";
        GameObject spawnPointObject = GameObject.Find(spawnPointName);

        if (spawnPointObject != null)
        {
            Transform spawnPointTransform = spawnPointObject.transform;

            // Set the player's position to the spawn point
            player.transform.position = spawnPointTransform.position;
        }
        else
        {
            Debug.LogWarning($"Spawn point not found in the scene: {scene.name}");
        }
    }

    // Subscribe to the sceneLoaded event when this script is enabled
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Unsubscribe from the sceneLoaded event when this script is disabled
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}

