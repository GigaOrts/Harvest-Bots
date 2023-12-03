using UnityEngine;

public class Resource : MonoBehaviour
{
    public bool IsOrdered { get; private set; }

    public void SetOrderedStatus()
    {
        IsOrdered = true;
    }

    public void ResetOrderedStatus()
    {
        IsOrdered = false;
    }
}
