using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector2 moveInput;
    [SerializeField] private int playerId;

    private float Acceleration => App.Instance.GameSettings.CatAcceleration;
    private float MaxSpeed => App.Instance.GameSettings.CatMaxSpeed;
    private float Friction => App.Instance.GameSettings.CatFriction;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.drag = Friction;
    }

    private void Update()
    {
        Vector3 force = (Vector3)moveInput * Acceleration;
        _rb.AddForce(force * Time.deltaTime);

        if (_rb.velocity.magnitude > MaxSpeed)
            _rb.velocity = _rb.velocity.normalized * MaxSpeed;
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
}