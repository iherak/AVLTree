using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
            //tree = new AVL();
            //tree.root = null;
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
            printTree(tree.root, 10);
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
                for (int i = 0; i < space; i++)
                    textBox1.Text += "          ";
                textBox1.Text += root.value.ToString()+"\r\n";
            }
            printTree(root.Right, space+2);
            printTree(root.Left, space-2);
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (addTextbox.Text.Length != 0)
            {
                tree.root = tree.insert(Convert.ToInt32(addTextbox.Text), tree.root);
                textBox1.Text = "";
                printTree(tree.root, 10);
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (delTextBox.Text.Length != 0)
            {
                tree.root = tree.delete(Convert.ToInt32(delTextBox.Text), ref tree.root);
                textBox1.Text = "";
                printTree(tree.root, 10);
            }
        }
    }
}
