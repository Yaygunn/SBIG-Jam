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

        private void Awake()
        {
            if ( _startGrowt>= _requiredGrowtToChangeState)
            {
                MakeFruit();
            }
            else
            {
                EndFruit();
            }
        }
        public override void GrowCrop()
        {
            currentGrowCycle += _growAmountForTurn;

            if (HasFruit)
                return;

            if (currentGrowCycle < _requiredGrowtToChangeState) 
                MakeFruit();
                
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
        }

        private void EndFruit()
        {
            HasFruit = false;
            foreach (GameObject fruit in _fruitGameObjects)
            {
                fruit.SetActive(false);
            }
        }
    }


}

