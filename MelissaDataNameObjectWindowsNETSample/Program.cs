using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using MelissaData;

namespace MelissaDataNameObjectWindowsNETSample
{
  class Program
  {
    static void Main(string[] args)
    {
      // Variables
      string license = "";
      string testName = "";
      string dataPath = "";

      ParseArguments(ref license, ref testName, ref dataPath, args);
      RunAsConsole(license, testName, dataPath);
    }

    static void ParseArguments(ref string license, ref string testName, ref string dataPath, string[] args)
    {
      for (int i = 0; i < args.Length; i++)
      {
        if (args[i].Equals("--license") || args[i].Equals("-l"))
        {
          if (args[i + 1] != null)
          {
            license = args[i + 1];
          }
        }
        if (args[i].Equals("--dataPath") || args[i].Equals("-d"))
        {
          if (args[i + 1] != null)
          {
            dataPath = args[i + 1];
          }
        }
        if (args[i].Equals("--name") || args[i].Equals("-n"))

        {
          if (args[i + 1] != null)
          {
            testName = args[i + 1];
          }
        }
      }
    }

    static void RunAsConsole(string license, string testName, string dataPath)
    {
      Console.WriteLine("\n\n===== WELCOME TO MELISSA DATA NAME OBJECT WINDOWS NET SAMPLE =====\n");

      NameObject nameObject = new NameObject(license, dataPath);

      bool shouldContinueRunning = true;

      if (nameObject.mdNameObj.GetInitializeErrorString() != "No Error")
      {
        shouldContinueRunning = false;
      }

      while (shouldContinueRunning)
      {
        DataContainer dataContainer = new DataContainer();

        if (string.IsNullOrEmpty(testName))
        {
          Console.WriteLine("\nFill in each value to see the Name Object results");
          Console.WriteLine("Name:");

          Console.CursorTop -= 1;
          Console.CursorLeft = 7;

          dataContainer.Name = Console.ReadLine();
        }
        else
        {
          dataContainer.Name = testName;
        }

        // Print user input
        Console.WriteLine("\n============================== INPUTS ==============================\n");
        Console.WriteLine($"\t               Name: {dataContainer.Name}");

        // Execute Name Object
        nameObject.ExecuteObjectAndResultCodes(ref dataContainer);

        // Print output
        Console.WriteLine("\n============================== OUTPUT ==============================\n");
        Console.WriteLine("\n\tName Object Information:");

        Console.WriteLine($"\t      Prefix: {nameObject.mdNameObj.GetPrefix()}");
        Console.WriteLine($"\t  First Name: {nameObject.mdNameObj.GetFirstName()}");
        Console.WriteLine($"\t Middle Name: {nameObject.mdNameObj.GetMiddleName()}");
        Console.WriteLine($"\t   Last Name: {nameObject.mdNameObj.GetLastName()}");
        Console.WriteLine($"\t      Suffix: {nameObject.mdNameObj.GetSuffix()}");
        Console.WriteLine($"\t      Gender: {nameObject.mdNameObj.GetGender()}");
        Console.WriteLine($"\t  Salutation: {nameObject.mdNameObj.GetSalutation()}");
        Console.WriteLine($"\tResult Codes: {dataContainer.ResultCodes}");

        String[] rs = dataContainer.ResultCodes.Split(',');
        foreach (String r in rs)
          Console.WriteLine($"        Note: {nameObject.mdNameObj.GetResultCodeDescription(r, mdName.ResultCdDescOpt.ResultCodeDescriptionLong)}");

        bool isValid = false;

        if (!string.IsNullOrEmpty(testName))
        {
          isValid = true;
          shouldContinueRunning = false;
        }

        while (!isValid)
        {
          Console.WriteLine("\nTest another name? (Y/N)");
          string testAnotherResponse = Console.ReadLine();

          if (!string.IsNullOrEmpty(testAnotherResponse))
          {
            testAnotherResponse = testAnotherResponse.ToLower();
            if (testAnotherResponse == "y")
            {
              isValid = true;
            }
            else if (testAnotherResponse == "n")
            {
              isValid = true;
              shouldContinueRunning = false;
            }
            else
            {
              Console.Write("Invalid Response, please respond 'Y' or 'N'");
            }
          }
        }
      }
      Console.WriteLine("\n============ THANK YOU FOR USING MELISSA DATA NET OBJECT ===========\n");
    }
  }


  class NameObject
  {
    // Path to name object data files (.dat, etc)
    string dataFilePath;

    // Create instance of Melissa Data Name Object
    public mdName mdNameObj = new mdName();

    public NameObject(string license, string dataPath)
    {
      // Set license string and set path to data files  (.dat, etc)
      mdNameObj.SetLicenseString(license);
      dataFilePath = dataPath;
      mdNameObj.SetPathToNameFiles(dataFilePath);

      // If you see a different date than expected, check your license string and either download the new data files or use the Melissa Updater program to update your data files.  
      mdName.ProgramStatus pStatus = mdNameObj.InitializeDataFiles();

      // If an issue occurred while initializing the data files, this will throw
      if (pStatus != mdName.ProgramStatus.NoError)
      {
        Console.WriteLine("Failed to Initialize Object.");
        Console.WriteLine(pStatus);
        return;
      }

      Console.WriteLine($"                DataBase Date: {mdNameObj.GetDatabaseDate()}");
      Console.WriteLine($"              Expiration Date: {mdNameObj.GetLicenseExpirationDate()}");

      /**
       * This number should match with the file properties of the Melissa Data Object binary file.
       * If TEST appears with the build number, there may be a license key issue.
       */
      Console.WriteLine($"               Object Version: {mdNameObj.GetBuildNumber()}\n");
    }

    //This will call the functions to process the input name as well as generate the result codes
    public void ExecuteObjectAndResultCodes(ref DataContainer data)
    {
      mdNameObj.ClearProperties();

      mdNameObj.SetFullName(data.Name);
      mdNameObj.Parse();
      mdNameObj.Genderize();
      mdNameObj.Salutate();
      data.ResultCodes = mdNameObj.GetResults();

      // ResultsCodes explain any issues name object has with the object.
      // List of result codes for Name object
      // https://wiki.melissadata.com/?title=Result_Code_Details#Name_Object
    }
  }

  public class DataContainer
  {
    public string RecID { get; set; }
    public string Name { get; set; }
    public string ResultCodes { get; set; } = "";
  }
}
