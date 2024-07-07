using Audio;
using FMOD.Studio;
using FMODUnity;
using Managers.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YInput;

[CreateAssetMenu(fileName = "Prologue", menuName = "Scriptables/Turn/Prologue")]
public class Prologue : BaseTurn
{
    [SerializeField] EventReference Dialogue1;
    [SerializeField] EventReference Dialogue2;
    [SerializeField] EventReference Dialogue3;
    [SerializeField] EventReference Dialogue4;
    [SerializeField] EventReference Dialogue5;
    [SerializeField] EventReference Dialogue7;
    FModCommunication _com = new FModCommunication();

    private EventInstance SoundInstance;
    float _timer;
    bool targetAchieved;
    bool _continue;
    public override IEnumerator TurnOperations()
    {
        _continue = true;
        yield return null;
        EventHub.Ev_CookFail += CookFail;
        UIManager.Instance.ShowCraftUI();
        UIManager.Instance.HideCombatUI();
        EventHub.ShowWeapon(false);
        InputHandler.Instance.EnableUIMod();
        Cursor.visible = false;

        PlayReferance(Dialogue1);
        //yield return new WaitForSeconds(32);
        float timer = 0;
        while (true)
        {
            timer += Time.deltaTime;
            if(timer > 32)
            {
                _com.StopInstance(ref SoundInstance);
                break;
            }

            if (IsPressed())
                break;
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                break;
            }
            yield return null;
        }
        EventHub.ShowPrologueText();
        while (true)
        {
            yield return null;
            if (IsPressed())
                break;
        }
        //close black screen
        EventHub.ClosePrologue();
        InputHandler.Instance.EnableGameplayMod();
        yield return new WaitForSeconds(1);

        PlayReferance(Dialogue2);
        yield return new WaitForSeconds(4);
        EventHub.ShowWeapon(true);
        targetAchieved = false;
        EventHub.Ev_CropPicked += TargetAction;
        yield return new WaitForSeconds(9);
        if (_continue)
        {
            PlayReferance(Dialogue3);

            _timer = 0;

            while (!targetAchieved)
            {
                yield return null;
                _timer += Time.deltaTime;
                if (_timer > 40)
                {
                    PlayReferance(Dialogue3);
                    _timer = 0;
                }
                if (!_continue)
                    break;
            }
            if (_continue)
            {


                EventHub.ShowWeapon(false);
                targetAchieved = false;
                EventHub.Ev_CropPicked -= TargetAction;
                PlayReferance(Dialogue4);
                EventHub.Ev_ThrowInToCauldron += TargetAction;
                _timer = 0;
                while (!targetAchieved)
                {
                    if (!_continue)
                        break;
                    yield return null;
                    _timer += Time.deltaTime;
                    if (_timer > 40)
                    {
                        PlayReferance(Dialogue4);
                        _timer = 0;
                    }

                }
                if (_continue)
                {

                    PlayReferance(Dialogue5);
                    targetAchieved = false;

                    while (!targetAchieved)
                    {
                        yield return null;
                        if (!_continue)
                            break;
                    }
                }
                if (_continue)
                {

                EventHub.Ev_ThrowInToCauldron -= TargetAction;
                targetAchieved = false;
                PlayReferance(Dialogue7);
                }
            }
        }
    }

    private void TargetAction()
    {
        targetAchieved = true;
    }
    private bool IsPressed()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            return true;
        if(Input.GetKeyUp(KeyCode.Return))
            return true;
        return false;
    }

    private void CookFail()
    {
        _continue = false;
        EndTurn();
    }

    private void PlayReferance(EventReference referance)
    {
        _com.StopInstance(ref SoundInstance);
        _com.RelaeseInstance(ref SoundInstance);

        SoundInstance = RuntimeManager.CreateInstance(referance);
        SoundInstance.start();
    }
    public override void EndBeforeEndTurn()
    {
        EventHub.Ev_CropPicked -= TargetAction;
        EventHub.Ev_ThrowInToCauldron -= TargetAction;
        EventHub.Ev_CookFail -= CookFail;


        SoundInstance.release() ;
    }

    protected override void EndTurn()
    {
        base.EndTurn();
        EndBeforeEndTurn();
    }
    

}
