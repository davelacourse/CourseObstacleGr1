using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _vitesseJoueur = 10f; // Vitesse de déplacement joueur
    [SerializeField] private float _vitesseRotation = 1000f; // Vitesse de déplacement joueur
    
    private void Update()
    {
        PlayerMove();
    }

    // Méthode responsable des déplacements du joueur
    private void PlayerMove()
    {
        float dirX = Input.GetAxis("Horizontal");
        float dirY = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(dirX, 0f, dirY);

        // Normalise le vector à 1 peut importe la direction
        direction = direction.normalized;

        //Déplace le joueur dans dans la direction voulu
        transform.Translate(direction * Time.deltaTime * _vitesseJoueur, Space.World);

        if(direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = 
                Quaternion.RotateTowards(transform.rotation, toRotation, _vitesseRotation * Time.deltaTime);
        }
    }
}
