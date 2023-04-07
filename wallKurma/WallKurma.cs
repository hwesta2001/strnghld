using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(WallCekme), typeof(RayWallCollider))]
public class WallKurma : MonoBehaviour
{
    [SerializeField] Button startButon;
    [SerializeField] Button buildingButon;
    public Transform ilkDuvar, ikinciDuvar;
    [SerializeField] WallUpdate wallOneUpdate, wallTwoUpdate;

    public GameObject wallBasNormal, wallBasAralik;

    public float duvarYuksekligi = 5;

    public Action wallKuruldu;  // çarmaKontrol updatini kontrol ediyor
    public Action StartWallCreate;

    public bool frontColliderUpdate;
    public LayerMask wallCantCollideLayers;

    public enum State { init, ilkDuvar, ikinciDuvar, son }
    public State state;

    //public static WallKurma ins;

    bool nesneyeCarpiyor;
    public bool halatCarpiyor;

    Color initColor = Color.white;
    MeshRenderer rend1, rend2;



    void Awake()
    {
        Globals.WALL_KURMA = this;
    }

    void Start()
    {

        StateChane(State.init);
        rend1 = ilkDuvar.GetComponentInChildren<MeshRenderer>();
        rend2 = ikinciDuvar.GetComponentInChildren<MeshRenderer>();
    }

    void StateChane(State _state)
    {
        state = _state;
        switch (state)
        {

            case State.init:
                InitState();
                break;

            case State.ilkDuvar:
                IlkDuvarState();
                break;

            case State.ikinciDuvar:
                IkinciDuvarState();
                break;

            case State.son:
                SonState();
                break;

        }

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
            switch (state)
            {
                case State.ilkDuvar:
                    if (nesneyeCarpiyor)
                    {
                        TextControl.ins.AlertText("Can't place there m'Lord!", color: Color.red);
                    }
                    else
                    {
                        StateChane(State.ikinciDuvar);
                    }

                    break;

                case State.ikinciDuvar:
                    if (nesneyeCarpiyor)
                    {
                        TextControl.ins.AlertText("Can't place there m'Lord!", color: Color.red);
                    }
                    else
                    {
                        StateChane(State.son);
                    }
                    break;

                case State.son:
                    break;

                case State.init:
                    break;
            }
        }
    }

    public void WallUpdate(bool canUpdate, Transform other)
    {
        if (canUpdate)
        {

            if (state == State.ilkDuvar)
            {
                wallOneUpdate.onlyRotate = false;
            }
            else if (state == State.ikinciDuvar)
            {
                wallTwoUpdate.onlyRotate = false;
            }
            else
            {
                wallOneUpdate.onlyRotate = false;
                wallTwoUpdate.onlyRotate = false;
            }

        }
        else
        {
            if (state == State.ilkDuvar)
            {
                wallOneUpdate.onlyRotate = true;
                DuvarSabitle.Yaslan(ilkDuvar, other);
            }
            else if (state == State.ikinciDuvar)
            {
                wallTwoUpdate.onlyRotate = true;
                DuvarSabitle.Yaslan(ikinciDuvar, other);
            }
            //else
            //{
            //    wallOneUpdate.enabled = false;
            //    wallTwoUpdate.enabled = false;
            //}
        }
    }



    void InitState()
    {


        wallTwoUpdate.onlyRotate = false;
        wallOneUpdate.onlyRotate = false;


        frontColliderUpdate = false;
        ilkDuvar.localScale = new Vector3(1, duvarYuksekligi * .2f, 1);
        ikinciDuvar.localScale = new Vector3(1, duvarYuksekligi * 0.2f, 1);


        ilkDuvar.gameObject.SetActive(false);
        ikinciDuvar.gameObject.SetActive(false);
        startButon.gameObject.SetActive(true);
        buildingButon.gameObject.SetActive(false);

        wallOneUpdate.wallState = 0;
        wallTwoUpdate.wallState = 0;
    }


    void IlkDuvarState()
    {


        frontColliderUpdate = true;

        startButon.gameObject.SetActive(false);
        buildingButon.gameObject.SetActive(true);

        ilkDuvar.gameObject.SetActive(true);

        wallOneUpdate.enabled = true;
        wallOneUpdate.onlyRotate = false;
        ikinciDuvar.gameObject.SetActive(false);

        wallOneUpdate.wallState = 1;
        wallTwoUpdate.wallState = 1;

    }

    void IkinciDuvarState()
    {
        wallKuruldu?.Invoke();

        wallOneUpdate.onlyRotate = true;

        ikinciDuvar.gameObject.SetActive(true);

        wallTwoUpdate.enabled = true;
        wallTwoUpdate.onlyRotate = false;

        wallOneUpdate.wallState = 2;
        wallTwoUpdate.wallState = 2;
    }

    void SonState()
    {
        frontColliderUpdate = false;

        wallKuruldu?.Invoke();

        wallOneUpdate.enabled = false;
        wallTwoUpdate.enabled = false;

        StartWallCreate?.Invoke();

        StartCoroutine(TurntoInýt());

        wallOneUpdate.wallState = 3;
        wallTwoUpdate.wallState = 3;
    }

    IEnumerator TurntoInýt()
    {
        yield return new WaitForEndOfFrame();
        StateChane(State.init);
    }




    public void IlkButonTrigger()
    {
        if (state == State.ilkDuvar) return;
        StateChane(State.ilkDuvar);
    }
    public void IkinciButonTrigger()
    {
        StateChane(State.init);
    }



    public void CarpmaAyarla(bool isCarpiyor, Color currentColor)
    {
        nesneyeCarpiyor = isCarpiyor;

        if (initColor == currentColor) return;
        initColor = currentColor;

        rend1.material.color = currentColor;
        rend2.material.color = currentColor;



    }



}
