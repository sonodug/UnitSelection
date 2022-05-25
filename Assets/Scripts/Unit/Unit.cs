using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private void Start()
    {
        UnitSelections.Instance.units.Add(this.gameObject);
    }

    private void OnDestroy()
    {
        UnitSelections.Instance.units.Remove(this.gameObject);
    }
}
