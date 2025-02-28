using UnityEngine;

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
        if(collision.gameObject.tag == "Player")
        {
            if (!_isHit && this.gameObject.tag != "FinNiveau")
            {
                GetComponent<MeshRenderer>().material = _materialHit;
                GameManager.Instance.AddScore();
                _isHit = true;
            }
            else if (!_isHit && this.gameObject.tag == "FinNiveau")
            {
                Debug.Log("Fin de Partie Hit(s)=" + GameManager.Instance.Score);
                collision.gameObject.SetActive(false);
            }
        }
    }
}
