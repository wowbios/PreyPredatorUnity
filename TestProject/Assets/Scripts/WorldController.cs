using Assets.Scripts;
using UnityEngine;
using Random = System.Random;

public class WorldController : MonoBehaviour
{
    public AnimalsSettings[] Animals;
    public int XMin, XMax, YMin, YMax;

    public static WorldController Instance;

    public readonly Random Rand = new Random();

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        SpawnAnimals();
    }

    private void SpawnAnimals()
    {
        foreach (AnimalsSettings animalsSettingse in Animals) SpawnAnimals(animalsSettingse);
    }

    private void SpawnAnimals(AnimalsSettings settings)
    {
        for (var i = 0; i < settings.Count; i++) SpawnAnimal(settings.Prefab);
    }

    private void SpawnAnimal(GameObject prefab) => Instantiate(prefab, GetRandomPosition(), Quaternion.identity);

    public Vector2 GetRandomPosition()
    {
        float x = (float)Rand.Next(XMin * 100, XMax * 100) / 100;
        float y = (float)Rand.Next(YMin * 100, YMax * 100) / 100;
        return new Vector2(x, y);
    }
}
