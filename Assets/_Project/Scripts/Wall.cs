using System.Collections;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private GameObject wallObject;
    [SerializeField] private ColorModifier colorModifier;
    [SerializeField] Transform[] deactivatedTransforms;
    private Coroutine currentCoroutine;

    private void Awake()
    {
        Player.Instance.OnColorSwap += Instance_OnColorSwap;
    }

    private void Start()
    {
        Instance_OnColorSwap(Player.Instance.GetCurrentColor());
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
        if (Player.Instance != null)
        {    
            Player.Instance.OnColorSwap -= Instance_OnColorSwap;
        }
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
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        currentCoroutine = StartCoroutine(MovingProcess(wallObject.transform.localPosition, Vector3.zero, 0.3f));
    }

    public void Deactivate()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        currentCoroutine = StartCoroutine(MovingProcess(wallObject.transform.localPosition, GetRandomPositionInTransforms(), 0.3f));
    }

    public Color GetPortalColor()
    {
        return colorModifier.CurrentColor;
    }

    private IEnumerator MovingProcess(Vector3 beginPosition, Vector3 targetPosition, float timeToMove)
    {
        float elapsedTime = 0f;

        while (elapsedTime <= timeToMove)
        {
            elapsedTime += Time.deltaTime;
            wallObject.transform.localPosition = Vector3.Lerp(beginPosition, targetPosition, (float)elapsedTime/timeToMove);
            yield return null;
        }

        wallObject.transform.localPosition = targetPosition;
    }

    private Vector3 GetRandomPositionInTransforms()
    {
        if (deactivatedTransforms.Length == 0)
        {
            return Vector3.zero;
        }

        int rand = Random.Range(0, deactivatedTransforms.Length);
        return deactivatedTransforms[rand].localPosition;
    }
}
