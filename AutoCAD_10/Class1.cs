using System;
using System.Collections.Generic;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;

namespace AutoCAD_10
{
    public class Class1
    {
        [CommandMethod("CopyX")]
        public static void CopyExercise()
        {
            // Get the current document and database
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            // Start a transaction
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                try
                {
                    // Open the BlockTable for read
                    BlockTable bt;
                    bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;

                    // Open the Block Table record Modelspace for write
                    BlockTableRecord btr;
                    btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    using (Circle c1 = new Circle())
                    {
                        c1.Center = new Point3d(0, 0, 0);
                        c1.Radius = 1;
                        c1.ColorIndex = 1;

                        // Add the new object to the BlockTable record
                        btr.AppendEntity(c1);
                        trans.AddNewlyCreatedDBObject(c1, true);

                        Circle c2 = new Circle();
                        c2.Center = new Point3d(10, 10, 0);
                        c2.Radius = 2;

                        // Add the new object to the BlockTable record
                        btr.AppendEntity(c2);
                        trans.AddNewlyCreatedDBObject(c2, true);

                        Circle c3 = new Circle();
                        c3.Center = new Point3d(30, 30, 0);
                        c3.Radius = 5;
                        c3.ColorIndex = 5;

                        // Add the new object to the BlockTable record
                        btr.AppendEntity(c3);
                        trans.AddNewlyCreatedDBObject(c3, true);

                        // Create a collection and the 3 Circle objects
                        DBObjectCollection col = new DBObjectCollection();
                        col.Add(c1);
                        col.Add(c2);
                        col.Add(c3);

                        foreach (Circle cir in col)
                        {
                            if (cir.Radius == 2)
                            {
                                Circle c4 = cir.Clone() as Circle;
                                c4.ColorIndex = 3; // Green
                                c4.Radius = 10;

                                // Add the new object to the BlockTable record
                                btr.AppendEntity(c4);
                                trans.AddNewlyCreatedDBObject(c4, true);
                            }
                        }
                    }
                    trans.Commit();
                }
                catch (System.Exception ex)
                {
                    doc.Editor.WriteMessage("Error encountered: " + ex.Message);
                    trans.Abort();
                }
            }
        }

