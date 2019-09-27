using UnityEngine;

[CreateAssetMenu()]
public class RacerEquipement : ScriptableObject
{
    public Equipement[] equipements;

    public float GetSpeedFactorFor(Tile.Type type)
    {
        if (equipements != null)
        {
            foreach (Equipement equipement in equipements)
            {
                if (type == equipement.type)
                    return equipement.speedFactor;
            }
        }
        return 1f;
    }

    [System.Serializable]
    public class Equipement
    {
        public Tile.Type type;
        public float speedFactor;
    }
}
