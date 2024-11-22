using System;
using UnityEngine;

[RequireComponent(typeof(InputListener), typeof(MoveForward))]
public class Player : Singleton<Player>
{
    [SerializeField] private ColorModifier colorModifier;
    private InputListener inputs;
    private MoveForward moveForward;

    public event Action<Color> OnColorSwap;

    protected override void Awake()
    {
        base.Awake();
        
        inputs = GetComponent<InputListener>();
        moveForward = GetComponent<MoveForward>();
        inputs.OnInteract += Inputs_OnInteract;
    }

    private void Start()
    {
        colorModifier.SwapColor();
        OnColorSwap?.Invoke(colorModifier.CurrentColor);

        
    }

    private void Update()
    {
        GameManager.Instance.SetScore(transform.position.z);
    }

    private void Inputs_OnInteract()
    {
        colorModifier.SwapColor();
        OnColorSwap?.Invoke(colorModifier.CurrentColor);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player collides with a wall
        if (other.TryGetComponent(out Wall wall))
        {
            // Compare player color with portal color
            Color portalColor = wall.GetPortalColor();
            if (ColorModifier.ColorsAreEqual(colorModifier.CurrentColor, portalColor))
            {
                moveForward.Speed += 1f;
            }
            else
            {
                Handheld.Vibrate();
                GameManager.Instance.Play();
            }
        }
    }

    public Color GetCurrentColor()
    {
        return colorModifier.CurrentColor;
    }
}