        [CommandMethod("EraseX")]
        public static void EraseExercise()
        {
            // Get the current document and database
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            // Start a transaction
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                try
                {
                    // Open the BlockTable for read
                    BlockTable bt;
                    bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;

                    // Open the Block Table record Modelspace for write
                    BlockTableRecord btr;
                    btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    using (Line ln = new Line())
                    {
                        ln.StartPoint = new Point3d(0, 0, 0);
                        ln.EndPoint = new Point3d(10, 10, 0);

                        // Add the new object to the BlockTable record
                        btr.AppendEntity(ln);
                        trans.AddNewlyCreatedDBObject(ln, true);

                        // Create a circle 
                        using (Circle c1 = new Circle())
                        {
                            c1.Center = new Point3d(0, 0, 0);
                            c1.Radius = 5;

                            // Add the new object to the BlockTable record
                            btr.AppendEntity(c1);
                            trans.AddNewlyCreatedDBObject(c1, true);

                            // Create a Polyline
                            Polyline pl = new Polyline();
                            pl.AddVertexAt(0, new Point2d(0, 0), 0, 0, 0);
                            pl.AddVertexAt(1, new Point2d(-10, -10), 0, 0, 0);
                            pl.AddVertexAt(2, new Point2d(20, -20), 0, 0, 0);

                            // Add the new object to the BlockTable record
                            btr.AppendEntity(pl);
                            trans.AddNewlyCreatedDBObject(pl, true);

                            // Create the collection
                            DBObjectCollection col = new DBObjectCollection();
                            col.Add(ln);
                            col.Add(c1);
                            col.Add(pl);

                            foreach (Entity ent in col)
                            {
                                //if (ent.GetType() == typeof(Circle))
                                if (ent is Circle)
                                {
                                    ent.Erase(true);
                                }
                                else if (ent is Line)
                                {
                                    ent.ColorIndex = 2;
                                }
                                else
                                {
                                    ent.ColorIndex = 3;
                                }
                            }
                        }
                    }

                    // Commit the transaction
                    trans.Commit();
                    doc.SendStringToExecute("._zoom e ", false, false, false);
                }
                catch (System.Exception ex)
                {
                    doc.Editor.WriteMessage("Error encountered: " + ex.Message);
                    trans.Abort();
                }
            }
        }

        [CommandMethod("MoveX")]
        public static void MoveExercise()
        {
            // Get the current document and database
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            // Start a transaction
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                try
                {
                    // Open the BlockTable for read
                    BlockTable bt;
                    bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;

                    // Open the Block Table record Modelspace for write
                    BlockTableRecord btr;
                    btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    // Specify the MTexts parameters (e.g. Textrstring, insertionPoint)
                    Point3d insPt = new Point3d(0, 0, 0);
                    using (MText mtx1 = new MText())
                    {
                        mtx1.Location = insPt;
                        mtx1.Height = 5;
                        mtx1.Contents = "Move Me";
                        mtx1.ColorIndex = 3; //green

                        btr.AppendEntity(mtx1);
                        trans.AddNewlyCreatedDBObject(mtx1, true);

                        MText mtx2 = new MText();
                        mtx2.Location = insPt;
                        mtx2.Height = 5;
                        mtx2.Contents = "Don't Move Me";
                        mtx2.ColorIndex = 2; //yellow

                        btr.AppendEntity(mtx2);
                        trans.AddNewlyCreatedDBObject(mtx2, true);

                        MText mtx3 = new MText();
                        mtx3.Location = insPt;
                        mtx3.Height = 5;
                        mtx3.Contents = "Don't Move Me Either";
                        mtx3.ColorIndex = 1; //red

                        btr.AppendEntity(mtx3);
                        trans.AddNewlyCreatedDBObject(mtx3, true);

                        DBObjectCollection col = new DBObjectCollection();
                        col.Add(mtx1);
                        col.Add(mtx2);
                        col.Add(mtx3);

                        foreach (MText mtx in col)
                        {
                            if (mtx.Text.ToLower() == "move me")
                            {
                                // Create a matrix and move the circle using a vector from (0,0,0) to (50,50,0)
                                Point3d startPt = new Point3d(0, 0, 0);
                                Vector3d destVector = startPt.GetVectorTo(new Point3d(50, 50, 0));

                                mtx.TransformBy(Matrix3d.Displacement(destVector));
                            }
                        }
                    }
                    // Commit the transaction
                    trans.Commit();
                    doc.SendStringToExecute("._zoom e ", false, false, false);
                }
                catch (System.Exception ex)
                {
                    doc.Editor.WriteMessage("Error encountered: " + ex.Message);
                    trans.Abort();
                }
            }
        }

        [CommandMethod("MirrorX")]
        public static void MirrorExercise()
        {
            // Get the current document and database
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            // Start a transaction
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                try
                {
                    // Open the Block table for read
                    BlockTable bt;
                    bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;

                    // Open the Block table record Model space for write
                    BlockTableRecord btr;
                    btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    string str = "Mirrored";
                    using (MText mtx1 = new MText())
                    {
                        Point3d insPt = new Point3d(-10, 0, 0);
                        mtx1.Location = insPt;
                        mtx1.Contents = str;
                        mtx1.Height = 5;
                        mtx1.ColorIndex = 2;

                        btr.AppendEntity(mtx1);
                        trans.AddNewlyCreatedDBObject(mtx1, true);

                        insPt = new Point3d(0, 0, 0);
                        MText mtx2 = new MText();
                        mtx2.Location = insPt;
                        mtx2.Contents = str;
                        mtx2.Height = 3;
                        mtx2.ColorIndex = 1;

                        btr.AppendEntity(mtx2);
                        trans.AddNewlyCreatedDBObject(mtx2, true);

                        MText mtx3 = new MText();
                        insPt = new Point3d(10, 0, 0);
                        mtx3.Location = insPt;
                        mtx3.Contents = str;
                        mtx3.Height = 5;
                        mtx3.ColorIndex = 2;

                        btr.AppendEntity(mtx3);
                        trans.AddNewlyCreatedDBObject(mtx3, true);

                        DBObjectCollection col = new DBObjectCollection();
                        col.Add(mtx1);
                        col.Add(mtx2);
                        col.Add(mtx3);

                        // Loop through the collection
                        foreach (MText mtx in col)
                        {
                            if (mtx.Height == 3 && mtx.ColorIndex == 1)
                            {
                                Point3d startPt = new Point3d(0, 15, 0);
                                Point3d endPt = new Point3d(20, 15, 0);
                                Line3d ln = new Line3d(startPt, endPt);

                                mtx.TransformBy(Matrix3d.Mirroring(ln));
                            }
                        }
                    }
                    // Commit the transaction
                    trans.Commit();
                    doc.SendStringToExecute("._zoom e ", false, false, false);
                }
                catch (System.Exception ex)
                {
                    doc.Editor.WriteMessage("Error encountered: " + ex.Message);
                    trans.Abort();
                }
            }
        }

        [CommandMethod("RotateX")]
        public static void RotateExercise()
        {
            // Get the current document and database
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            // Start a transaction
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                try
                {
                    // Open the Block table for read
                    BlockTable bt;
                    bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;

                    // Open the Block table record Model space for write
                    BlockTableRecord btr;
                    btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    using (MText mtx = new MText())
                    {
                        Point3d insPt = new Point3d(10, 10, 0);
                        mtx.Location = insPt;
                        mtx.Height = 5;
                        mtx.Contents = "Rotating MText";

                        btr.AppendEntity(mtx);
                        trans.AddNewlyCreatedDBObject(mtx, true);

                        MText mtx2 = mtx.Clone() as MText;
                        mtx2.ColorIndex = 1;
                        mtx2.Contents = "Rotated MText";

                        btr.AppendEntity(mtx2);
                        trans.AddNewlyCreatedDBObject(mtx2, true);

                        Matrix3d curUCSMatrix = doc.Editor.CurrentUserCoordinateSystem;
                        CoordinateSystem3d curUCS = curUCSMatrix.CoordinateSystem3d;

                        mtx2.TransformBy(Matrix3d.Rotation(0.5235,
                                                             curUCS.Zaxis,
                                                             new Point3d(0, 0, 0)));


                    }
                    // Commit the transaction
                    trans.Commit();
                    doc.SendStringToExecute("._zoom e ", false, false, false);
                }
                catch (System.Exception ex)
                {
                    doc.Editor.WriteMessage("Error encountered: " + ex.Message);
                    trans.Abort();
                }
            }
        }

        [CommandMethod("ScaleX")]
        public static void ScaleExercise()
        {
            // Get the current document and database
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            // Start a transaction
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                try
                {
                    // Open the Block table for read
                    BlockTable bt;
                    bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;

                    // Open the Block table record Model space for write
                    BlockTableRecord btr;
                    btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    using (Circle c1 = new Circle())
                    {
                        c1.Center = new Point3d(0, 0, 0);
                        c1.Radius = 5;

                        // Add the new object to the BlockTable record
                        btr.AppendEntity(c1);
                        trans.AddNewlyCreatedDBObject(c1, true);

                        Circle c2 = new Circle();
                        c2.Center = new Point3d(10, 0, 0);
                        c2.Radius = 2.5;
                        c2.ColorIndex = 1;

                        // Add the new object to the BlockTable record
                        btr.AppendEntity(c2);
                        trans.AddNewlyCreatedDBObject(c2, true);

                        Circle c3 = new Circle();
                        c3.Center = new Point3d(20, 0, 0);
                        c3.Radius = 5;

                        // Add the new object to the BlockTable record
                        btr.AppendEntity(c3);
                        trans.AddNewlyCreatedDBObject(c3, true);

                        // Create a collection and the 3 Circle objects
                        DBObjectCollection col = new DBObjectCollection();
                        col.Add(c1);
                        col.Add(c2);
                        col.Add(c3);

                        foreach (Circle cir in col)
                        {
                            if (cir.Radius == 2.5 && cir.ColorIndex == 1)
                            {
                                Point3d centPt = cir.Center;
                                cir.TransformBy(Matrix3d.Scaling(4, centPt));
                            }
                        }
                    }

                    // Save the new objects to the database
                    trans.Commit();
                    doc.SendStringToExecute("._zoom e ", false, false, false);
                }
                catch (System.Exception ex)
                {
                    doc.Editor.WriteMessage("Error encountered: " + ex.Message);
                    trans.Abort();
                }
            }
        }
    }
}
