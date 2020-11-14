using UnityEngine;

public class CloseTrigger : MonoBehaviour
{
    [SerializeField] private Plane plane;

    private void Start()
    {
        if (plane == null) {
            plane = GetComponentInParent<Plane>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        plane.TrickManager.SetClose(true);
    }

    private void OnTriggerExit(Collider other)
    {
        plane.TrickManager.SetClose(false);
    }
}