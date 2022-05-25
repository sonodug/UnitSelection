using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitClick : MonoBehaviour
{
    [SerializeField] private LayerMask _clickable;
    [SerializeField] private LayerMask _ground;

    [SerializeField] private ParticleSystem _groundMarkerEffect;

    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _clickable))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    UnitSelections.Instance.ShiftClickSelect(hit.collider.gameObject);
                }
                else
                {
                    UnitSelections.Instance.ClickSelect(hit.collider.gameObject);
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (UnitSelections.Instance.selectedUnits.FirstOrDefault() == null)
            {
                
            }
            else
            {
                RaycastHit hit;
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, _ground))
                {
                    _groundMarkerEffect.transform.position = hit.point;
                    Instantiate(_groundMarkerEffect, _groundMarkerEffect.transform.position, _groundMarkerEffect.transform.rotation);
                }
            }
        }
    }
}
