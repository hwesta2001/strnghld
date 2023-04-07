using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInput
{

    public static bool GetTouch => Input.touchCount > 0;
    public static bool CantUpdate => !GetSagTik && !GetTouch && TouchOnGui;
    public static float Sens => GetTouch ? Globals.ins.sens : 1f;
    public static float Smooth => GetTouch ? Globals.ins.smoot : 1f;

    public static bool TouchOnGui
    {
        get
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                if (TouchYerleri.ins.TouchisOnGui(Input.GetTouch(i)))
                {
                    return true;
                }
            }
            return false;
        }
    }
    public static Vector2 KeyInput
    {
        get
        {

            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {

                return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            }
            else
            {

                return new Vector2(Globals.ins.joy.Horizontal, Globals.ins.joy.Vertical) * 10;
            }
        }
    }

    public static bool KeyJump
    {
        get
        {
            return Input.GetButtonDown("Jump");
        }
    }

    public static Vector2 MouseDelta
    {
        get
        {
            if (GetTouch)
            {

                return TouchDelta();
            }
            else
            {
                if (!GetSagTik) return Vector2.zero;
                return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            }
        }
    }

    static Touch _touch
    {
        get
        {
            if (JoyStickMoving())
            {
                if (Input.touchCount > 1)
                {
                    Touch[] touches = Input.touches;
                    float x = touches[0].position.x;
                    int a = 0;
                    for (int i = 0; i < touches.Length; i++)
                    {
                        float y = touches[i].position.x;
                        if (y >= x)
                        {
                            x = y;
                            a = i;
                        }
                    }
                    return touches[a];
                }
                else
                {
                    return Input.GetTouch(0);
                }
            }
            else
            {
                return Input.GetTouch(0);
            }
        }
    }

    static bool JoyTouch()
    {
        return JoyStickMoving() && Input.touchCount <= 1;
    }

    static Vector2 TouchDelta()
    {

        if (JoyTouch())
        {
            return Vector2.zero;
        }

        Touch touch = _touch;
        Vector2 delta = Vector2.zero;
        if (touch.phase == TouchPhase.Moved)
        {
            float ara = Globals.ins.ara;
            delta = touch.deltaPosition;
            delta *= Globals.ins.kat * 1280 / Screen.width;
            delta.x *= 1.5f;
            float x = Mathf.Abs(delta.x);
            float y = Mathf.Abs(delta.y);

            if (x < ara)
            {
                delta.x = 0;
            }
            if (y < ara)
            {
                delta.y = 0;
            }
            if (Globals.ins.nowBuilding)
            {
                delta.y *= .5f;
                delta.x *= .5f;
            }
        }
        return delta;

    }


    static bool GetSagTik
    {
        get
        {
            if (Input.GetMouseButton(1))
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = false;
                return true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                return false;
            }
        }
    }




    public static bool NotWASD
    {
        get
        {
            if (JoyStickMoving())
            {
                return false;
            }
            else
            {
                if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }

    static bool JoyStickMoving()
    {
        if (Globals.ins.joy.Horizontal != 0 || Globals.ins.joy.Vertical != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool InsaaTButonTrigger;
    public static bool BuildHandler()
    {


        if (GetTouch)
        {
            if (InsaaTButonTrigger)
            {
                InsaaTButonTrigger = false;
                return true;
            }
            foreach (Touch item in Input.touches)
            {
                if (item.tapCount >= 2)
                {
                    return true;
                }
            }
            return false;
        }


        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return false;
            }
            InsaaTButonTrigger = false;
            return true;
        }


        return false;
    }




    public static bool MouseHitOnObject(Transform targetTransform)
    {

        Ray ray = Globals.ins.mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 1000))
        {
            if (hit.transform == targetTransform)
            {
                Debug.Log("ture ");
                return true;

            }
            else
            {
                Debug.Log("falseee ");

                return false;
            }
        }
        return false;
    }

}

