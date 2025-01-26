using DubbelBubbel.Player;
using UnityEngine;

public class Spikes : MonoBehaviour
{
	[SerializeField]
	private float forceMultiplier = 25;

    private bool isInBubble = false;

	private float hurtCooldown = 2;
	private float hurtTime = 0;
	private bool canHurt = false;

	private Player player;

	[SerializeField]
	private GameObject bubbleEnclosure;

	public void Awake() 
	{ 
		player = FindFirstObjectByType<Player>();
		UpdateBubbleEnclosure();
	}

	public void UpdateBubbleEnclosure()
	{
		bubbleEnclosure.SetActive(isInBubble);
	}

	private void Update()
	{
		if (canHurt && !isInBubble)
		{
			if (hurtTime < hurtCooldown)
			{
				hurtTime += Time.deltaTime;
			}
			else
			{
				canHurt = false;
				hurtTime = 0;
			}
		}
	}

	private void OnCollisionStay(Collision collision)
	{
		if (!canHurt && !isInBubble)
		{
			if(collision.collider.transform.gameObject == player.gameObject)
			{
				player.GetComponent<Rigidbody>().AddForce(Vector3.up * forceMultiplier);
				canHurt = true;
				hurtTime = 0;
				GameManager.Instance.gameData.LoseHealth();
				return;
			}
		}

		if (!isInBubble)
		{
			if (GameManager.Instance.gameData.PowerUp >= 3)
			{
				var bubble = collision.collider.GetComponentInParent<Bubble>();
				if (bubble != null)
				{
					isInBubble = true;
					UpdateBubbleEnclosure();
					bubble.Entrap(this.gameObject, () => {
						isInBubble = false;
						UpdateBubbleEnclosure();
					});
				}
			}
		}

	}
}
