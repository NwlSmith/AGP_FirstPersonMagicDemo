using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Services
{
    #region Variables.
    // Ensures you don't get a null reference exception.
    private static Player _player;
    public static Player Player
    {
        get
        {
            Debug.Assert(_player != null);
            return _player;
        }
        private set => _player = value;
    }

    #endregion

    #region Functions

    public static void InitializeServices()
    {
        Player = new Player();
    }
    #endregion
}
