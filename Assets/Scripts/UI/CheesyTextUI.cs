using UnityEngine;

public class CheesyTextUI : MonoBehaviour
{

    [SerializeField]
    private float timeToShow = 5;

    [SerializeField]
    private TMPro.TextMeshProUGUI textField; 

    private float showTimer = 0;

    public void ShowText(string text)
    {
        this.gameObject.SetActive(true);
		textField.text = text;
		showTimer = timeToShow;

	}
    // Update is called once per frame
    void Update()
    {
        showTimer -= Time.deltaTime;
        if(showTimer <= 0)
        {
            showTimer = 0;
            this.gameObject.SetActive(false);
        }
    }
}
