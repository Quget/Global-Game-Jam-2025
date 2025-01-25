using DubbelBubbel.Player;
using UnityEngine;
using UnityEngine.InputSystem;

public class PowerUp : MonoBehaviour

{

    private Player player;
    private InputAction interactAction;
    private float distanceToBeInRange = 2;

	[SerializeField]
	private AudioClip powerupPickUpClip;

	void Awake()
    {
        player = FindFirstObjectByType<Player>();
        GameManager.Instance.gameData.PowerUpItems.Add(this);
        interactAction = InputSystem.actions.FindAction("Interact");
		interactAction.performed += InteractAction_performed;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InteractAction_performed(InputAction.CallbackContext obj)
	{
        var distance = Vector3.Distance(player.transform.position, transform.position);
		if (distance < distanceToBeInRange)
		{
            interactAction.performed -= InteractAction_performed;
            AudioSource.PlayClipAtPoint(powerupPickUpClip, transform.position);
            GameManager.Instance.gameData.PickUpPowerUp();
            Destroy(this.gameObject);
		}
	}

	private void OnDestroy()
	{
		interactAction.performed -= InteractAction_performed;
	}

	// public void OnCollisionEnter(Collision collision)
	// {
	//     if (collision.collider.transform.parent.gameObject == player.gameObject)
	// 	{
	//         Debug.Log("Old PowerUp: " + GameManager.Instance.gameData.PowerUp);
	//         interactAction.performed -= InteractAction_performed;
	//         GameManager.Instance.gameData.PowerUpItems.Remove(this);
	//         GameManager.Instance.gameData.PickUpPowerUp();
	//         Destroy(this.gameObject);
	//         Debug.Log("Powerup picked up");
	//         Debug.Log("New PowerUp: " + GameManager.Instance.gameData.PowerUp);
	//     }
	// }

}
