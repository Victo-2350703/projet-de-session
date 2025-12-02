using UnityEngine;
using UnityEngine.InputSystem;

public class MooseLookVerticale : MonoBehaviour
{
    public InputActionReference lookAction; // référence à l’action "Look"
    public float sensitivity = 5f;
    private Vector2 lookInput;
    private float yRotation = 0f;
    void Start()
    {
    }

    void Update()
    {
        lookInput = lookAction.action.ReadValue<Vector2>();

        float sourisX = lookInput.x * sensitivity * Time.deltaTime;
        float sourisY = lookInput.y * sensitivity * Time.deltaTime;

        yRotation -= sourisX;
        yRotation = Mathf.Clamp(yRotation, -90f, 90f);

        // Rotation verticale (caméra)
        // => Si la caméra est un enfant du joueur :
        Camera mainCam = Camera.main;
        if (mainCam != null)
        {
            mainCam.transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);
        }

        // Rotation verticale (joueur)
        transform.Rotate(Vector3.left * sourisY);
    }
}

