using _3DEnvironmentPlayground.Shared;
using System.Net.NetworkInformation;
using Blazor3D.Viewers;
using Blazor3D.Settings;
using Blazor3D.Scenes;
using Blazor3D.Lights;
using Blazor3D.Maths;
using Blazor3D.Materials;
using Blazor3D.Objects;
using Blazor3D.Geometires;
using Blazor3D.Enums;
using Blazor3D.Cameras;
using Blazor3D.Helpers;
using Blazor3D.Events;
using Blazor3D.Core;

namespace _3DEnvironmentTests
{
    public class UnitTest1
    {

        //ensure we can only get one instance of "Drawing" class
        [Fact]
        public void TestDrawingSingleton()
        {
            var drawing = Drawing.GetInstance();
            var drawing2 = Drawing.GetInstance();
            Assert.Equal(drawing, drawing2);
        }

        //ensure defualt scene is setup how we expect
        [Fact]
        public void TestDefaultState()
        {
            var drawing = Drawing.GetInstance();
            drawing.SetDefaultState();

            //there should be 3 objects, a diffuse light, a point light, and a cube
            Assert.Equal(3, drawing.Scene.Children.Count);

            Object3D cube = drawing.Scene.Children[2];

            //the cube should be 1 by 1 by 1, at 0, 0, 0
            Assert.Equal(0, cube.Position.X);
            Assert.Equal(0, cube.Position.Y);
            Assert.Equal(0, cube.Position.Z);

            Assert.Equal(1, cube.Scale.X);
            Assert.Equal(1, cube.Scale.Y);
            Assert.Equal(1, cube.Scale.Z);
        }

        [Fact]
        public void TestMeshFactory()
        {
            var mesh = MeshFactory.GetMesh(MeshShape.Sphere);

            //mesh type should be the same shape we passed in
            Assert.Equal("SphereGeometry", mesh.Geometry.Type);

            //mesh should be at 0, 0, 0
            Assert.Equal(0, mesh.Position.X);
            Assert.Equal(0, mesh.Position.Y);
            Assert.Equal(0, mesh.Position.Z);

            //test parameterized mesh factory get
            mesh = MeshFactory.GetMesh("CapsuleGeometry", new Vector3(0, 1, 2), new Vector3(3, 4, 5), Guid.NewGuid());
            Assert.Equal("CapsuleGeometry", mesh.Geometry.Type);
            Assert.Equal(0, mesh.Position.X);
            Assert.Equal(1, mesh.Position.Y);
            Assert.Equal(2, mesh.Position.Z);

            Assert.Equal(3, mesh.Scale.X);
            Assert.Equal(4, mesh.Scale.Y);
            Assert.Equal(5, mesh.Scale.Z);
        }

        [Fact]
        public void TestCommands()
        {
            var drawing = Drawing.GetInstance();
            drawing.SetDefaultState();

            //can we add shapes?
            Command addShapeCommand = new AddShapeCommand(drawing, MeshShape.Cylinder);
            drawing.ExecuteCommand(addShapeCommand);

            var mesh = (Mesh)drawing.Scene.Children[3];

            Assert.Equal("CylinderGeometry", mesh.Geometry.Type);

            //can we undo add shape command?
            drawing.Undo();

            Assert.Equal(3, drawing.Scene.Children.Count);

            //add it back so we can test delete command
            drawing.ExecuteCommand(addShapeCommand);

            mesh = (Mesh)drawing.Scene.Children[3];
            Command delCommand = new DeleteCommand(drawing, mesh);
            drawing.ExecuteCommand(delCommand);
            Assert.Equal(3, drawing.Scene.Children.Count);

            drawing.Undo();
            Assert.Equal(4, drawing.Scene.Children.Count);

            //can we transform a shape? 
            Command transform = new TransformCommand(drawing, mesh, TransformType.Scale, Axis.Vertical, 1);
            drawing.ExecuteCommand(transform);

            Assert.Equal(2, mesh.Scale.Y);
            drawing.Undo();
            Assert.Equal(1, mesh.Scale.Y);
            drawing.ExecuteCommand(transform);

            //can we duplicate a shape?
            Command dupe = new DuplicateCommand(drawing, mesh);
            drawing.ExecuteCommand(dupe);
            Assert.Equal(5, drawing.Scene.Children.Count);
            drawing.Undo();
            Assert.Equal(4, drawing.Scene.Children.Count);
        }

        [Fact]
        public void TestDrawingSavingLoading()
        {
            var drawing = Drawing.GetInstance();
            drawing.SetDefaultState();
            drawing.Scene.Children.Add(new Mesh());

            var saveJSON = DrawingSaverLoader.GetJSONForDrawing(drawing);

            drawing.SetDefaultState();

            DrawingSaverLoader.LoadDrawingFromJSON(drawing, saveJSON);

            Assert.Equal(4, drawing.Scene.Children.Count);
        }
    }
}