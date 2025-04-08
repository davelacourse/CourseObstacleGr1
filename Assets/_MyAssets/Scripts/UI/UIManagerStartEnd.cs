using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;

public class UIManagerStartEnd : UI
{
    [Header("Scène Départ")]
    [SerializeField] private GameObject _panelMain = default(GameObject);
    [SerializeField] private GameObject _panelInstructions = default(GameObject);
    [SerializeField] private GameObject _boutonDemarrer = default(GameObject);
    [SerializeField] private GameObject _boutonRetour = default(GameObject);

    [Header("Scène fin")]
    [SerializeField] private TMP_Text _txtTemps = default(TMP_Text);
    [SerializeField] private TMP_Text _txtCollisions = default(TMP_Text);
    [SerializeField] private TMP_Text _txtPointage = default(TMP_Text);
    [SerializeField] private GameObject _boutonRetourFin = default(GameObject);

    private bool _instructionsOn = false;


    private void Start()
    {
        // Si sur scène de départ vérifie s'il existe un GameManager et le détruit si c'est le cas
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            GameManager gameManager = FindAnyObjectByType<GameManager>();
            if (gameManager != null) 
            {
                Destroy(gameManager);
            }
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(_boutonDemarrer);
        }
        else if(SceneManager.GetActiveScene().buildIndex >= SceneManager.sceneCountInBuildSettings - 1)
        {
            _txtTemps.text = "Temps : " + GameManager.Instance.TempsFin.ToString("f2");
            _txtCollisions.text = "Collisions : " + GameManager.Instance.Score;
            float total = GameManager.Instance.TempsFin + GameManager.Instance.Score;
            _txtPointage.text = "Pointage : " + total.ToString("f2");

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(_boutonRetourFin);
        }
    }

    public void ToggleInstructions()
    {
        _panelMain.SetActive(!_panelMain.activeSelf); // Inverse la visibilité du panneau
        _panelInstructions.SetActive(!_panelInstructions.activeSelf);

        if(_instructionsOn)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(_boutonDemarrer);
            _instructionsOn = false;
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(_boutonRetour);
            _instructionsOn = true;
        }
    }
}
