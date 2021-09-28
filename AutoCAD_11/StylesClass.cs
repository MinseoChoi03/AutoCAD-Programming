using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;

namespace AutoCAD_11
{
    public class StylesClass
    {
        [CommandMethod("ListStyles")]
        public void ListStyles()
        {
            //Get the current docucment and databases
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                TextStyleTable stTab = trans.GetObject(db.TextStyleTableId, OpenMode.ForRead) as TextStyleTable;
                foreach (ObjectId stID in stTab)
                {
                    TextStyleTableRecord tstr = trans.GetObject(stID, OpenMode.ForRead) as TextStyleTableRecord;
                    doc.Editor.WriteMessage("\nStyle name : " + tstr.Name);
                }
                //Commit the transactoin
                trans.Commit();
            }
        }

        [CommandMethod("UpdateCurrentTextStyleFont")]
        public void UpdateCurrentTextStyleFont()
        {
            //Get the current docucment and databases
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                //Open the current text style for write
                TextStyleTableRecord tstr;
                tstr = trans.GetObject(db.Tablestyle, OpenMode.ForWrite) as TextStyleTableRecord;

                //Get the current font setting
                Autodesk.AutoCAD.GraphicsInterface.FontDescriptor font;
                font = tstr.Font;

                //Update the text style's typeface with "ARCHITECT"
                Autodesk.AutoCAD.GraphicsInterface.FontDescriptor newFont;
                newFont = new Autodesk.AutoCAD.GraphicsInterface.FontDescriptor("ARCHITECT", font.Bold, font.Italic, font.CharacterSet, font.PitchAndFamily);
                tstr.Font = newFont;

                doc.Editor.Regen();

                //Commit the transactoin
                trans.Commit();
            }
        }

        [CommandMethod("SetCurrentTextStyle")]
        public void SetCurrentTextStyle()
        {
            //Get the current docucment and databases
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                TextStyleTable stTab = trans.GetObject(db.TextStyleTableId, OpenMode.ForRead) as TextStyleTable;
                foreach (ObjectId stID in stTab)
                {
                    TextStyleTableRecord tstr = trans.GetObject(stID, OpenMode.ForRead) as TextStyleTableRecord;
                    if (tstr.Name == "ARCHITECT")
                    {
                        Application.SetSystemVariable("TEXTSTYLE", "ARCHITECT");
                        doc.Editor.WriteMessage("\nStyle name : " + tstr.Name + "is now the default TextStyle.");
                        //Commit the transactoin
                        trans.Commit();
                        break;
                    }
                }
            }
        }
    }
}
