using System.Collections;
using System.Collections.Generic;
using Controller.Player;
using Enums.Golem;
using Managers.Global;
using UnityEngine;

public class EnemyRagdoll : MonoBehaviour
{
    [SerializeField] private float _ragdollForce = 320f;
    [SerializeField] private float _rotationForce = 1.5f;
    [SerializeField] private float _maxAngleDeviation = 15f;
    
    private Rigidbody _rb;
    private EGolemType _golemType;
    private SkinnedMeshRenderer _meshRenderer;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    private void Start()
    {
        EventHub.Ev_TurnChange += OnTurnChange;
    }
    
    private void OnDestroy()
    {
        EventHub.Ev_TurnChange -= OnTurnChange;
    }

    public void Initialize(EGolemType golemType)
    {
        _golemType = golemType;

        HandleChangeTexture();
        ILikeToMoveIt();
    }

    private void OnTurnChange()
    {
        PlayerController player = GlobalObject.Player.GetComponent<PlayerController>();
     
        // IF the new state is a CRAFT state (ie after COMBAT) then kill MiniGolem
        if (player.StateCurrent == player.StateCraft)
        {
            Destroy(gameObject);
        }
    }
    
    private void ILikeToMoveIt()
    {
        Vector3 forceDirection = Vector3.up;
        float angleDeviation = Random.Range(-_maxAngleDeviation, _maxAngleDeviation);
        forceDirection = Quaternion.Euler(angleDeviation, angleDeviation, 0) * forceDirection;
        
        _rb.AddForce(forceDirection * _ragdollForce, ForceMode.Impulse);
        
        Vector3 randomTorque = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * _rotationForce;
        _rb.AddTorque(randomTorque, ForceMode.Impulse);
    }
    
    private void HandleChangeTexture()
    {
        string baseTexturePath = $"Art/Golem/{_golemType.ToString().ToLower()}/{_golemType.ToString().ToLower()}_dizzy";

        _meshRenderer.material.SetTexture("_MainTex", Resources.Load<Texture>(baseTexturePath));
    }
}
