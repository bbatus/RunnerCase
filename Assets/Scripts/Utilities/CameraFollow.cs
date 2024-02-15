using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
 public static CameraFollow instance = null;
    public Transform Target;
    public Transform camTransform;
    public Vector3 Offset;
    public float SmoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;
    [SerializeField] public bool isFinishPos = false;
    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

    }
    private void Start()
    {
        Offset = camTransform.position - Target.position;
    }

    private void FixedUpdate() {
         // if (isFinishPos)
        //  return;
        // update position
        Vector3 targetPosition = Target.position + Offset;
        camTransform.position = Vector3.SmoothDamp(transform.position, new Vector3(transform.position.x, transform.position.y, targetPosition.z), ref velocity, SmoothTime);

        // update rotation
        // transform.LookAt(Target);
    }
}
