using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;

namespace AutoCAD_8
{
    public class Class1
    {
        [CommandMethod("DrawArc")]
        public void DrawArc()
        {
            //Get the drawing document and the database object
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            //Create the transaction object inside the using block
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                try
                {
                    //Get the BlockTable object
                    BlockTable bt;
                    bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;

                    BlockTableRecord btr;
                    btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    //Create the Arc
                    Point3d centerPt = new Point3d(10, 10, 0);
                    double arcRad = 20.0;
                    double startAngle = 1.0;
                    double endAngle = 3.0;

                    Arc arc = new Arc(centerPt, arcRad, startAngle, endAngle);
                    arc.SetDatabaseDefaults();
                    btr.AppendEntity(arc);
                    trans.AddNewlyCreatedDBObject(arc, true);

                    trans.Commit();
                }
                catch (System.Exception ex)
                {
                    doc.Editor.WriteMessage("Error encountered : " + ex.Message);
                    trans.Abort();
                }
            }
        }

        [CommandMethod("DrawCircle")]
        public void DrawCircle()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                try
                {
                    doc.Editor.WriteMessage("Drawing a Circle!");
                    BlockTable bt;
                    bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;

                    BlockTableRecord btr;
                    btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    //Specify the Circle's parameters (i.e centerpoint, radius, etc. etc)
                    Point3d centerPt = new Point3d(100, 100, 0);
                    double circleRad = 100.0;
                    using (Circle circle = new Circle())
                    {
                        circle.Radius = circleRad;
                        circle.Center = centerPt;

                        btr.AppendEntity(circle);
                        trans.AddNewlyCreatedDBObject(circle, true);
                    }

                    trans.Commit();
                }
                catch (System.Exception ex)
                {
                    doc.Editor.WriteMessage("Error encountered : " + ex.Message);
                    trans.Abort();
                }
            }
        }

        [CommandMethod("DrawMText")]
        public void DrawMText()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                try
                {
                    edt.WriteMessage("Drawing MText Exercise!");
                    BlockTable bt;
                    bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;

                    BlockTableRecord btr;
                    btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    //Specify the MTexts parameters (e.g. Textrstring, insertionPoin)
                    string txt = "Hello AutoCAD from C#!";
                    Point3d insPt = new Point3d(200, 200, 0);
                    using (MText mtx = new MText())
                    {
                        mtx.Contents = txt;
                        mtx.Location = insPt;

                        btr.AppendEntity(mtx);
                        trans.AddNewlyCreatedDBObject(mtx, true);
                    }
                    trans.Commit();
                }
                catch (System.Exception ex)
                {
                    edt.WriteMessage("Error encountered : " + ex.Message);
                    trans.Abort();
                }
            }
        }
        [CommandMethod("DraqLine")]
        public void DraqLine()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                try
                {
                    BlockTable bt;
                    bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;

                    BlockTableRecord btr;
                    btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    //Send a message to the user
                    edt.WriteMessage("\n Drawing a Line object : ");
                    Point3d pt1 = new Point3d(0, 0, 0);
                    Point3d pt2 = new Point3d(100, 100, 0);
                    Line In = new Line(pt1, pt2);

                    In.ColorIndex = 1; //Color is red

                    btr.AppendEntity(In);

                    trans.AddNewlyCreatedDBObject(In, true);
                    trans.Commit();
                }
                catch (System.Exception ex)
                {
                    edt.WriteMessage("Error encountered : " + ex.Message); 
                    trans.Abort();
                }
            }
        }
    }
}
