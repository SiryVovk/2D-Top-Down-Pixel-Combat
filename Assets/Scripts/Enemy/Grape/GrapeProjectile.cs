using System.Collections;
using UnityEngine;

public class GrapeProjectile : MonoBehaviour
{
    [SerializeField] private AnimationCurve animationCurve;
    [SerializeField] private GameObject projectileShadow;
    [SerializeField] private GameObject projectileSplash;

    [SerializeField] private float duration = 1f;
    [SerializeField] private float hightY = 3f;

    private void Start()
    {
        GameObject shadow = Instantiate(projectileShadow, transform.position, Quaternion.identity);

        Vector3 endPosition = PlayerControl.Instanse.transform.position;
        StartCoroutine(ProjectileCurveRoutin(transform.position, endPosition));
        StartCoroutine(GrapeShadowMoveRoutin(shadow, transform.position, endPosition));
    }

    private IEnumerator ProjectileCurveRoutin(Vector3 startPosition, Vector3 endPosition)
    {
        float timePased = 0f;

        while (timePased < duration)
        {
            timePased += Time.deltaTime;
            float linearT = timePased / duration;
            float hightT = animationCurve.Evaluate(linearT);
            float hight = Mathf.Lerp(0, hightY, hightT);

            transform.position = Vector2.Lerp(startPosition, endPosition, linearT) + new Vector2(0f, hight);

            yield return null;
        }

        Instantiate(projectileSplash, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private IEnumerator GrapeShadowMoveRoutin(GameObject shadow, Vector3 startPosition, Vector3 endPosition)
    {
        float timePased = 0f;

        while (timePased < duration)
        {
            timePased += Time.deltaTime;
            float linearT = timePased / duration;

            shadow.transform.position = Vector2.Lerp(startPosition, endPosition, linearT);

            yield return null;
        }

        Destroy(shadow);
    }
}
