using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class UIManager : UI
{
    //Définition singleton
    public static UIManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
        _playerInputActions.Player.Pause.performed += Event_Pause;
    }

    private void OnDestroy()
    {
        _playerInputActions.Player.Disable();
        _playerInputActions.Player.Pause.performed -= Event_Pause;
    }

    private void Event_Pause(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        TogglePause();
    }

    [SerializeField] private TMP_Text _txtTemps = default(TMP_Text);
    [SerializeField] private TMP_Text _txtCollisions = default(TMP_Text);
    [SerializeField] private GameObject _panelPause = default(GameObject);
    [SerializeField] private GameObject _boutonContinuer = default(GameObject);

    private bool _enPause = false;
    private PlayerInputActions _playerInputActions;

    private void Start()
    {
        UpdateScore();
        _enPause = false;
        Time.timeScale = 1f;
    }

    private void Update()
    {
        GestionTempsUI();
        // Utilisation du Input Manager (old)
        /*
        if(Input.GetButtonDown("Pause"))
        {
            TogglePause();
        }
        */
    }

    public void TogglePause()
    {
        _panelPause.SetActive(!_panelPause.activeSelf);
        if(_enPause)
        {
            Time.timeScale = 1f;
            _enPause = false;
        }
        else
        {
            Time.timeScale = 0f;
            _enPause = true;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(_boutonContinuer);
        }
    }

    private void GestionTempsUI()
    {
        float temps = Time.time - GameManager.Instance.TempsDepart;
        _txtTemps.text = "Temps : " + temps.ToString("f2");
    }

    public void UpdateScore()
    {
        _txtCollisions.text = "Collisions : " + GameManager.Instance.Score.ToString();
    }
}
