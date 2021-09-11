using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MPack;

public class CustomParticle : MonoBehaviour, IPoolableObj
{
    ParticleSpawner spawner;
    Vector3LerpTimer positionLerp;

    public void Instantiate()
    {
    }

    public void DeactivateObj(Transform poolCollection)
    {
        gameObject.SetActive(false);

        if (poolCollection != null) transform.SetParent(poolCollection);
    }

    public void Reinstantiate()
    {
        gameObject.SetActive(true);
        transform.SetParent(null);
    }

    public void Shoot(ParticleSpawner _spawner, Vector3 from, Vector3 to, float time) {
        spawner = _spawner;
        positionLerp = new Vector3LerpTimer(from, to, time);
        transform.position = from;
    }

    private void Update() {
        if (positionLerp.Timer.UpdateEnd) {
            spawner.ParticleShootEnd(this);
        }
        else {
            transform.position = positionLerp.Value;
        }
    }
}
