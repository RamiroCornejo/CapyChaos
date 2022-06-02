using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class ShopMessage : MonoBehaviour
{
    [SerializeField] Button yesButton = null;
    [SerializeField] Button noButton = null;
    [SerializeField] Button okButton = null;
    [SerializeField] TextMeshProUGUI messageTxt = null;

    public void DispatchQuestion(UnityAction yesAction, UnityAction noAction, string question)
    {
        okButton.gameObject.SetActive(false);
        noButton.gameObject.SetActive(true);
        yesButton.gameObject.SetActive(true);

        yesButton.onClick.RemoveAllListeners();
        noButton.onClick.RemoveAllListeners();

        yesButton.onClick.AddListener(yesAction);
        noButton.onClick.AddListener(noAction);
        messageTxt.text = question;
    }

    public void DispatchMessage(UnityAction okAction, string message)
    {
        okButton.gameObject.SetActive(true);
        noButton.gameObject.SetActive(false);
        yesButton.gameObject.SetActive(false);

        okButton.onClick.RemoveAllListeners();
        okButton.onClick.AddListener(okAction);

        messageTxt.text = message;
    }

    public void Close()
    {

    }

}
