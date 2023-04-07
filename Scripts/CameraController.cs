using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    [SerializeField] // inspectörden ekle cameraTasýyýcýsýnýn trasformu
    Transform cameraTransform;
    Camera mainCamera; // field of view deðiþtirmek için Globalsden chachele
    [SerializeField]
    float initfov = 50;
    [SerializeField]
    bool setInitCamYpos = false;
    [SerializeField]
    float initYpos = 1.76f, camSpeed = 30f, cameraYuksekligi = 33f;

    [SerializeField] // takip etmek icin
    CameraState currentState;

    CharacterController controller;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(2f);
        mainCamera = Globals.ins.mainCamera;
        controller = Globals.playerController;
        currentState = CameraState.initState;
        CycleCameraState();
        initfov = 50;
        if (!setInitCamYpos)
        {
            initYpos = cameraTransform.localPosition.y;
        }
    }
    private void Update()
    {
        if (currentState == CameraState.Fps) return;

        UpdateCamera(camSpeed);

    }

    public void CycleCameraState()
    {
        if (currentState == CameraState.Fps)
        {
            float _fov = initfov + 11f;
            ChangeState(CameraState.nullState, _fov);
            //currentState = CameraState.Top1;
            controller.enabled = false;
            Vector3 rot = new Vector3(60, cameraTransform.localEulerAngles.y, cameraTransform.localEulerAngles.z);
            cameraTransform.DOLocalRotate(rot, 0.9f);
            cameraTransform.DOLocalMoveY(cameraYuksekligi, 1f).OnComplete(() => ChangeState(CameraState.Top1, _fov)); ;

        }
        else if (currentState == CameraState.Top1)
        {
            ChangeState(CameraState.nullState);
            //currentState = CameraState.Fps;
            controller.enabled = true;
            Vector3 rot = new Vector3(0, cameraTransform.localEulerAngles.y, cameraTransform.localEulerAngles.z);
            cameraTransform.DOLocalRotate(rot, 0.9f);
            cameraTransform.DOLocalMoveY(initYpos, 1f).OnComplete(() => ChangeState(CameraState.Fps));
        }
        else if (currentState == CameraState.initState)
        {
            //currentState = CameraState.Fps;
            cameraTransform.localPosition = new Vector3(0, initYpos, 0);
            controller.enabled = true;
            ChangeState(CameraState.Fps);
        }
    }


    void ChangeState(CameraState state, float? _fov = null)
    {


        currentState = state;
        GlobalVeriler.CAMERA_STATE = currentState;
        CameraFovSet(_fov ?? initfov);
    }

    private void CameraFovSet(float _fow)
    {
        DOTween.To(x => mainCamera.fieldOfView = x, mainCamera.fieldOfView, _fow, 1f);
    }

    void UpdateCamera(float speed)
    {
        if (PlayerInput.NotWASD) return;
        Vector2 targetDir = PlayerInput.KeyInput;
        targetDir.Normalize();
        Vector3 targetPos = (transform.forward * targetDir.y + transform.right * targetDir.x) * speed * Time.deltaTime;
        transform.position = transform.position + targetPos;
    }
}
