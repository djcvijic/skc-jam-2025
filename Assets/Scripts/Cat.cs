using System;
using UnityEngine;

public class Cat : PlayerInteractor
{
    [SerializeField] private float speed = 1f;

    protected override void Update()
    {
        base.Update();
        UpdateMovement_Temp();
    }

    private void UpdateMovement_Temp()
    {
        var moveX = Input.GetAxisRaw("Horizontal");
        var moveY = Input.GetAxisRaw("Vertical");
        var moveVector = new Vector2(moveX, moveY).normalized;
        transform.Translate(moveVector * (speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var chair = other.GetComponentInParent<Chair>();
        if (chair != null)
        {
            Debug.Log($"Reached chair: {chair.gameObject.name}");
        }
    }
}