using UnityEngine;
using UnityEngine.UI;

public class RaycastDrawer : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Image _point;
    [SerializeField] private Ray _ray;
    [SerializeField] private float _maxDistance = 50f;
    [SerializeField] private float _radius = 0.1f;

    private string _tagObject = "Cube";

    public void Update()
    {
        _ray = _camera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        Debug.DrawRay(_ray.origin, _ray.direction * _maxDistance, Color.yellow);

        if (Physics.Raycast(_ray, out hit, Mathf.Infinity))
        {
            Transform objectHit = hit.transform;

            if (objectHit.tag == _tagObject)
            {
                _point.color = Color.red;
            }
            else
            {
                _point.color = Color.white;
            }
        }
    }
}