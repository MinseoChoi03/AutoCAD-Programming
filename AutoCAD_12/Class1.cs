using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;

namespace AutoCAD_12
{
    public class Class1
    {
        [CommandMethod("GetName")]
        public void GetNameUsingGetString()
        {
            // Get the document object
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Editor edt = doc.Editor;

            // Prompt the user using PromptStringOptions
            PromptStringOptions prompt = new PromptStringOptions("Enter your name: ");
            prompt.AllowSpaces = true;

            // Get the results of the user input using a PromptResult
            PromptResult result = edt.GetString(prompt);
            if (result.Status == PromptStatus.OK)
            {
                string name = result.StringResult;
                edt.WriteMessage("Hello there: " + name);
                Application.ShowAlertDialog("Your name is : " + name);
            }
            else
            {
                edt.WriteMessage("No name entered.");
                Application.ShowAlertDialog("No name entered.");
            }
        }

        [CommandMethod("SetLayerUsingGetString")]
        public void SetLayerUsingGetString()
        {
            // Get the document object
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;

            using (Transaction trans = doc.TransactionManager.StartTransaction())
            {
                LayerTable lyTab = trans.GetObject(db.LayerTableId, OpenMode.ForRead) as LayerTable;

                PromptStringOptions prompt = new PromptStringOptions("Enter layer to make current: ");
                prompt.AllowSpaces = false;

                // Get the results of the user input using a PromptResult
                PromptResult result = edt.GetString(prompt);
                if (result.Status == PromptStatus.OK)
                {
                    string layerName = result.StringResult;

                    // Check if the entered layer name exist in the layer database
                    if (lyTab.Has(layerName) == true)
                    {
                        // Set the layer current
                        db.Clayer = lyTab[layerName];

                        // Commit the transaction
                        trans.Commit();
                    }
                    else
                    {
                        Application.ShowAlertDialog("The layer " + layerName + " you entered does not exist.");
                    }
                }
                else
                {
                    Application.ShowAlertDialog("No layer entered.");
                }
            }
        }

        // This method will draw a Line based on the user input
        [CommandMethod("CreateLineUsingGetPoint")]
        public void CreateLineUsingGetPoint()
        {
            // Get the document object
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;

            // Prompt for the starting point
            PromptPointOptions ppo = new PromptPointOptions("Enter start point: ");
            PromptPointResult ppr = edt.GetPoint(ppo);
            Point3d startPt = ppr.Value;

            // Prompt for the end point and specify the startpoint as the basepoint
            ppo = new PromptPointOptions("Enter end point: ");
            ppo.UseBasePoint = true;
            ppo.BasePoint = startPt;
            ppr = edt.GetPoint(ppo);
            Point3d endPt = ppr.Value;

            if (startPt == null || endPt == null)
            {
                edt.WriteMessage("Invalid point.");
                return;
            }
            // Start the Transaction
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                // Get the BlockTable
                BlockTable bt;
                bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;

                BlockTableRecord btr;
                btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                // Construct the Line based on the 2 points above
                Line ln = new Line(startPt, endPt);
                ln.SetDatabaseDefaults();

                // Add the Line to the drawing
                btr.AppendEntity(ln);
                trans.AddNewlyCreatedDBObject(ln, true);

                // Commit the Transaction
                trans.Commit();
            }
        }
    }
}
