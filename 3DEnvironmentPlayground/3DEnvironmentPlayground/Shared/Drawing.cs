using Blazor3D.Scenes;
using Blazor3D.Settings;
using Blazor3D.Viewers;
using Blazor3D.Lights;
using Blazor3D.Geometires;
using static System.Formats.Asn1.AsnWriter;
using Blazor3D.Maths;
using Blazor3D.Events;
using Blazor3D.Cameras;

namespace _3DEnvironmentPlayground.Shared
{
    public class Drawing
    {

        //These are exposed so that the front-end component can be set to them
        public Viewer? View3D;
        public ViewerSettings ViewerSettings;
        public Scene Scene;
        
        private Guid lastSelectedGuid;

        public Drawing()
        {
            ViewerSettings = new ViewerSettings()
            {
                ContainerId = "MainContainer",
                CanSelect = true,
            };
            Scene = new Scene();
            SetDefaultState();
        }

        public void OnObjectSelected(Object3DArgs e)
        {
            lastSelectedGuid = e.UUID;
            Console.WriteLine($"last Selected Object: {e.UUID}");
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
    }
}
