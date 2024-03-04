using UnityEngine;

public class HealthPotPickup : MonoBehaviour
{
    public int HealthValue = 8;

    private bool isClaimed = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D hitInfo) {
        //Debug.Log("name: " + this.name);
        //Debug.Log("id: " + this.GetInstanceID());
        //Debug.Log("collision detected");
        if (hitInfo.gameObject.tag == "Player" && !isClaimed) {
            isClaimed = true;
            //Debug.Log("is player");
            Huntress playerController = hitInfo.gameObject.GetComponent<Huntress>();
            playerController.PickupHealthPot(HealthValue);
            Destroy(gameObject);
        }
    }
}
