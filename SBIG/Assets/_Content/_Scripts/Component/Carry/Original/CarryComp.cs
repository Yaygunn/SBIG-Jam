using UnityEngine;

namespace Components.Carry.Original
{
    public class CarryComp : MonoBehaviour, ICarryComp
    {
        public void LogicUpdate()
        {
            //logic decision making
            //send ray forward first look for is it cauldron if not look for BaseCarriable Component //cauldron script is not made yet
        }

        public void OnActivateCauldronButton()
        {
            // try to cook with cauldron
            print("cook");
        }

        public void OnInteractButton()
        {
            // tryto pick, drop or throw to cauldron according to decision in logic
            print("interact");
        }

    }
}
