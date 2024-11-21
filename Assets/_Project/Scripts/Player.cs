using UnityEngine;

[RequireComponent(typeof(InputListener))]
public class Player : MonoBehaviour
{
    [SerializeField] private ColorModifier colorModifier;
    private InputListener inputs;

    private void Awake()
    {
        inputs = GetComponent<InputListener>();
        inputs.OnInteract += Inputs_OnInteract;
    }

    private void Start()
    {
        colorModifier.SwapColor();
    }

    private void Inputs_OnInteract()
    {
        colorModifier.SwapColor();
    }
}
