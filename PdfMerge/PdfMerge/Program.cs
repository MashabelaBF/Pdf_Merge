using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using iText.IO.Image;
using iText.IO.Source;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Element;


namespace PdfMerge
{
    class Program
    {
        static void Main(string[] args)
        {
                      
            Console.Write("Press (M) to Merge PDF or (A) to add images to an existing PDF: ");
            var choose = Console.ReadLine();

            try
            {
                if (choose.ToUpper() == "A")
                {
                    AddImage();
                }
                else if (choose.ToUpper() == "M")
                {
                    MergePdf();
                }
                else
                {
                
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            
            /// /// <summary>
            /// merge two or more pdf documents
            /// </summary>
            /// <returns></returns>
            void MergePdf()
            {
            
                var dest = "";
                var count = 1;
                var exit = false;

                List<string> arrDir = new List<string>();


                try
                {
                    //check if the user wants to insert more pdf documents
                    while (exit != true)
                    {
                        Console.Write("Please enter the pdf directory " + count + ": ");
                        var currentDir = Console.ReadLine();
                        var formatedDir = currentDir.Replace(@"\", @"\\");

                        //the destination to save the file
                        dest = formatedDir + "merged.pdf";
                        //add directories to an array
                        arrDir.Add(formatedDir);

                        Console.Write("Press (Y) to exit and (N) to enter another directory: ");
                        var exitStatus = Console.ReadLine();


                        if (exitStatus.ToUpper() == "Y")
                        {
                            exit = true;
                        }
                        else if (exitStatus.ToUpper() == "N")
                        {
                            exit = false;
                        }
                        else
                        {
                            exit = false;
                        }
                        count++;
                    }

                    //Initialize destination document
                    PdfDocument resultDoc = new PdfDocument(new PdfWriter(dest));

                    for (int i = 1; i <= arrDir.Count(); i++)
                    {
                        var elem = arrDir[i - 1];

                        PdfDocument pdfDoc = new PdfDocument(new PdfReader(elem));
                        int numOfpages = pdfDoc.GetNumberOfPages();

                        for (int j = 1; j <= numOfpages; j++)
                        {
                            PdfPage page = pdfDoc.GetPage(j).CopyTo(resultDoc);
                            resultDoc.AddPage(page);
                        }

                        pdfDoc.Close();
                    }
                    //Close documents
                    resultDoc.Close();

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                   
                }
                
            }


            /// <summary>
            /// add image to an existing pdf file 
            /// </summary>
            /// <returns></returns>
            void AddImage()
            {

                var source = "";
                var check = true;
                
                //list of all the images
                List<string> arrDir = new List<string>();

                try
                {
                    Console.Write("\nPlease Enter the Source document: ");
                    var inputSource = Console.ReadLine();

                    //format the directory the user inserts
                    source = inputSource.Replace(@"\", @"\\");

                    //check if the user wants to enter more images
                    while (check != false)
                    {
                        Console.Write("\nPlease input image directory you wish to merge to pdf: ");
                        var imgDirectory = Console.ReadLine();

                        var imgDir = imgDirectory.Replace(@"\", @"\\"); ;
                        Console.Write("****Your merge file dir is\n");
                        Console.Write(imgDir);

                        Console.Write("\nIf you want to add more images enter (Y) to Exit enter (N): ");
                        var exitStatus = Console.ReadLine();

                        arrDir.Add(imgDir);

                        if (exitStatus.ToUpper() == "Y")
                        {
                            check = true;

                        }
                        else if (exitStatus.ToUpper() == "N")
                        {
                            check = false;
                        }
                        else
                        {
                            check = false;
                        }

                    }

                    // Modify PDF located at "source" and save to "target"
                    PdfDocument pdfDocument = new PdfDocument(new PdfReader(source), new PdfWriter(source + "new.pdf"));

                    // Document to add layout elements: paragraphs, images etc
                    Document document = new Document(pdfDocument);

                    // Load image from disk
                    ImageData imageData;

                    //get the directories from the array
                    for (int i = 0; i < arrDir.Count; i++)
                    {
                        var currentValue = arrDir[i];
                        imageData = ImageDataFactory.Create(currentValue);

                        //get the width and height of the image and the number of pages the doc contains
                        var hyt = 360;
                        var wid = 540;
                        int pnum = pdfDocument.GetNumberOfPages();

                        // Create layout image object and provide parameters. Page number = 1
                        iText.Layout.Element.Image testImage = new iText.Layout.Element.Image(imageData).ScaleAbsolute(wid, hyt).SetFixedPosition(pnum + 1, 0, 0);

                        // This adds the image to the page
                        document.Add(testImage);

                    }


                    //adding the image using convas method
                    //PdfPage page = pdfDocument.GetFirstPage();
                    //new PdfCanvas(page.NewContentStreamAfter(), pdfDocument.GetFirstPage().GetResources(), pdfDocument).AddImage(imageData,0,0,false);

                    // Don't forget to close the document.
                    // When you use Document, you should close it rather than PdfDocument instance
                    document.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                

            }


            /// <summary>
            /// method for concverting pdf,bmp,tiff to pdf
            /// </summary>
            /// <returns></returns>
            void TestConvert()
            {
                //PdfDocument doc = new PdfDocument();
                ////PdfSection section = doc.Sections.Add();
                //PdfPageBase page = doc.Pages.Add();

                ////Load a tiff image from system
                //PdfImage image = PdfImage.FromFile(@"D:\Users\Bran.mashabela\Pictures\test.tiff");

                //Set image display location and size in PDF
                //float widthFitRate = image.PhysicalDimension.Width / page.Canvas.ClientSize.Width;
                //float heightFitRate = image.PhysicalDimension.Height / page.Canvas.ClientSize.Height;
                //float fitRate = Math.Max(widthFitRate, heightFitRate);
                //float fitWidth = image.PhysicalDimension.Width / fitRate;
                //float fitHeight = image.PhysicalDimension.Height / fitRate;
                //page.Canvas.DrawImage(image, 30, 30, fitWidth, fitHeight);
                //page.Canvas.DrawImage(image, 50, 50);


                ////save and launch the file
                //doc.SaveToFile("D:/Users/Bran.mashabela/Pictures/imagepdf.pdf");
                //doc.Close();

                //System.Diagnostics.Process.Start("image to pdf.pdf");
                //PdfImage images = PdfImage.FromFile(@"D:\Users\Bran.mashabela\Pictures\bear.bmp");
                //PdfImage imagess = PdfImage.FromFile(@"D:\Users\Bran.mashabela\Pictures\pic.jpg");
                //PdfImage imagesss = PdfImage.FromFile(@"D:\Users\Bran.mashabela\Pictures\Capture.png");
                //PdfImage pdftes = PdfImage.FromFile(@"D:\Users\Bran.mashabela\Pictures\one.pdf");

                //string filePath1 = @"D:\Users\Bran.mashabela\Pictures\bear.bmp";
                //string filePath2 = @"D:\Users\Bran.mashabela\Pictures\pic.jpg";
                //string filePath3 = @"D:\Users\Bran.mashabela\Pictures\Capture.png";
                //string filePath4 = @"D:\Users\Bran.mashabela\Pictures\one.pdf";

                //Bitmap image1 = new Bitmap(filePath1);
                //Bitmap image2 = new Bitmap(filePath2);
                //Bitmap image3 = new Bitmap(filePath3);
                //Bitmap image4 = new Bitmap(filePath4);

                //List<Bitmap> images = new List<Bitmap>();
                //images.Add(image1);
                //images.Add(image2);
                //images.Add(image3);

                //page.Canvas.DrawImage(images, 50, 50);
                //page.Canvas.DrawImage(imagess, 50, 100);
                //page.Canvas.DrawImage(imagesss, 50, 150);
                //page.Canvas.DrawImage(pdftes, 50, 200);
                //// PDFDocument doc = new PDFDocument(images.ToArray());
                //doc.SaveToFile(@"D:\Users\Bran.mashabela\Pictures\output.pdf");
                //doc.Close();
               // return "Created PDF";
            }




        }

       
        



    }
}
