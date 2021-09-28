using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;

namespace AutoCAD_13
{
    public class Class1
    {
        [CommandMethod("SelectAllAndChangeLayer")]
        public void SelectAllAndChangeLayer()
        {
            // Get the document, database, and editor object
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;

            using (Transaction trans = doc.TransactionManager.StartTransaction())
            {
                // Create a selectionset and select all the object in the drawing
                PromptSelectionResult ssPrompt = edt.SelectAll();
                SelectionSet ss = ssPrompt.Value;

                // Check if there is object selected
                if (ssPrompt.Status == PromptStatus.OK)
                {
                    // Loop through the selectionset and change the layer
                    foreach (SelectedObject sObj in ss)
                    {
                        if (sObj != null)
                        {
                            // Create a new entity and assign the object to that entity
                            Entity ent = trans.GetObject(sObj.ObjectId, OpenMode.ForWrite) as Entity;
                            ent.Layer = "Misc";
                        }
                    }
                    // Commit the transaction
                    trans.Commit();
                }
                else
                {
                    edt.WriteMessage("No object selected.");
                }
            }
        }


        [CommandMethod("SelectObjectOnScreen")]
        public void SelectObjectOnScreen()
        {
            // Get the current drawing document and database
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                // Prompt the user to select objects
                PromptSelectionResult psr = edt.GetSelection();
                if (psr.Status == PromptStatus.OK)
                {
                    // Create a selectionset and assign the selected objects
                    SelectionSet ss = psr.Value;
                    edt.WriteMessage("\nThere are a total of " + ss.Count.ToString() + " objects selected.");

                    foreach (SelectedObject sObj in ss)
                    {
                        // Open the selected object for write and assign to an Entity object
                        Entity ent = trans.GetObject(sObj.ObjectId, OpenMode.ForWrite) as Entity;
                        if (ent != null)
                        {
                            ent.ColorIndex = 1;
                        }
                    }
                }
                // Commit the transaction
                trans.Commit();
            }
        }


        [CommandMethod("SelectWindowAndChangeColor")]
        public void SelectWindowAndChangeColor()
        {
            // Get the Document, Database and Editor objects
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;

            // Start the transaction
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                // Specify the coordinate of the window
                PromptSelectionResult psr = edt.SelectWindow(new Point3d(0, 0, 0), new Point3d(500, 500, 0));
                if (psr.Status == PromptStatus.OK)
                {
                    // Create a selectionset then assign the selected value in it
                    SelectionSet ss = psr.Value;
                    foreach (SelectedObject sObj in ss)
                    {
                        // Open the object for write and assign to an Entity and change the color to red
                        Entity ent = trans.GetObject(sObj.ObjectId, OpenMode.ForWrite) as Entity;
                        if (ent != null)
                        {
                            ent.ColorIndex = 1;
                        }
                    }
                }
                else
                {
                    edt.WriteMessage("No object selected.");
                }

                // Commit the transaction
                trans.Commit();
            }
        }


        [CommandMethod("SelectCrossingWindowAndDelete")]
        public void SelectCrossingWindowAndDelete()
        {
            // Get the Drawing document, Database and Editor objects
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;

            using (Transaction trans = doc.TransactionManager.StartTransaction())
            {
                // Specify the coordinate for the crossing
                PromptSelectionResult psr = edt.SelectCrossingWindow(new Point3d(0, 0, 0), new Point3d(500, 500, 0));
                // If the PromptStatus is OK then object is selected
                if (psr.Status == PromptStatus.OK)
                {
                    // Store in a selectionset then display the count of the object selected
                    SelectionSet ss = psr.Value;

                    edt.WriteMessage("\nTotal objects selected: " + ss.Count.ToString());

                    // Then loop through the selection set and delete the object
                    foreach (SelectedObject sObj in ss)
                    {
                        // Check if a valid object is returned
                        if (sObj != null)
                        {
                            // Open the object for writing
                            Entity ent = trans.GetObject(sObj.ObjectId, OpenMode.ForWrite) as Entity;
                            if (ent != null)
                            {
                                // Erase the entity
                                ent.Erase(true);
                            }
                        }
                    }
                    // Commit the transaction
                    trans.Commit();
                }
                else
                {
                    edt.WriteMessage("\nNo object selected.");
                }
            }
        }


        [CommandMethod("SelectFenceAndChangeLayer")]
        public void SelectFenceAndChangeLayer()
        {
            // Get the Document, Database and Editor object
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;

            // Set the transaction
            using (Transaction trans = doc.TransactionManager.StartTransaction())
            {
                // Select objects by fence
                Point3dCollection p3dCol = new Point3dCollection();
                p3dCol.Add(new Point3d(0, 0, 0));
                p3dCol.Add(new Point3d(500, 300, 0));
                PromptSelectionResult psr = edt.SelectFence(p3dCol);

                // Check if there are objects selected
                if (psr.Status == PromptStatus.OK)
                {
                    SelectionSet ss = psr.Value;

                    foreach (SelectedObject sObj in ss)
                    {
                        if (sObj != null)
                        {
                            Entity ent = trans.GetObject(sObj.ObjectId, OpenMode.ForWrite) as Entity;
                            if (ent != null)
                            {
                                // Change the layer
                                ent.Layer = "Misc";
                            }
                        }
                    }
                }
                else
                {
                    edt.WriteMessage("\nNo object selected.");
                }

                // Commit the Transaction
                trans.Commit();
            }
        }


        [CommandMethod("PickFirstSelection", CommandFlags.UsePickSet)]
        public void PickFirstSelection()
        {
            // Get the Editor object
            Editor edt = Application.DocumentManager.MdiActiveDocument.Editor;

            // Get the PickFirst selection set
            PromptSelectionResult psr;
            psr = edt.SelectImplied();

            // Create a SelectionSet
            SelectionSet ss;

            // Check if the prompt status is OK which means that objects were already selected.
            if (psr.Status == PromptStatus.OK)
            {
                ss = psr.Value;
                Application.ShowAlertDialog("\nNumber of objects in PickFirst selection: " + ss.Count.ToString());
            }
            else
            {
                Application.ShowAlertDialog("\nNo object selected.");
            }


            // Clear the PickFirst selection set
            ObjectId[] ids = new ObjectId[0];
            edt.SetImpliedSelection(ids);

            // Request for objects to be selected in the drawing area
            psr = edt.GetSelection();

            // if the prompt status is OK, objects were selected
            if (psr.Status == PromptStatus.OK)
            {
                ss = psr.Value;
                Application.ShowAlertDialog("\nNumber of objects selected: " + ss.Count.ToString());
            }
            else
            {
                Application.ShowAlertDialog("\nNo object selected.");
            }
        }
}
