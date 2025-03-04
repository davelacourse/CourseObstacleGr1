using System.Collections.Generic;
using UnityEngine;

public class GestionPieges : MonoBehaviour
{
    [SerializeField] private List<GameObject> _listePieges = new List<GameObject>();
    [SerializeField] private float _intensiteForce = 1000;

    [Header("Vecteur pour le déplacement de l'obstacle")]
    [SerializeField] private float _directionX = 0;
    [SerializeField] private float _directionY = -1;
    [SerializeField] private float _directionZ = 0;

    private List<Rigidbody> _listeRb = new List<Rigidbody>();
    private Vector3 _direction;
    private bool _isTrigger = false;
    
    private void Start()
    {
        _direction = new Vector3(_directionX, _directionY, _directionZ);
        // Récupère tous les rigidbody de chacun de mes pièges
        foreach(var piege in _listePieges)
        {
            _listeRb.Add(piege.GetComponent<Rigidbody>());
        }
        
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !_isTrigger)
        {
            // Active la gravité, applique la force et fait apparaître chacun des pièges
            foreach(var rb in _listeRb)
            {
                rb.gameObject.SetActive(true);
                rb.useGravity = true;
                rb.AddForce(_direction * _intensiteForce);
            }
            _isTrigger = true;
        }
    }
}
