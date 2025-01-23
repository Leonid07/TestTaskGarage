using UnityEngine;

public class InteractableController : MonoBehaviour
{
    public string interactableTag = "Interactable";
    public Vector3 holdPositionOffset = new Vector3(0, 1.5f, 1f);
    public float interactionRange = 5f;
    public float pickupSpeed = 5f;

    private Camera mainCamera;
    private GameObject heldObject; // Текущий удерживаемый предмет
    private Rigidbody heldObjectRigidbody; // Rigidbody удерживаемого предмета
    private bool isPickingUp; // Флаг для проверки, в процессе ли объект перемещения к позиции

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        HandleInput();

        if (isPickingUp && heldObject != null)
        {
            MoveHeldObjectToHoldPosition();
        }
    }

    private void HandleInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                HandleInteraction(touch.position);
            }
        }

        // Для редактора и ПК
        else if (Input.GetMouseButtonDown(0))
        {
            HandleInteraction(Input.mousePosition);
        }
    }

    private void HandleInteraction(Vector2 screenPosition)
    {
        Ray ray = mainCamera.ScreenPointToRay(screenPosition);
        if (Physics.Raycast(ray, out RaycastHit hit, interactionRange))
        {
            Debug.Log($"Hit: {hit.collider.name}");

            if (hit.collider.CompareTag(interactableTag))
            {
                GameObject clickedObject = hit.collider.gameObject;

                if (heldObject == clickedObject)
                {
                    DropObject();
                }
                else
                {
                    if (heldObject != null)
                    {
                        DropObject();
                    }
                    PickupObject(clickedObject);
                }
            }
        }
        else
        {
            Debug.Log("No hit detected.");
        }
    }

    private void PickupObject(GameObject obj)
    {
        heldObject = obj;
        heldObjectRigidbody = obj.GetComponent<Rigidbody>();

        if (heldObjectRigidbody != null)
        {
            heldObjectRigidbody.isKinematic = true; // Отключаем физику
        }

        isPickingUp = true;
    }

    private void DropObject()
    {
        if (heldObject == null) return;

        heldObject.transform.SetParent(null);

        if (heldObjectRigidbody != null)
        {
            heldObjectRigidbody.isKinematic = false; // Включаем физику
        }

        heldObject = null;
        heldObjectRigidbody = null;
        isPickingUp = false;
    }

    private void MoveHeldObjectToHoldPosition()
    {
        if (heldObject == null) return;

        Vector3 holdPosition = mainCamera.transform.position + mainCamera.transform.TransformDirection(holdPositionOffset);
        heldObject.transform.position = Vector3.MoveTowards(heldObject.transform.position, holdPosition, pickupSpeed * Time.deltaTime);

        if (Vector3.Distance(heldObject.transform.position, holdPosition) < 0.1f)
        {
            heldObject.transform.position = holdPosition;
            heldObject.transform.SetParent(mainCamera.transform);
            isPickingUp = false;
        }
    }
}