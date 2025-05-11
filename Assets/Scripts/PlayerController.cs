using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Cat))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Cat _cat;
    private Rigidbody2D _rb;
    private Vector2 moveInput;
    [SerializeField] private int playerId;

    private float Acceleration => App.Instance.GameSettings.CatAcceleration;
    private float MaxSpeed => App.Instance.GameSettings.CatMaxSpeed;
    private float Friction => App.Instance.GameSettings.CatFriction;

    private void Start()
    {
        _cat = GetComponent<Cat>();
        _rb = GetComponent<Rigidbody2D>();
        _rb.drag = Friction;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        if (moveInput != Vector2.zero)
        {
            CatAnimationEventManager.TriggerAnimationChange("CatWalk", playerId);
        }
        else
        {
            CatAnimationEventManager.TriggerAnimationChange("CatIdle", playerId);
        }

        if (moveInput.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (moveInput.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void Update()
    {
        UpdateMove();
    }

    private void UpdateMove()
    {
        if (_cat.InMischief) return;

        Vector3 force = (Vector3)moveInput * Acceleration;
        _rb.AddForce(force * Time.deltaTime);

        if (_rb.velocity.magnitude > MaxSpeed)
            _rb.velocity = _rb.velocity.normalized * MaxSpeed;
    }
}