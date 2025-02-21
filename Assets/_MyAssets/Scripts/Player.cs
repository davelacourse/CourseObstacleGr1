using UnityEngine;

public class Player : MonoBehaviour
{
    private const string IS_WALKING = "isWalking";
    
    [SerializeField] private float _vitesseJoueur = 10f; // Vitesse de déplacement joueur
    [SerializeField] private float _vitesseRotation = 1000f; // Vitesse de déplacement joueur

    private Animator _animator;  // Attribut qui contient le controlleur d'animation
    private PlayerInputActions _playerInputActions;

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
    }

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

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
            _animator.SetBool(IS_WALKING, true);  // Déclence l'animation de marche
        }
        else
        {
            _animator.SetBool(IS_WALKING, false); // Déclence l'animation de Idle
        }
    }
}
