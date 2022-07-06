using System.Collections;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Transform targetToFollow;
    private Vector3 followDistance;

    [SerializeField] float smooth = 0.15f;

    private void Start()
    {
        targetToFollow = GameObject.FindGameObjectWithTag("Player").transform;
        followDistance = transform.position - targetToFollow.position;
    }
    private void LateUpdate()
    {
        Vector3 target_Pos = targetToFollow.position + followDistance;
        transform.position = Vector3.Lerp(transform.position, target_Pos, smooth);
        transform.LookAt(targetToFollow);
       
    }

}
