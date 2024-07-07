using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Components.Crops
{
    public class Crop : BaseCrop
    {
        [field:SerializeField] public GameObject FruitPrefab { get; private set; }
        [SerializeField] private GameObject[] _fruitGameObjects;

        [SerializeField] private int _requiredGrowtToChangeState = 10;
        [SerializeField] private int _growAmountForTurn = 6;
        [SerializeField] private int _startGrowt = 7;

        public bool HasFruit {  get; private set; }

        private int currentGrowCycle;

        private float Health = 100;

        public static List<GameObject> Crops = new List<GameObject>();
        private void Awake()
        {
            EventHub.Ev_GrowPlants += GrowCrop;

            if ( _startGrowt>= _requiredGrowtToChangeState)
            {
                MakeFruit();
            }
            else
            {
                HasFruit = false;
                foreach (GameObject fruit in _fruitGameObjects)
                {
                    fruit.SetActive(false);
                }
            }
        }
        private void OnDestroy()
        {
            EventHub.Ev_GrowPlants -= GrowCrop;
            EndFruit() ;
        }
        public override void GrowCrop()
        {
            currentGrowCycle += _growAmountForTurn;

            if (HasFruit)
                return;

            if (currentGrowCycle >= _requiredGrowtToChangeState)
            {
                MakeFruit();
                currentGrowCycle -= _requiredGrowtToChangeState;
            }
                
        }
        public override void Harvest()
        {
                EndFruit();
        }

        private void MakeFruit()
        {
            HasFruit = true;
            foreach (GameObject fruit in _fruitGameObjects) 
            {
                fruit.SetActive(true);
            }
            Health = 100;
            Crops.Add(gameObject);
        }

        private void EndFruit()
        {
            if (!HasFruit)
                return;

            HasFruit = false;
            foreach (GameObject fruit in _fruitGameObjects)
            {
                fruit.SetActive(false);
            }
            Crops.Remove(gameObject);
        }

        public bool GiveDamage()
        {
            float damageAmount = 30;
            Health -= damageAmount;
            if(Health < 0)
            {
                EndFruit();
                return true;
            }
            return false;
        }
    }


}

