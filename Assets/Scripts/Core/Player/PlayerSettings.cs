﻿using UnityEngine;

namespace Beam.Core.Player
{
    /*
     * Player preferences
     * Can write to this object when user adjusts their settings.
     * This object should be persistent between scenes.
     */
    [CreateAssetMenu(fileName = "PlayerSettings", menuName = "ScriptableObjects/PlayerSettings", order = 1)]
    [SerializeField]
    public class PlayerSettings : ScriptableObject
    {
        public float mouseSensitivity;
        public float volume;
        public bool triggeredCrouch;
    }
}
