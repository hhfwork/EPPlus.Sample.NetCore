﻿/*************************************************************************************************
  Required Notice: Copyright (C) EPPlus Software AB. 
  This software is licensed under PolyForm Noncommercial License 1.0.0 
  and may only be used for noncommercial purposes 
  https://polyformproject.org/licenses/noncommercial/1.0.0/

  A commercial license to use this software can be purchased at https://epplussoftware.com
 *************************************************************************************************
  Date               Author                       Change
 *************************************************************************************************
  01/27/2020         EPPlus Software AB           Initial release EPPlus 5
 *************************************************************************************************/
using System;
using System.IO;
using System.Threading.Tasks;
using EPPlusSampleApp.Core;
using EPPlusSamples.Comments;
using EPPlusSamples.CreateFileSystemReport;
using EPPlusSamples.DataValidation;
using EPPlusSamples.EncryptionAndProtection;
using EPPlusSamples.FormulaCalculation;
using EPPlusSamples.FXReportFromDatabase;
using EPPlusSamples.LoadDataFromCsvFilesIntoTables;
using EPPlusSamples.LoadingData;
using EPPlusSamples.OpenWorkbookAddDataAndChart;
using EPPlusSamples.PerformanceAndProtection;
using EPPlusSamples.PivotTables;
using EPPlusSamples.SalesReport;
using EPPlusSamples.Sparklines;

