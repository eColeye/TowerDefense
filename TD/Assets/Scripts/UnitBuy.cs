using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBuy : MonoBehaviour
{
    public Transform unitFolder;
    public GameObject[] unit;
    /*
     * 0 : Turret
     * 1 : Gunner
     * 2 : Booma
     * 3 : Octo
     * 4 : Rocket
     * 5 : Sniper
     * 6 : Plane
     */
    public GameObject buyBar;
    private GameObject buyingObject = null;
    private Transform buyingTrans = null;
    private string objectKey;

    public Camera mainCamera;
    public static bool canPlace = true;
    
    public void Buy(string key)
    {
        canPlace = true;
        switch (key)
        {
            //check if have currency before buying
            case "Turret":
                if (GameManager.unitCost["Turret"] <= GameManager.GameMoney)
                {
                    objectKey = key;
                    Change();
                    buyBar.SetActive(false);
                    buyingObject = Instantiate(unit[0]);
                    buyingObject.transform.SetParent(unitFolder, false);
                    buyingTrans = buyingObject.GetComponent<Transform>();
                }
                return;
            case "Gunner":
                if (GameManager.unitCost["Gunner"] <= GameManager.GameMoney)
                {
                    objectKey = key;
                    Change();
                    buyBar.SetActive(false);
                    buyingObject = Instantiate(unit[1]);
                    buyingObject.transform.SetParent(unitFolder, false);
                    buyingTrans = buyingObject.GetComponent<Transform>();
                }
                return;
            case "Booma":
                if (GameManager.unitCost["Booma"] <= GameManager.GameMoney)
                {
                    objectKey = key;
                    Change();
                    buyBar.SetActive(false);
                    buyingObject = Instantiate(unit[2]);
                    buyingObject.transform.SetParent(unitFolder, false);
                    buyingTrans = buyingObject.GetComponent<Transform>();
                }
                return;
            case "Octo":
                if (GameManager.unitCost["Octo"] <= GameManager.GameMoney)
                {
                    objectKey = key;
                    Change();
                    buyBar.SetActive(false);
                    buyingObject = Instantiate(unit[3]);
                    buyingObject.transform.SetParent(unitFolder, false);
                    buyingTrans = buyingObject.GetComponent<Transform>();
                }
                return;
            case "Rocket":
                if (GameManager.unitCost["Rocket"] <= GameManager.GameMoney)
                {
                    objectKey = key;
                    Change();
                    buyBar.SetActive(false);
                    buyingObject = Instantiate(unit[4]);
                    buyingObject.transform.SetParent(unitFolder, false);
                    buyingTrans = buyingObject.GetComponent<Transform>();
                }
                return;
            case "Sniper":
                if (GameManager.unitCost["Sniper"] <= GameManager.GameMoney)
                {
                    objectKey = key;
                    Change();
                    buyBar.SetActive(false);
                    buyingObject = Instantiate(unit[5]);
                    buyingObject.transform.SetParent(unitFolder, false);
                    buyingTrans = buyingObject.GetComponent<Transform>();
                }
                return;
            case "Plane":
                if (GameManager.unitCost["Plane"] <= GameManager.GameMoney)
                {
                    objectKey = key;
                    Change();
                    buyBar.SetActive(false);
                    buyingObject = Instantiate(unit[6]);
                    buyingObject.transform.SetParent(unitFolder, false);
                    buyingTrans = buyingObject.GetComponent<Transform>();
                }
                return;

            default: return;
        }
    }

    private void Clicked()
    {
        GameManager.GameMoney -= GameManager.unitCost[objectKey];
        TextUpdater textUpdater = FindObjectOfType<TextUpdater>();
        textUpdater.ReloadText();

        switch (objectKey) 
        {
            case "Turret":
                buyingObject.GetComponent<Turrets>().active = true;
                buyingObject = null;
                buyingTrans = null;
                return;
            case "Gunner":
                buyingObject.GetComponent<Gunner>().active = true;
                buyingObject = null;
                buyingTrans = null;
                return;
            case "Booma":
                buyingObject.GetComponent<RangSlinger>().active = true;
                buyingObject = null;
                buyingTrans = null;
                return;
            case "Octo":
                buyingObject.GetComponent<OctoSides>().active = true;
                buyingObject = null;
                buyingTrans = null;
                return;
            case "Rocket":
                buyingObject.GetComponent<RocketFire>().active = true;
                buyingObject = null;
                buyingTrans = null;
                return;
            case "Sniper":
                buyingObject.GetComponent<Sniper>().active = true;
                buyingObject = null;
                buyingTrans = null;
                return;
            case "Plane":
                buyingObject.GetComponent<airplane>().active = true;
                buyingObject = null;
                buyingTrans = null;
                return;
        }
    }
    private void Change()
    {
        if (buyingObject != null)
        {
            Destroy(buyingObject);
            buyingTrans = null;
        }
    }

    public void Garbage()
    {
        Debug.Log("GARBAGE");
        Destroy(buyingObject);
        buyingObject = null;
        buyingTrans = null;
    }

    public void Update()
    {
        if(buyingObject != null)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = mainCamera.nearClipPlane; // set the z-coordinate to the near clipping plane of the camera
            buyingTrans.position = mainCamera.ScreenToWorldPoint(mousePosition);

            //left mouse button was pressed
            if (Input.GetMouseButtonDown(0))
            {
                if (canPlace)
                {
                    Clicked();
                }                
            }
        }
    }
}
