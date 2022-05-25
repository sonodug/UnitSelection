using System.Collections.Generic;
using UnityEngine;

public class UnitSelections : MonoBehaviour
{
    public List<GameObject> units = new List<GameObject>();
    public List<GameObject> selectedUnits = new List<GameObject>();

    private static UnitSelections _instance;
    public static UnitSelections Instance => _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void ClickSelect(GameObject unitToAdd)
    {
        DeselectAll();
        selectedUnits.Add(unitToAdd);
        unitToAdd.transform.GetChild(0).gameObject.SetActive(true);

        EnableMovement(unitToAdd);
    }

    public void ShiftClickSelect(GameObject unitToAdd)
    {
        if (!selectedUnits.Contains(unitToAdd))
        {
            selectedUnits.Add(unitToAdd);
            unitToAdd.transform.GetChild(0).gameObject.SetActive(true);

            EnableMovement(unitToAdd);
        }
        else
        {
            DisableMovement(unitToAdd);

            unitToAdd.transform.GetChild(0).gameObject.SetActive(false);
            selectedUnits.Remove(unitToAdd);
        }
    }

    public void DragSelect(GameObject unitToAdd)
    {
        if (!selectedUnits.Contains(unitToAdd))
        {
            selectedUnits.Add(unitToAdd);
            unitToAdd.transform.GetChild(0).gameObject.SetActive(true);

            EnableMovement(unitToAdd);
        }
    }

    public void DeselectAll()
    {
        foreach (GameObject unitToDeselect in selectedUnits)
        {
            DisableMovement(unitToDeselect);
            unitToDeselect.transform.GetChild(0).gameObject.SetActive(false);
        }

        selectedUnits.Clear();
    }

    private void DisableMovement(GameObject unit)
    {
        if (unit.TryGetComponent<UnitMovement>(out UnitMovement movement))
            movement.enabled = false;
    }

    private void EnableMovement(GameObject unit)
    {
        if (unit.TryGetComponent<UnitMovement>(out UnitMovement movement))
            movement.enabled = true;
    }
}
