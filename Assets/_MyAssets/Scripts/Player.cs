using UnityEngine;

public class Player : MonoBehaviour
{
    private const string IS_WALKING = "isWalking";
    
    [SerializeField] private float _vitesseJoueur = 10f; // Vitesse de déplacement joueur
    [SerializeField] private float _vitesseRotation = 1000f; // Vitesse de déplacement joueur
    [SerializeField] private float _jumpForce = 1000f; // Vitesse de déplacement joueur
    [SerializeField] private Transform _feetTransform = default(Transform);
    [SerializeField] private LayerMask _floorLayer = default(LayerMask);

    private Animator _animator;  // Attribut qui contient le controlleur d'animation
    private PlayerInputActions _playerInputActions;
    private Rigidbody _rb;
    private bool isGrounded = true;

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
    }

    private void OnDestroy()
    {
        _playerInputActions.Player.Disable();
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {

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
        _rb.linearVelocity = new Vector3(direction.x * Time.fixedDeltaTime * _vitesseJoueur
            , _rb.linearVelocity.y, direction.z * Time.fixedDeltaTime * _vitesseJoueur);  // Utilise la vitesse

        
        
        //_rb.AddForce(direction * Time.fixedDeltaTime * _vitesseJoueur); // Pousse par une force sur l'objet

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

        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            if(Physics.CheckSphere(_feetTransform.position, 0.5f, _floorLayer))
            {
                _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            }
        }


        if (!Physics.CheckSphere(_feetTransform.position, 0.5f, _floorLayer))
        {
            _rb.AddForce(Vector3.down * 50f); //, ForceMode.Impulse);
        }
        
    }
}
