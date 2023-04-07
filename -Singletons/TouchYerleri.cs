using UnityEngine;

public class TouchYerleri : MonoBehaviour
{
    
    [SerializeField] RectTransform[] rect;  // daha sonra butonlara otoaddto diye bir List e cevirebiliriz.
                                            // böylece bu scripti de static yapabilir.
    public static TouchYerleri ins;

    void Awake()
    {
        ins = this;
    }

    public bool TouchisOnGui(Touch touch)
    {
        for (int i = 0; i < rect.Length; i++)
        {
            //Canvas.ForceUpdateCanvases();
            Vector3 local = rect[i].anchoredPosition;

            if (rect[i].anchoredPosition.x < 0)
            {
                local.x = Screen.width + rect[i].anchoredPosition.x;
            }
            else if (rect[i].anchoredPosition.x == 0)
            {
                local.x = Screen.width * 0.5f;
            }
         

            Vector2 center = new Vector2(local.x, local.y);
            float size = rect[i].rect.size.x;

            if (Sinirlar(touch.position, center, size))
            {
                return true;
            }
        }
        return false;
    }



    bool Sinirlar(Vector2 point, Vector2 center, float size)
    {
        //float oranX = 1f;// Screen.width / 1280f;
        //float oranY = 1f;// Screen.height / 720f;

        //Vector2 origin = new Vector2(center.x * oranX, center.y * oranY);

        //float x = size * .5f;
        //float y = size * .5f;

        //float eksiX = origin.x - x;
        //float eksiY = origin.y - y;
        //float artiX = origin.x + x;
        //float artiY = origin.y + y;

        float eksiX = center.x - size * .5f;
        float eksiY = center.y - size * .5f;
        float artiX = center.x + size * .5f;
        float artiY = center.y + size * .5f;


        //Debug.Log("x-  " + eksiX + "x+  " + artiX + "y-  " + eksiY + "y+  " + artiY);

        if (point.x >= eksiX && point.x <= artiX)
        {
            return point.y >= eksiY && point.y <= artiY;
        }

        return false;
    }
}