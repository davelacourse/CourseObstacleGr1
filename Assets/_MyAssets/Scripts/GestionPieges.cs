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
    
    private void Start()
    {
        _direction = new Vector3(_directionX, _directionY, _directionZ);
        foreach(var piege in _listePieges)
        {
            _listeRb.Add(piege.GetComponent<Rigidbody>());
        }
        
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //_rb.useGravity = true;
            //_rb.AddForce(_direction * _intensiteForce);
        }
    }
}
