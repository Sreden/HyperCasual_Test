using UnityEngine;

public class ColorModifier : MonoBehaviour
{
    [SerializeField] private ColorPalette colorPalette;
    [SerializeField] private bool isInitRandom;
    [SerializeField] protected MeshRenderer meshRenderer;

    private Color currentColor = Color.black;
    public Color CurrentColor => currentColor;

    private void Awake()
    {
        if (!isInitRandom)
        {
            return;
        }

        currentColor = colorPalette.GetRandomColor();
        SetColor(currentColor);
    }

    public static bool ColorsAreEqual(Color a, Color b)
    {
        return Mathf.Approximately(a.r, b.r) &&
               Mathf.Approximately(a.g, b.g) &&
               Mathf.Approximately(a.b, b.b) &&
               Mathf.Approximately(a.a, b.a);
    }

    public void SwapColor()
    {
        // Switch to the next color in the palette
        SetColor(GetNextColor());
    }

    protected Color GetNextColor()
    {
        Color nextColor;
        if (ColorsAreEqual(currentColor, colorPalette.color1))
        {
            nextColor = colorPalette.color2;
        }
        else if (ColorsAreEqual(currentColor, colorPalette.color2))
        {
            nextColor = colorPalette.color3;
        }
        else
        {
            nextColor = colorPalette.color1;
        }

        return nextColor;
    }

    public virtual void SetColor(Color color)
    {
        currentColor = color;
        meshRenderer.material.color = color;
    }
}
