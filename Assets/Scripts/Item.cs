using DubbelBubbel.Player;
using UnityEngine;
using UnityEngine.InputSystem;

public class Item : MonoBehaviour

{
    private Player player;
    private InputAction interactAction;
    private float distanceToBeInRange = 2;

    void Awake()
    {
        player = FindFirstObjectByType<Player>();
        GameManager.Instance.gameData.AddItemToWorld(this);
		interactAction = InputSystem.actions.FindAction("Interact");
		interactAction.performed += InteractAction_performed;
    }

	private void InteractAction_performed(InputAction.CallbackContext obj)
	{
        var distance = Vector3.Distance(player.transform.position, transform.position);
		if (distance < distanceToBeInRange)
		{
            Debug.Log("Old Inventory: " + GameManager.Instance.gameData.Inventory.Count);
            Debug.Log("Old Items in world: " + GameManager.Instance.gameData.ItemsInWorld.Count);
            interactAction.performed -= InteractAction_performed;
            GameManager.Instance.gameData.PickUpItem(this);
            Destroy(this.gameObject);
            Debug.Log("Item picked up");
            Debug.Log("New Inventory: " + GameManager.Instance.gameData.Inventory.Count);
            Debug.Log("New Items in world: " + GameManager.Instance.gameData.ItemsInWorld.Count);			
		}
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
            interactAction.performed -= InteractAction_performed;
            GameManager.Instance.gameData.PickUpItem(this);
            Destroy(this.gameObject);
            Debug.Log("New Inventory: " + GameManager.Instance.gameData.Inventory.Count);
            Debug.Log("New Items in world: " + GameManager.Instance.gameData.ItemsInWorld.Count);
        }
	}
}
