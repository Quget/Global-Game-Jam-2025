using UnityEngine;

public class CheesyTextTrigger : MonoBehaviour
{
	[SerializeField]
	private string textToShow =  "";
	private void OnTriggerEnter(Collider other)
	{
		UIManager.instance.ShowText(textToShow);
		Destroy(gameObject);
	}
}
