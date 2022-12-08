

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageAdjuster
{
    public partial class Form1 : Form
    {
        private List<Image> images = new List<Image>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            // ドロップされたデータが画像かどうか判定
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // ドロップを受け入れる
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                // ドロップを受け入れない
                e.Effect = DragDropEffects.None;
            }

        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            // ドロップされたデータを取得
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            // ドロップされた画像をリストに追加
            foreach (string file in files)
            {
                var img = Image.FromFile(file);
                img.RotateFlip(RotateFlipType.RotateNoneFlipX);
                img.Save(file);
            }

        }
    }
}