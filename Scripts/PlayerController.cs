
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform cameraTransform;
    [SerializeField] float mouseSensivity = 500f;
    [SerializeField] float walkSpeed = 60f;

    [SerializeField] [Range(0, 0.2f)] float mouseSmootTime = 0.03f;
    [SerializeField] float gravity = -13f;


    [SerializeField] AnimationCurve jumpFall;
    [SerializeField] float jumpMultiplier = 4f;


    float velcityY = 0f;
    float mousePitch = 0f;
    CharacterController controller;
    //PlayerInput input;

    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMouseDeltaVelocity = Vector2.zero;
    private bool jumping;

    private void Start()
    {
        controller = Globals.playerController;
        //input = GetComponent<PlayerInput>(); // bu classtaki veriler statik olduðu icin chache gerek yok.
        UpdateMouselook();
    }
    void Update()
    {

        UpdateMovement();
        if (PlayerInput.CantUpdate) return;
        if (PlayerInput.TouchOnGui) return;
        UpdateMouselook();
    }


    void UpdateMouselook()
    {

        Vector2 targetDelta = PlayerInput.MouseDelta;
        //targetDelta.Normalize();
        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetDelta,
                                ref currentMouseDeltaVelocity, mouseSmootTime * PlayerInput.Smooth);



        if (GlobalVeriler.CAMERA_STATE == CameraState.Fps)
        {
            mousePitch -= currentMouseDelta.y * Time.deltaTime * mouseSensivity * PlayerInput.Sens;
            mousePitch = Mathf.Clamp(mousePitch, -88f, 90f);
            cameraTransform.localEulerAngles = Vector3.right * mousePitch;
        }
        else if (GlobalVeriler.CAMERA_STATE == CameraState.Top1)
        {
            mousePitch -= currentMouseDelta.y * Time.deltaTime * mouseSensivity * PlayerInput.Sens;
            mousePitch = Mathf.Clamp(mousePitch, 45f, 89f);
            cameraTransform.localEulerAngles = Vector3.right * mousePitch;
        }
        else
        {
            mousePitch = cameraTransform.localEulerAngles.x;
        }

        transform.Rotate(Vector3.up * currentMouseDelta.x * Time.deltaTime * mouseSensivity * PlayerInput.Sens);

    }



    void UpdateMovement()
    {
        if (!controller.enabled) return;

        Vector2 targetDir = PlayerInput.KeyInput;
        targetDir.Normalize();


        if (controller.isGrounded)
        {
            velcityY = 0f;
        }

        velcityY += gravity * Time.deltaTime;
        controller.Move(Vector3.up * velcityY * Time.deltaTime);
        JumpInput();

        if (PlayerInput.NotWASD) return;

        Vector3 velocity = (transform.forward * targetDir.y + transform.right * targetDir.x) * walkSpeed * Time.deltaTime;
        controller.Move(velocity);

    }


    void JumpInput()
    {
        if (PlayerInput.KeyJump && !jumping)
        {
            jumping = true;
            StartCoroutine(JumpEvent());

        }

    }

    IEnumerator JumpEvent()
    {
        controller.slopeLimit = 90f;
        float timeinAir = 0f;
        do
        {
            float jumpForce = jumpFall.Evaluate(timeinAir);
            controller.Move(Vector3.up * jumpForce * jumpMultiplier * Time.deltaTime);
            timeinAir += Time.deltaTime;
            yield return null;
        } while (!controller.isGrounded && controller.collisionFlags != CollisionFlags.Above);
        controller.slopeLimit = 45f;
        jumping = false;
    }



}
