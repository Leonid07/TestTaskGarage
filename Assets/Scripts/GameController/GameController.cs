using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private JoystickUI _moveJoystick;
    [SerializeField] private JoystickUI _cameraJoystick;

    private void Update()
    {
        _playerController.Move(_moveJoystick.InputVector);
        _cameraController.Rotate(_cameraJoystick.InputVector);
    }
}