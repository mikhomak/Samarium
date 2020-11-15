using UnityEngine;

public class Border : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Plane>()) {
            Rigidbody rbd = other.GetComponent<Rigidbody>();
            if (rbd) {
                rbd.AddForce(Vector3.down * 500f, ForceMode.Impulse);
            }
        }
    }
}