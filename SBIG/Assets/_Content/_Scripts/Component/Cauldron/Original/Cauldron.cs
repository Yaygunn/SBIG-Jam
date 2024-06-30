using Components.Cookables;
using Enums.CookableType;
using System.Collections.Generic;
using UnityEngine;

namespace Components.Cauldrons.Original
{
    public class Cauldron : BaseCauldron
    {
        List<ECookableType> _cookablesInCauldron = new List<ECookableType>();

        public override void ThrowInCauldron(BaseCookable cookable) 
        {
            _cookablesInCauldron.Add(cookable.type);
            cookable.ThrowenToCauldron();
        }

        public override void Cook() 
        {
            print("cook is empty fill here");
        }
    }
}
