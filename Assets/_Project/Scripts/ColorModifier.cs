using UnityEngine;

public class ColorModifier : MonoBehaviour
{
    [SerializeField] private ColorPalette colorPalette;
    [SerializeField] private MeshRenderer meshRenderer;
    private Color currentColor = Color.black;

    public void SwapColor()
    {
        // Switch to the next color in the palette
        if (ColorsAreEqual(currentColor, colorPalette.color1))
        {
            currentColor = colorPalette.color2;
        }
        else if (ColorsAreEqual(currentColor, colorPalette.color2))
        {
            currentColor = colorPalette.color3;
        }
        else
        {
            currentColor = colorPalette.color1;
        }

        SetColor(currentColor);
    }

    public void SetColor(Color color)
    {
        meshRenderer.material.color = color;
    }

    private bool ColorsAreEqual(Color a, Color b)
    {
        return Mathf.Approximately(a.r, b.r) &&
               Mathf.Approximately(a.g, b.g) &&
               Mathf.Approximately(a.b, b.b) &&
               Mathf.Approximately(a.a, b.a);
    }
}
