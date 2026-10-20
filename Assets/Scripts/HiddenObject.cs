
using UnityEngine;

public class HiddenObject : MonoBehaviour
{
    public Light playerFlashlight;
    public LayerMask occlusionMask;

    public float maxDistance = 3f;
    public float lightConeAngle = 10f;

    private Renderer[] objectRenderers;
    private bool isPermanentlyRevealed;

    private void Start()
    {
        objectRenderers = GetComponentsInChildren<Renderer>(true);
        SetVisibility(false);
    }

    private void Update()
    {
        if (isPermanentlyRevealed)
            return;

        if (playerFlashlight == null || !playerFlashlight.enabled)
            return;

        CheckFlashlightExposure();
    }

    private void CheckFlashlightExposure()
    {
        Vector3 origin = playerFlashlight.transform.position;
        float effectiveRange = Mathf.Min(maxDistance, playerFlashlight.range);
        float effectiveAngle = lightConeAngle;

        if (playerFlashlight.type == LightType.Spot)
        {
            effectiveAngle = Mathf.Min(
                effectiveAngle,
                playerFlashlight.spotAngle * 0.5f
            );
        }

        foreach (Renderer objRenderer in objectRenderers)
        {
            if (objRenderer == null || !objRenderer.gameObject.activeInHierarchy)
                continue;

            Vector3 targetPoint = objRenderer.bounds.center;
            Vector3 toTarget = targetPoint - origin;
            float distance = toTarget.magnitude;

            if (distance <= 0.001f || distance > effectiveRange)
                continue;

            if (Vector3.Angle(playerFlashlight.transform.forward,
                              toTarget.normalized) > effectiveAngle)
                continue;

            if (Physics.Raycast(
                origin,
                toTarget.normalized,
                out RaycastHit hit,
                distance + 0.1f,
                occlusionMask,
                QueryTriggerInteraction.Ignore))
            {
                bool hitTarget =
                    hit.transform == transform ||
                    hit.transform.IsChildOf(transform);

                if (!hitTarget)
                    continue;
            }
            else
            {
                continue;
            }

            RevealPermanently();
            return;
        }
    }

    private void RevealPermanently()
    {
        isPermanentlyRevealed = true;
        SetVisibility(true);

        Debug.Log($"{gameObject.name} has been permanently revealed.");
    }

    private void SetVisibility(bool visible)
    {
        if (objectRenderers == null)
            return;

        foreach (Renderer objRenderer in objectRenderers)
        {
            if (objRenderer != null)
                objRenderer.enabled = visible;
        }
    }
}
