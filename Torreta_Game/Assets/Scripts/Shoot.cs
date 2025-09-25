using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    InputManager inputAction;
    public GameObject bullet;
    public Transform puntaCanon;
    void Start()
    {
        inputAction = new InputManager();
        inputAction.Enable();
        
    }

    void Update()
    {
        if (inputAction.Player.Jump.triggered)
        {
            Instantiate(bullet, puntaCanon.position, puntaCanon.rotation);
        }
    }
}
