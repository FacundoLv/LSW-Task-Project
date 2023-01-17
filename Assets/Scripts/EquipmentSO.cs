using UnityEngine;

[CreateAssetMenu(fileName = "Equipment SO", menuName = "LSW Task/Equipment SO", order = 0)]
public class EquipmentSO : ScriptableObject
{
    public Slot slot;
    public Sprite[] sprites;
}