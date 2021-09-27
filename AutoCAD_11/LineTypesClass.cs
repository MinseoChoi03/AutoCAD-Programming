using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;

namespace AutoCAD_11
{
    class LineTypesClass
    {
        [CommandMethod("ListLineTypes")]
        public void ListLineTypes()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                LinetypeTable ltTab = trans.GetObject(db.LinetypeTableId, OpenMode.ForRead) as LinetypeTable;
                foreach(ObjectId ltID in ltTab)
                {
                    LinetypeTableRecord lttr = trans.GetObject(ltID, OpenMode.ForRead) as LinetypeTableRecord;
                    doc.Editor.WriteMessage("\n Line Type name : " + lttr.name);
                }
                trans.Commit();
            }
        }


    }
}
