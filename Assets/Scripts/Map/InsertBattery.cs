using UnityEngine;

public class InsertBattery : MonoBehaviour
{
    public bool canInsertBattery = true;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaceBattery()
    {
        canInsertBattery = false;

        Debug.Log("Battery geplaatst"); 
    }
}
