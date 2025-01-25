using UnityEngine;

public class PlayerSpikeInteraction : MonoBehaviour
{
	private float hurtCooldown = 2;
	private float hurtTime = 0;
	private bool isHurt = false;

	private void Update()
	{
		if (isHurt)
		{
			if (hurtTime < hurtCooldown)
			{
				hurtTime += Time.deltaTime;
			}
			else
			{
				isHurt = false;
			}
		}
	}

	private void OnCollisionStay(Collision collision)
	{
		if (!isHurt)
		{
			var spikes = collision.collider.GetComponent<Spikes>();
			if (spikes != null)
			{
				isHurt = true;
				hurtTime = 0;
				GameManager.Instance.gameData.LoseHealth();
			}
		}
	}
}
