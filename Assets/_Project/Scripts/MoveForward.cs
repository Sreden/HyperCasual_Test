using UnityEngine;

public class MoveForward : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float maxSpeed = 40f;
    [SerializeField] private float upSpeedTimer = 1f;
    float elapsedTime = 0f;
    private bool isActive = false;

    public float Speed
    {
        get => speed;
        set
        {
            // Ensure speed does not exceed maxSpeed
            speed = (value > maxSpeed) ? maxSpeed : value;
        }
    }

    public bool IsActive { get => isActive; set => isActive = value; }

    private void Update()
    {
        if (!isActive)
        {
            return;
        }

        elapsedTime += Time.deltaTime;
        if (elapsedTime >= upSpeedTimer)
        {
            elapsedTime = 0f;
            Speed += 0.5f;
        }

        transform.Translate(Speed * Time.deltaTime * Vector3.forward);
    }
}
