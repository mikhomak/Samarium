using UnityEngine;

public class SicrleofShit : MonoBehaviour
{
    private bool hasApplied;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private AudioClip doubleScoreAudioClip;

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Plane>() != null) {
            if (!hasApplied) {
                levelManager.DoubleCurrentScore();
                levelManager.specialEffectsAudioSource.PlayOneShot(doubleScoreAudioClip);                
                hasApplied = true;
                Destroy(gameObject);
            }
        }
    }


}