using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;

    private Vector2 inputVect = Vector2.zero;

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(inputVect.x * moveSpeed, rb.velocity.y);
    }

    void OnMove(InputValue value)
    {
        inputVect = value.Get<Vector2>();
    }
}
