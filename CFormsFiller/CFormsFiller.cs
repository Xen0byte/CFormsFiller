using System;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;

namespace CFormsFiller
{
    public partial class CFormsFiller : Form
    {
        public CFormsFiller()
        {
            InitializeComponent();

            StaticValues.bufferPath = Environment.CurrentDirectory + "\\Buffer.txt";
            StaticValues.persistentXML = Environment.CurrentDirectory + "\\CustomCodes.xml";

            StaticValues.lastUsedCode = Convert.ToInt64(File.ReadAllText(Environment.CurrentDirectory + "\\PersistentCounter.txt"));
            StaticValues.firstAvailableCode = StaticValues.lastUsedCode + 1;

            textBox2.Text = StaticValues.firstAvailableCode.ToString().Substring(0, StaticValues.firstAvailableCode.ToString().Length - 4);
            label2.Text = "-----> The generation of 14 digit codes will start from " + StaticValues.firstAvailableCode.ToString() + ".";

            StaticValues.exists = false;
            StaticValues.parentExists = false;

            StaticValues.bufferText = null;
        }

        private void ConsoleBox(object sender, EventArgs e)
        {
            Console.SetOut(new ControlWriter(textBox4));
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox1.Text = "This tool generates sequential custom SNOMED codes for unique clinical statements.\r\nThe first " + textBox2.Text.Length.ToString() + " digits of the code are user-defined, and the last 4 digits are ascending from 0001 to 9999.";
            label2.Text = "-----> The generation of " + (Convert.ToInt32(textBox2.Text.Length.ToString()) + 4) + " digit codes will start from " + textBox2.Text + "0001.";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Read.ReadXML();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Write.WriteXML();
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text != StaticValues.firstAvailableCode.ToString().Substring(0, StaticValues.firstAvailableCode.ToString().Length - 4))
            {
                DialogResult dialogResult = MessageBox.Show("Are you absolutely sure you want to save this seed? Proceeding will overwrite the current counter and progress with the current seed will end. Click 'No' to go back.", "WARNING!", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    File.WriteAllText(Environment.CurrentDirectory + "\\PersistentCounter.txt", textBox2.Text + "0000");
                    StaticValues.lastUsedCode = Convert.ToInt64(File.ReadAllText(Environment.CurrentDirectory + "\\PersistentCounter.txt"));
                    StaticValues.firstAvailableCode = StaticValues.lastUsedCode + 1;
                }
                else if (dialogResult == DialogResult.No)
                {
                    textBox2.Text = StaticValues.firstAvailableCode.ToString().Substring(0, StaticValues.firstAvailableCode.ToString().Length - 4);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("This action will reset the persistent counter and the dataset of unique sequential custom SNOMED codes to the default values. Are you sure you want to proceed?", "WARNING!", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                File.WriteAllText(Environment.CurrentDirectory + "\\PersistentCounter.txt", "31231231230000");
                File.WriteAllText(Environment.CurrentDirectory + "\\CustomCodes.xml", "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<collection></collection>");

                StaticValues.lastUsedCode = Convert.ToInt64(File.ReadAllText(Environment.CurrentDirectory + "\\PersistentCounter.txt"));
                StaticValues.firstAvailableCode = StaticValues.lastUsedCode + 1;

                textBox2.Text = StaticValues.firstAvailableCode.ToString().Substring(0, StaticValues.firstAvailableCode.ToString().Length - 4);
                textBox1.Text = "This tool generates sequential custom SNOMED codes for unique clinical statements.\r\nThe first " + textBox2.Text.Length.ToString() + " digits of the code are user-defined, and the last 4 digits are ascending from 0001 to 9999.";
                label2.Text = "-----> The generation of " + (Convert.ToInt32(textBox2.Text.Length.ToString()) + 4) + " digit codes will start from " + textBox2.Text + "0001.";
            }
        }
    }

    public static class StaticValues
    {

        public static int counterTotal { get; set; }
        public static int counterSuccessful { get; set; }
        public static int counterSourceLines { get; set; }
        public static string filePath { get; set; }
        public static string bufferPath { get; set; }
        public static string bufferText { get; set; }
        public static string pType { get; set; }
        public static string pLabel { get; set; }
        public static string pCode { get; set; }
        public static string cType { get; set; }
        public static string cLabel { get; set; }
        public static string cCode { get; set; }
        public static string pattern { get; set; }
        public static string persistentXML { get; set; }
        public static string replaceRegexGroup { get; set; }
        public static bool exists { get; set; }
        public static bool parentExists { get; set; }
        public static long lastUsedCode { get; set; }
        public static long firstAvailableCode { get; set; }
        public static XElement lastParent { get; set; }
        public static XElement lastChild { get; set; }
    }
}