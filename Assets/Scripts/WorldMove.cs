using UnityEngine;

public class WorldMove : MonoBehaviour
{

    [SerializeField] Transform targetToFollow;

    void Update()
    {
        MoveWorld();
    }

    private void MoveWorld()
    {
        Vector3 newPos = new Vector3(targetToFollow.position.x, transform.position.y, transform.position.z);
        transform.position = newPos;
    }
}
