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
                foreach(ObjectId stID in stTab)
                {
                    TextStyleTableRecord tstr = trans.GetObject(stID, OpenMode.ForRead) as TextStyleTableRecord;
                    doc.Editor.WriteMessage("\nStyle name : " + tstr.Name);
                }
                //Commit the transactoin
                trans.Commit();
            }
        }
    }
}
