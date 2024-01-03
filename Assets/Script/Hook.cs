using UnityEngine;
using UnityEngine.Events;

public class Hook : MonoBehaviour
{
   [SerializeField] private DistanceJoint2D joint;
    public Transform anchorPoint;
    public Player playerScript;
    private LineRenderer lineRenderer;
    public bool isSwinging;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        isSwinging = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !playerScript.isGrounded )
        {
            isSwinging = !isSwinging;
            playerScript.isSwinging = isSwinging;
            if (isSwinging)
            {
                StartSwing();
        
            }
            else
            {
                StopSwing();
            }
        }
        
        joint.enabled = isSwinging;
        if (isSwinging)
        {
            lineRenderer.SetPosition(0, anchorPoint.position); 
            lineRenderer.SetPosition(1, playerScript.transform.position); 
        }
    }

    void StartSwing()
    {
        lineRenderer.enabled = true; 
    }

    void StopSwing()
    {
        lineRenderer.enabled = false;
        playerScript.isGrounded = true;
    }
}

