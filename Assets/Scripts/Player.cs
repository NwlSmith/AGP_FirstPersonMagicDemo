using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{

    public Player(GameObject gameObject) : base(gameObject)
    {

    }

    /// <summary>
    /// Player dies.
    /// </summary>
    protected override void Die()
    {
        Debug.Log("Player has died.");
    }

    
}
