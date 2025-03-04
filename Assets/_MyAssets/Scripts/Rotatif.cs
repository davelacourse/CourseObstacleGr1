using UnityEngine;

public class Rotatif : MonoBehaviour
{
    [SerializeField] private float _vitesseRotation = 10f;

    private void Update()
    {
        transform.Rotate(_vitesseRotation * Time.deltaTime, 0f, 0f);
    }
}
