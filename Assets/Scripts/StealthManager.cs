
using UnityEngine;

public class StealthManager : MonoBehaviour
{
    public bool isStealth { get; private set; } = true;

    [SerializeField] private Material stealthMaterial;
    [SerializeField] private Material normalMaterial;
    [SerializeField] private PlayerManager playerManager;

    private void Start()
    {
        ResetStealthVisuals();
    }

    public void ResetStealthVisuals()
    {
        isStealth = true;

        UpdateLaserMaterials(stealthMaterial);
        UpdateLightColors(Color.green);
    }

    public void LeaveStealth()
    {
        if (!isStealth)
            return;

        // Do not change visuals or state if the lifecycle rejects detection.
        if (playerManager == null || !playerManager.CanReceiveDetection())
            return;

        isStealth = false;

        UpdateLaserMaterials(normalMaterial);
        UpdateLightColors(Color.red);
    }

    // Compatibility method for existing detection callers.
    public void SetFalse()
    {
        LeaveStealth();
    }

    private void UpdateLaserMaterials(Material targetMaterial)
    {
        if (targetMaterial == null)
            return;

        GameObject[] lasers = GameObject.FindGameObjectsWithTag("StealthLaser");

        foreach (GameObject laser in lasers)
        {
            Renderer[] renderers =
                laser.GetComponentsInChildren<Renderer>(true);

            foreach (Renderer laserRenderer in renderers)
                laserRenderer.sharedMaterial = targetMaterial;
        }
    }

    private void UpdateLightColors(Color targetColor)
    {
        Light[] lights =
            FindObjectsByType<Light>(FindObjectsSortMode.None);

        foreach (Light lightSource in lights)
        {
            if (!lightSource.CompareTag("Player"))
                lightSource.color = targetColor;
        }
    }
}
