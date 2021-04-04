using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    
    /// <summary>
    /// Player dies.
    /// </summary>
    protected override void Die()
    {
        Debug.Log("Player has died.");
    }

    
}
