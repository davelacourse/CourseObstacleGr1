using UnityEngine;
using TMPro;

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
    }

    [SerializeField] private TMP_Text _txtTemps = default(TMP_Text);
    [SerializeField] private TMP_Text _txtCollisions = default(TMP_Text);
    [SerializeField] private GameObject _panelPause = default(GameObject);

    private bool _enPause = false;

    private void Start()
    {
        UpdateScore();
    }

    private void Update()
    {
        GestionTempsUI();
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
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
