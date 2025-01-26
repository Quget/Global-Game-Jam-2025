using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    public TextMeshProUGUI pickupTutorial, shootTutorial, interactTutorial;

    public void ShowPickupTutorial(bool show = true) => pickupTutorial.gameObject.SetActive(show);
    public void ShowShootTutorial(bool show = true) => shootTutorial.gameObject.SetActive(show);
    public void ShowInteractTutorial(bool show = true) => interactTutorial.gameObject.SetActive(show);
}
