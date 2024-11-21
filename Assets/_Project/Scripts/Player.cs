using System;
using UnityEngine;

[RequireComponent(typeof(InputListener))]
public class Player : Singleton<Player>
{
    [SerializeField] private ColorModifier colorModifier;
    private InputListener inputs;

    public event Action<Color> OnColorSwap;

    protected override void Awake()
    {
        base.Awake();
        
        inputs = GetComponent<InputListener>();
        inputs.OnInteract += Inputs_OnInteract;
    }

    private void Start()
    {
        colorModifier.SwapColor();
        OnColorSwap?.Invoke(colorModifier.CurrentColor);
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
            Debug.Log("hit");
            // Compare player color with portal color
            Color portalColor = wall.GetPortalColor();
            if (ColorModifier.ColorsAreEqual(colorModifier.CurrentColor, portalColor))
            {
                
            }
            else
            {
                GameManager.Instance.Replay();
            }
        }
    }
}
