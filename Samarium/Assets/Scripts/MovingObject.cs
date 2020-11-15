using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField] private Vector3 direction;
    [SerializeField] private float speed;
    [SerializeField] private float distanceToChange;

    private Vector3 posChange;

    private void Start()
    {
        posChange = transform.position;
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(posChange, transform.position) > distanceToChange) {
            direction *= -1;
            posChange = transform.position;
        }

        transform.position += direction * speed;
    }
}