using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

namespace NASP_Labos1
{
    public partial class Form1 : Form
    {
        public AVL tree;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tree = new AVL();
            tree.root = null;
        }

        private void browse_Click(object sender, EventArgs e)
        {
            tree = new AVL();
            OpenFileDialog file = new OpenFileDialog();
            file.CheckFileExists = true;
            if (file.ShowDialog() == DialogResult.OK)
            {
                StreamReader reader = new StreamReader(file.FileName);
                string content = reader.ReadToEnd();
                List<int> numbersList = new List<int>();
                string[] numbers = content.Split(' ');
                for (int i = 0; i < numbers.Length; i++)
                {
                    int num;
                    if (int.TryParse(numbers[i], out num) && num >= 0)
                        numbersList.Add(Convert.ToInt32(num));
                }
                tree.root = null;
                foreach (int i in numbersList)
                {
                    tree.root = tree.insert(i, tree.root);
                }
                reader.Close();
            }
            printTree(tree.root);
        }

        private void printTree(AVL.Node root)
        {
            textBox1.Text = "";
            printTree(tree.root, 0);
        }

        private void printTree(AVL.Node root, int space)
        {
            if (root == null)
            {
                //textBox1.Text += "\r\n";
                return;
            }
            else
            {
                printTree(root.Left, space + 1);
                textBox1.Text += new string('\t', space) + root.value + "\r\n";
                printTree(root.Right, space + 1);
            }
        }

        private bool TryGetNumberAndClear(TextBox textBox, out int number)
        {
            number = 0;

            if (string.IsNullOrWhiteSpace(textBox.Text)) return false;

            if (!int.TryParse(textBox.Text, out number))
                return false;

            textBox.Text = "";

            return true;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            int number;
            if (TryGetNumberAndClear(addTextbox, out number))
            {
                tree.root = tree.insert(number, tree.root);
                printTree(tree.root);
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            int number;
            if (TryGetNumberAndClear(delTextBox, out number))
            {
                tree.root = tree.delete(number, ref tree.root);
                printTree(tree.root);
            }
        }
    }
}
