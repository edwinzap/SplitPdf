using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

class Program
{
    static void Main(string[] args)
    {
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

        string? pdfFilePath;
        if(args.Length == 0) 
        {
            Console.Write("Enter the file path: ");
            pdfFilePath = Console.ReadLine();
        }
        else
        {
            // The first argument is the path to the PDF file
            pdfFilePath = args[0];
        }

        pdfFilePath = pdfFilePath.Replace("\"", "");
        // Create a new directory with the same name as the PDF file
        var outputFolder = Path.Combine(Path.GetDirectoryName(pdfFilePath), Path.GetFileNameWithoutExtension(pdfFilePath));
        Console.WriteLine("Output folder: " + outputFolder);
        Directory.CreateDirectory(outputFolder);

        PdfDocument pdfFile = PdfReader.Open(pdfFilePath, PdfDocumentOpenMode.Import);
        var totalPages = pdfFile.PageCount;

        Console.WriteLine("Total pages: " + totalPages);


        Console.Write("Would like to split pages by step? (y/n): ");
        bool shouldSplitEachPage = Console.ReadLine() == "y";

        var pageNumbersToSplitOn = new List<int>();
        if (shouldSplitEachPage)
        {
            Console.Write("Step: ");
            var step = int.Parse(Console.ReadLine());
            for (int i = step; i < totalPages; i += step)
            {
                pageNumbersToSplitOn.Add(i);
            }
        }
        else
        {
            // The second argument is a list of page numbers to split on
            string? pageNumbers;

            if (args.Length < 2)
            {
                Console.Write("\nPage numbers to split on:");
                pageNumbers = Console.ReadLine();
            }
            else
            {
                pageNumbers = args[1];
            }
            pageNumbersToSplitOn = GetPageNumbers(pageNumbers);
        }

        Console.WriteLine("Split on: " + string.Join(",", pageNumbersToSplitOn));

        int previousPageNumber = 0;
        pageNumbersToSplitOn.Add(totalPages);
        foreach (var pageNumber in pageNumbersToSplitOn)
        {
            if (pageNumber <= totalPages)
            {
                PdfDocument outputPDFDocument = new PdfDocument();
                var pagesToTake = Enumerable.Range(previousPageNumber + 1, pageNumber - previousPageNumber).ToList();
                foreach (var page in pagesToTake)
                {
                   outputPDFDocument.AddPage(pdfFile.Pages[page - 1]);
                }
                string fileName = $"{previousPageNumber} - {pageNumber}";
                SaveOutputPDF(outputPDFDocument, fileName, outputFolder);
            }
            previousPageNumber = pageNumber;
        }
        Console.WriteLine("Done!");
        Console.ReadLine();
    }

    private static List<int> GetPageNumbers(string numbers)
    {
        numbers.Replace(" ", "");
        return new List<int>(Array.ConvertAll(numbers.Split(","), int.Parse));
    }


    private static void SaveOutputPDF(PdfDocument outputPDFDocument, string fileName, string outputFolder)
    {
        string outputPDFFilePath = Path.Combine(outputFolder, fileName + ".pdf");
        outputPDFDocument.Save(outputPDFFilePath);
    }
}
