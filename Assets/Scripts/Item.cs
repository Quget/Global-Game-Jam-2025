using DubbelBubbel.Player;
using UnityEngine;

public class Item : MonoBehaviour

{
    private Player player;

    void Awake()
    {
        player = FindFirstObjectByType<Player>();
        GameManager.Instance.gameData.AddItemToWorld(this);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.transform.parent.gameObject == player.gameObject)
		{
            Debug.Log("Old Inventory: " + GameManager.Instance.gameData.Inventory.Count);
            Debug.Log("Old Items in world: " + GameManager.Instance.gameData.ItemsInWorld.Count);
            GameManager.Instance.gameData.PickUpItem(this);
            Destroy(this.gameObject);
            Debug.Log("New Inventory: " + GameManager.Instance.gameData.Inventory.Count);
            Debug.Log("New Items in world: " + GameManager.Instance.gameData.ItemsInWorld.Count);
        }
	}
}
