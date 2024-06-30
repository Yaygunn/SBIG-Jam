using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Components.Carry
{
    public interface ICarryComp
    {
        public void LogicUpdate();
        public void OnInteractButton();
        public void OnActivateCauldronButton();
    }
}
