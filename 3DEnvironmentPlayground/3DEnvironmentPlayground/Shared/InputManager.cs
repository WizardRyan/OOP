using Blazor3D.Objects;

namespace _3DEnvironmentPlayground.Shared
{
    public static class InputManager
    {
        public static float MovementAmount = 1;
        public static float ScaleAmount = 1;

        private static List<string> _scaleKeys = new List<string> { "q", "w" ,"e", "a", "s", "d" };
        private static List<string> _translateKeys = new List<string> { "u", "i", "o", "j", "k", "l" };
        private static List<string> _verticalKeys = new List<string> { "w", "s", "i", "k" };
        private static List<string> _horizontalKeys = new List<string> { "a", "d", "j", "l" };
        private static List<string> _forwardKeys = new List<string> { "q", "e", "u", "o" };
        private static List<string> _positiveKeys = new List<string> { "w", "e", "d", "i", "o", "l" };
        private static List<string> _negativeKeyes = new List<string> { "q", "a", "s", "u", "j", "k" };

        public static Command? ProcessInput(string key, Mesh? mesh, Drawing drawing)
        {
            if(mesh != null)
            {
                TransformType? type = null;
                Axis? axis = null;
                float? amount = null;

                if(_scaleKeys.Contains(key))
                {
                    type = TransformType.Scale;
                    amount = ScaleAmount;
                }
                else if(_translateKeys.Contains(key))
                {
                    type = TransformType.Translate;
                    amount = MovementAmount;
                }

                if (_negativeKeyes.Contains(key))
                {
                    amount *= -1;
                }

                if (_verticalKeys.Contains(key))
                {
                    axis = Axis.Vertical;
                }
                else if (_horizontalKeys.Contains(key))
                {
                    axis = Axis.Horizontal;
                }
                else if (_forwardKeys.Contains(key))
                {
                    axis = Axis.Forward;
                }

                if(type != null && axis != null && amount != null)
                {
                    return new TransformCommand(drawing, mesh, (TransformType)type, (Axis)axis, (float)amount);
                }

                if(key == "Delete")
                {
                    return new DeleteCommand(drawing, mesh);
                }

                if(key == " ")
                {
                    return new DuplicateCommand(drawing, mesh);
                }
            }

            if(key == "z")
            {
                drawing.Undo();
            }

            return null;
        }
    }
}
