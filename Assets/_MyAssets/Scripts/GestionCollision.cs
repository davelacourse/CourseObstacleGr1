using UnityEngine;
using UnityEngine.SceneManagement;

public class GestionCollision : MonoBehaviour
{
    [SerializeField] private Material _materialHit = default(Material);
    private bool _isHit;

    private void Start()
    {
        _isHit = false;
   }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!_isHit && this.gameObject.tag != "FinNiveau")
            {
                GetComponent<MeshRenderer>().material = _materialHit;
                GameManager.Instance.AddScore();
                _isHit = true;
            }
            else if (!_isHit && this.gameObject.tag == "FinNiveau")
            {
                int noScene = SceneManager.GetActiveScene().buildIndex;
                if (noScene >= SceneManager.sceneCountInBuildSettings - 2)
                {
                    GameManager.Instance.SetTempsFin(Time.time - GameManager.Instance.TempsDepart);
                }
                SceneManager.LoadScene(noScene + 1);

            }
        }
    }
}
