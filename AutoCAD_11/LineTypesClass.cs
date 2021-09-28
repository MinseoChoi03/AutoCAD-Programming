using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;

namespace AutoCAD_11
{
    public class LineTypesClass
    {
        [CommandMethod("ListLineTypes")]
        public void ListLineTypes()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                LinetypeTable ltTab = trans.GetObject(db.LinetypeTableId, OpenMode.ForRead) as LinetypeTable;
                foreach (ObjectId ltID in ltTab)
                {
                    LinetypeTableRecord lttr = trans.GetObject(ltID, OpenMode.ForRead) as LinetypeTableRecord;
                    doc.Editor.WriteMessage("\n Line Type name : " + lttr.Name);
                }
                trans.Commit();
            }
        }

        [CommandMethod("LoadLineTypes")]
        public void LoadLineTypes()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                try
                {
                    LinetypeTable ltTab = trans.GetObject(db.LinetypeTableId, OpenMode.ForRead) as LinetypeTable;
                    string ltName = "CENTER";
                    if (ltTab.Has(ltName))
                    {
                        doc.Editor.WriteMessage("LineType already exist");
                        trans.Abort();
                    }
                    else
                    {
                        //Load the CENTER Linetype
                        db.LoadLineTypeFile(ltName, "acad.lin");

                        doc.Editor.WriteMessage("LineType [" + ltName + "] was created successfully");

                        //Commit the transaction
                        trans.Commit();
                    }
                }
                catch (System.Exception ex)
                {
                    doc.Editor.WriteMessage("Error  encountered : " + ex.Message);
                    trans.Abort();
                }
                trans.Commit();
            }
        }

        [CommandMethod("SetCurrentLineType")]
        public void SetCurrentLineType()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            string ltypeName = "DASHED2";
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                LinetypeTable ltTab = trans.GetObject(db.LinetypeTableId, OpenMode.ForRead) as LinetypeTable;
                if (ltTab.Has(ltypeName))
                {
                    db.Celtype = ltTab[ltypeName];
                    doc.Editor.WriteMessage("LineType " + ltypeName + "is now the current LineType");

                    trans.Commit();
                }
            }
        }

        [CommandMethod("DeleteLineType")]
        public void DeleteLineType()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                try
                {
                    LinetypeTable ltTab = trans.GetObject(db.LinetypeTableId, OpenMode.ForRead) as LinetypeTable;
                    db.Celtype = ltTab["ByLayer"];
                    foreach (ObjectId ltID in ltTab)
                    {
                        LinetypeTableRecord lttr = trans.GetObject(ltID, OpenMode.ForRead) as LinetypeTableRecord;
                        if (lttr.Name == "DASHED2")
                        {
                            lttr.UpgradeOpen();

                            //Delete the linetype
                            lttr.Erase(true);

                            //Commit the transaction
                            trans.Commit();
                            doc.Editor.WriteMessage("\nLineType deleted successfully");
                            break;
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    doc.Editor.WriteMessage("Error encountered : " + ex.Message);
                    trans.Abort();
                }
            }
        }

        [CommandMethod("SetLineTypeToObject")]
        public void SetLineTypeToObject()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                BlockTable bt;
                bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;

                BlockTableRecord btr;
                btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                Point3d pt1 = new Point3d(0, 0, 0);
                Point3d pt2 = new Point3d(100, 100, 0);
                Line ln = new Line(pt1, pt2);

                //Set the LineType
                ln.Linetype = "HIDDEN";

                btr.AppendEntity(ln);
                trans.AddNewlyCreatedDBObject(ln, true);

                trans.Commit();
            }
        }
    }
}
