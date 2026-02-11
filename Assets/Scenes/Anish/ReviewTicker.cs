using UnityEngine;

public class ReviewTicker : MonoBehaviour
{
    public ReviewItem reviewPrefab;
    public float reviewDuration = 4.0f;
    public string[] reviews;

    float width;
    float pixelsPerSecond;
    int rightIndex = 0;

    ReviewItem currReview;

    void Awake()
    {
        // Load in all of the current reviews
        // Format each review to contain displayable information
        // Combine each review into a massive string that can be looped over
        // Potentially might need to create some other asset or use another object for the ScrollBar itselfs
    }

    void Start()
    {
        // Give the Review Crawl motion to start moving
        width = GetComponent<RectTransform>().rect.width;
        pixelsPerSecond = width / reviewDuration;
        AddToTicker(reviews[0]);
    }

    // Update is called once per frame
    void Update()
    {
        // "Move" the text from right to left (looping back if end of string is reached)
        if ((currReview.GetWidth != 0) && (currReview.GetXPosition <= -currReview.GetWidth))
        {
            Debug.Log(currReview.GetXPosition);
            Debug.Log(-currReview.GetWidth);
            rightIndex = (rightIndex + 1) % reviews.Length;
            AddToTicker(reviews[rightIndex]);
        }
    }

    void AddToTicker(string review)
    {
        currReview = Instantiate(reviewPrefab, transform);
        currReview.Initialize(width, pixelsPerSecond, review);
    }
}
