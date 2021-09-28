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
    }
}
