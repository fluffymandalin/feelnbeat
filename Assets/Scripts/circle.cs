using UnityEngine;
using TMPro; // Import TextMeshPro namespace

public class circle : MonoBehaviour
{
    public GameObject circleShapePrefab; // Assign your circle shape prefab in the Inspector
    public float[] appearanceTimesInSeconds; // Specify the appearance times in seconds
    public float durationInSeconds = 0.2f; // Duration for which the prefab should stay visible
    public Vector3 circleScale = Vector3.one;
    public UIManager uiManager; // Reference to the UIManager script in the Inspector

    private float currentTime = 0f; // Current time in seconds
    private int appearanceIndex = 0; // Index of the current appearance
    private GameObject currentCircleShape; // Reference to the instantiated circle shape
    private bool isTappedInside = false; // Flag to track if the user tapped inside the circle shape
    private int consecutiveTaps = 0; // Counter for consecutive correct taps
    private int consecutiveErrors = 0; // Counter for consecutive errors (debug logs)

    private void Start()
    {
        // Initialize counters to 0 when the game starts
        consecutiveTaps = 0;
        consecutiveErrors = 0;
    }

    private void Update()
    {
        currentTime += Time.deltaTime; // Update the current time

        // Check if the current time matches any appearance time
        if (appearanceIndex < appearanceTimesInSeconds.Length)
        {
            if (currentTime >= appearanceTimesInSeconds[appearanceIndex])
            {
                // Time to make the circle shape prefab visible
                currentCircleShape = Instantiate(circleShapePrefab, transform.position, Quaternion.identity);

                // Adjust the scale of the circle shape object
                currentCircleShape.transform.localScale = circleScale;

                appearanceIndex++; // Move to the next appearance time
            }
        }

        // Check if the prefab should disappear
        if (currentCircleShape != null && currentTime >= appearanceTimesInSeconds[appearanceIndex - 1] + durationInSeconds)
        {
            // Time to hide the circle shape prefab
            Destroy(currentCircleShape);
            isTappedInside = false; // Reset the flag when the circle shape disappears
        }

        // Check for user input (touch or click)
        if (Input.GetMouseButtonDown(0)) // Left mouse button or touch input
        {
            // Cast a ray from the camera to the point of user input
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == currentCircleShape)
                {
                    // User tapped inside the circle collider
                    // Handle the correct touch action here
                    isTappedInside = true;
                    consecutiveTaps++;
                    consecutiveErrors = 0; // Reset the consecutive errors counter
                }
            }
        }

        // Check if the user tapped outside the circle when it should have been inside
        if (currentCircleShape != null && !isTappedInside && currentTime <= appearanceTimesInSeconds[appearanceIndex - 1] + durationInSeconds)
        {

            if (consecutiveErrors >= 8)
            {
                // Display "try again" message and handle game over
                uiManager.UpdateDebugText2("Incorrect tap. Stick with the rhythm! Try again");
                uiManager.debugText2.gameObject.SetActive(true); // Show the text
                consecutiveErrors = 0;
                // Handle game over logic here
            }
        }

        // Check if the user achieved 8 consecutive correct taps
        if (consecutiveTaps >= 8)
        {
            // Display "perfect" message and handle success
            uiManager.UpdateDebugText1("Perfect");
            uiManager.debugText1.gameObject.SetActive(true); // Show the text
            consecutiveTaps = 0;
            // Handle success logic here
        }
    }
}
