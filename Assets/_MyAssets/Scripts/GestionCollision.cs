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
        if (!_isHit)
        {
            GetComponent<MeshRenderer>().material = _materialHit;
            GameManager.Instance.AddScore();
            _isHit = true;
        }

    }
}
