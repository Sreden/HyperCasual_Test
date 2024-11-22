using System;
using System.Collections;
using UnityEngine;

public class PlayerColorModifier : ColorModifier
{
    [SerializeField] private float changeColorTime = 0.5f;

    private const string OuterColorParamName = "_OuterColor";
    private const string InnerColorParamName = "_InnerColor";
    private const string RadiusParamName = "_Radius";

    private Coroutine changeColorCoroutine = null;

    public override void SetColor(Color color)
    {
        if (changeColorCoroutine != null)
        {
            StopCoroutine(changeColorCoroutine);
        }

        ResetMaterial();
        base.SetColor(color);
        changeColorCoroutine = StartCoroutine(ChangeColorProcess());
    }

    private void ResetMaterial()
    {
        meshRenderer.material.SetColor(OuterColorParamName, CurrentColor);
        meshRenderer.material.SetColor(InnerColorParamName, GetNextColor());
        meshRenderer.material.SetFloat(RadiusParamName, 0.1f);
    }

    private IEnumerator ChangeColorProcess()
    {
        float elapsedTime = 0.1f;
        while (elapsedTime < changeColorTime)
        {
            meshRenderer.material.SetFloat(RadiusParamName, elapsedTime / (changeColorTime + 0.1f));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        ResetMaterial();
    }
}