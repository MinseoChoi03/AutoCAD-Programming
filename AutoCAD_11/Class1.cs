﻿using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.Geometry;

namespace AutoCAD_11
{
    public class Class1
    {
        [CommandMethod("ListLayers")]
        public static void ListLayers()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                LayerTable lyTab = trans.GetObject(db.LayerTableId, OpenMode.ForRead) as LayerTable;
                foreach (ObjectId lyID in lyTab)
                {
                    LayerTableRecord lytr = trans.GetObject(lyID, OpenMode.ForRead) as LayerTableRecord;
                    doc.Editor.WriteMessage("\nLayer name: " + lytr.Name);
                }

                // Commit the transaction
                trans.Commit();
            }
        }

        [CommandMethod("CreateLayer")]
        public static void CreateLayer()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                LayerTable lyTab = trans.GetObject(db.LayerTableId, OpenMode.ForRead) as LayerTable;
                if (lyTab.Has("Misc"))
                {
                    doc.Editor.WriteMessage("Layer already exist.");
                    trans.Abort();
                }
                else
                {
                    lyTab.UpgradeOpen();
                    LayerTableRecord ltr = new LayerTableRecord();
                    ltr.Name = "Misc";
                    ltr.Color = Color.FromColorIndex(ColorMethod.ByLayer, 1);
                    lyTab.Add(ltr);
                    trans.AddNewlyCreatedDBObject(ltr, true);
                    db.Clayer = lyTab["Misc"];

                    doc.Editor.WriteMessage("Layer [" + ltr.Name + "] was created successfully.");

                    // Commit the transaction
                    trans.Commit();
                }
            }
        }

        [CommandMethod("UpdateLayer")]
        public static void UpdateLayer()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                try
                {
                    LayerTable lyTab = trans.GetObject(db.LayerTableId, OpenMode.ForRead) as LayerTable;
                    foreach (ObjectId lyID in lyTab)
                    {
                        LayerTableRecord lytr = trans.GetObject(lyID, OpenMode.ForRead) as LayerTableRecord;
                        if (lytr.Name == "Misc")
                        {
                            lytr.UpgradeOpen();

                            // Update the Color
                            lytr.Color = Color.FromColorIndex(ColorMethod.ByLayer, 2);

                            // Update the LineType
                            LinetypeTable ltTab = trans.GetObject(db.LinetypeTableId, OpenMode.ForRead) as LinetypeTable;
                            if (ltTab.Has("Hidden") == true)
                            {
                                lytr.LinetypeObjectId = ltTab["Hidden"];
                            }

                            // Commit the transaction
                            trans.Commit();
                            doc.Editor.WriteMessage("\nCompleted updating Layer: " + lytr.Name);
                            break;
                        }
                        else
                        {
                            doc.Editor.WriteMessage("\nSkipping Layer [" + lytr.Name + "].");
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    doc.Editor.WriteMessage("Error encountered: " + ex.Message);
                    trans.Abort();
                }
            }
        }

        [CommandMethod("SetLayerOnOff")]
        public static void SetLayerOnOff()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                try
                {
                    LayerTable lyTab = trans.GetObject(db.LayerTableId, OpenMode.ForRead) as LayerTable;
                    db.Clayer = lyTab["0"];
                    foreach (ObjectId lyID in lyTab)
                    {
                        LayerTableRecord lytr = trans.GetObject(lyID, OpenMode.ForRead) as LayerTableRecord;
                        if (lytr.Name == "Misc")
                        {
                            lytr.UpgradeOpen();

                            // Turn the layer ON or OFF
                            lytr.IsOff = true;
                            //lytr.IsOff = false;

                            // Commit the transaction
                            trans.Commit();
                            doc.Editor.WriteMessage("\nLayer " + lytr.Name + " has been turned Off.");
                            break;
                        }
                        else
                        {
                            doc.Editor.WriteMessage("\nLayer not found.");
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    doc.Editor.WriteMessage("Error encountered: " + ex.Message);
                    trans.Abort();
                }
            }
        }

        [CommandMethod("SetLayerFrozenOrThaw")]
        public static void SetLayerFrozenOrThaw()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                try
                {
                    LayerTable lyTab = trans.GetObject(db.LayerTableId, OpenMode.ForRead) as LayerTable;
                    db.Clayer = lyTab["0"];
                    foreach (ObjectId lyID in lyTab)
                    {
                        LayerTableRecord lytr = trans.GetObject(lyID, OpenMode.ForRead) as LayerTableRecord;
                        if (lytr.Name == "Misc")
                        {
                            lytr.UpgradeOpen();

                            // Freeze or Thaw the layer
                            lytr.IsFrozen = true;
                            //lytr.IsFrozen = false;

                            // Commit the transaction
                            trans.Commit();
                            doc.Editor.WriteMessage("\nLayer " + lytr.Name + " has been frozen.");
                            break;
                        }
                        else
                        {
                            doc.Editor.WriteMessage("\nLayer not found.");
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    doc.Editor.WriteMessage("Error encountered: " + ex.Message);
                    trans.Abort();
                }
            }
        }

        [CommandMethod("DeleteLayer")]
        public static void DeleteLayer()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                try
                {
                    LayerTable lyTab = trans.GetObject(db.LayerTableId, OpenMode.ForRead) as LayerTable;
                    db.Clayer = lyTab["0"];
                    foreach (ObjectId lyID in lyTab)
                    {
                        LayerTableRecord lytr = trans.GetObject(lyID, OpenMode.ForRead) as LayerTableRecord;
                        if (lytr.Name == "Misc")
                        {
                            lytr.UpgradeOpen();

                            // Delete the layer
                            lytr.Erase(true);

                            // Commit the transaction
                            trans.Commit();
                            doc.Editor.WriteMessage("\nSuccessfully deleted Layer [" + lytr.Name + "]");
                            break;
                        }
                    }
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
