using System;
using System.Collections;
using System.Collections.Generic;
using Components.Weapons.Original;
using Manager.Task;
using UnityEngine;

public class WeaponMagazine : MonoBehaviour
{
    public Material EmptyMagazine;
    
    [SerializeField] private float _rotationSpeed = 1f;
    
    private MeshRenderer _meshRenderer;
    private bool _magazineIsFinished;
    private Weapon _weapon;
    
    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _weapon = GetComponentInParent<Weapon>();
    }

    private void Start()
    {
        _meshRenderer.material = EmptyMagazine;
    }

    private void OnEnable()
    {
        EventHub.Ev_MagazineEnded += OnMagazineEnded;
        EventHub.Ev_ReloadFinished += OnReloadFinished;
    }
    
    private void OnDisable()
    {
        EventHub.Ev_MagazineEnded -= OnMagazineEnded;
        EventHub.Ev_ReloadFinished -= OnReloadFinished;
    }

    private void OnMagazineEnded()
    {
        _magazineIsFinished = true;
        
        _meshRenderer.material = EmptyMagazine;
    }
    
    private void OnReloadFinished()
    {
        _magazineIsFinished = false;
        
        _meshRenderer.material = _weapon.CurrentMagazine.MagMaterial;
        
        if (TaskManager.Instance.IsCurrentTask("Reload weapon"))
        {
            TaskManager.Instance.CompleteTask("Reload weapon");   
        }
    }

    private void LateUpdate()
    {
        if (!_magazineIsFinished)
        {
            transform.Rotate(Vector3.up, UnityEngine.Random.Range(0, _rotationSpeed));
        }
    }
}
