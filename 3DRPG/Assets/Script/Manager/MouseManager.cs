using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class MouseManager : SingleTon<MouseManager>
{


    public event Action<Vector3> OnMouseClicked;
    public event Action<GameObject> OnClickedEnemy;

    RaycastHit hitInfo;

    public Texture2D point, doorway, attack, target, arrow;
    protected override void Awake()
    {
        base.Awake();
    }

    void Update()
    {
        SetCursorTexture();
        MouseControl();
    }

    void SetCursorTexture()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray,out hitInfo))
        {
            //«–ªª Û±ÍÃ˘Õº
            switch (hitInfo.collider.gameObject.tag)
            {
                case "Ground":
                    Cursor.SetCursor(target, new Vector2(16, 16),CursorMode.Auto);
                    break;
                case "Enemy":
                    Cursor.SetCursor(attack, new Vector2(2, 2), CursorMode.Auto);
                    break;
                case "Attackable":
                    Cursor.SetCursor(attack, new Vector2(2, 2), CursorMode.Auto);
                    break;
                case "Portal":
                    Cursor.SetCursor(doorway, new Vector2(2, 2), CursorMode.Auto);
                    break;
            }
        }
    }

    void MouseControl()
    {
        if (Input.GetMouseButtonDown(0) && hitInfo.collider != null)
        {
            if (hitInfo.collider.gameObject.CompareTag("Ground"))
            {
                OnMouseClicked?.Invoke(hitInfo.point);
            }else if (hitInfo.collider.gameObject.CompareTag("Enemy"))
            {
                OnClickedEnemy?.Invoke(hitInfo.collider.gameObject);
            }
            else if (hitInfo.collider.gameObject.CompareTag("Attackable"))
            {
                OnClickedEnemy?.Invoke(hitInfo.collider.gameObject);
            }
            else if (hitInfo.collider.gameObject.CompareTag("Portal"))
            {
                OnMouseClicked?.Invoke(hitInfo.point);
            }
        }
    }
}
