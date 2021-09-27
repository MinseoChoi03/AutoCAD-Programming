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
