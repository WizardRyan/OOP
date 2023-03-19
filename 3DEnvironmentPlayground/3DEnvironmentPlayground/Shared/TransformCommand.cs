using Blazor3D.Objects;

namespace _3DEnvironmentPlayground.Shared
{

    public enum TransformType
    {
        Translate,
        Scale
    }

    public enum Axis
    {
        Vertical,
        Horizontal,
        Forward
    }
    public class TransformCommand : Command
    {

        private Drawing _drawing;
        private Mesh _mesh;
        private TransformType _transformType;
        private Axis _axis;
        private float _amount;

        public TransformCommand(Drawing drawing, Mesh mesh, TransformType transformType, Axis axis, float amount) {
            _drawing = drawing;
            _mesh = mesh;
            _transformType = transformType;
            _axis = axis;
            _amount = amount;
        }

        public override void Execute()
        {
            _drawing.TransformMesh(_mesh, _transformType, _axis, _amount);
        }

        public override void UnExecute()
        {
            _drawing.TransformMesh(_mesh, _transformType, _axis, _amount * -1);
        }
    }
}
