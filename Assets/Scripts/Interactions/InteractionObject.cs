using UnityEngine;

public class InteractionObject : MonoBehaviour
{
    
    public virtual InteractionResultInfo Interact(InteractionResultInfo result)
    {
        
        return result;
    }

    public enum InteractionResult
    {
        Interrupted,
        Success
    }

   

}

public class InteractionResultInfo
{
    public InteractionObject.InteractionResult result;
}