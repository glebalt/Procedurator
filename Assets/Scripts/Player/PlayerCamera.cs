using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform cameraHolder;
    public Transform player;

    private static float rotX;

    private static float rotY;

    public static float RotX {get {return rotX;}}
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        cameraHolder.transform.position = player.position + new Vector3(0, .8f, 0);
        rotY += PlayerInput.playerInput.mouseX;
        rotX += PlayerInput.playerInput.mouseY;
        
        cameraHolder.transform.rotation = Quaternion.Euler(-rotX, rotY, 0);
    }
    
    
}
