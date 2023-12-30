using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTrigger : MonoBehaviour
{
    
   public GameManager.PortalType portalType;
   

    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
             GameManager.Instance.EnterPortal(portalType);
        }
    }
}
