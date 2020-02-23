using HostMgd.ApplicationServices;
using System.Collections.Generic;
using Teigha.DatabaseServices;

namespace Cabinet_Maker_NanoCad
{
    public class Draw
    {
        // Get the current document and database
        //Document acDoc = Application.DocumentManager.MdiActiveDocument;
        Database acCurDb = Application.DocumentManager.MdiActiveDocument.Database;

        //, List<AlignedDimension> rotatedDimensions, List<MText> acMTexts
        public void DrawObjectsFromPolylineList(List<Polyline> poly2D,string layer)
        {
            // Start a transaction
            using (var acTrans = acCurDb.TransactionManager.StartTransaction())
            {

                // Open the Block table for read
                var acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId,
                    OpenMode.ForRead) as BlockTable;

                // Open the Block table record Model space for write
                var acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace],
                    OpenMode.ForWrite) as BlockTableRecord;

                // Add the new object to the block table record and the transaction

                foreach (var polyline in poly2D)
                {
                    polyline.Layer = layer;
                    acBlkTblRec.AppendEntity(polyline);

                    acTrans.AddNewlyCreatedDBObject(polyline, true);
                }

                //foreach (var dimension in rotatedDimensions)
                //{
                //    acBlkTblRec.AppendEntity(dimension);


                //    acTrans.AddNewlyCreatedDBObject(dimension, true);
                //}
                //foreach (var actext in acMTexts)
                //{
                //    actext.Layer = "Wypis elementow";
                //    acBlkTblRec.AppendEntity(actext);
                //    acTrans.AddNewlyCreatedDBObject(actext, true);
                //}


                // Save the new object to the database
                acTrans.Commit();
            }

        }

        public void DrawDimensionList(List<AlignedDimension> rotatedDimensions, string layer)
        {
            // Start a transaction
            using (var acTrans = acCurDb.TransactionManager.StartTransaction())
            {

                // Open the Block table for read
                var acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId,
                    OpenMode.ForRead) as BlockTable;

                // Open the Block table record Model space for write
                var acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace],
                    OpenMode.ForWrite) as BlockTableRecord;

                foreach (var dimension in rotatedDimensions)
                {
                    dimension.Layer = layer;
                    acBlkTblRec.AppendEntity(dimension);


                    acTrans.AddNewlyCreatedDBObject(dimension, true);
                }
                

                // Save the new object to the database
                acTrans.Commit();
            }

        }

    }
}