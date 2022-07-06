using System.Collections;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] GameObject[] prefabPlatformTypes;
    [SerializeField] Transform startPlatformTransform;
    [SerializeField] int maxCountVisiblePlatforms = 40;
    [SerializeField] float maxDistanceFromLastPlatform = 5.5f;
    [SerializeField] float minDistanceFromLastPlatform = 1f;

    private PlayerController playerController;
    private Vector3 lastCreatedPlatformPosition;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();

        InitializeFirstPlatformPosition();
        StartCoroutine(SpawnPlatform());
    }

    private void InitializeFirstPlatformPosition()
    {
        lastCreatedPlatformPosition = transform.position;
        lastCreatedPlatformPosition.x += transform.localScale.x / 2;
    }

    IEnumerator SpawnPlatform()
    {
        while (!playerController.isGameOver)
        {
            if (startPlatformTransform.childCount < maxCountVisiblePlatforms)
            {
                Vector3 distanceFromLastPlatform = new Vector3(Random.Range(minDistanceFromLastPlatform, maxDistanceFromLastPlatform), 0f, 0f);
                var newPlatform = Instantiate(prefabPlatformTypes[Random.Range(0, prefabPlatformTypes.Length)], startPlatformTransform);

                SetNewPlatformPosition(distanceFromLastPlatform, newPlatform);
            }
            yield return null;
        }
    }

    private void SetNewPlatformPosition(Vector3 distanceFromLastPlatform, GameObject newPlatform)
    {
        lastCreatedPlatformPosition.x += newPlatform.transform.localScale.x / 2;
        lastCreatedPlatformPosition += distanceFromLastPlatform;
        newPlatform.transform.position = lastCreatedPlatformPosition;

        lastCreatedPlatformPosition.x += newPlatform.transform.localScale.x / 2;
    }
}
