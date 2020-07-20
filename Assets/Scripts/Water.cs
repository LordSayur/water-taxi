using UnityEngine;

[ExecuteInEditMode]
public class Water : MonoBehaviour
{
    [SerializeField]
    private float _waveSpeed = 0.5f;
    private MeshRenderer _meshRenderer;

    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        _meshRenderer.sharedMaterial.SetTextureOffset(1, new Vector2(_waveSpeed * Time.time, _waveSpeed * Time.time));
    }
}
