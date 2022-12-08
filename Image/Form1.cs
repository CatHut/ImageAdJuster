

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
            // �h���b�v���ꂽ�f�[�^���摜���ǂ�������
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // �h���b�v���󂯓����
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                // �h���b�v���󂯓���Ȃ�
                e.Effect = DragDropEffects.None;
            }

        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            // �h���b�v���ꂽ�f�[�^���擾
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            // �h���b�v���ꂽ�摜�����X�g�ɒǉ�
            foreach (string file in files)
            {
                var img = Image.FromFile(file);
                img.RotateFlip(RotateFlipType.RotateNoneFlipX);
                img.Save(file);
            }

        }
    }
}