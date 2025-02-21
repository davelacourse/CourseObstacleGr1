using UnityEngine;

public class Player : MonoBehaviour
{
    private const string IS_WALKING = "isWalking";
    
    [SerializeField] private float _vitesseJoueur = 10f; // Vitesse de d�placement joueur
    [SerializeField] private float _vitesseRotation = 1000f; // Vitesse de d�placement joueur

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

    // M�thode responsable des d�placements du joueur
    private void PlayerMove()
    {
        float dirX = Input.GetAxis("Horizontal");
        float dirY = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(dirX, 0f, dirY);

        // Normalise le vector � 1 peut importe la direction
        direction = direction.normalized;

        //D�place le joueur dans dans la direction voulu
        transform.Translate(direction * Time.deltaTime * _vitesseJoueur, Space.World);

        if(direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = 
                Quaternion.RotateTowards(transform.rotation, toRotation, _vitesseRotation * Time.deltaTime);
            _animator.SetBool(IS_WALKING, true);  // D�clence l'animation de marche
        }
        else
        {
            _animator.SetBool(IS_WALKING, false); // D�clence l'animation de Idle
        }
    }
}
