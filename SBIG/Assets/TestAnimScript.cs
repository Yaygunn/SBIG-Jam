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

    public enum EGolemType
    {
        FIRE,
        WOOD,
        ROCK,
        WATER
    }

    private SkinnedMeshRenderer _skinnedMeshRenderer;


    public EGolemState GolemState;
    public EGolemType GolemType;



    void Awake()
    {
        AssignComponents();
    }
    void Start()
    {
        HandleChangeTexture();
    }

    void OnValidate()
    {
        AssignComponents();
        HandleChangeTexture();
    }


    void Update()
    {
        HandleChangeTexture();
    }


    void HandleChangeTexture()
    {
        string baseTexturePath = $"Art/Golems/{GolemType.ToString().ToLower()}/{GolemState.ToString().ToLower()}";

        _skinnedMeshRenderer.material.SetTexture("_MainTex", Resources.Load<Texture>(baseTexturePath));
    }

    void AssignComponents()
    {
        _skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }

}
