using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour {
    private static Message self;
    private string messageText;
    private Text textObject;
    private CanvasRenderer cv;
    private CanvasGroup cg;
    private Canvas parent;
    private int originalParentSortingOrder = 0;
    private int tempSortingorder = 10;

    void Start () {
        textObject = GetComponentInChildren<Text> ();
        // Debug.Log ("textObject is " + textObject);
        cv = GetComponent<CanvasRenderer> ();
        cg = GetComponentInChildren<CanvasGroup>();
        parent = transform.parent.GetComponent<Canvas> ();
        HideMessage ();
        self = gameObject.GetComponent<Message> ();
    }

    private void ShowMessage (float readTime, float fadeRate, float fadeDelay) {
        textObject.text = messageText;
        StartCoroutine (DisplayAndFadeMessage (readTime, fadeRate, fadeDelay));
    }

    private IEnumerator DisplayAndFadeMessage (float readTime, float fadeRate, float fadeDelay) {
        //https://answers.unity.com/questions/889908/i-created-an-ui-button-but-click-does-not-work.html
        parent.sortingOrder = tempSortingorder;

        //https://answers.unity.com/questions/881620/uigraphiccrossfadealpha-only-works-for-decrementin.html
        if (cv != null && cg != null) {
            cv.SetAlpha (1f);
            cg.alpha = 1f;
            yield return new WaitForSeconds (readTime);
            while (true) {
                float alpha = cv.GetAlpha ();
                if (alpha <= 0) {
                    break;
                }
                float newAlpha = alpha - fadeRate;
                cv.SetAlpha (newAlpha);
                cg.alpha = newAlpha;
                yield return new WaitForSeconds (fadeDelay);
            }
            HideMessage ();
        } else {
            Debug.Log ("Missing canvas renderer or canvas group");
        }
    }

    private void HideMessage () {
        cv.SetAlpha (0f);
        cg.alpha = 0;
        parent.sortingOrder = originalParentSortingOrder;
    }

    public static void SetAndDisplayMessage(float readTime, float fadeRate, float fadeDelay, string message) {
        self.messageText = message;
        self.ShowMessage(readTime, fadeRate, fadeDelay);
    }
}