using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public void GoToPlay()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

    public void Quit()
    {
        
    }
}
