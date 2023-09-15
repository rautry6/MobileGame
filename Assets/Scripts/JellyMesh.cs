using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyMesh : MonoBehaviour
{
    public float intensity = 1f;
    public float mass = 1f;
    public float stiffness = 1f;
    public float damping = 0.75f;

    private Mesh _original_mesh, _mesh_clone;
    private MeshRenderer _renderer;
    private JellyVertex[] _jv;
    private Vector3[] _vertex_array;

    void Start()
    {
        _original_mesh = GetComponent<MeshFilter>().sharedMesh;
        _mesh_clone = Instantiate(_original_mesh);
        GetComponent<MeshFilter>().sharedMesh = _mesh_clone;
        _renderer = GetComponent<MeshRenderer>();

        _jv = new JellyVertex[_mesh_clone.vertices.Length];
        for (int i = 0; i < _mesh_clone.vertices.Length; i++)
        {
            _jv[i] = new JellyVertex(i, transform.TransformPoint(_mesh_clone.vertices[i]));
        }
    }

    void FixedUpdate()
    {
        _vertex_array = _original_mesh.vertices;
        for (int i = 0; i < _jv.Length; i++)
        {
            Vector3 target = transform.TransformPoint(_vertex_array[_jv[i].id]);
            float intense = (1 - (_renderer.bounds.max.y - target.y) / _renderer.bounds.size.y) * intensity;
            _jv[i].Shake(target, mass, stiffness, damping);
            target = transform.InverseTransformPoint(_jv[i].position);
            _vertex_array[_jv[i].id] = Vector3.Lerp(_vertex_array[_jv[i].id], target, intensity);
        }
        _mesh_clone.vertices = _vertex_array;
    }

    public class JellyVertex
    {
        public int id;
        public Vector3 position;
        public Vector3 velocity, force;

        public JellyVertex(int d, Vector3 pos)
        {
            id = d;
            position = pos;
        }

        public void Shake(Vector3 target, float m, float s, float d)
        {
            force = (target - position) * s;
            velocity = (velocity + force / m) * d;
            position += velocity;
            if ((velocity + force + force / m).magnitude < 0.001f)
            {
                position = target;
            }
        }
    }

    
}
