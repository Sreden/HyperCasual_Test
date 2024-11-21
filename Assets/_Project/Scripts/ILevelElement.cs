using UnityEngine;

public interface ILevelElement
{
    public void Reset();
    public void OnColorChange(Color color);
}