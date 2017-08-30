using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Linq;

namespace CFormsFiller
{
    public static class Read
    {
        public static void ReadXML()
        {
            StaticValues.counterSourceLines = 0;
            Stream readStream = null;
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFileDialog.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((readStream = openFileDialog.OpenFile()) != null)
                    {
                        using (readStream)
                        {
                            StaticValues.filePath = openFileDialog.FileName;
                            ProcessXML(StaticValues.filePath);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: Could not read file from disk. " + ex.Message);
                }
            }
        }

        public static void ProcessXML(string file)
        {
            Cursor.Current = Cursors.WaitCursor;

            StaticValues.counterTotal = 0;
            StaticValues.counterSuccessful = 0;

            foreach (var line in File.ReadLines(file))
            {
                StaticValues.counterSourceLines++;

                if (line.Contains("code=") || line.Contains("relationship="))
                {
                    StaticValues.counterTotal++;

                    if (!line.Contains("<item"))
                    {
                        if (line.Contains("code="))
                        {
                            StaticValues.pattern = "^\\s*<(\\w*)\\s*label=\"(.*?)\".*code=\"(\\d*?)\".*$";
                            RegexToXML(line, StaticValues.pattern);
                        }
                        else if (line.Contains("relationship="))
                        {
                            StaticValues.pattern = "^\\s*<(\\w*)\\s*label=\"(.*?)\".*relationship=\"(\\d*?)\".*$";
                            RegexToXML(line, StaticValues.pattern);
                        }
                    }
                    else if (line.Contains("<item"))
                    {
                        StaticValues.pattern = "^\\s*<(\\w*)\\s*code=\"(\\d*?)\">(.*?)<.*$";
                        RegexToXML(line, StaticValues.pattern);
                    }
                }
            }

            Console.WriteLine();
            if (StaticValues.counterTotal == StaticValues.counterSuccessful)
            {
                Console.WriteLine("The read operation has completed successfully!");
                Console.WriteLine(StaticValues.counterSuccessful + " items out of " + StaticValues.counterTotal + " have been processed.");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine(StaticValues.counterSuccessful + " items out of " + StaticValues.counterTotal + " have been processed.");
                Console.WriteLine();
            }

            Cursor.Current = Cursors.Default;
        }

        public static void RegexToXML(string source, string pattern)
        {
            XDocument xDoc = XDocument.Load(StaticValues.persistentXML, LoadOptions.SetLineInfo);
            MatchCollection matches = Regex.Matches(source, pattern);
            int lineCounter = 0;
            int lineFound = 0;

            foreach (Match match in matches)
            {
                if (!source.Contains("<item"))
                {
                    StaticValues.parentExists = false;

                    StaticValues.pType = match.Groups[1].Value;
                    StaticValues.pLabel = match.Groups[2].Value;
                    StaticValues.pCode = match.Groups[3].Value;

                    foreach (var line in File.ReadAllLines(StaticValues.persistentXML))
                    {
                        lineCounter++;
                        if (line.Contains("type=\"" + StaticValues.pType + "\"") && line.Contains("label=\"" + StaticValues.pLabel + "\""))
                        {
                            StaticValues.exists = true;
                            StaticValues.parentExists = true;
                            lineFound = lineCounter;
                        }
                    }

                    if (StaticValues.exists == false)
                    {
                        StaticValues.parentExists = false;
                        StaticValues.lastParent = new XElement("U-" + StaticValues.firstAvailableCode.ToString(), new XAttribute("type", StaticValues.pType), new XAttribute("label", StaticValues.pLabel), new XAttribute("legacy", StaticValues.pCode), new XAttribute("unique", StaticValues.firstAvailableCode));
                        xDoc.Element(XName.Get("collection")).Add(StaticValues.lastParent);
                        xDoc.Save((StaticValues.persistentXML));
                        StaticValues.lastUsedCode++;
                        StaticValues.firstAvailableCode++;
                        File.WriteAllText(Environment.CurrentDirectory + "\\PersistentCounter.txt", StaticValues.lastUsedCode.ToString());
                    }
                    else if (StaticValues.exists == true)
                        Console.WriteLine("SKIPPED: " + FirstCharToUpper(StaticValues.pType) + " '" + StaticValues.pLabel + "' at line " + StaticValues.counterSourceLines + " of the source file already exists at line " + lineFound + " of the destination file.");

                    StaticValues.exists = false;
                    lineCounter = 0;
                    lineFound = 0;
                }
                else if (source.Contains("<item") && (StaticValues.parentExists == false))
                {
                    StaticValues.cType = match.Groups[1].Value;
                    StaticValues.cCode = match.Groups[2].Value;
                    StaticValues.cLabel = match.Groups[3].Value;

                    foreach (var line in File.ReadAllLines(StaticValues.persistentXML))
                    {
                        lineCounter++;

                        if (line.Contains("label=\"" + StaticValues.cLabel + "\""))
                            lineFound = lineCounter;
                    }

                    if (StaticValues.parentExists == false)
                    {
                        StaticValues.lastChild = new XElement("U-" + StaticValues.firstAvailableCode.ToString(), new XAttribute("type", StaticValues.cType), new XAttribute("label", StaticValues.cLabel), new XAttribute("legacy", StaticValues.cCode), new XAttribute("unique", StaticValues.firstAvailableCode));
                        xDoc.Element(XName.Get("collection")).Element(XName.Get(StaticValues.lastParent.Name.ToString())).Add(StaticValues.lastChild);
                        xDoc.Save((StaticValues.persistentXML));
                        StaticValues.lastUsedCode++;
                        StaticValues.firstAvailableCode++;
                        File.WriteAllText(Environment.CurrentDirectory + "\\PersistentCounter.txt", StaticValues.lastUsedCode.ToString());
                    }

                    lineCounter = 0;
                    lineFound = 0;
                }
                else if (source.Contains("<item") && (StaticValues.parentExists == true))
                {
                    StaticValues.cType = match.Groups[1].Value;
                    StaticValues.cCode = match.Groups[2].Value;
                    StaticValues.cLabel = match.Groups[3].Value;

                    foreach (var line in File.ReadAllLines(StaticValues.persistentXML))
                    {
                        lineCounter++;

                        if (line.Contains("label=\"" + StaticValues.cLabel + "\""))
                            lineFound = lineCounter;
                    }

                    Console.WriteLine("SKIPPED: " + FirstCharToUpper(StaticValues.cType) + " '" + StaticValues.cLabel + "' at line " + StaticValues.counterSourceLines + " of the source file is already an item of " + StaticValues.pLabel + ".");

                    lineCounter = 0;
                    lineFound = 0;
                }

                StaticValues.counterSuccessful++;
            }
        }
        public static string FirstCharToUpper(string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException("ARGH!");
            return input.First().ToString().ToUpper() + string.Join("", input.Skip(1));
        }
    }
}