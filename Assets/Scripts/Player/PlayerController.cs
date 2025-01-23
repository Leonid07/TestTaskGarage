using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Скорость передвижения
    public float gravity = -9.81f; // Сила гравитации
    public float groundDistance = 0.2f; // Радиус проверки земли
    public LayerMask groundLayer; // Слой для проверки земли
    private CharacterController _characterController; // Ссылка на компонент CharacterController
    private Vector3 _velocity; // Вертикальная скорость
    private bool _isGrounded; // Находится ли игрок на земле

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    public void Move(Vector2 input)
    {
        // Проверяем, находится ли персонаж на земле
        Vector3 groundCheckPosition = transform.position;
        _isGrounded = Physics.CheckSphere(groundCheckPosition, groundDistance, groundLayer);

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f; // Сбрасываем вертикальную скорость при приземлении
        }

        // Преобразуем ввод в локальные координаты движения
        Vector3 moveDirection = new Vector3(input.x, 0, input.y);
        Vector3 worldMove = transform.TransformDirection(moveDirection); // Преобразуем в мировые координаты

        // Движение персонажа
        _characterController.Move(worldMove * moveSpeed * Time.deltaTime);

        // Применение гравитации
        _velocity.y += gravity * Time.deltaTime;
        _characterController.Move(_velocity * Time.deltaTime);
    }
}
