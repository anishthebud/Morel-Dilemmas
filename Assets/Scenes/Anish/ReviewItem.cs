using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Source Link: https://www.youtube.com/watch?v=mptVj9-I0gQ 

public class ReviewItem : MonoBehaviour
{
    float width;
    float pixelsPerSecond;
    RectTransform rt;

    public float GetXPosition { get { return rt.anchoredPosition.x; } }
    public float GetWidth { get { return rt.rect.width; }  }

    bool initialized = false;

    public void Initialize(float tickerWidth, float ppS, CustomerReview review)
    {
        this.width = tickerWidth;
        this.pixelsPerSecond = ppS;
        rt = GetComponent<RectTransform>();
        GetComponent<TextMeshProUGUI>().text = " " + review.starsGiven + " Stars: " + review.writtenReview + " ||";
        initialized = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!initialized || rt == null) return;
        rt.position += Vector3.left * pixelsPerSecond * Time.deltaTime;
        if (GetXPosition <= 0 - width - GetWidth)
        {
            Destroy(gameObject);
        }
    }
}