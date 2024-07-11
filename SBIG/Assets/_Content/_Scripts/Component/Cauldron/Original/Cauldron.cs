using System;
using Components.Cookables;
using Enums.CookableType;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using FMODUnity;
using Audio;
using FMOD.Studio;
using Manager.Task;
using Managers.UI;
using YInput;
using Scriptables.Turn.Craft.Child;

namespace Components.Cauldrons.Original
{
    public class Cauldron : BaseCauldron
    {
        List<ECookableType> _cookablesInCauldron = new List<ECookableType>();
        private Animator _animator;
        readonly private int necesserySize = 1;
        private string _isCookingBool = "isCooking";

        [SerializeField] private bool _isCooking;
        [SerializeField] EventReference _eventReference;
        EventInstance _eventInstance;
        private void OnEnable()
        {
            _animator = GetComponentInChildren<Animator>();

            if (!_eventReference.IsNull)
            {
                _eventInstance = RuntimeManager.CreateInstance(_eventReference);
                _eventInstance.set3DAttributes(RuntimeUtils.To3DAttributes(transform));
                _eventInstance.start(); // Play idle sound from the start   
            }
        }

        private void OnDisable()
        {
            if (_eventInstance.isValid())
            {
                _eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                _eventInstance.release();
            }
        }

        public override void ThrowInCauldron(BaseCookable cookable) 
        {
            _cookablesInCauldron.Add(cookable.type);
            cookable.ThrowenToCauldron();
            EventHub.ThrowInToCauldron();
            
            if (TaskManager.Instance.IsCurrentTask("Add plant to cauldron"))
            {
                TaskManager.Instance.CompleteTask("Add plant to cauldron");   
            }
            
            if(_cookablesInCauldron.Count>= 3)
            {
                _cookablesInCauldron.Clear();
                Cook2();
            }
        }
        private void Update()
        {
            if(_isCooking)
            {
                _isCooking = false;
                Cook2();
            }
        }

        public void Cook2()
        {
            if (_eventInstance.isValid())
                _eventInstance.setPaused(true);
            
            
            _cookablesInCauldron.Clear();
            UIManager.Instance.HideCraftUI();
            EventHub.CauldronStartCook();
            _animator.SetTrigger("cook");
            Invoke("EndCook", 4);

        }

        private IEnumerator Cooking()
        {
            float upDuringCooking = 0.5f;
            transform.position += new Vector3(0, upDuringCooking, 0);
            
 
            float cookTime = 3;
            yield return new WaitForSeconds(cookTime);

            _animator.SetBool(_isCookingBool, false);

            float cookEndTime = 1.5f;
            yield return new WaitForSeconds(cookEndTime);

            
        }

        private void EndTurn()
        {
            if(Craft1.Instance != null)
                Craft1.Instance._continue = false;
        }

        private void EndCook()
        {
            if (_eventInstance.isValid())
                _eventInstance.setPaused(false);
            
            EventHub.CauldronEndCook();
            EventHub.CookFail();
            
            if (TaskManager.Instance.IsCurrentTask("Wait for cauldron to finish cooking"))
            {
                TaskManager.Instance.CompleteTask("Wait for cauldron to finish cooking");   
            }

            UIManager.Instance.ShowCraftUI();
            Invoke("EndTurn", 2);
        }
    }
}
