using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using MelissaData;

namespace MelissaNameObjectWindowsDotnet
{
  /// <summary>
  /// Name Object automates the handling of name data, making it simple to send
  /// personalized business mail, tailored specifically to the gender of the people in
  /// your mailing list, while screening out vulgar or obviously false names.
  /// </summary>
  /// <remarks>
  /// High-level flow of this sample:
  ///   1. SETUP     - create an mdName instance, hand it the license string and the
  ///                  path to the data files, then InitializeDataFiles() (one time).
  ///   2. INPUT     - feed a full name in with SetFullName().
  ///   3. PROCESS   - Parse() splits the name; Genderize() and Salutate() derive the
  ///                  gender and salutation from the parsed result.
  ///   4. READ      - pull the individual fields back out with the Get* getters
  ///                  (GetFirstName, GetLastName, GetGender, GetSalutation, ...).
  ///   5. INTERPRET - GetResults() returns comma-separated result codes describing
  ///                  what the object did/found; each code has a human description.
  ///
  /// The pieces in this file map onto that flow:
  ///   - Program        : console harness (argument parsing + the interactive loop).
  ///   - NameObject     : thin wrapper around mdName that owns setup + the call sequence.
  ///   - DataContainer  : plain holder for one record's input and output.
  ///
  /// Where mdName comes from:
  ///   The MelissaData namespace and its mdName class live in mdName_cSharpCode.cs,
  ///   a generated C# wrapper over mdName.dll that the accompanying
  ///   MelissaNameObjectWindowsDotnet.ps1 script downloads on every run.
  ///
  /// Reference:
  ///   Quickstart    : https://docs.melissa.com/on-premise-api/name-object/name-object-quickstart.html
  ///   Release notes : https://releasenotes.melissa.com/on-premise-api/name-object/
  ///   Result codes  : https://docs.melissa.com/on-premise-api/name-object/result-codes.html
  /// </remarks>
  class Program
  {
    /// <summary>
    /// Entry point. Reads the optional command-line arguments, then hands control to
    /// RunAsConsole, which performs the actual Name Object setup and processing.
    /// </summary>
    /// <param name="args">The raw command-line arguments</param>
    static void Main(string[] args)
    {
      // Populated by ParseArguments below.
      string license = "";
      string testName = "";
      string dataPath = "";

      ParseArguments(ref license, ref testName, ref dataPath, args);
      RunAsConsole(license, testName, dataPath);
    }

    /// <summary>
    /// Reads the supported command-line options into the ref parameters.
    ///
    /// Recognized flags (each followed by its value, e.g. "--name Ray Melissa"):
    ///   --license / -l   : the Melissa license string
    ///   --dataPath / -d  : path to the Name Object data files
    ///   --name / -n      : a name to test in one-shot mode
    /// </summary>
    /// <param name="license">Receives the Melissa license string.</param>
    /// <param name="testName">Receives the name to test in one-shot mode.</param>
    /// <param name="dataPath">Receives the path to the Name Object data files.</param>
    /// <param name="args">The raw command-line arguments to parse.</param>
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

