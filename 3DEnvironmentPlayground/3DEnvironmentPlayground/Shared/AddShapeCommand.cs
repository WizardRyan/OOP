using _3DEnvironmentPlayground.Shared;
using Blazor3D.Objects;

namespace _3DEnvironmentPlayground.Shared
{
    public class AddShapeCommand : Command
    {
        private Drawing _drawing;
        private Mesh _mesh;
        public AddShapeCommand(Drawing drawing, MeshShape shape)
        {
            _drawing = drawing;
            _mesh = MeshFactory.GetMesh(shape);
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
