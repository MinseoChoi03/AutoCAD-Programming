using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;

namespace AutoCAD_14
{
    class A4TitleBlock : TBlock
    {
        public int Height { get; set; }
        public int Width { get; set; }

        public A4TitleBlock()
        {
            Height = 210;
            Width = 297;
        }
    }
}
