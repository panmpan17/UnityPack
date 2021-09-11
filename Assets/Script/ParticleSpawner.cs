using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MPack;

public class ParticleSpawner : MonoBehaviour
{
    PrefabPool<CustomParticle> pool;

    [SerializeField]
    CustomParticle prefab;

    [SerializeField]
    Timer spawnTimer;

    private void Awake() {
        pool = new PrefabPool<CustomParticle>(prefab, true, "ParticleCollection");
    }

    private void Update() {
        if (spawnTimer.UpdateEnd) {
            spawnTimer.Reset();

            CustomParticle particle = pool.Get();
            particle.Shoot(this,
                           new Vector3(Random.Range(-5, 5), Random.Range(-3, 3)),
                           new Vector3(Random.Range(-5, 5), Random.Range(-3, 3)),
                           Random.Range(1, 1.5f));
        }
    }

    public void ParticleShootEnd(CustomParticle particle) {
        pool.Put(particle);
    }
}
