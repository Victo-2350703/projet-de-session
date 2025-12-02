using UnityEngine;
using UnityEngine.InputSystem;

public class MooseLookHorizontale : MonoBehaviour
{
    public InputActionReference lookAction; // référence à l’action "Look"
    public float sensitivity = 5f;
    private Vector2 lookInput;
    private float xRotation = 0f;
    void Start()
    {
        // Verrouille le curseur au centre de l’écran et le rend invisible
        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        lookAction.action.Enable();
    }

    void Update()
    {
        lookInput = lookAction.action.ReadValue<Vector2>();
        
        float sourisX = lookInput.x * sensitivity * Time.deltaTime;
        float sourisY = lookInput.y * sensitivity * Time.deltaTime;

        xRotation -= sourisY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Rotation verticale (caméra)
        // => Si la caméra est un enfant du joueur :
        Camera mainCam = Camera.main;
        if (mainCam != null)
        {
            mainCam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }

        // Rotation horizontale (joueur)
        transform.Rotate(Vector3.up * sourisX);
    }
}
