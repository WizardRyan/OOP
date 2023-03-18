using Blazor3D.Geometires;
using Blazor3D.Maths;
using Blazor3D.Objects;

namespace _3DEnvironmentPlayground.Shared
{
    public enum MeshShape
    {
        Box,
        Capsule,
        Cone,
        Cylinder,
        Tetrahedron,
        Sphere
    }
    public static class MeshFactory
    {
        public static Mesh GetMesh(MeshShape shape)
        {
            return shape switch
            {
                MeshShape.Box => new Mesh(),
                MeshShape.Capsule => new Mesh { Geometry = new CapsuleGeometry() },
                MeshShape.Cylinder => new Mesh { Geometry = new CylinderGeometry() },
                MeshShape.Tetrahedron => new Mesh { Geometry = new TetrahedronGeometry() },
                MeshShape.Sphere => new Mesh { Geometry = new SphereGeometry() },
                MeshShape.Cone => new Mesh { Geometry = new ConeGeometry() },
                _ => new Mesh(),
            };
        }

        public static Mesh GetMesh(string shape, Vector3 position, Vector3 scale)
        {
            return shape switch
            {
                "BoxGeometry" => new Mesh { Geometry = new BoxGeometry(), Position = position, Scale = scale },
                "CapsuleGeometry" => new Mesh { Geometry = new CapsuleGeometry(), Position = position, Scale = scale },
                "CylinderGeometry" => new Mesh { Geometry = new CylinderGeometry(), Position = position, Scale = scale },
                "TetrahedronGeometry" => new Mesh { Geometry = new TetrahedronGeometry(), Position = position, Scale = scale },
                "SphereGeometry" => new Mesh { Geometry = new SphereGeometry(), Position = position, Scale = scale },
                "ConeGeometry" => new Mesh { Geometry = new ConeGeometry(), Position = position, Scale = scale },
                _ => new Mesh(),
            };
        }
    }
}
