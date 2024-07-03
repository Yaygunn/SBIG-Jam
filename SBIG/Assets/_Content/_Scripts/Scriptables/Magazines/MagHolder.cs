using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scriptables.Magazines
{
    [CreateAssetMenu(fileName = "MagHolder", menuName = "Scriptables/Magazines/Holder", order = -1)]
    public class MagHolder : ScriptableObject
    {
        [SerializeField] private SMagRate[] _magList;

        public BaseMagazine GetRandomMagazine()
        {
            float randomNumber = GetRandomNumber();

            foreach(SMagRate mag in _magList)
            {
                randomNumber -= mag.RandomRate;

                if (randomNumber <= 0)
                    return mag.Mag;
            }
            return null;
        }

        private float GetRandomNumber()
        {
            float sum = 0;
            foreach(SMagRate mag in _magList)
            {
                sum += mag.RandomRate;
            }
            return UnityEngine.Random.Range(0, sum);
        }
    }

    [Serializable]
    struct SMagRate
    {
        [field: SerializeField] public BaseMagazine Mag { get; private set; }
        [field: SerializeField] public float RandomRate { get; private set; }
    }
}
