using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// iTextSharp namespaces
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace FilesSearcherProject
{
    class FileSearchPDF : FileSearchBase
    {
        /// <summary>
        /// Search PDF File
        /// </summary>
        /// <param name="fileNamePath"></param>
        /// <param name="searchStr"></param>
        /// <param name="searchResults"></param>
        /// <returns></returns>
        public override int Search(string fileNamePath, string searchStr, out IList<int> searchResults)
        {
            try
            {
                CheckFile(fileNamePath);
                CheckSearchString(searchStr);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exiting. Invalid Parameters!.\r\n {0}", e.Message);
                throw e;
            }

            // Validate the file is still there
            if (!File.Exists(fileNamePath))
            {
                throw new FileNotFoundException(fileNamePath);
            }

            try
            {
                PdfReader pdfReader = new PdfReader(fileNamePath);
                ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();

                for (int page = 1; page < pdfReader.NumberOfPages; page++)
                {
                    string currentPageText = PdfTextExtractor.GetTextFromPage(pdfReader, 5, strategy);
                    if (currentPageText.Contains(searchStr))
                    {
                        this.AddFound(5);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            searchResults = this.GetRows;
            return searchResults.Count;
        }

        /// <summary>
        /// create pdf file
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="filePath"></param>
        /// <param name="fileContent"></param>
        public void WriteToNewPDf(string fileName, string filePath, string fileContent)
        {
            // Create an iTextSharp document object
            Document doc = new Document(PageSize.A4);
            
            if (String.IsNullOrWhiteSpace(filePath))
            {
                filePath = Environment.CurrentDirectory;
                Console.WriteLine("Invalid Path Provide. Useing Current Directory Instead:\r\n{0}", filePath);
            }

            // Get Instance to a PdfWriter Object
            string qualifiedFileName = System.IO.Path.Combine(filePath, fileName);
            PdfWriter.GetInstance(doc, new FileStream(qualifiedFileName, FileMode.Create));

            // Open the document for writing
            doc.Open();

            // Add paragraph to the document
            doc.Add(new Paragraph(fileContent));

            // Close the document
            doc.Close();
        }

        /// <summary>
        /// Add content to an existing PDF document
        /// </summary>
        /// <param name="fileName"></param>
        //public void AppendToExistingPdf(string fileName, string filePath, string content)
        //{
        //    //variables
        //    String pathin = "d:/iTextAction.pdf";
        //    String pathout = "d:/pdfcontentadded.pdf";

        //    //create a document object
        //    //var doc = new Document(PageSize.A4);

        //    //create PdfReader object to read from the existing document
        //    PdfReader reader = new PdfReader(pathin);

        //    //select two pages from the original document
        //    reader.SelectPages("1-2");

        //    //create PdfStamper object to write to get the pages from reader 
        //    PdfStamper stamper = new PdfStamper(reader, new FileStream(pathout, FileMode.Create));

        //    // PdfContentByte from stamper to add content to the pages over the original content
        //    PdfContentByte pbover = stamper.GetOverContent(1);

        //    //add content to the page using ColumnText
        //    ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase("Hello World"), 100, 400, 0);

        //    // PdfContentByte from stamper to add content to the pages under the original content
        //    PdfContentByte pbunder = stamper.GetUnderContent(1);

        //    //add image from a file 
        //    iTextSharp.text.Image img = new Jpeg(imageToByteArray(System.Drawing.Image.FromFile("d://baby.jpg")));

        //    //add the image under the original content
        //    pbunder.AddImage(img, img.Width / 2, 0, 0, img.Height / 2, 0, 0);

        //    //close the stamper
        //    stamper.Close();
        //}
    }
}
