using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private Vector2 moveData;
    [SerializeField] private float moveSpeed;
    [SerializeField] private JoyStickCtrl moveJoy;

    [Header("Aim")]
    [SerializeField] private Vector2 aimData;
    [SerializeField] private float turnSpeed;
    [SerializeField] private JoyStickCtrl aimJoy;

    [SerializeField] private CharacterController characterController;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private CameraCtrl cameraCtrl;
    [SerializeField] private Animator playerAnimator;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    private void Start()
    {
      characterController = GetComponent<CharacterController>();

        moveJoy.inputData += GetInputMoveData;
        aimJoy.inputData += GetInputAimData;

        mainCamera = Camera.main;
        cameraCtrl =FindObjectOfType<CameraCtrl>();
        playerAnimator = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();
    }


    private void FixedUpdate()
    {
        if (PlayerCtrl.Instance.PlayerHeath.CurrentHeath <= 0) return;
        MovePlayer();
        RotatePlayer();
        RotateCamera();
    }
    public void GetInputMoveData(Vector2 data)
    {
        moveData = data;
    }
    public void GetInputAimData(Vector2 data)
    {
        aimData = data;
    }

    public void MovePlayer()
    {
        Vector3 moveDir = CalculateDir(moveData);

        if (moveData.magnitude > 0)
        {
            PlayAudio();
        }
        else
        {
            StopAudio();
        }

        characterController.Move(moveDir * moveSpeed * Time.deltaTime);

        float forward = Vector3.Dot(moveDir, transform.forward);
        float right = Vector3.Dot(moveDir, transform.right);

        playerAnimator.SetFloat("rightDir" , right);
        playerAnimator.SetFloat("forwardDir" , forward);

        characterController.Move(Vector3.down * Time.deltaTime * 8f);
    }

    public void RotatePlayer()
    {
        Vector2 aim = moveData;
        if (aimData.magnitude != 0)
        {
            aim = aimData;
        }
        Vector3 aimDir = CalculateDir(aim);

        if(aim.magnitude > 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation,
                Quaternion.LookRotation(aimDir, Vector3.up), turnSpeed*Time.deltaTime);
        }

        float dir = Vector3.Dot(aimDir, Vector3.right);

        playerAnimator.SetFloat("turnDir" , dir);
    }

    public void RotateCamera()
    {
        //if (aimData.magnitude == 0)
        //{
            if (moveData.magnitude != 0 && cameraCtrl != null)
            {
                cameraCtrl.RotateCamera(moveData.x);
            }
        //}
        //else
        //{
        //    if(cameraCtrl != null)
        //    {
        //        cameraCtrl.RotateCamera(aimData.x);
        //    }
        //}
        
    }

    public Vector3 CalculateDir(Vector2 data)
    {
        Vector3 rightDir = mainCamera.transform.right;
        Vector3 forwardDir = Vector3.Cross(rightDir, Vector3.up);
        Vector3 dir = data.x * rightDir + data.y * forwardDir;
        return dir;
    }

    public void PlayAudio()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void StopAudio()
    { 
        if(audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
