using Blazor3D.Scenes;
using Blazor3D.Settings;
using Blazor3D.Viewers;
using Blazor3D.Lights;
using Blazor3D.Geometires;
using static System.Formats.Asn1.AsnWriter;
using Blazor3D.Maths;
using Blazor3D.Events;
using Blazor3D.Cameras;
using Blazor3D.Objects;

namespace _3DEnvironmentPlayground.Shared
{
    public class Drawing
    {

        //These are exposed so that the front-end component can be set to them
        public Viewer? View3D;
        public ViewerSettings ViewerSettings;
        public Scene Scene;
        
        private Guid lastSelectedGuid;
        private List<Command> _commands = new List<Command>();

        private static Drawing? singletonDrawing;

        private Drawing()
        {
            ViewerSettings = new ViewerSettings()
            {
                ContainerId = "MainContainer",
                CanSelect = true,
            };
            Scene = new Scene();
            SetDefaultState();
        }

        //Implemenation of singleton design pattern. The above constructors "private" accessor ensures consumers of the class must use GetInstance, and that only one instance may exist.
        public static Drawing GetInstance()
        {
            if(singletonDrawing == null)
            {
                singletonDrawing = new Drawing();
            }
            return singletonDrawing;
        }

        public void OnObjectSelected(Object3DArgs e)
        {
            lastSelectedGuid = e.UUID;
        }

        public void SetDefaultState()
        {
            Scene.BackGroundColor = "#483D8B";
            Scene.Children.Clear();
            Scene.Add(new AmbientLight());
            Scene.Add(new PointLight()
            {
                Intensity = 0.7f,
                Position = new Vector3(100, 200, 100)
            });
            Scene.Add(MeshFactory.GetMesh(MeshShape.Box));
            if(View3D!= null)
            {
                View3D.UpdateCamera(new PerspectiveCamera
                {
                    Position = new Vector3(3, 3, 3),
                    LookAt = new Vector3(0, 0, 0)
                });
                View3D.UpdateScene();
            }
        }

        public Mesh? GetCurrentlySelectedMesh()
        {
            return getMeshByGuid(lastSelectedGuid);
        }

        private Mesh? getMeshByGuid(Guid guid)
        {
            return (Mesh?)Scene.Children.Find(x => x.Uuid== guid);
        }

        // commands pattern + commands inherently utilize strategy pattern through polymorphism
        public void ExecuteCommand(Command command)
        {
            command.Execute();
            _commands.Add(command);
        }

        //undo design pattern
        public void Undo()
        {
            if(_commands.Count > 0)
            {
                _commands[_commands.Count - 1].UnExecute();
                _commands.RemoveAt(_commands.Count - 1);
            }
        }


        //this method is called externally by the TransformCommand class
        public void TransformMesh(Mesh mesh, TransformType transformType, Axis axis, float amount)
        {
            Vector3 transformation;
            if(axis == Axis.Vertical)
            {
                transformation = new Vector3(0, amount, 0);
            }
            else if (axis == Axis.Horizontal)
            {
                transformation = new Vector3(amount, 0, 0);
            }
            else
            {
                transformation = new Vector3(0, 0, amount);
            }

            if(transformType == TransformType.Translate)
            {
                mesh.Position = new Vector3(mesh.Position.X + transformation.X, mesh.Position.Y + transformation.Y, mesh.Position.Z + transformation.Z);
            }
            else
            {
                mesh.Scale = new Vector3(mesh.Scale.X + transformation.X, mesh.Scale.Y + transformation.Y, mesh.Scale.Z + transformation.Z);
            }

            if (View3D != null)
            {
                View3D.UpdateScene();
            }
        }

        public void DeleteMesh(Mesh mesh)
        {
            var foundMesh = Scene.Children.Find(x => x.Uuid == mesh.Uuid);
            if(foundMesh != null)
            {
                Scene.Children.Remove(foundMesh);
                if(View3D != null)
                {
                    View3D.UpdateScene();
                }
            }
        }

        public void AddMesh(Mesh mesh)
        {
            Scene.Add(mesh);
            if(View3D != null)
            {
                View3D.UpdateScene();
            }
        }
    }
}
