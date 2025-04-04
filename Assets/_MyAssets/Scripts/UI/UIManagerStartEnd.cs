using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManagerStartEnd : UI
{
    [SerializeField] private GameObject _panelMain = default(GameObject);
    [SerializeField] private GameObject _panelInstructions = default(GameObject);
    
    

    public void ToggleInstructions()
    {
        _panelMain.SetActive(!_panelMain.activeSelf); // Inverse la visibilité du panneau
        _panelInstructions.SetActive(!_panelInstructions.activeSelf);
    }
}
