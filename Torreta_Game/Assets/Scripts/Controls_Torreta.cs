using UnityEngine;
using UnityEngine.InputSystem;

public class Controls_Torreta : MonoBehaviour
{
    public Transform pivoteCuerpo;
    public Transform pivoteCañon;
    public Vector2 delta;
    public float deltaY;
    public float deltaX;

    public float sensitivity = 0.5f;

    void Update()
    {
        delta = Mouse.current.delta.ReadValue();

        deltaX = delta.x * sensitivity;
        deltaY = delta.y * sensitivity;

        pivoteCuerpo.Rotate(0f, deltaX, 0f);
        pivoteCañon.Rotate(0f, 0f, deltaY);
    }
}
