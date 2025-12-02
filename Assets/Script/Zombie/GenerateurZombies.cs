using Unity.VisualScripting;
using UnityEngine;

public class GenerateurZombies : MonoBehaviour
{
    [SerializeField, Tooltip("Zone d'apparition des zombies. X = horizontal, Y = vertical.")]
    Vector2 zoneApparition;
    public Vector2 ZoneApparition { get { return zoneApparition; } }

    [SerializeField, Tooltip("prefab du zombie")]
    Object prefab;
    public Object Prefab { get { return prefab; } }


    [SerializeField, Tooltip("Temps d'apparition min en secondes.")]
    float tempsApparitionMin = 0.5f;
    [SerializeField, Tooltip("Temps d'apparition max en secondes.")]
    float tempsApparitionMax = 2.0f;

    float tempsApparition;

    // Update is called once per frame
    void Update()
    {
        tempsApparition -= Time.deltaTime;
        if(tempsApparition <= 0.0f)
        {
            GenererZombie();
            tempsApparition = Mathf.Lerp(tempsApparitionMin, tempsApparitionMax, Random.value);
        }
    }

    void GenererZombie()
    {
        Vector3 positionAlea = transform.position + new Vector3(
            (Random.value * zoneApparition.x) - (zoneApparition.x / 2),
            0.0f,
            (Random.value * zoneApparition.y) - (zoneApparition.y / 2)
        );

        var objZombie = Instantiate(prefab, positionAlea, Quaternion.identity);
        var zombie = objZombie.GetComponent<Zombie>();
        zombie.Destination = new Vector3(zombie.Destination.x, 0.0f, 0.0f);
    }
}
