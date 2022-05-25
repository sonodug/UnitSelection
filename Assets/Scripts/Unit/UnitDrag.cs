using System.Collections.Generic;
using UnityEngine;

public class UnitDrag : MonoBehaviour
{
    [SerializeField] private RectTransform _boxVisual;

    private Camera _camera;

    private Rect _selectionBox;

    private Vector2 _startPosition;
    private Vector2 _endPosition;

    private void Start()
    {
        _camera = Camera.main;
        _boxVisual.sizeDelta = Vector2.zero;
    }

    private void Update()
    {
        ClickInput();
        DragInput();
        ReleaseClickInput();
    }

    public void DrawVisual()
    {
        Vector2 boxStart = _startPosition;
        Vector2 boxEnd = _endPosition;

        Vector2 boxCenter = (boxStart + boxEnd) / 2;
        _boxVisual.position = boxCenter;

        Vector2 boxSize = new Vector2(Mathf.Abs(boxStart.x - boxEnd.x), Mathf.Abs(boxStart.y - boxEnd.y));

        _boxVisual.sizeDelta = boxSize;
    }

    public void DrawSelection()
    {
        if (Input.mousePosition.x < _startPosition.x)
        {
            _selectionBox.xMin = Input.mousePosition.x;
            _selectionBox.xMax = _startPosition.x;
        }
        else
        {
            _selectionBox.xMin = _startPosition.x; ;
            _selectionBox.xMax = Input.mousePosition.x;
        }

        if (Input.mousePosition.y < _startPosition.y)
        {
            _selectionBox.yMin = Input.mousePosition.y;
            _selectionBox.yMax = _startPosition.y;
        }
        else
        {
            _selectionBox.yMin = _startPosition.y;
            _selectionBox.yMax = Input.mousePosition.y;
        }
    }

    public void SelectUnits()
    {
        foreach (var unit in UnitSelections.Instance.units)
        {
            if (_selectionBox.Contains(_camera.WorldToScreenPoint(unit.transform.position)))
            {
                UnitSelections.Instance.DragSelect(unit);
            }
        }
    }

    private void ClickInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startPosition = Input.mousePosition;
            _selectionBox = new Rect();
        }
    }

    private void DragInput()
    {
        if (Input.GetMouseButton(0))
        {
            _endPosition = Input.mousePosition;
            DrawVisual();
            DrawSelection();
        }
    }

    private void ReleaseClickInput()
    {
        if (Input.GetMouseButtonUp(0))
        {
            SelectUnits();
            _startPosition = Vector2.zero;
            _endPosition = Vector2.zero;
            DrawVisual();
        }
    }
}
