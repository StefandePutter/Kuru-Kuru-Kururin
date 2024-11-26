using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class InputManager : MonoBehaviour
{
    
    private Controls m_Controls;

    public Vector2 moveValue;
    public bool isUsingSpecial = false;

    private void Awake()
    {
        m_Controls = new Controls();

        // on move
        m_Controls.Player.Move.Enable();
        m_Controls.Player.Move.performed += OnMove;
        m_Controls.Player.Move.canceled += OffMove;

        // on ability use
        m_Controls.Player.Special.Enable();
        m_Controls.Player.Special.performed += flipSpecialBool;
        m_Controls.Player.Special.canceled += flipSpecialBool;
    }

    private void OnMove(CallbackContext _context)
    {
        moveValue = _context.ReadValue<Vector2>();
    }

    private void OffMove(CallbackContext _context)
    {
        moveValue = Vector2.zero;
    }

    private void flipSpecialBool(CallbackContext _context)
    {
        Debug.Log("used Special attack");
        isUsingSpecial = !isUsingSpecial;
    }
}
