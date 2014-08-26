
using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace CropPdf
{


    static class Program
    {


        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
#if false
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
#endif
            string PdfObjectPath = System.IO.Path.Combine(System.Reflection.Assembly.GetExecutingAssembly().Location, @"../../../Drawings/dwg1566.pdf");
            PdfObjectPath = System.IO.Path.GetFullPath(PdfObjectPath);


            double dblCropLeft = 335;
            double dblCropTop = 320;
            double dblCropRight = dblCropLeft;
            double dblCropBottom = dblCropTop;



            using (PdfSharp.Pdf.PdfDocument doc = new PdfSharp.Pdf.PdfDocument())
            {
                PdfSharp.Pdf.PdfPage page = doc.AddPage();

                using (PdfSharp.Drawing.XPdfForm form = PdfSharp.Drawing.XPdfForm.FromFile(PdfObjectPath))
                {

                    // http://www.pdfsharp.net/wiki/CombineDocuments-sample.ashx
                    // http://www.pdfsharp.net/wiki/ExportImages-sample.ashx
                    // http://www.pdfsharp.net/wiki/Graphics-sample.ashx#Clip_through_path_16
                    // http://www.pdfsharp.net/wiki/Booklet-sample.ashx
                    // http://www.pdfsharp.net/wiki/WorkOnPdfObjects-sample.ashx
                    // http://www.pdfsharp.net/wiki/Unicode-sample.ashx
                    // http://www.pdfsharp.net/wiki/PDFsharpSamples.ashx
                    // http://www.c-sharpcorner.com/uploadfile/hirendra_singh/how-to-make-image-editor-tool-in-C-Sharp-cropping-image/
                    // http://www.namedquery.com/cropping-pdf-using-itextsharp
                    foreach (var item in form.Page.Elements)
                    { 
                        Console.WriteLine(item.Key);
                        Console.WriteLine(item.Value);
                    }



                    page.Width = form.PointWidth - dblCropLeft - dblCropRight;
                    page.Height = form.PointHeight - dblCropLeft - dblCropRight;


                    PdfSharp.Drawing.XRect rectTarget = new PdfSharp.Drawing.XRect(0, 0, form.PointWidth, form.PointHeight);

                    //PdfSharp.Drawing.XPdfForm frm2 = doc;


                    //PdfSharp.Drawing.XRect rect = new PdfSharp.Drawing.XRect(0, 0, form.PointWidth, form.PointHeight);
                    //PdfSharp.Pdf.PdfRectangle x = new PdfSharp.Pdf.PdfRectangle(rect);
                    // form.Page.TrimBox = x
                    // form.Page.CropBox = x

                    using (PdfSharp.Drawing.XGraphics gfx = PdfSharp.Drawing.XGraphics.FromPdfPage(page))
                    {
                        // (x,y) = (55,70)
                        // (x,y) = (11,14)
                        // (x,y) = (335,320)

                        // double y = 320; gfx.DrawLine(PdfSharp.Drawing.XPens.Red, 0, y, 335, y);
                        // double x = 335; gfx.DrawLine(PdfSharp.Drawing.XPens.Red, x, 0, x, 320);

                        // gfx.RotateTransform(30)


                        //gfx.IntersectClip(rect);
                        gfx.Save();
                        gfx.TranslateTransform(-dblCropLeft, -dblCropTop);
                        gfx.DrawImage(form, rectTarget);
                        gfx.Restore();
                    } // End Using gfx

                } // End Using form

                string strFileName = @"d:\test.pdf";
                doc.Save(strFileName);
                System.Diagnostics.Process.Start(strFileName);
            } // End Using doc



            Console.WriteLine(Environment.NewLine);
            Console.WriteLine(" --- Press any key to continue --- ");
            // Console.ReadKey();
        } // End Sub Main


    }


}
