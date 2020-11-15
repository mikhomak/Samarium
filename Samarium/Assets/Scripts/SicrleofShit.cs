using UnityEngine;

public class SicrleofShit : MonoBehaviour
{
    private bool hasApplied;
    [SerializeField] private LevelManager levelManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Plane>() != null) {
            levelManager.DoubleCurrentScore();
            
        }
    }


}