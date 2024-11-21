using UnityEngine;

[CreateAssetMenu(fileName = "ColorPalette", menuName = "Custom/Color Palette", order = 1)]
public class ColorPalette : ScriptableObject
{
    public Color color1;
    public Color color2;
    public Color color3;

    public Color GetRandomColor()
    {
        int rand = Random.Range(0, 3);

        return rand switch
        {
            0 => color1,
            1 => color2,
            2 => color3,
            _ => color1,
        };
    }
}
