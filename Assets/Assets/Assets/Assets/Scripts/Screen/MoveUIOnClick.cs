using UnityEngine;
using UnityEngine.UI;

public class MoveUIOnClick : MonoBehaviour
{
    public Button btn;
    public RectTransform targetPosition;
    public RectTransform movingObject;
    public float movementSpeed = 0.1f;

    private bool isMoving = false;

    
     void Start()
    {
        Button btn = gameObject.GetComponent<Button>();

        btn.onClick.AddListener(OnButtonClick);
    }

    void Update()
    {
        if (isMoving)
        {
            // Calculate the step size based on the movement speed and frame rate
            float step = movementSpeed * Time.deltaTime;

            // Move the UI element towards the target position
            movingObject.position = Vector3.MoveTowards(movingObject.position, targetPosition.position, step);

            // Check if the UI element has reached the target position
            if (Vector3.Distance(movingObject.position, targetPosition.position) < 0.01f)
            {
                // Stop moving when the target position is reached
                isMoving = false;
            }
        }
    }

    public void OnButtonClick()
    {
        // Set isMoving to true when the button is clicked
        isMoving = true;
    }
}
