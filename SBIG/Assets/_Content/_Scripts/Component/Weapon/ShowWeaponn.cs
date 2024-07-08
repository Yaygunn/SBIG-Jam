using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowWeaponn : MonoBehaviour
{
    [SerializeField] GameObject weaponparts;
    WeaponAnim anim;
    private void OnEnable()
    {
        EventHub.Ev_ShowWeapon += Show;
        anim = GetComponent<WeaponAnim>();
    }

    private void OnDisable()
    {
        EventHub.Ev_ShowWeapon -= Show;

    }

    private void Show(bool show)
    {
        if (!show)
        {
            anim.Reset();
        }
        StartCoroutine(showw(show));
    }

    private IEnumerator showw(bool show)
    {
        yield return null;
        yield return null;
        weaponparts.SetActive(show);
    }
}
