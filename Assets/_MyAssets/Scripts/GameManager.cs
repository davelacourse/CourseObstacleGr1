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
    private float _tempsNiveau1;
    private int _collisionsNiveau1;

    private void Start()
    {
        _score = 0;
        _tempsDepart = Time.time;
    }

    public void AddScore()
    {
        _score++;
    }

    public void SetNiveau1(float temps)
    {
        _tempsNiveau1 = temps;
        _collisionsNiveau1 = _score;
    }

    public void AfficherFinDePartie()
    {
        Debug.Log("*** Fin de partie ****");
        Debug.Log("Temps Niveau 1 = " + _tempsNiveau1.ToString("f2") + " secondes");
        Debug.Log("Collisions Niveau 1 = " + _collisionsNiveau1);
        Debug.Log("*******************************");
        Debug.Log("Temps Niveau 2 = " + ((Time.time-_tempsDepart) - _tempsNiveau1).ToString("f2") + " secondes");
        Debug.Log("Collisions Niveau 2 = " + (_score - _collisionsNiveau1));
        Debug.Log("*******************************");
        Debug.Log("Temps total avec collisions = " + ((Time.time - _tempsDepart) + _score).ToString("f2") + " secondes");
    }
}
