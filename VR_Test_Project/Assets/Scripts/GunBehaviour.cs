using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
public class GunBehaviour : MonoBehaviour
{
    public bool auto = false;
    public bool shooting = false;
    public bool canShoot = true;

    public float shootCD = 0.1f;
    public float trailSpeed = 100f;

    public GameObject shootOrigin;
    public GameObject DecalProjector;
    public AudioSource audioSource;
    public TrailRenderer trailPrefab;
    public new ParticleSystem particleSystem;
    public void Shoot()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
        if (particleSystem != null)
        {
            particleSystem.Play();
        }
        Vector3 target = Vector3.zero;
        RaycastHit hit;
        if (Physics.Raycast(shootOrigin.transform.position, transform.forward * 100, out hit))
        {
            Debug.DrawLine(shootOrigin.transform.position, hit.point, Color.green, 1f);
            if (hit.transform.gameObject.tag == "Target")
            {
                GameManager.instance.AddScore(hit.transform.gameObject.GetComponent<TargetScoring>().scoreValue);
            }
            target = hit.point;
            GameObject decal = Instantiate(DecalProjector, hit.point + hit.normal * 0.01f, Quaternion.LookRotation(hit.normal * -1), hit.transform);

            try
            {
                hit.transform.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(-hit.normal * 5, hit.point, ForceMode.Impulse);
            }
            catch
            {

            }
        }
        else
        {
            Debug.DrawLine(shootOrigin.transform.position, shootOrigin.transform.position + transform.forward * 100f, Color.red, 1f);
            target = shootOrigin.transform.position + transform.forward * 100f;
        }
        StartCoroutine(CrearRastro(shootOrigin.transform.position, target));
        StartCoroutine(ShootCD());
    }
    private void Update()
    {
        if (auto && shooting && canShoot)
        {
            Shoot();
        }
    }
    public IEnumerator ShootCD()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootCD);
        canShoot = true;
    }
    public void StartShooting()
    {
        shooting = true;
    }
    public void StopShooting()
    {
        shooting = false;
    }
    private IEnumerator CrearRastro(Vector3 inicio, Vector3 fin)
    {
        TrailRenderer rastro = Instantiate(trailPrefab, inicio, Quaternion.identity);
        float distancia = Vector3.Distance(inicio, fin);
        float distanciaRestante = distancia;

        while (distanciaRestante > 0)
        {
            rastro.transform.position = Vector3.MoveTowards(rastro.transform.position, fin, trailSpeed * Time.deltaTime);
            distanciaRestante -= trailSpeed * Time.deltaTime;
            yield return null;
        }

        rastro.transform.position = fin;
        Destroy(rastro.gameObject, rastro.time);
    }
}
