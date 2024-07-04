using UnityEngine;


public class TestAnimScript : MonoBehaviour
{

    public enum EGolemState
    {
        HAPPY,
        HUNGRY,
        ANGRY,
        DIZZY
    }

    public enum ETempAnim
    {
        HEADBUTT,
        CHARGE,
        WALK,
        IDLE
    }

    public enum EGolemType
    {
        FIRE,
        WOOD,
        ROCK,
        WATER
    }

    private SkinnedMeshRenderer _skinnedMeshRenderer;
    private Animator _animator;
    public EGolemState GolemState;
    public EGolemType GolemType;
    public ETempAnim TempAnim;


    void Awake()
    {
        AssignComponents();
    }
    void Start()
    {
        HandleChangeTexture();
        HandleChangeAnimation();
    }

    void OnValidate()
    {
        AssignComponents();
        HandleChangeTexture();
        HandleChangeAnimation();
    }


    void Update()
    {
        HandleChangeTexture();
        HandleChangeAnimation();
    }


    void HandleChangeTexture()
    {
        string baseTexturePath = $"Art/Golems/{GolemType.ToString().ToLower()}/{GolemState.ToString().ToLower()}";

        _skinnedMeshRenderer.material.SetTexture("_MainTex", Resources.Load<Texture>(baseTexturePath));
    }


    void HandleChangeAnimation()
    {

        void ResetAllTriggers()
        {
            _animator.ResetTrigger("isHeadbutt");
            _animator.ResetTrigger("isCharge");
            _animator.SetFloat("speed", 0);
        }

        switch (TempAnim)
        {
            case ETempAnim.HEADBUTT:
                ResetAllTriggers();
                _animator.SetTrigger("isHeadbutt");
                break;
            case ETempAnim.CHARGE:
                ResetAllTriggers();
                _animator.SetBool("isCharge", true);
                break;
            case ETempAnim.WALK:
                ResetAllTriggers();
                _animator.SetFloat("speed", 1);
                break;
            case ETempAnim.IDLE:
                ResetAllTriggers();
                break;
            default:
                break;
        }

    }

    void AssignComponents()
    {
        _skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        _animator = GetComponent<Animator>();
    }

}
