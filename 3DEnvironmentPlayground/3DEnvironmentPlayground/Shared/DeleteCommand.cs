using Blazor3D.Objects;

namespace _3DEnvironmentPlayground.Shared
{
    public class DeleteCommand : Command
    {
        private Drawing _drawing;
        private Mesh _mesh;

        public DeleteCommand(Drawing drawing, Mesh mesh)
        {
            _drawing = drawing;
            _mesh = mesh;
        }

        public override void Execute()
        {
            _drawing.DeleteMesh(_mesh);
        }

        public override void UnExecute()
        {
            _drawing.AddMesh(_mesh);
        }
    }
}
