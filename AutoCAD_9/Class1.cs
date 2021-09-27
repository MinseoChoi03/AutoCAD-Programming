using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;

namespace AutoCAD_9
{
    public class Class1
    {
        [CommandMethod("MultipleCopy")]
        public void MultipleCopy()
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

                    using(Circle c1 = new Circle())
                    {
                        c1.Center = new Point3d(0, 0, 0);
                        c1.Radius = 5;

                        //Add the new object Block Table Record
                        btr.AppendEntity(c1);
                        trans.AddNewlyCreatedDBObject(c1, true);

                        using(Circle c2 = new Circle())
                        {
                            c2.Center = new Point3d(0, 0, 0);
                            c2.Radius = 7;

                            //Add to the Block Table Record
                            btr.AppendEntity(c2);
                            trans.AddNewlyCreatedDBObject(c2, true);

                            DBObjectCollection col = new DBObjectCollection();
                            col.Add(c1);
                            col.Add(c2);

                            foreach(Entity acEnt in col)
                            {
                                Entity ent;
                                ent = acEnt.Clone() as Entity;
                                ent.ColorIndex = 1; //red

                                //Create mtrix and move each copied entity 20 units to the right
                                ent.TransformBy(Matrix3d.Displacement(new Vector3d(20, 0, 0)));

                                // Add the cloned object
                                btr.AppendEntity(ent);
                                trans.AddNewlyCreatedDBObject(ent, true);
                            }
                        }
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
        [CommandMethod("SingleCopy")]
        public void SingleCopy()
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


                    //Create a Circle that is 2,3 with a radius of 4.25
                    using (Circle c1 = new Circle())
                    {
                        c1.Center = new Point3d(2, 3, 0);
                        c1.Radius = 4.25;

                        //Add the new object to the BlockTable reocrd
                        btr.AppendEntity(c1);
                        trans.AddNewlyCreatedDBObject(c1, true);

                        //Create a copy of the circle and change its radius
                        Circle c1Clone = c1.Clone() as Circle;
                        c1Clone.Radius = 1;

                        //Add the cloned circle
                        btr.AppendEntity(c1Clone);
                        trans.AddNewlyCreatedDBObject(c1Clone, true);
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
    }
}
