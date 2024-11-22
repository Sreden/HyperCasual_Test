using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(InputListener), typeof(MoveForward), typeof(Rigidbody))]
public class Player : Singleton<Player>
{
    [SerializeField] private ColorModifier colorModifier;
    private InputListener inputs;
    private MoveForward moveForward;
    private Rigidbody rb;

    public event Action<Color> OnColorSwap;

    protected override void Awake()
    {
        base.Awake();
        inputs = GetComponent<InputListener>();
        moveForward = GetComponent<MoveForward>();
        moveForward.IsActive = false;
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;

        inputs.OnInteract += Inputs_OnInteract;

        if (GameManager.Instance == null)
        {
            Instance_OnReady();
        }
        else
        {
            GameManager.Instance.OnReady += Instance_OnReady;
        }
    }

    private void Instance_OnReady()
    {
        moveForward.IsActive = true;
    }

    private void Start()
    {
        colorModifier.SwapColor();
        OnColorSwap?.Invoke(colorModifier.CurrentColor);
    }

    private void OnDestroy()
    {
        if (GameManager.Instance == null)
        {
            return;
        }

        GameManager.Instance.OnReady -= Instance_OnReady;
    }

    private void Update()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SetScore(transform.position.z);
        }
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
            if (!ColorModifier.ColorsAreEqual(colorModifier.CurrentColor, portalColor))
            {
                Handheld.Vibrate();
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.Play();
                }
                else
                {
                    SceneManager.LoadScene("MainScene");
                }
            }
        }
    }

    public Color GetCurrentColor()
    {
        return colorModifier.CurrentColor;
    }
}
