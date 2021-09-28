using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;

namespace AutoCAD_14
{
    public class TBlock
    {
        //Properties for Lables
        private string projectNameLabel;
        private string drawingTitleLabel;
        private string addressLabel;
        private string projectNoLabel;
        private string clientRefLabel;
        private string drawingDateLabel;
        private string drawingScaleLabel;
        private string revisionNoLabel;
        private string drawingNoLabel;


        //Properties for the actual drawing/project info
        public string ProjectName { get; set; }
        public string DrawingTitle { get; set ; }
        public string Address { get; set; }
        public string ProjectNo { get; set; }
        public string ClientRef { get; set; }
        public string DrawingDate { get; set; }
        public string DrawingScale { get; set; }
        public string RevisionNo { get; set; }
        public string DrawingNo { get; set; }

        public TBlock()
        {
            //Labels
            projectNameLabel = "Project Name";
            drawingTitleLabel = "Dwg Title";
            addressLabel = "Address";
            projectNoLabel = "Proj. No";
            clientRefLabel = "Client Ref";
            drawingDateLabel = "Date : ";
            drawingScaleLabel = "Scale";
            revisionNoLabel = "Rev. No";
            drawingNoLabel = "Dwg. No";

            //Default Values
            ProjectName = "Default Project Name";
            DrawingTitle = "Default Drawing Title";
            Address = "Default Location";
            ProjectNo = "2019-##";
            ClientRef = "";
            DrawingDate = DateTime.Today.ToShortDateString();
            RevisionNo = "00";
            DrawingNo = "A#.#";
            DrawingScale = "1:###";
        }

        public void DrawTitleBlock(int height, int width)
        {
            //TODO
        }
    }
}
