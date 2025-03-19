using Newtonsoft.Json;
using UnityEngine;

[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
public class SaveData : MonoBehaviour
{
    [SerializeField][JsonProperty("Name")] private string _name;
    [SerializeField][JsonProperty("Cost")] private int _cost;

    [JsonProperty("Position")] private Vector3 _position;
    private GameObject _buildingPrefab;

    public string Name => _name;
    public int Cost => _cost;
    public Vector3 Position => _position;

    private void Awake()
    {
        _buildingPrefab = GetComponent<GameObject>();
        _position = transform.position;
    }
}