namespace EPPlusSamples
{
	class Sample_Main
	{
		static async Task Main(string[] args)
		{
			try
			{
                //EPPlus 5 uses a dual licens model. This requires you to specifiy the License you are using to be able to use the library. 
                //This sample sets the LicenseContext in the appsettings.json file. An alternative is the commented row below.
                //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                //See https://epplussoftware.com/Developers/LicenseException for more info.

                string connectionStr = "Data Source=EPPlusSample.sqlite;Version=3;";

                //Set the output directory to the SampleApp folder where the app is running from. 
                FileUtil.OutputDir = new DirectoryInfo($"{AppDomain.CurrentDomain.BaseDirectory}SampleApp");

                // Sample 1 - Simply creates a new workbook from scratch
                // containing a worksheet that adds a few numbers together 
                Console.WriteLine("Running sample 1");
                string sample1Path = GettingStartedSample.Run();
                Console.WriteLine("Sample 1 created: {0}", sample1Path);
                Console.WriteLine();

                // Sample 2 - Simply reads some values from the file generated by sample 1
                // and outputs them to the console
                Console.WriteLine("Running sample 2");
                ReadWorkbookSample.Run();
                Console.WriteLine();

                //Sample 3 - Load and save using async methods
                Console.WriteLine("Running sample 3-Async-Await");
                await UsingAsyncAwaitSample.RunAsync(connectionStr);
                Console.WriteLine("Sample 3 created {0}", FileUtil.OutputDir.Name);
                Console.WriteLine();

                //Sample 4 - Shows a few ways to load data (Datatable, IEnumerable and more).
                Console.WriteLine("Running sample 4 - LoadingDataWithTables");
                LoadingDataWithTablesSample.Run();
                Console.WriteLine("Sample 4 (LoadingDataWithTables) created {0}", FileUtil.OutputDir.Name);
                Console.WriteLine();
                //Sample 4 - Shows how to load dynamic/ExpandoObject 
                LoadingDataWithDynamicObjects.Run();
                Console.WriteLine("Sample 4 (LoadingDataWithDynamicObjects) created {0}", FileUtil.OutputDir.Name);
                Console.WriteLine();
                // Sample 4 - LoadFromCollectionWithAttributes
                LoadingDataFromCollectionWithAttributes.Run();
                Console.WriteLine("Sample 4 (LoadingDataFromCollectionWithAttributes) created {0}", FileUtil.OutputDir.Name);
                Console.WriteLine();

                //Sample 5 Loads two csv files into tables and creates an area chart and a Column/Line chart on the data.
                //This sample also shows how to use a secondary axis.
                Console.WriteLine("Running sample 5");
                var output = await ImportAndExportCsvFilesSample.Run();
                Console.WriteLine("Sample 5 created: {0}", output);
                Console.WriteLine();

                //Sample 6 Calculate - Shows how to calculate formulas in the workbook.
                Console.WriteLine("Sample 6 - Calculate formulas");
                CalculateFormulasSample.Run();
                Console.WriteLine("Sample 6 created {0}", FileUtil.OutputDir.Name);
                Console.WriteLine();

                //Sample 7
                //Open sample 1 and add a pie chart.
                Console.WriteLine("Running sample 7 - Open a workbook and add data and a pie chart");
                output = OpenWorkbookAndAddDataAndChartSample.Run();
                Console.WriteLine("Sample 7 created:", output);
                Console.WriteLine();

                // Sample 8 - creates a workbook from scratch 
                //Shows how to use Ranges, Styling, Namedstyles and Hyperlinks
                Console.WriteLine("Running sample 8");
                output = SalesReportFromDatabase.Run(connectionStr);
                Console.WriteLine("Sample 8 created: {0}", output);
                Console.WriteLine();

                //Sample 9
                //This sample shows the performance capabilities of the component and shows sheet protection.
                //Load X(param 2) rows with five columns
                Console.WriteLine("Running sample 9");
                output = PerformanceAndProtectionSample.Run(65534);
                Console.WriteLine("Sample 9 created:", output);
                Console.WriteLine();

                //Sample 10 - Linq
                //Opens Sample 9 and perform some Linq queries
                Console.WriteLine("Running sample 10-Linq");
                ReadDataUsingLinq.Run();
                Console.WriteLine();

                //Sample 11 - Conditional Formatting
                Console.WriteLine("Running sample 11");
                ConditionalFormatting.Run();
                Console.WriteLine("Sample 11 created {0}", FileUtil.OutputDir.Name);
                Console.WriteLine();

                //Sample 12 - Data validation
                Console.WriteLine("Running sample 12");
                output = DataValidationSample.Run();
                Console.WriteLine("Sample 12 created {0}", output);
                Console.WriteLine();

                //Sample 13 - Filter
                Console.WriteLine("Running sample 13-Filter");
                await Filter.RunAsync(connectionStr);
                Console.WriteLine("Sample 13 created {0}", FileUtil.OutputDir.Name);
                Console.WriteLine();

                //Sample 14 - Shapes & Images
                Console.WriteLine("Running sample 14-Shapes & Images");
                ShapesAndImagesSample.Run();
                Console.WriteLine("Sample 14 created {0}", FileUtil.OutputDir.Name);
                Console.WriteLine();

                //Sample 15 - Themes and Chart styling
                Console.WriteLine("Running sample 15-Theme and Chart styling");
                //Run the sample with the default office theme
                await ChartsAndThemesSample.RunAsync(connectionStr,
                                                     FileUtil.GetFileInfo("15-ChartsAndThemes.xlsx"), null);

                //Run the sample with the integral theme. Themes can be exported as thmx files from Excel and can then be applied to a package.
                await ChartsAndThemesSample.RunAsync(connectionStr,
                                                     FileUtil.GetFileInfo("15-ChartsAndThemes-IntegralTheme.xlsx"),
                                                     FileUtil.GetFileInfo("15-ChartsAndThemes", "integral.thmx"));
                Console.WriteLine("Sample 15 created {0}", FileUtil.OutputDir.Name);
                Console.WriteLine();

                //Sample 16 - Shows how to add sparkline charts.
                Console.WriteLine("Running sample 16-Sparklines");
                SparkLinesSample.Run();
                Console.WriteLine("Sample 16 created {0}", FileUtil.OutputDir.Name);
                Console.WriteLine();

                // Sample 17 - Creates a workbook based on a template.
                // Populates a range with data and set the series of a linechart.
                Console.WriteLine("Running sample 17");
                output = FxReportFromDatabase.Run(connectionStr);
                Console.WriteLine("Sample 17 created: {0}", output);
                Console.WriteLine();

                //sample 18 - pivot tables
                Console.WriteLine("running sample 18");
                //This sample demonstrates how to create and work with pivot tables.
                output = PivotTablesSample.Run(connectionStr);
                //The second class demonstrates how to style you pivot table.
                PivotTablesStylingSample.Run();
                Console.WriteLine("sample 18 created {0}", output);
                Console.WriteLine();

                //Sample 19 Swedish Quiz : Shows Encryption, workbook- and worksheet protection.
                Console.WriteLine("Running sample 19");
                DrawingsSample.Run();
                Console.WriteLine("Sample 19 created: {0}", FileUtil.OutputDir.FullName);
                Console.WriteLine();

                //Sample 20
                //Creates an advanced report on a directory in the filesystem.
                //Parameter 2 is the directory to report. Parameter 3 is how deep the scan will go. Parameter 4 Skips Icons if set to true (The icon handling is slow)
                //This example demonstrates how to use outlines, tables,comments, shapes, pictures and charts.                
                Console.WriteLine("Running sample 20");
                output = CreateAFileSystemReport.Run(new DirectoryInfo(System.Reflection.Assembly.GetEntryAssembly().Location).Parent, 5, true);
                Console.WriteLine("Sample 20 created:", output);
                Console.WriteLine();

                //Sample 21 - Shows how to work with macro-enabled workbooks(VBA).
                Console.WriteLine("Running sample 21-VBA");
                WorkingWithVbaSample.Run();
                Console.WriteLine("Sample 21 created {0}", FileUtil.OutputDir.Name);
                Console.WriteLine();

                //Sample 22 - Ignore cell errors using the IngnoreErrors Collection
                Console.WriteLine("Running sample 22-Suppress Errors");
                IgnoreErrorsSample.Run();
                Console.WriteLine("Sample 22 created {0}", FileUtil.OutputDir.Name);
                Console.WriteLine();

                //Sample 23 - Comments and Threaded comments
                Console.WriteLine("Running sample 23-Comments/Notes and Threaded Comments");
                CommentsSample.Run();
                Console.WriteLine("Sample 23 created {0}", FileUtil.OutputDir.Name);
                Console.WriteLine();

                //Sample 24 - Table slicers and Pivot table slicers
                Console.WriteLine("Running sample 24-Table and Pivot Table Slicers");
                SlicerSample.Run(connectionStr);
                Console.WriteLine("Sample 24 created {0}", FileUtil.OutputDir.Name);
                Console.WriteLine();

                //Sample 25 - Import and Export DataTable
                Console.WriteLine("Running sample 25 - Import and Export DataTable");
                DataTableSample.Run(connectionStr);
                Console.WriteLine("Sample 25 finished.");
                Console.WriteLine();

                //Sample 26 - Form Controls & Drawing Groups
                Console.WriteLine("Running sample 26 - Form controls");
                FormControlsSample.Run();
                Console.WriteLine("Sample 26 finished.");
                Console.WriteLine();

                //Sample 27 - Custom Named Table, Pivot Table and Slicer styles
                Console.WriteLine("Running sample 27 - Custom table and slicer styles");
                CustomTableSlicerStyleSample.Run(connectionStr);
                Console.WriteLine("Sample 27 finished.");
                Console.WriteLine();

                //Sample 28 - Custom Named Table, Pivot Table and Slicer styles
                Console.WriteLine("Running sample 28 - Working with tables");
                await TablesSample.RunAsync(connectionStr);
                Console.WriteLine("Sorting tables sample...");
                await SortingTablesSample.RunAsync(connectionStr);
                Console.WriteLine("Sample 28 finished.");
                Console.WriteLine();

                //Sample 29 - Add references to external workbooks
                Console.WriteLine("Running sample 29 - External Links");
                ExternalLinksSample.Run();
                Console.WriteLine("Sample 29 finished.");
                Console.WriteLine();

                // Sample 30 - Working with ranges
                Console.WriteLine("Running sample 30 - Working with ranges");
                CopyRangeSample.Run();
                FillRangeSample.Run();
                SortingRangesSample.Run();
                Console.WriteLine("Sample 30 finished.");
                Console.WriteLine();

                // Sample 31 - Html Export
                Console.WriteLine("Running sample 31 - Html export");
                HtmlTableExportSample.Run();
                HtmlRangeExportSample.Run();
                Console.WriteLine("Sample 31 finished.");
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
			}
            var prevColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Genereted sample workbooks can be found in {FileUtil.OutputDir.FullName}");
            Console.ForegroundColor = prevColor;

            Console.WriteLine();
			Console.WriteLine("Press the return key to exit...");
			Console.ReadKey();
		}
	}
}
