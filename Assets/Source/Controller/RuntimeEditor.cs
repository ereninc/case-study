using UnityEngine;
using UnityEngine.SceneManagement;

public class RuntimeEditor : ControllerBaseModel
{
    [SerializeField] private KeyCode reload;
    [SerializeField] private KeyCode deleteSave;
    
    void Update()
    {
        if (Input.GetKeyDown(reload))
        {
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(deleteSave))
        {
            EToolBar.ResetPrefs();
        }
    }
}