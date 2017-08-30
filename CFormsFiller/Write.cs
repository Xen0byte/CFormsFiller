using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Linq;

namespace CFormsFiller
{
    public static class Write
    {
        public static void WriteXML()
        {
            File.WriteAllText(StaticValues.bufferPath, "");
            StaticValues.counterSourceLines = 0;
            Stream writeStream = null;
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFileDialog.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((writeStream = openFileDialog.OpenFile()) != null)
                    {
                        using (writeStream)
                        {
                            StaticValues.filePath = openFileDialog.FileName;
                            StaticValues.bufferText = File.ReadAllText(StaticValues.filePath);

                            RefactorXML(StaticValues.filePath);

                            File.WriteAllText(StaticValues.bufferPath, StaticValues.bufferText);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: Could not read file from disk. " + ex.Message);
                }
            }
        }

        public static void RefactorXML(string file)
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
                            ReplaceCode(line, StaticValues.pattern);
                        }
                        else if (line.Contains("relationship="))
                        {
                            StaticValues.pattern = "^\\s*<(\\w*)\\s*label=\"(.*?)\".*relationship=\"(\\d*?)\".*$";
                            ReplaceCode(line, StaticValues.pattern);
                        }
                    }
                    else if (line.Contains("<item"))
                    {
                        StaticValues.pattern = "^\\s*<(\\w*)\\s*code=\"(\\d*?)\">(.*?)<.*$";
                        ReplaceCode(line, StaticValues.pattern);
                    }
                }

                ReplaceStuff(line, StaticValues.replaceRegexGroup);
            }

            Console.WriteLine();
            if (StaticValues.counterTotal == StaticValues.counterSuccessful)
            {
                Console.WriteLine("The write operation has completed successfully!");
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

        public static void ReplaceCode(string source, string regex)
        {
            XDocument xDoc = XDocument.Load(StaticValues.persistentXML, LoadOptions.SetLineInfo);
            MatchCollection matches = Regex.Matches(source, regex);

            foreach (Match match in matches)
            {
                if (!source.Contains("<item"))
                {
                    StaticValues.pType = match.Groups[1].Value;
                    StaticValues.pLabel = match.Groups[2].Value;
                    StaticValues.pCode = match.Groups[3].Value;

                    try
                    {
                        StaticValues.replaceRegexGroup = Regex.Replace(source, match.Groups[3].Value, xDoc.Descendants().Where(x => (string)x.Attribute("label") == StaticValues.pLabel).First().Attribute("unique").Value);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("SKIPPED: " + FirstCharToUpper(StaticValues.pType) + " '" + StaticValues.pLabel + "' at line " + StaticValues.counterSourceLines + " of the source file does not exist in the dataset.");
                        Console.WriteLine(ex.Message);
                    }
                }
                else if (source.Contains("<item"))
                {
                    StaticValues.cType = match.Groups[1].Value;
                    StaticValues.cCode = match.Groups[2].Value;
                    StaticValues.cLabel = match.Groups[3].Value;

                    try
                    {
                        StaticValues.replaceRegexGroup = Regex.Replace(source, match.Groups[2].Value, xDoc.Descendants().Where(x => (string)x.Attribute("label") == StaticValues.cLabel).First().Attribute("unique").Value);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("SKIPPED: " + FirstCharToUpper(StaticValues.cType) + " '" + StaticValues.cLabel + "' at line " + StaticValues.counterSourceLines + " of the source file does not exist in the dataset.");
                        Console.WriteLine(ex.Message);
                    }
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

        public static void ReplaceStuff(string oldLine, string newLine)
        {
            StaticValues.bufferText = StaticValues.bufferText.Replace(oldLine, newLine);
        }
    }
}