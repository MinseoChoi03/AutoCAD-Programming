using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;

namespace AutoCAD_13
{
    public class Exercise
    {
        /*Exercises
            1) Create a method that selects all the object in the layer Wall
            2) Create a method that selects all the object in the layer Stairs and change the colors to Red


        */
        // Solutions to Exercises
        // ******************************************************************************************


        [CommandMethod("ReceptaclesExercise")]
        public void ReceptaclesExercise()
        {
            // Get the Editor object
            Editor edt = Application.DocumentManager.MdiActiveDocument.Editor;

            edt.WriteMessage("\nSelecting all the Receptacles in the drawing");

            // Create a selectionset filter using a TypedValue
            TypedValue[] tv = new TypedValue[3];
            tv.SetValue(new TypedValue((int)DxfCode.Start, "INSERT"), 0);
            tv.SetValue(new TypedValue((int)DxfCode.BlockName, "Receptacle"), 1);
            tv.SetValue(new TypedValue((int)DxfCode.LayerName, "Power"), 2);

            // Now create a filter with these values
            SelectionFilter filter = new SelectionFilter(tv);
            // Create a PromptSelectionResult with the filter
            PromptSelectionResult psr = edt.SelectAll(filter);

            // Check if there is object selected
            if (psr.Status == PromptStatus.OK)
            {
                // Create a selectionset and store the value from the Prompt
                SelectionSet ss = psr.Value;

                // Display the count of the French-Doors selected
                edt.WriteMessage("\nThe number of Receptacles selected is: " + ss.Count.ToString());
            }
            else
            {
                edt.WriteMessage("\nThere is no object selected.");
            }
        }

        [CommandMethod("LightingFixturesExercise")]
        public void LightingFixturesExercise()
        {
            // Get the document, database, and editor object
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;

            edt.WriteMessage("\nSelecting all the Lighting Fixtures in the drawing");

            using (Transaction trans = doc.TransactionManager.StartTransaction())
            {
                // Create a selectionset filter using a TypedValue
                TypedValue[] tv = new TypedValue[3];
                tv.SetValue(new TypedValue((int)DxfCode.Start, "INSERT"), 0);
                tv.SetValue(new TypedValue((int)DxfCode.BlockName, "Lighting Fixture"), 1);
                tv.SetValue(new TypedValue((int)DxfCode.LayerName, "Lighting"), 2);

                // Now create a filter with these values
                SelectionFilter filter = new SelectionFilter(tv);
                // Create a PromptSelectionResult with the filter
                PromptSelectionResult psr = edt.SelectAll(filter);

                // Check if there is object selected
                if (psr.Status == PromptStatus.OK)
                {
                    // Create a selectionset and store the value from the Prompt
                    SelectionSet ss = psr.Value;

                    // Loop through the selectionset and change the layer
                    foreach (SelectedObject sObj in ss)
                    {
                        if (sObj != null)
                        {
                            // Create a new entity and assign the object to that entity
                            Entity ent = trans.GetObject(sObj.ObjectId, OpenMode.ForWrite) as Entity;
                            ent.Layer = "Power";
                        }
                    }
                    // Display the count of the French-Doors selected
                    edt.WriteMessage("\nThe number of Lighting Fixtures selected is: " + ss.Count.ToString());
                }
                else
                {
                    edt.WriteMessage("\nThere is no object selected.");
                }
                // Commit the transaction
                trans.Commit();
            }
        }
    }
}
