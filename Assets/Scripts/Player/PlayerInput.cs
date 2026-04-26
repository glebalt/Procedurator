using UnityEngine;

public class PlayerInput : MonoBehaviour
{
   public PlayerInventory playerInventory;

    public struct Input
    {
        public float mouseX;
        public float mouseY;

        public float keyboardX;
        public float keyboardZ;
        public bool reqSprint;

    }


    public struct GunInput
    {
        public bool reqReload;
        public bool reqShoot;
        public bool reqShootAuto;
        public bool reqADS;
        public bool holdingLMB;
    }
    
    public static Input playerInput;

    public static GunInput gunInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gunInput.reqADS = false;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInput();
        UpdateGunInput();
    }

    void UpdateInput()
    {
        playerInput.keyboardX = UnityEngine.Input.GetAxisRaw("Horizontal");
        playerInput.keyboardZ = UnityEngine.Input.GetAxisRaw("Vertical");
        playerInput.mouseX = UnityEngine.Input.GetAxis("Mouse X");
        playerInput.mouseY = UnityEngine.Input.GetAxis("Mouse Y");
        playerInput.reqSprint = UnityEngine.Input.GetKey(KeyCode.LeftShift);
        
        for (int i = 0; i < 4; i++)
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                print(i);
                playerInventory.SelectSlot(i);
            }
        }
    }

    void UpdateGunInput()
    {
        gunInput.reqReload = UnityEngine.Input.GetKeyDown(KeyCode.R);
        gunInput.reqShoot = UnityEngine.Input.GetKeyDown(KeyCode.Mouse0);
        gunInput.reqShootAuto = UnityEngine.Input.GetKey(KeyCode.Mouse0);
        gunInput.holdingLMB = UnityEngine.Input.GetKey(KeyCode.Mouse0);
        if (UnityEngine.Input.GetKeyDown(KeyCode.Mouse1))
        {
            gunInput.reqADS = !gunInput.reqADS;
        }
    }
}
