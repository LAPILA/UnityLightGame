using UnityEngine;

public class MoveObjectOnTrigger : MonoBehaviour
{
    public Transform spawnDestination;
    public float slideForce = 2f;

    private bool hasMoved = false;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!hasMoved && other.CompareTag("Player")) {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

            player.transform.position = spawnDestination.position;
            mainCamera.transform.position = spawnDestination.position;

            Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
            playerRb.AddForce(Vector2.down * slideForce, ForceMode2D.Impulse);

            hasMoved = true;
        }
    }
}
