﻿using Blazor3D.Core;
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
        public MeshData(string meshType, Vector3 position, Vector3 scale, Guid guid)
        {
            MeshType = meshType;
            Position = position;
            Scale = scale;
            Guid = guid;
        }
        public Guid Guid { get; set; }
    }

    public class DrawingSave
    {
        public DrawingSave(string bgColor) {
            MeshDatas = new List<MeshData>();
            BgColor= bgColor;
        }

        public List<MeshData> MeshDatas { get;set; }
        public string BgColor { get; set; }
    }

    public class DrawingSaverLoader
    {


        public static string GetJSONForMesh(Mesh mesh)
        {
            Console.WriteLine($"Raw Mesh Data: {JsonSerializer.Serialize(mesh)}");
            var data = new MeshData(mesh.Geometry.Type, mesh.Position, mesh.Scale, mesh.Uuid);
            return JsonSerializer.Serialize(data);
        }

        public static Mesh GetMeshFromJSON(string json)
        {
            var meshData = JsonSerializer.Deserialize<MeshData>(json);
            return MeshFactory.GetMesh(meshData.MeshType, meshData.Position, meshData.Scale, meshData.Guid);
        }

        public static string GetJSONForDrawing(Drawing drawing)
        {
            var drawingSave = new DrawingSave(drawing.Scene.BackGroundColor);
            foreach(var obj in drawing.Scene.Children)
            {
                if (obj.Type == "Mesh") {
                    Mesh mesh = (Mesh)obj;
                    drawingSave.MeshDatas.Add(new MeshData(mesh.Geometry.Type, mesh.Position, mesh.Scale, mesh.Uuid));
                }
            }
            return JsonSerializer.Serialize(drawingSave);
        }

        public static void LoadDrawingFromJSON(Drawing drawing, string json)
        {
            drawing.SetDefaultState();
            Object3D? theOneCube = null;
            foreach(var obj in drawing.Scene.Children)
            {
                if(obj.Type == "Mesh")
                {
                    theOneCube = obj;
                }
            }
            if(theOneCube != null)
            {
                drawing.Scene.Children.Remove(theOneCube);
            }

            var drawingSave = JsonSerializer.Deserialize<DrawingSave>(json);
            if(drawingSave != null)
            {
                drawing.Scene.BackGroundColor = drawingSave.BgColor;
                foreach(var meshData in drawingSave.MeshDatas)
                {
                    drawing.AddMesh(MeshFactory.GetMesh(meshData.MeshType, meshData.Position, meshData.Scale, meshData.Guid));
                }
            }

        }
    }
}
