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
    }
}
