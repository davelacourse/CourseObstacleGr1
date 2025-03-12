using UnityEngine;

public class Player : MonoBehaviour
{
    private const string IS_WALKING = "isWalking";
    private const string IS_JUMPING = "isJumping";
    private const string IS_DANCING = "isDancing";

    [SerializeField] private float _vitesseJoueur = 10f; // Vitesse de déplacement joueur
    [SerializeField] private float _vitesseRotation = 1000f; // Vitesse de déplacement joueur

    [Header("Gestion Saut")]
    [SerializeField] private float _jumpForce = 1000f; // Vitesse de déplacement joueur
    [SerializeField] private Transform _feetTransform = default(Transform);
    [SerializeField] private LayerMask _floorLayer = default(LayerMask);

    private Animator _animator;  // Attribut qui contient le controlleur d'animation
    private PlayerInputActions _playerInputActions;
    private Rigidbody _rb;

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
        _playerInputActions.Player.Jump.performed += Jump_performed;
        _playerInputActions.Player.Dance.performed += Dance_performed;
    }

    private void Dance_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _animator.SetBool(IS_DANCING, true);
    }

    private void OnDestroy()
    {
        //S'assure de disable la map et aussi d'annuler les souscription aux events
        _playerInputActions.Player.Disable();
        _playerInputActions.Player.Jump.performed -= Jump_performed;  //Souscrit à l'event Jump performed
        _playerInputActions.Player.Dance.performed -= Dance_performed; //Souscrit à l'event Jump performed
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    // Méthode responsable des déplacements du joueur
    private void PlayerMove()
    {
        // float dirX = Input.GetAxis("Horizontal");  //Input Manager
        // float dirY = Input.GetAxis("Vertical");    //Input Manager
        Vector2 direction2D = _playerInputActions.Player.Move.ReadValue<Vector2>();
        
        Vector3 direction = new Vector3(direction2D.x, 0f, direction2D.y);

        // Normalise le vector à 1 peut importe la direction
        direction = direction.normalized;

        //Déplace le joueur dans dans la direction voulu
        //transform.Translate(direction * Time.deltaTime * _vitesseJoueur, Space.World);

        //Déplace le rigidbody du joueur
        float RbMoveOnX = direction.x * Time.fixedDeltaTime * _vitesseJoueur;
        float RbMoveOnZ = direction.z * Time.fixedDeltaTime * _vitesseJoueur;
        float RbMoveOnY = _rb.linearVelocity.y;
 
        _rb.linearVelocity = new Vector3(RbMoveOnX, RbMoveOnY, RbMoveOnZ);  // Utilise la vitesse

        //_rb.AddForce(new Vector3(RbMoveOnX, RbMoveOnY, RbMoveOnZ)); // Pousse par une force sur l'objet

        //Rotation du joueur
        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = 
                Quaternion.RotateTowards(transform.rotation, toRotation, _vitesseRotation * Time.deltaTime);
            _animator.SetBool(IS_WALKING, true);  // Déclence l'animation de marche
            _animator.SetBool(IS_DANCING, false);
        }
        else
        {
            _animator.SetBool(IS_WALKING, false); // Déclence l'animation de Idle
        }

        //Vérifie si le joueur ne touche pas le une force le pousse vers le bas
        if (!Physics.CheckSphere(_feetTransform.position, 0.5f, _floorLayer))
        {
            _rb.AddForce(Vector3.down * 60f);
        }
        else
        {
           _animator.SetBool(IS_JUMPING, false);
        }
    }

    private void Jump_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _animator.SetBool(IS_JUMPING, true);
        _animator.SetBool(IS_DANCING, false);
        // Si le joueur touche le sol on peut sauter 
        if (Physics.CheckSphere(_feetTransform.position, 0.5f, _floorLayer))
        {
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }
}
