using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;

    private void OnEnable()
    {
        thrust.Enable();
    }

    private void Update()
    {
        if (thrust.IsPressed())
        {
            Debug.Log("Dang,I need some space!");
        }
    }
}