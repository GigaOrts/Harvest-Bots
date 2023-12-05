using UnityEngine;

public class BaseFlagHandler : MonoBehaviour
{
    public Flag Flag { get; private set; }
    public bool IsFlagPlaced { get; private set; }

    public void SetFlag(Flag flag)
    {
        Flag = flag;
        IsFlagPlaced = true;
    }
}
