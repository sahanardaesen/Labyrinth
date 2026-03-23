using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))] // Rigidbody yoksa otomatik ekler, hata riskini sıfırlar.
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float maxSpeed = 5f; // Topun sonsuza kadar hızlanmasını önlemek için.

    private Rigidbody _rb;
    private Vector2 _moveInput;

    private void Awake() => _rb = GetComponent<Rigidbody>();

    public void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        LimitVelocity();
    }

    private void ApplyMovement()
    {
        // Hareket vektörünü oluştur
        Vector3 movement = new Vector3(_moveInput.x, 0f, _moveInput.y);
        
        // Kuvvet uygula
        _rb.AddForce(movement * speed, ForceMode.Force);
    }

    private void LimitVelocity()
    {
        // Topun hızı maxSpeed'i aşarsa, hızını sabitle (Optimizasyon ve kontrol)
        if (_rb.linearVelocity.magnitude > maxSpeed)
        {
            _rb.linearVelocity = _rb.linearVelocity.normalized * maxSpeed;
        }
    }
}