using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers.Global
{
    public static class GlobalObject
    {
        private static GameObject _player;
        private static List<Transform> _entrances = new List<Transform>();

        public static GameObject Player
        {
            get
            {
                if (_player == null)
                {
                    _player = GameObject.FindWithTag("Player");
                }

                return _player;
            }
        }
        
        // Temporary solution for storing all entrance points in the scene,
        // Needs to be moved to a LevelManager or similar
        public static List<Transform> Entrances
        {
            get
            {
                if (_entrances.Count == 0)
                {
                    GameObject[] entrances = GameObject.FindGameObjectsWithTag("Entrance");
                    
                    foreach (GameObject entrance in entrances)
                    {
                        _entrances.Add(entrance.transform);
                    }
                }

                return _entrances;
            }
        }
    }
}
