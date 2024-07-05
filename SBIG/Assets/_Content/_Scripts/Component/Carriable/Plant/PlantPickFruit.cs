using Components.Crops;
using UnityEngine;

namespace Components.Carriables.Plant
{
    public class PlantPickFruit : BaseCarriable
    {
        Crop _crop;

        [SerializeField] private string _cropName;
        private string _noFriutText = "no fruit";

        public override bool IsPickable => _crop.HasFruit;

        private void Awake()
        {
            _crop = GetComponent<Crop>();
        }
        public override BaseCarriable PickUp(Transform camera)
        {
            if(!_crop.HasFruit)
            {
                print("Cropt has no fruit");
                return null;
            }
            _crop.Harvest();

            BaseCarriable fruit = Instantiate(_crop.FruitPrefab, new Vector3(-20, -20, 0), Quaternion.identity)
                    .GetComponent<BaseCarriable>();

            fruit.PickUp(camera);

            return fruit;
        }

        public override string GetUiText()
        {
            if(IsPickable)
            {
                return _cropName;
            }
            return _noFriutText;
        }


    }
}