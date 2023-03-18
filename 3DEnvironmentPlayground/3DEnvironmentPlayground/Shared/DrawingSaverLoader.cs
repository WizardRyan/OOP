using Blazor3D.Maths;
using Blazor3D.Objects;
using Microsoft.JSInterop;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace _3DEnvironmentPlayground.Shared
{
    public class MeshData
    {
        public string MeshType { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 Scale { get; set; }
        public MeshData(string meshType, Vector3 position, Vector3 scale) {
            MeshType= meshType;
            Position= position;
            Scale = scale;
        }
    }

    public class DrawingSaverLoader
    {


        public static string GetJSONForMesh(Mesh mesh)
        {
            var data = new MeshData(mesh.Geometry.Type, mesh.Position, mesh.Scale);
            return JsonSerializer.Serialize(data);
        }

        public static Mesh GetMeshFromJSON(string json)
        {
            var meshData = JsonSerializer.Deserialize<MeshData>(json);
            return MeshFactory.GetMesh(meshData.MeshType, meshData.Position, meshData.Scale);
        }
    }
}