    /// <summary>
    /// Sets up the Name Object once, then drives the input -> process -> output cycle.
    ///
    /// In interactive mode (no --name) it loops, asking for a new name each pass until
    /// the user answers "N". In one-shot mode (--name supplied) it runs a single pass
    /// on testName and exits.
    /// </summary>
    /// <param name="license">The Melissa license string used to initialize the object.</param>
    /// <param name="testName">A name to process in one-shot mode; if empty, the program prompts interactively.</param>
    /// <param name="dataPath">Path to the Name Object data files.</param>
    static void RunAsConsole(string license, string testName, string dataPath)
    {
      Console.WriteLine("\n\n=========== WELCOME TO MELISSA NAME OBJECT WINDOWS DOTNET ==========\n");

      // Construct the wrapper. This is where the object is licensed, pointed at the
      // data files, and initialized (see the NameObject constructor below).
      NameObject nameObject = new NameObject(license, dataPath);

      bool shouldContinueRunning = true;

      // Gate the program on a successful initialization. If the data files could not
      // be loaded (bad/expired license, missing or wrong-path data files, ...),
      // GetInitializeErrorString() returns the reason instead of "No Error" and we
      // skip the processing loop entirely.
      if (nameObject.mdNameObj.GetInitializeErrorString() != "No Error")
      {
        shouldContinueRunning = false;
      }

      while (shouldContinueRunning)
      {
        // Holder for this pass's input and result codes.
        DataContainer dataContainer = new DataContainer();

        if (string.IsNullOrEmpty(testName))
        {
          // Interactive mode: prompt the user for a name. 
          Console.WriteLine("\nFill in each value to see the Name Object results");
          Console.WriteLine("Name:");

          Console.CursorTop -= 1;
          Console.CursorLeft = 7;

          dataContainer.Name = Console.ReadLine();
        }
        else
        {
          // One-shot mode: use the name passed on the command line.
          dataContainer.Name = testName;
        }

        // Print user input
        Console.WriteLine("\n============================== INPUTS ==============================\n");
        Console.WriteLine($"\t                 Name: {dataContainer.Name}");

        // Execute Name Object
        // Runs the parse/genderize/salutate sequence and stores the result codes on dataContainer.
        nameObject.ExecuteObjectAndResultCodes(ref dataContainer);

        // Print output
        // Each Get* getter below returns one component the object produced for the most
        // recently processed name. These read directly from the mdName instance, which
        // still holds the results from the Execute call above. Prefix/First/Middle/Last/Suffix
        // come from Parse(), Gender from Genderize(), and Salutation from Salutate().
        Console.WriteLine("\n============================== OUTPUT ==============================\n");
        Console.WriteLine("\n\tName Object Information:");

        Console.WriteLine($"\t           Prefix: {nameObject.mdNameObj.GetPrefix()}");
        Console.WriteLine($"\t       First Name: {nameObject.mdNameObj.GetFirstName()}");
        Console.WriteLine($"\t      Middle Name: {nameObject.mdNameObj.GetMiddleName()}");
        Console.WriteLine($"\t        Last Name: {nameObject.mdNameObj.GetLastName()}");
        Console.WriteLine($"\t           Suffix: {nameObject.mdNameObj.GetSuffix()}");
        Console.WriteLine($"\t           Gender: {nameObject.mdNameObj.GetGender()}");
        Console.WriteLine($"\t       Salutation: {nameObject.mdNameObj.GetSalutation()}");
        Console.WriteLine($"\t     Result Codes: {dataContainer.ResultCodes}");

        // Result codes come back as a single comma-separated string (e.g. "NS01,NS02").
        // Split it and ask the object for a readable description of each code.
        // ResultCodeDescriptionLong requests the long-form text; a short form is also
        // available via ResultCodeDescriptionShort
        String[] rs = dataContainer.ResultCodes.Split(',');
        foreach (String r in rs)
          Console.WriteLine($"        {r}: {nameObject.mdNameObj.GetResultCodeDescription(r, mdName.ResultCdDescOpt.ResultCodeDescriptionLong)}");

        bool isValid = false;

        // In one-shot mode there is nothing more to do after a single pass: mark the
        // input handled and stop the outer loop.
        if (!string.IsNullOrEmpty(testName))
        {
          isValid = true;
          shouldContinueRunning = false;
        }

        // Interactive mode: ask whether to process another name. Keep prompting until
        // we get a valid Y/N. "N" ends the program; "Y" falls through to another pass.
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
      Console.WriteLine("\n============ THANK YOU FOR USING MELISSA DOTNET OBJECT ===========\n");
    }
  }

