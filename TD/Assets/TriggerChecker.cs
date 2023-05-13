using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerChecker : MonoBehaviour
{    private void OnTriggerStay2D(Collider2D col)
    {
        UnitBuy.canPlace = false;
        Debug.Log("CAN'T PLACE NOW");
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        UnitBuy.canPlace = true;
        Debug.Log("CAN PLACE NOW");
    }
}
