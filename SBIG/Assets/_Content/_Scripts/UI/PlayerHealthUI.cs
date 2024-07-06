using Controller.Player;
using Managers.Global;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private RectMask2D _healthMask;
    private float _endPadding = 460f;
    private float _startPadding = 25f;
    private float _targetPadding;
    void Start()
    {
        _healthMask.padding = new Vector4(_healthMask.padding.x, _healthMask.padding.y, _startPadding, _healthMask.padding.w);
        
        EventHub.Ev_PlayerHealthChange += OnPlayerHealthChange;
    }
    
    private void OnDestroy()
    {
        EventHub.Ev_PlayerHealthChange -= OnPlayerHealthChange;
    }
    
    private void OnPlayerHealthChange()
    {
        float playerHealth = (float) GlobalObject.Player.GetComponent<PlayerController>().PlayerHealth;
        _targetPadding = MapHealthToSlider(playerHealth);
        
        Vector4 initialPadding = _healthMask.padding;
        _healthMask.padding = new Vector4(initialPadding.x, initialPadding.y, _targetPadding, initialPadding.w);
    }
    
    private float MapHealthToSlider(float health)
    {
        float sliderValue = _endPadding + ( (health - 0) / (100f - 0) ) * (_startPadding - _endPadding);
        return sliderValue;
    }
}
