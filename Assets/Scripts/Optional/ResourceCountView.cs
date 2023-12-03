using TMPro;
using UnityEngine;

public class ResourceCountView : MonoBehaviour
{
    [SerializeField] private TextMeshPro _textTMP;
    
    void Update()
    {
        _textTMP.text = (transform.childCount - 1).ToString();
    }
}
