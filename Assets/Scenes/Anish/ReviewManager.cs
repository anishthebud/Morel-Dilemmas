using UnityEngine;
using System.Collections.Generic;

public class ReviewManager : MonoBehaviour
{
    public static ReviewManager Instance { get; private set; }
    public CustomerReview[] reviewDict;
    public List<CustomerReview> currentReviews = new List<CustomerReview>();
    public int dayNumber;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }
        reviewDict = Resources.LoadAll<CustomerReview>(""); // Change the path when resources get migrated
    }

    public void HandleCustomerRemoved()
    {
        // Currently a randomly generated number, but will change once dishes are stored
        float starsGiven = Random.Range(1, 5 + 1);
        AddReview(starsGiven);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Start anew for each day (reset the current reviews back to empty after each day)
        dayNumber = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Get the call from the CustomerManager to add the review to the list after a customer has left
        
    }

    void NextDay()
    {
        dayNumber += 1;
        ReviewTicker.Instance.reviews.Clear();
    }

    void AddReview(float starsGiven)
    {
        List<CustomerReview> potentialReviews = new List<CustomerReview>();
        foreach (CustomerReview review in reviewDict)
        {
            if (Mathf.Approximately(review.starsGiven, starsGiven))
            {
                potentialReviews.Add(review);
            }
        }
        CustomerReview selectedReview = potentialReviews[Random.Range(0, potentialReviews.Count)];
        currentReviews.Add(selectedReview);
        ReviewTicker.Instance.HandleReviewAdded(selectedReview);
    }
}
