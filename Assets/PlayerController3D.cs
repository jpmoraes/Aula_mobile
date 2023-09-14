using UnityEngine;

public class PlayerController3D : MonoBehaviour
{
    private Vector3 targetPosition;
    private bool isMoving = false;

    private void Update()
    {
       
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // Converter a posição do toque para uma posição no mundo
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit))
                    {
                        targetPosition = hit.point;
                        isMoving = true;
                    }
                    break;

                case TouchPhase.Ended:
                    isMoving = false;
                    break;
            }
        }

        if (isMoving)
        {
            float moveSpeed = 5f;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }
}

