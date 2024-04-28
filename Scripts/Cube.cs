using System;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]

public class Cube : MonoBehaviour
{
    private float _currentChance = 1f;
    private float _explosionForce = 500f;
    private Rigidbody _rigidbody;

    public event Action CubeDestroyed;

    public float ExplosionForce
    {
        get => _explosionForce;

        set
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            _explosionForce = value;
        }
    }

    public float CurrentChance
    {
        get => _currentChance;

        set
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            _currentChance = value;
        }
    }

    private void Awake()
    {
        GetComponent<Renderer>().material.color = GetRandomColor();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnMouseUpAsButton()
    {
        if (CanDivided())
            InstantiateCube();

        CubeDestroyed?.Invoke();
        Destroy(gameObject);
    }

    public void AddForce(float explosionRadius)
    {
        float explosinRadius = transform.localScale.x;
        _rigidbody.AddExplosionForce(explosionRadius, transform.position, explosinRadius);
        print(("value") + explosionRadius);
    }

    private void InstantiateCube()
    {
        int divider = 2;
        int maxCubeCount = 7;
        int minCubeCount = 2;
        int randomCountCube = UnityEngine.Random.Range(minCubeCount, maxCubeCount);

        Cube currentCube;

        for (int i = 0; i < randomCountCube; i++)
        {
            currentCube = Instantiate(this, transform.position, transform.rotation);
            currentCube.AddForce(ExplosionForce);
            currentCube.transform.localScale = transform.localScale / divider;
            currentCube.CurrentChance = _currentChance / divider;
            currentCube.ExplosionForce = _explosionForce / divider;
        }
    }

    private Color GetRandomColor()
    {
        float maxValue = 1.0f;
        float randomR = UnityEngine.Random.Range(0.0f, maxValue);
        float randomG = UnityEngine.Random.Range(0.0f, maxValue);
        float randomB = UnityEngine.Random.Range(0.0f, maxValue);

        return new Color(randomR, randomG, randomB);
    }

    private bool CanDivided()
    {
        float maxChance = 1f;
        float randomChance = UnityEngine.Random.Range(0.0f, maxChance);

        return randomChance < CurrentChance;
    }
}