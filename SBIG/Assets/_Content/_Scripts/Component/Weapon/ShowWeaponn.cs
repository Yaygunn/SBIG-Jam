using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowWeaponn : MonoBehaviour
{
    [SerializeField] GameObject weaponparts;
    private void OnEnable()
    {
        EventHub.Ev_ShowWeapon += Show;
    }

    private void OnDisable()
    {
        EventHub.Ev_ShowWeapon -= Show;

    }

    private void Show(bool show)
    {
        weaponparts.SetActive(show);
    }
}
