using DubbelBubbel.Player;
using UnityEngine;
using UnityEngine.InputSystem;

public class Item : MonoBehaviour

{
    private Player player;
    private InputAction interactAction;
    private float distanceToBeInRange = 2;

	[SerializeField]
	private AudioClip itemPickUpClip;

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
			PickedUp();
		}
	}

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.transform.parent.gameObject == player.gameObject)
		{
			PickedUp();

		}
	}

	private void PickedUp()
	{
		AudioSource.PlayClipAtPoint(itemPickUpClip, player.transform.position);
		GameManager.Instance.gameData.PickUpItem(this);
		Destroy(this.gameObject);
	}

	private void OnDestroy()
	{
		interactAction.performed -= InteractAction_performed;
	}
}
