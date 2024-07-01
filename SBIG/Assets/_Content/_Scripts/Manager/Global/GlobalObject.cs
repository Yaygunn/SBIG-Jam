using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers.Global
{
    public static class GlobalObject
    {
        private static GameObject _player;

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
    }
}
