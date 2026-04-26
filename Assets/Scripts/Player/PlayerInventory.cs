using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int[] weaponIdInInventorySlot;
    
    //sv98 - 0
    //mp5 - 1
    public HandsAnimator[] handsAnimators;

    public int currentSlotId = -1;

    public int currentItemID = -1;
    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectSlot(int i) //slot number
    {
         if (currentItemID != -1)
         {
                handsAnimators[currentItemID].root.transform.localPosition = new Vector3(0, -100, 0);
                handsAnimators[currentItemID].root.gameObject.SetActive(false);
         }
         
         currentSlotId = i;
         int itemId = weaponIdInInventorySlot[i];
         currentItemID = itemId;
         handsAnimators[currentItemID].root.gameObject.SetActive(true);
         handsAnimators[currentItemID].root.transform.localPosition = new Vector3(0, 0, 0);
         handsAnimators[itemId].gunAnimatorHandler.CallEquip();
          
    }
}
