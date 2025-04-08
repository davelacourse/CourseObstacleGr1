using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Définition singleton
    public static GameManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    private int _score;
    public int Score => _score;  //accesseur à l'attribut _score

    private float _tempsDepart;
    public float TempsDepart => _tempsDepart;

    private float _tempsFin;
    public float TempsFin => _tempsFin;

    private void Start()
    {
        _score = 0;
        _tempsDepart = Time.time;
    }

    public void AddScore()
    {
        _score++;
        UIManager.Instance.UpdateScore();
    }

    public void SetTempsFin(float temps)
    {
        _tempsFin = temps;
        
    }
}
