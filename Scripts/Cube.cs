using System;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefabs;
    [SerializeField] private float _explosionForce;
    [SerializeField] private GameObject _effect;

    private float _currentChance = 1f;

    public Renderer Renderer { get; private set; }

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
        Renderer = GetComponent<Renderer>();
    }

    private void OnMouseUpAsButton()
    {
        if (IsSeparation())
            InstantiateCube();

        Instantiate(_effect, transform.position, transform.rotation);
        Explode();

        Destroy(gameObject);
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
            currentCube.transform.localScale = transform.localScale / 2;
            currentCube.CurrentChance = _currentChance / divider;
            currentCube.Renderer.material.color = GetRandomColor();
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

    private bool IsSeparation()
    {
        float maxChance = 1f;
        float randomChance = UnityEngine.Random.Range(0.0f, maxChance);

        print("Current Chance " + CurrentChance);

        if (randomChance > CurrentChance)
            return false;
        else
            return true;
    }

    private void Explode()
    {
        float divisionRadius = 2f;
        float explosinRadius = transform.localScale.x/ divisionRadius;

        foreach (Rigidbody expodableObject in GetExpodableObject(explosinRadius))
        {
            expodableObject.AddExplosionForce(_explosionForce, transform.position, explosinRadius);
        }
    }

    private List<Rigidbody> GetExpodableObject(float explosinRadius)
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, explosinRadius);

        List<Rigidbody> barrels = new();

        foreach (Collider hit in hits)
            if (hit.attachedRigidbody != null)
                barrels.Add(hit.attachedRigidbody);

        return barrels;
    }
}