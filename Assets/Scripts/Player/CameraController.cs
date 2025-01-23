using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    public float rotationSpeed = 100f;

    public float minPitch = -35f;
    public float maxPitch = 30f;
    private float yaw; // Угол поворота по оси Y
    private float pitch; // Угол наклона по оси X

    public void Rotate(Vector2 input)
    {
        // Вычисляем изменение угла по Y и X
        yaw += input.x * rotationSpeed * Time.deltaTime;
        pitch -= input.y * rotationSpeed * Time.deltaTime;

        // Ограничиваем угол наклона камеры
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        // Устанавливаем поворот камеры
        transform.eulerAngles = new Vector3(pitch, yaw, 0);

        // Поворачиваем персонажа только по оси Y
        _playerTransform.rotation = Quaternion.Euler(0, yaw, 0);
    }
}
