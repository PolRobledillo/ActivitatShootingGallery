using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class RandomGunshotMark : MonoBehaviour
{
    public float lifetime = 5f;
    public List<Material> gunshotMarks = new List<Material>();
    private DecalProjector decalProjector;

    private void Awake()
    {
        decalProjector = GetComponent<DecalProjector>();
        if (decalProjector != null)
        {
            RandomizeGunshotMark();
        }
        StartCoroutine(MarkLifetime());
    }
    private void RandomizeGunshotMark()
    {
        if (gunshotMarks != null && gunshotMarks.Count > 0)
        {
            int randomIndex = Random.Range(0, gunshotMarks.Count);
            decalProjector.material = gunshotMarks[randomIndex];
        }
    }
    public IEnumerator MarkLifetime()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}
