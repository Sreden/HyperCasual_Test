using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private GameObject lasers;
    [SerializeField] private ColorModifier colorModifier;

    private void Awake()
    {
        Player.Instance.OnColorSwap += Instance_OnColorSwap;
    }

    private void Instance_OnColorSwap(Color color)
    {
        if (ColorModifier.ColorsAreEqual(colorModifier.CurrentColor, color))
        {
            Deactivate();
        }
        else
        {
            Activate();
        }
    }

    public void Activate()
    {
        lasers.SetActive(true);
    }

    public void Deactivate()
    {
        lasers.SetActive(false);
    }

    public Color GetPortalColor()
    {
        return colorModifier.CurrentColor;
    }
}
