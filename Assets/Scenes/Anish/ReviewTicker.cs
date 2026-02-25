using UnityEngine;
using System.Collections.Generic;

public class ReviewTicker : MonoBehaviour
{
    public static ReviewTicker Instance { get; private set; }

    public ReviewItem reviewPrefab;
    public float reviewDuration = 4.0f;
    public List<CustomerReview> reviews = null;

    float width;
    float pixelsPerSecond;
    int rightIndex = 0;

    ReviewItem currReview;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }
        TryAssignReviewsFromManager();
    }

    void Start()
    {
        TryAssignReviewsFromManager();

        width = GetComponent<RectTransform>().rect.width;
        pixelsPerSecond = width / reviewDuration;
        if (reviews != null && reviews.Count > 0)
        {
            AddToTicker(reviews[0]);
        }
    }

    // Update is called once per frame
    void Update()
    {

        // "Move" the text from right to left (looping back if end of string is reached)
        if (currReview != null && (currReview.GetWidth != 0) && (currReview.GetXPosition <= -currReview.GetWidth) && reviews != null && reviews.Count > 0)
        {
            rightIndex = (rightIndex + 1) % reviews.Count;
            AddToTicker(reviews[rightIndex]);
        }
    }

    void TryAssignReviewsFromManager()
    {
        if (ReviewManager.Instance != null && ReviewManager.Instance.currentReviews != null)
        {
            reviews = ReviewManager.Instance.currentReviews;
        }
        else if (reviews == null)
        {
            reviews = new List<CustomerReview>();
        }
    }

    void AddToTicker(CustomerReview review)
    {
        currReview = Instantiate(reviewPrefab, transform);
        currReview.Initialize(width, pixelsPerSecond, review);
    }

    public void HandleReviewAdded(CustomerReview review)
    {
        if (reviews == null)
        {
            Debug.Log("In function 1 called");
            reviews = new List<CustomerReview>();
        }

        if (!ReferenceEquals(reviews, ReviewManager.Instance.currentReviews))
        {
            Debug.Log("In function 2 called");
            reviews.Add(review);
        }

        if (currReview == null && reviews.Count > 0)
        {
            Debug.Log("In function 3 called");
            rightIndex = reviews.Count - 1;
            AddToTicker(reviews[rightIndex]);
        }
    }

    // Methods that were originally in review manager
}
