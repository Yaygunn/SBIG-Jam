using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmAnim : MonoBehaviour
{
    [SerializeField] Animator _animator;

    void Start()
    {
        EventHub.Ev_ReloadStarted += Reload;
    }
    private void OnDestroy()
    {
        EventHub.Ev_ReloadStarted -= Reload;
    }

    private void Reload()
    {
        _animator.SetTrigger("Reload");
    }

    public void Reset()
    {
        _animator.SetTrigger("Reset");
    }
}
