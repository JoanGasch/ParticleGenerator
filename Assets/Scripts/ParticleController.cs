using UnityEngine;
using System.Collections.Generic;

public class ParticleController : MonoBehaviour
{
    public int numberOfParticles = 10;
    public float initialVelocity = 5f;
    public float initialAngle = 45f;
    public float lifeTime = 3f;
    public float gravity = -9.8f;

    public GameObject particle;

    private List<GameObject> particles = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CreateParticleExplosion();
        }

        for (int i = particles.Count - 1; i >= 0; i--)
        {
            GameObject particleObj = particles[i];
            Particle p = particleObj.GetComponent<Particle>();

            p.activeTime += Time.deltaTime;

            if (p.activeTime > p.maxLifeTime)
            {
                Destroy(particleObj);
                particles.RemoveAt(i);
            }
            else
            {
                UpdateParticlePosition(p, p.activeTime);
            }
        }
    }

    void CreateParticleExplosion()
    {
        foreach (GameObject p in particles)
        {
            Destroy(p);
        }
        particles.Clear();

        for (int i = 0; i < numberOfParticles; i++)
        {
            float randomVel = initialVelocity + Random.Range(-2f, 2f);
            float randomLife = lifeTime + Random.Range(-1, 1f);

            float finalAngle;
            if (Random.value > 0.5f)
            {
                finalAngle = Random.Range(0, 360f);
            }
            else
            {
                finalAngle = initialAngle;
            }

            GameObject newParticle = Instantiate(particle, Vector3.zero, Quaternion.identity);
            Particle p = newParticle.GetComponent<Particle>();

            p.initialVelocity = randomVel;
            p.initialAngle = finalAngle;
            p.maxLifeTime = randomLife;
            p.activeTime = 0;
            p.gravity = gravity;

            particles.Add(newParticle);
        }
    }

    void UpdateParticlePosition(Particle p, float time)
    {
        float angleRad = p.initialAngle * Mathf.Deg2Rad;

        float vx = p.initialVelocity * Mathf.Cos(angleRad);
        float vy = p.initialVelocity * Mathf.Sin(angleRad);

        float x = vx * time;
        float y = vy * time + 0.5f * p.gravity * time * time;

        p.transform.position = new Vector3(x, y, 0);
    }
}
