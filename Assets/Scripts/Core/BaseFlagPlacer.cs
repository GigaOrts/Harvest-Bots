using UnityEngine;

public class BaseFlagPlacer : MonoBehaviour
{
    [SerializeField] private Flag _flagPrefab;

    private const string LayerPlaceable = "Placeable";
    private BaseFlagHandler _currentflagHandler;

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (hitInfo.collider.gameObject.layer.Equals(LayerMask.NameToLayer(LayerPlaceable)))
                {
                    if (_currentflagHandler == null)
                        return;

                    if (_currentflagHandler.IsFlagPlaced == false)
                    {
                        PlaceFlag(hitInfo);
                    }
                    else
                    {
                        ReplaceFlag(hitInfo);
                    }
                }
                else if (hitInfo.collider.TryGetComponent(out BaseFlagHandler flagHandler))
                {
                    _currentflagHandler = flagHandler;
                    Debug.Log(_currentflagHandler.name);
                }
            }
        }
    }

    private void ReplaceFlag(RaycastHit hitInfo)
    {
        _currentflagHandler.Flag.transform.position = hitInfo.point;
    }

    private void PlaceFlag(RaycastHit hitInfo)
    {
        Flag flag = Instantiate(_flagPrefab, hitInfo.point, Quaternion.identity);
        _currentflagHandler.SetFlag(flag);
    }
}
