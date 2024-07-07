using Components.Cookables;
using Enums.CookableType;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using FMODUnity;
using Audio;
using FMOD.Studio;

namespace Components.Cauldrons.Original
{
    public class Cauldron : BaseCauldron
    {
        List<ECookableType> _cookablesInCauldron = new List<ECookableType>();
        private Animator _animator;
        readonly private int necesserySize = 3;
        private string _isCookingBool = "isCooking";

        private void OnEnable()
        {
            _animator = GetComponentInChildren<Animator>();
        }

        public override void ThrowInCauldron(BaseCookable cookable) 
        {
            _cookablesInCauldron.Add(cookable.type);
            cookable.ThrowenToCauldron();
            EventHub.ThrowInToCauldron();
        }

        public override void Cook()
        {
            if(_cookablesInCauldron.Count > necesserySize)
            {
                _cookablesInCauldron.Clear();
                StartCoroutine(Cooking());
            }
            else
            {
                EventHub.CookFail();
            }
        }

        private IEnumerator Cooking()
        {
            EventHub.CauldronStartCook();
            _animator.SetBool(_isCookingBool, true);
 
            float cookTime = 3;
            yield return new WaitForSeconds(cookTime);

            _animator.SetBool(_isCookingBool, false);

            float cookEndTime = 2;
            yield return new WaitForSeconds(cookEndTime);

            EventHub.CauldronEndCook();
        }
    }
}
