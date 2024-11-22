using UnityEngine;

public class MoveForward : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float maxSpeed = 40f;

    public float Speed
    {
        get => speed;
        set
        {
            // Ensure speed does not exceed maxSpeed
            speed = (value > maxSpeed) ? maxSpeed : value;
        }
    }

    private void Update()
    {
        transform.Translate(Speed * Time.deltaTime * Vector3.forward);
    }
}