  /// <summary>
  /// Wrapper that owns a single Melissa Name Object instance and encapsulates the two
  /// things every Melissa object needs: one-time setup (license + data files) and the
  /// per-record processing sequence. Reuse one instance across many names; do NOT
  /// re-initialize per name.
  /// </summary>
  class NameObject
  {
    // Path to the Name Object data files.
    string dataFilePath;

    // The underlying Melissa Name Object instance.
    public mdName mdNameObj = new mdName();

    /// <summary>
    /// Performs the mandatory one-time setup, in this required order:
    ///   1. SetLicenseString    - authorize the object.
    ///   2. SetPathToNameFiles  - tell it where the data files live.
    ///   3. InitializeDataFiles - load the data into memory.
    /// </summary>
    /// <param name="license">The Melissa license string used to authorize the object.</param>
    /// <param name="dataPath">Path to the folder containing the Name Object data files.</param>
    public NameObject(string license, string dataPath)
    {
      // Set license string and set path to data files 
      mdNameObj.SetLicenseString(license);
      dataFilePath = dataPath;
      mdNameObj.SetPathToNameFiles(dataFilePath);

      // Load the data files. The returned ProgramStatus reports whether initialization succeeded.
      // If you see a different date than expected, check your license string and either download the new data files
      // or use the Melissa Updater program to update your data files.
      mdName.ProgramStatus pStatus = mdNameObj.InitializeDataFiles();

      // If an issue occurred, please investigate the common causes.
      // Common causes: an invalid/expired license, or missing/wrong-path data files.
      if (pStatus != mdName.ProgramStatus.NoError)
      {
        Console.WriteLine("Failed to Initialize Object.");
        Console.WriteLine(pStatus);
        return;
      }

      // Diagnostic information, handy for confirming the object loaded the data you expect:

      // Build date of the data files
      Console.WriteLine($"                DataBase Date: {mdNameObj.GetDatabaseDate()}");

      // When the license stops working
      Console.WriteLine($"              Expiration Date: {mdNameObj.GetLicenseExpirationDate()}");

      // This number should match with the file properties of the Melissa Object binary file.
      // If TEST appears with the build number, there may be a license key issue.
      Console.WriteLine($"               Object Version: {mdNameObj.GetBuildNumber()}\n");
    }

    /// <summary>
    /// Runs the full Name Object processing sequence for one name and captures its
    /// result codes. This is the canonical per-record call pattern to copy into your
    /// own application:
    ///   ClearProperties -> SetFullName -> Parse -> Genderize -> Salutate -> GetResults
    /// </summary>
    /// <param name="data">
    /// The record to process. Its Name is read as input, and ResultCodes is populated
    /// with this run's result codes.
    /// </param>
    public void ExecuteObjectAndResultCodes(ref DataContainer data)
    {
      // Reset any state left over from a previous name. Important when reusing the same
      // object across multiple records so fields from a prior name don't bleed into this one.
      mdNameObj.ClearProperties();

      // Supply the raw full-name string to process
      mdNameObj.SetFullName(data.Name);

      // Split it into prefix/first/middle/last/suffix
      mdNameObj.Parse();

      // Infer gender from the parsed first name
      mdNameObj.Genderize();

      // Build a salutation from the parsed components
      mdNameObj.Salutate();

      // Collect the result codes for this run
      // ResultsCodes explain any issues Name Object has with the object.
      // List of result codes for Name Object
      // https://docs.melissa.com/on-premise-api/name-object/result-codes.html
      data.ResultCodes = mdNameObj.GetResults();
    }
  }

  /// <summary>
  /// Data holder for a single record: carries the input name in and the result codes out.
  /// </summary>
  public class DataContainer
  {
    // Record identifier. Never set or read by this sample.
    public string RecID { get; set; }

    // Input: the full name to process.
    public string Name { get; set; }

    // Output: comma-separated result codes from GetResults().
    public string ResultCodes { get; set; } = "";
  }
}
