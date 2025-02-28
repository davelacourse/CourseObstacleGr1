using UnityEngine;

public class GameManager : MonoBehaviour
{
    //D�finition singleton
    public static GameManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private int _score;
    public int Score => _score;  //accesseur � l'attribut _score

    private void Start()
    {
        _score = 0;
    }

    public void AddScore()
    {
        _score++;
        Debug.Log("Hits : " + _score);
    }
}
