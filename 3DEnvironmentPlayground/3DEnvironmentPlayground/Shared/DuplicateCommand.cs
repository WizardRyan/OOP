using Blazor3D.Objects;

namespace _3DEnvironmentPlayground.Shared
{
    public class DuplicateCommand : Command
    {
        private Drawing _drawing;
        private Mesh _mesh;

        public DuplicateCommand(Drawing drawing, Mesh mesh)
        {
            _drawing = drawing;
            _mesh = MeshFactory.GetMesh(mesh.Geometry.Type, mesh.Position, mesh.Scale, Guid.NewGuid());
        }

        public override void Execute()
        {
            _drawing.AddMesh(_mesh);
        }

        public override void UnExecute()
        {
            _drawing.DeleteMesh(_mesh);
        }
    }
}
