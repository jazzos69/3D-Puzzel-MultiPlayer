using UnityEngine;

public class ActivateJumpPad : MonoBehaviour
{
    public Transform jumpPadSpawnPoint;
    public GameObject jumpPadPrefab;

   private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Instantiate(jumpPadPrefab, jumpPadSpawnPoint.position, jumpPadSpawnPoint.rotation);
            
        }
    }
    
    private void OnCollisionExit(Collision collision)
    {//dit heeft testen nodig
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(jumpPadPrefab);
        }
    }
}
