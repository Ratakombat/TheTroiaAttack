using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    #region
    public static Transform instance;
    #endregion

    private PlayerStats playerStats = null;
    [SerializeField] private GameObject thirdCamera;
    [SerializeField] private GameObject aimCamera;

    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    [SerializeField]
    private float rotationSpeed = 5f;

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private Transform barrelTransform;

    [SerializeField]
    private Transform bulletParent;

    [SerializeField]
    private float bulletHitMissDistance = 25f;

    [SerializeField]
    private float animationSmoothTime = 0.1f;

    [SerializeField]
    private float animationPlayTransition = 0.15f;

    [SerializeField]
    private Transform aimTarget;
    [SerializeField]
    private float aimDistance = 1f;

    [SerializeField]
    private int damage = 5;


    private CharacterController controller;
    private PlayerInput playerInput;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Transform cameraTransform;
    
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction shootAction;

    private Animator animator;
    int jumpAnimation;
    int recoilAnimation;

    //Audio
    [SerializeField] private AudioSource stepSource;
    [SerializeField] private AudioClip[] stepsClip;
    
    int moveXAnimationParameterId;
    int moveZAnimationParameterId;

    Vector2 currentAnimationBlendVector;
    Vector2 animationVelocity;


    

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        playerStats = GetComponent<PlayerStats>();

        

        cameraTransform = Camera.main.transform;
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        shootAction = playerInput.actions["Shoot"];

        Cursor.lockState = CursorLockMode.Locked;

        //Animations
        animator = GetComponent<Animator>();
        jumpAnimation = Animator.StringToHash("Pistol Jump");
        recoilAnimation = Animator.StringToHash("Pistol Shoot Recoil");

        moveXAnimationParameterId = Animator.StringToHash("MoveX");
        moveZAnimationParameterId = Animator.StringToHash("MoveZ");
        
        //-------------
        instance = this.transform;


    }

    private void OnEnable() {
        shootAction.performed += _ => ShootGun();
    }

    private void OnDisable() {
        shootAction.performed -= _ => ShootGun();
    }

    private void ShootGun(){

        if(!playerStats.IsDead())
        {
            
            
            RaycastHit hit;
            
            GameObject bullet = GameObject.Instantiate(bulletPrefab, barrelTransform.position, Quaternion.identity, bulletParent);
            BulletController bulletController = bullet.GetComponent<BulletController>();
            GetComponent<AudioSource>().Play();

            if(Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, Mathf.Infinity)){
                
                bulletController.target = hit.point;
                bulletController.hit = true;

                CharacterStats enemyStats = hit.transform.GetComponent<CharacterStats>();
                enemyStats.TakeDamage(damage);
                
            }
            else {
                bulletController.target = cameraTransform.position + cameraTransform.forward * bulletHitMissDistance;
                bulletController.hit = false;
            }

            animator.CrossFade(recoilAnimation, animationPlayTransition);
        }
    }

    void Update()
    {
        if (!playerStats.IsDead())
        {
            //Audio
            if (controller.isGrounded && controller.velocity.magnitude > 0.01f && !stepSource.isPlaying)
            {
                stepSource.clip = stepsClip[UnityEngine.Random.Range(0, stepsClip.Length)];
                stepSource.Play();
            }
            //--------------

            aimTarget.position = cameraTransform.position + cameraTransform.forward * aimDistance;


            groundedPlayer = controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            Vector2 input = moveAction.ReadValue<Vector2>();

            currentAnimationBlendVector = Vector2.SmoothDamp(currentAnimationBlendVector, input, ref animationVelocity, animationSmoothTime);

            Vector3 move = new Vector3(currentAnimationBlendVector.x, 0, currentAnimationBlendVector.y);

            move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
            move.y = 0f;
            controller.Move(move * Time.deltaTime * playerSpeed);

            

            //Blend Strafe Animation
            animator.SetFloat(moveXAnimationParameterId, currentAnimationBlendVector.x);
            animator.SetFloat(moveZAnimationParameterId, currentAnimationBlendVector.y);

            // Changes the height position of the player..
            if (jumpAction.triggered && groundedPlayer)
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
                animator.CrossFade(jumpAnimation, animationPlayTransition);
                
            }

            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);

            // Rotate towards camera direction.
            Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
            transform.rotation =  Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            
            
        }
        else 
        {
            thirdCamera.SetActive(false);
            aimCamera.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
        }



    } 


}
