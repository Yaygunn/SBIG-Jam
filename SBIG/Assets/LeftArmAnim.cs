using UnityEngine;
using System.Collections;

public class LeftArmAnim : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _magickParticles;

    public void TempLeftArmReloadAnimationEvent()
    {
        _magickParticles.Play();
        var material = _magickParticles.GetComponent<Renderer>().material;

        StartCoroutine(FadeParticlesMaterial(material, 0.5f, 0.5f, 2.1f));
    }

    private IEnumerator FadeParticlesMaterial(Material material, float fadeInDuration, float fadeOutDuration, float totalDuration)
    {
        float elapsedTime = 0;
        Color color = material.color;

        while (elapsedTime < fadeInDuration)
        {
            color.a = Mathf.Lerp(0, 1, elapsedTime / fadeInDuration);
            material.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        color.a = 1;
        material.color = color;

        yield return new WaitForSeconds(totalDuration - fadeInDuration - fadeOutDuration);

        elapsedTime = 0;
        while (elapsedTime < fadeOutDuration)
        {
            color.a = Mathf.Lerp(1, 0, elapsedTime / fadeOutDuration);
            material.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        color.a = 0;
        material.color = color;
    }
}