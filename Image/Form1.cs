

using Accessibility;
using CatHut;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;
using Image = System.Drawing.Image;

namespace ImageAdjuster
{


    public partial class Form1 : Form
    {

        enum EXEC_TYPE
        {
            MARGIN_ADJUST = 0,
            FLIP_HORIZONTAL,
            FLIP_VIRTICAL,
            TOP_ALIGN,
            BOTTOM_ALIGN,
            LEFT_ALIGN,
            RIGHT_ALIGN,
            NONE        //�����Ȃ�
        }

        enum DIRECTION
        {
            UP,
            DOWN,
            LEFT,
            RIGHT
        }


        private List<Image> images = new List<Image>();

        private Image m_SampleBefore;
        private Image m_SampleAfter;

        private int[] m_ExecOrder;
        private CheckBox[] m_ExecCheckBoxArr;
        private Label[] m_ExecOrderLabelArr;
        private AppSetting m_APS;

        bool m_EventEnable = true;


        public Form1()
        {
            InitializeComponent();

            InitializeMember();

            m_APS = new AppSetting();

            InitializeSetting();

            UpdateUI();
        }


        private void InitializeSetting()
        {
            //���s����������
            m_ExecOrder = new int[Enum.GetNames(typeof(EXEC_TYPE)).Length];
            for (int i = 0; i < m_ExecOrder.Length; i++)
            {
                m_ExecOrder[i] = -1;
            }

            //�ۑ����ꂽ�l��ǂݎ��
            SetExecOrder(m_APS.Settings.m_ExecOrder);

        }

        private void InitializeMember()
        {
            m_ExecCheckBoxArr = new CheckBox[Enum.GetNames(typeof(EXEC_TYPE)).Length];
            m_ExecCheckBoxArr[(int)EXEC_TYPE.MARGIN_ADJUST] = checkBox_MarginAdjust;
            m_ExecCheckBoxArr[(int)EXEC_TYPE.FLIP_HORIZONTAL] = checkBox_FlipHorizontal;
            m_ExecCheckBoxArr[(int)EXEC_TYPE.FLIP_VIRTICAL] = checkBox_FlipVirtical;
            m_ExecCheckBoxArr[(int)EXEC_TYPE.TOP_ALIGN] = checkBox_AlignTop;
            m_ExecCheckBoxArr[(int)EXEC_TYPE.BOTTOM_ALIGN] = checkBox_AlignBottom;
            m_ExecCheckBoxArr[(int)EXEC_TYPE.LEFT_ALIGN] = checkBox_AlignLeft;
            m_ExecCheckBoxArr[(int)EXEC_TYPE.RIGHT_ALIGN] = checkBox_AlignRight;



            m_ExecOrderLabelArr = new Label[Enum.GetNames(typeof(EXEC_TYPE)).Length];
            m_ExecOrderLabelArr[(int)EXEC_TYPE.MARGIN_ADJUST] = label_MarginAlignOrder;
            m_ExecOrderLabelArr[(int)EXEC_TYPE.FLIP_HORIZONTAL] = label_FlipHorizontalOrder;
            m_ExecOrderLabelArr[(int)EXEC_TYPE.FLIP_VIRTICAL] = label_FlipVirticalOrder;
            m_ExecOrderLabelArr[(int)EXEC_TYPE.TOP_ALIGN] = label_AlignTopOrder;
            m_ExecOrderLabelArr[(int)EXEC_TYPE.BOTTOM_ALIGN] = label_AlignBottomOrder;
            m_ExecOrderLabelArr[(int)EXEC_TYPE.LEFT_ALIGN] = label_AlignLeftOrder;
            m_ExecOrderLabelArr[(int)EXEC_TYPE.RIGHT_ALIGN] = label_AlignRightOrder;
        }



        private void SetExecOrder(int[] ExecOrder)
        {
            if(ExecOrder == null) { return; }

            for(int i = 0; i < ExecOrder.Length; i++)
            {
                m_ExecOrder[i] = ExecOrder[i];
            }

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

            //�ݒ�󋵎擾









            //// �h���b�v���ꂽ�f�[�^���擾
            //string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            //// �h���b�v���ꂽ�摜�����X�g�ɒǉ�
            //foreach (string file in files)
            //{

            //    // �ړ������摜��ۑ�����
            //    bmp.Save(file, ImageFormat.Png);

            //}
        }

        private static Bitmap AlignBottom(Image img, int pix)
        {
            // �摜��ǂݍ���
            Bitmap bmp = new Bitmap(img);

            // �摜���̃C���X�g�̈ʒu�����o����
            int x = 0;
            int y = 0;
            bool found = false;
            for (int i = bmp.Height - 1; i >= 0; i--)
            {
                for (int j = 0; j < bmp.Width; j++)
                {
                    Color pixel = bmp.GetPixel(j, i);
                    if (pixel.A > 0)
                    {
                        // �C���X�g����������
                        x = j;
                        y = i;
                        found = true;
                        break;
                    }
                }

                if (found)
                {
                    break;
                }
            }

            // �摜���ړ�����
            using (Graphics graphics = Graphics.FromImage(bmp))
            {
                // ��ԉ��̈ʒu���摜�̉�����10�s�N�Z���ɂȂ�悤�Ɉړ�����
                int newX = x;
                int newY = y + pix;
                graphics.DrawImage(bmp, newX, newY);
            }

            return bmp;
        }

        private void textBox_AlignBottom_TextChanged(object sender, EventArgs e)
        {
            var input = ValueLimit(textBox_AlignBottom.Text, 0, 1000);
            textBox_AlignBottom.Text = input.ToString();
            UpdatePictureBox();

        }

        private void listView_FileList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                listView_FileList.BeginUpdate();
                // �I������Ă���A�C�e�������X�g����폜����
                if (listView_FileList.SelectedItems.Count > 0)
                {
                    foreach (ListViewItem item in listView_FileList.SelectedItems)
                    {
                        listView_FileList.Items.Remove(item);
                    }
                }


                if (listView_FileList.Items.Count == 0)
                {
                    var item = new string[] { "�����Ƀt�@�C�����h���b�O���h���b�v", "" };
                    listView_FileList.Items.Add(new ListViewItem(item));
                }
                listView_FileList.EndUpdate();

            }
        }

        private void listView_FileList_DragDrop(object sender, DragEventArgs e)
        {
            // �h���b�v���ꂽ�f�[�^���擾
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            listView_FileList.BeginUpdate();
            {
                var hashset = new HashSet<string>();
                foreach (ListViewItem item in listView_FileList.Items)
                {
                    if (item.SubItems.Count == 2)
                    {
                        if (File.Exists(item.SubItems[1].Text)){
                            hashset.Add(item.SubItems[1].Text);
                        }
                    }
                }

                foreach (var file in files)
                {
                    var ext = Path.GetExtension(file);
                    if (ext != ".jpg" && ext != ".png" && ext != ".bmp" && ext != ".gif")
                    {
                        continue;
                    }
                    hashset.Add(file);
                }


                listView_FileList.Items.Clear();

                var itemList = new List<ListViewItem>();
                foreach (var file in hashset)
                {

                    var item = new string[] { Path.GetFileName(file), file };
                    itemList.Add(new ListViewItem(item));
                }

                listView_FileList.Items.AddRange(itemList.ToArray());


                if (listView_FileList.Items.Count == 0)
                {
                    var item = new string[] { "�����Ƀt�@�C�����h���b�O���h���b�v", "" };
                    listView_FileList.Items.Add(new ListViewItem(item));
                }
            }
            listView_FileList.EndUpdate();

            if(listView_FileList.SelectedItems.Count == 0)
            {
                listView_FileList.Items[0].Selected = true;
            }

        }

        private void listView_FileList_DragEnter(object sender, DragEventArgs e)
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

        private void listView_FileList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePictureBox();
        }

        private void UpdatePictureBox()
        {
            if (listView_FileList.SelectedItems.Count != 0)
            {
                var item = listView_FileList.SelectedItems[0];
                var path = item.SubItems[1].Text;

                var img = Exec(path);

                pictureBox_Before.Image = AdjustImage(pictureBox_Before, Image.FromFile(path));
                pictureBox_After.Image = AdjustImage(pictureBox_After, img);

            }
        }


        private Image AdjustImage(PictureBox pb, Image img)
        {
            // PictureBox�̃T�C�Y���擾����
            int pictureBoxWidth = pb.Width;
            int pictureBoxHeight = pb.Height;

            // ���摜�̃T�C�Y���擾����
            int imageWidth = img.Width;
            int imageHeight = img.Height;

            // �c������v�Z����
            float aspectRatio = (float)imageWidth / (float)imageHeight;

            // ���摜��PictureBox��菬�����ꍇ�́A�g�債�Ȃ�
            if (imageWidth <= pictureBoxWidth && imageHeight <= pictureBoxHeight)
            {

                // �摜��Graphics�I�u�W�F�N�g���擾����
                Graphics g = Graphics.FromImage(img);

                // �g��`��
                g.DrawRectangle(Pens.Red, 0, 0, img.Width - 1, img.Height - 1);

                // �摜�����̂܂ܕ\������
                return img;
            }
            else
            {
                Image resizedImage;
                if (aspectRatio > 1.0f)
                {
                    // �������k������{�����v�Z����
                    float scale = (float)pictureBoxWidth / imageWidth;

                    // ���摜���k������
                    resizedImage = new Bitmap(img, new Size((int)(pictureBoxWidth), (int)(imageHeight * scale)));
                }
                else
                {
                    // �c�����k������{�����v�Z����
                    float scale = (float)pictureBoxHeight / imageHeight;

                    // ���摜���k������
                    resizedImage = new Bitmap(img, new Size((int)(imageWidth * scale), (int)(pictureBoxHeight)));

                }

                // �摜��Graphics�I�u�W�F�N�g���擾����
                Graphics g = Graphics.FromImage(resizedImage);

                // �g��`��
                var frameWidth = resizedImage.Width - 1;
                var frameHeight = resizedImage.Height - 1;

                frameWidth -= Math.Max(0, 2 - (pictureBoxWidth - resizedImage.Width));
                frameHeight -= Math.Max(0, 2 - (pictureBoxHeight - resizedImage.Height));

                g.DrawRectangle(Pens.Red, 0, 0, frameWidth, frameHeight);

                //// �k�������摜��\������
                return resizedImage;
            }
        }


        private void listView_FileList_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            //// �w�i�F��Ԃɂ���
            //e.Graphics.FillRectangle(Brushes.WhiteSmoke, e.Bounds);

            //// �e�L�X�g��`�悷��
            //e.Graphics.DrawString(e.Header.Text, e.Font, Brushes.Black, e.Bounds);
        }

        private void listView_FileList_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
            //listView_FileList.Columns[0].Width = 275;
            //listView_FileList.Columns[1].Width = 0;
        }


        [DllImport("user32")]
        public static extern int ShowScrollBar(IntPtr handle, int wBar, int bShow);
        private void RemoveHorizontalScrollBar(object sender, LayoutEventArgs e)
        {
            ShowScrollBar(listView_FileList.Handle, 0, 0); //SB_HORZ, false
        }

        private void listView_FileList_Layout(object sender, LayoutEventArgs e)
        {
            RemoveHorizontalScrollBar(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }


        private Image Exec(string file)
        {
            var orderedArray = GetExecOrderedArray();

            var img = Image.FromFile(file);

            foreach (var exeType in orderedArray)
            {
                switch (exeType)
                {
                    case EXEC_TYPE.MARGIN_ADJUST:
                        {
                            var pix = int.Parse(textBox_MarginAdjust.Text);
                            img = MarginRemove(img);
                            img = MarginAdd(img, pix);
                        }
                        break;


                    case EXEC_TYPE.FLIP_HORIZONTAL:
                        {
                            img.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        }
                        break;

                    case EXEC_TYPE.FLIP_VIRTICAL:
                        {
                            img.RotateFlip(RotateFlipType.RotateNoneFlipY);
                        }
                        break;


                    case EXEC_TYPE.BOTTOM_ALIGN:
                        {
                            var pix = int.Parse(textBox_AlignBottom.Text);

                            img = RemoveBottomMargin(img);
                            img = AddMargin(img, DIRECTION.DOWN, pix);

                        }
                        break;

                    case EXEC_TYPE.TOP_ALIGN:
                        {
                            var pix = int.Parse(textBox_AlignTop.Text);

                            img = RemoveTopMargin(img);
                            img = AddMargin(img, DIRECTION.UP, pix);

                        }
                        break;

                    case EXEC_TYPE.LEFT_ALIGN:
                        {
                            var pix = int.Parse(textBox_AlignLeft.Text);

                            img = RemoveLeftMargin(img);
                            img = AddMargin(img, DIRECTION.LEFT, pix);

                        }
                        break;

                    case EXEC_TYPE.RIGHT_ALIGN:
                        {
                            var pix = int.Parse(textBox_AlignRight.Text);

                            img = RemoveRightMargin(img);
                            img = AddMargin(img, DIRECTION.RIGHT, pix);

                        }
                        break;


                    default:
                        //�������Ȃ�
                        break;
                }
            }

            return img;
        }

        private EXEC_TYPE[] GetExecOrderedArray()
        {
            var ret = new EXEC_TYPE[m_ExecOrder.Length];

            for (int i = 0; i < m_ExecOrder.Length; i++)
            {
                ret[i] = EXEC_TYPE.NONE;
            }

            for (int i = 0; i < m_ExecOrder.Length; i++)
            {
                if(m_ExecOrder[i] < 0) { continue; }

                ret[m_ExecOrder[i]] = (EXEC_TYPE)i;
            }

            return ret;
        }

        private void checkBox_AlignBottom_CheckedChanged(object sender, EventArgs e)
        {
            if (m_EventEnable == false) { return; }
            CheckBoxCheckedChanged(EXEC_TYPE.BOTTOM_ALIGN);
            UpdatePictureBox();
        }

        private void checkBox_MarginAdjust_CheckedChanged(object sender, EventArgs e)
        {
            if (m_EventEnable == false) { return; }
            CheckBoxCheckedChanged(EXEC_TYPE.MARGIN_ADJUST);
            UpdatePictureBox();
        }

        private void checkBox_FlipVirtical_CheckedChanged(object sender, EventArgs e)
        {
            if (m_EventEnable == false) { return; }
            CheckBoxCheckedChanged(EXEC_TYPE.FLIP_VIRTICAL);
            UpdatePictureBox();
        }

        private void checkBox_FlipHorizontal_CheckedChanged(object sender, EventArgs e)
        {
            if (m_EventEnable == false) { return; }
            CheckBoxCheckedChanged(EXEC_TYPE.FLIP_HORIZONTAL);
            UpdatePictureBox();
        }

        private void CheckBoxCheckedChanged(EXEC_TYPE et)
        {
            var preValue = int.MaxValue;
            if (m_ExecCheckBoxArr[(int)et].Checked)
            {
                m_ExecOrder[(int)et] = GetOrder();
            }
            else
            {
                preValue = m_ExecOrder[(int)et];
                m_ExecOrder[(int)et] = -1;
            }

            //���Ԃ̒l�������ꂽ��O�l�߂���B
            for(int i = 0; i < m_ExecOrder.Length; i++)
            {
                if (m_ExecOrder[i] > preValue)
                {
                    m_ExecOrder[i]--;
                }
            }

            UpdateUI();
            m_APS.Settings.m_ExecOrder = m_ExecOrder;
            m_APS.SaveData();
        }

        private int GetOrder()
        {
            var ret = 0;
            foreach (var item in m_ExecOrder)
            { 
                if(item != -1) { ret++; }
            }
            return ret;
        }

        private void UpdateUI()
        {
            m_EventEnable = false;

            for (int i = 0; i < m_ExecOrder.Length; i++)
            {
                //���x���X�V
                if(m_ExecOrderLabelArr[i] == null) { continue; }

                m_ExecOrderLabelArr[i].Text = (m_ExecOrder[i] + 1).ToString();
                if (m_ExecOrder[i] == -1)
                {
                    m_ExecOrderLabelArr[i].Text = "";
                }

                //�`�F�b�N�{�b�N�X�X�V
                if (m_ExecCheckBoxArr[i] == null) { continue; }

                m_ExecCheckBoxArr[i].Checked = !(m_ExecOrder[i] == -1);

            }

            m_EventEnable = true;

        }

        private Image MarginAdd(Image img, int pix)
        {
            if (pix == 0) {
                return img;
            }
            else
            {
                return MarginAdd((Bitmap)img, pix);
            }
        }

        private Image MarginAdd(Bitmap img, int pix)
        {
            if(pix == 0) { return img; }

            // �V����Bitmap�I�u�W�F�N�g���쐬����
            Bitmap newImg = new Bitmap(img.Width + pix * 2, img.Height + pix * 2);

            // �����̉摜��V�����摜�ɃR�s�[����
            for (int x = 0; x < img.Width; x++)
            {
                for (int y = 0; y < img.Height; y++)
                {
                    // �����̉摜����s�N�Z�����擾����
                    Color pixel = img.GetPixel(x, y);

                    // �V�����摜�Ƀs�N�Z����ݒ肷��
                    newImg.SetPixel(x + pix, y + pix, pixel);
                }
            }

            return newImg;
        }

        private Image MarginRemove(Image img)
        {
            return MarginRemove((Bitmap)img);
        }


        private Image MarginRemove(Bitmap img)
        {
            // �]�������o���邽�߂̍ŏ��l
            int minX = img.Width;
            int minY = img.Height;
            int maxX = 0;
            int maxY = 0;

            // �摜���X�L��������
            for (int x = 0; x < img.Width; x++)
            {
                for (int y = 0; y < img.Height; y++)
                {
                    // �s�N�Z�����擾����
                    Color pixel = img.GetPixel(x, y);

                    // �����łȂ��s�N�Z���̏ꍇ�́A�]���̋��E���X�V����
                    if (pixel.A != 0)
                    {
                        if (x < minX) minX = x;
                        if (y < minY) minY = y;
                        if (x > maxX) maxX = x;
                        if (y > maxY) maxY = y;
                    }
                }
            }

            // �V����Bitmap�I�u�W�F�N�g���쐬����
            Bitmap newImg = new Bitmap(maxX - minX + 1, maxY - minY + 1);

            // �]�����폜�����摜���쐬����
            for (int x = minX; x <= maxX; x++)
            {
                for (int y = minY; y <= maxY; y++)
                {
                    // �s�N�Z�����擾����
                    Color pixel = img.GetPixel(x, y);

                    // �V�����摜�Ƀs�N�Z����ݒ肷��
                    newImg.SetPixel(x - minX, y - minY, pixel);
                }
            }

            return newImg;
        }


        private Image RemoveTopMargin(Bitmap img)
        {
            // �����ȗ]���̏�[��T��
            int top = 0;
            for (int y = 0; y < img.Height; y++)
            {
                bool isBlank = true;
                for (int x = 0; x < img.Width; x++)
                {
                    // �e�s�N�Z���̃A���t�@�l���擾
                    Color pixel = img.GetPixel(x, y);
                    if (pixel.A > 0)
                    {
                        // �A���t�@�l��0���傫���s�N�Z�������������ꍇ�A�����������ȗ]���̏�[
                        isBlank = false;
                        break;
                    }
                }
                if (!isBlank)
                {
                    top = y;
                    break;
                }
            }

            // �o�͉摜���쐬
            Bitmap outputImage = new Bitmap(img.Width, img.Height - top);

            CopyPixel(img, 0, top, img.Width, img.Height, ref outputImage, 0, 0);

            // �o�͉摜��ۑ�
            return outputImage;
        }

        private void CopyPixel(Bitmap src, int s_x, int s_y, int width, int height, ref Bitmap dest, int d_x, int d_y) 
        {
            // �o�͉摜�ɓ��͉摜���瓧���ȗ]�����������������R�s�[����
            // �����̉摜��V�����摜�ɃR�s�[����
            for (int x = s_x; x < width; x++)
            {
                for (int y = s_y; y < height; y++)
                {
                    // �����̉摜����s�N�Z�����擾����
                    Color pixel = src.GetPixel(x, y);

                    // �V�����摜�Ƀs�N�Z����ݒ肷��
                    dest.SetPixel(x + d_x - s_x, y + d_y - s_y, pixel);
                }
            }
        }


        private Image RemoveBottomMargin(Image img)
        {
            return RemoveBottomMargin((Bitmap)img);
        }

        private Image RemoveTopMargin(Image img)
        {
            return RemoveTopMargin((Bitmap)img);
        }

        private Image RemoveLeftMargin(Image img)
        {
            return RemoveLeftMargin((Bitmap)img);
        }

        private Image RemoveRightMargin(Image img)
        {
            return RemoveRightMargin((Bitmap)img);
        }

        private Image RemoveBottomMargin(Bitmap img)
        {
            // �����ȗ]���̉��[��T��
            int bottom = 0;
            for (int y = img.Height - 1; y >= 0; y--)
            {
                bool isBlank = true;
                for (int x = 0; x < img.Width; x++)
                {
                    // �e�s�N�Z���̃A���t�@�l���擾
                    Color pixel = img.GetPixel(x, y);
                    if (pixel.A > 0)
                    {
                        // �A���t�@�l��0���傫���s�N�Z�������������ꍇ�A�����������ȗ]���̉��[
                        isBlank = false;
                        break;
                    }
                }
                if (!isBlank)
                {
                    bottom = y;
                    break;
                }
            }

            // �o�͉摜���쐬
            Bitmap outputImage = new Bitmap(img.Width, bottom + 1);

            CopyPixel(img, 0, 0, img.Width, bottom, ref outputImage, 0, 0);

            // �o�͉摜��ۑ�
            return outputImage;
        }


        private Image RemoveLeftMargin(Bitmap img)
        {
            // �����ȗ]���̍��[��T��
            int left = 0;
            for (int x = 0; x < img.Width; x++)
            {
                bool isBlank = true;
                for (int y = 0; y < img.Height; y++)
                {
                    // �e�s�N�Z���̃A���t�@�l���擾
                    Color pixel = img.GetPixel(x, y);
                    if (pixel.A > 0)
                    {
                        // �A���t�@�l��0���傫���s�N�Z�������������ꍇ�A�����������ȗ]���̍��[
                        isBlank = false;
                        break;
                    }
                }
                if (!isBlank)
                {
                    left = x;
                    break;
                }
            }

            // �o�͉摜���쐬
            Bitmap outputImage = new Bitmap(img.Width - left, img.Height);

            CopyPixel(img, left, 0, img.Width, img.Height, ref outputImage, 0, 0);

            // �o�͉摜��ۑ�
            return outputImage;
        }


        private Image RemoveRightMargin(Bitmap img)
        {
            // �����ȗ]���̉E�[��T��
            int right = 0;
            for (int x = img.Width - 1; x >= 0; x--)
            {
                bool isBlank = true;
                for (int y = 0; y < img.Height; y++)
                {
                    // �e�s�N�Z���̃A���t�@�l���擾
                    Color pixel = img.GetPixel(x, y);
                    if (pixel.A > 0)
                    {
                        // �A���t�@�l��0���傫���s�N�Z�������������ꍇ�A�����������ȗ]���̉E�[
                        isBlank = false;
                        break;
                    }
                }
                if (!isBlank)
                {
                    right = x;
                    break;
                }
            }

            // �o�͉摜���쐬
            Bitmap outputImage = new Bitmap(right + 1, img.Height);

            CopyPixel(img, 0, 0, right, img.Height, ref outputImage, 0, 0);

            // �o�͉摜��ۑ�
            return outputImage;
        }

        private Image AddMargin(Image img, DIRECTION direction, int pix)
        {

            if(pix == 0) { return img; }

            // ���̉摜�̕��ƍ������擾����
            int width = img.Width;
            int height = img.Height;

            // �V�����摜�̑傫�����v�Z����
            int newWidth = width;
            int newHeight = height;
            if (direction == DIRECTION.UP || direction == DIRECTION.DOWN)
            {
                newHeight += pix;
            }
            else if (direction == DIRECTION.LEFT || direction == DIRECTION.RIGHT)
            {
                newWidth += pix;
            }

            // �V�����摜���쐬����
            Bitmap newImage = new Bitmap(newWidth, newHeight);


            // ���̉摜��V�����摜�ɏd�˂�
            int x = 0;
            int y = 0;
            if (direction == DIRECTION.UP)
            {
                y = pix;
            }
            else if (direction == DIRECTION.LEFT)
            {
                x = pix;
            }

            CopyPixel((Bitmap)img, 0, 0, img.Width, img.Height, ref newImage, x, y);


            // �ύX���ꂽ�摜��Ԃ�
            return newImage;
        }


        private void textBox_MarginAdjust_TextChanged(object sender, EventArgs e)
        {
            var input = ValueLimit(textBox_MarginAdjust.Text, 0, 1000);
            textBox_MarginAdjust.Text = input.ToString();
            UpdatePictureBox();
        }

        private void textBox_AlignTop_TextChanged(object sender, EventArgs e)
        {
            var input = ValueLimit(textBox_AlignTop.Text, 0, 1000);
            textBox_AlignTop.Text = input.ToString();
            UpdatePictureBox();

        }

        private void textBox_AlignLeft_TextChanged(object sender, EventArgs e)
        {
            var input = ValueLimit(textBox_AlignLeft.Text, 0, 1000);
            textBox_AlignLeft.Text = input.ToString();
            UpdatePictureBox();

        }

        private void textBox_AlignRight_TextChanged(object sender, EventArgs e)
        {
            var input = ValueLimit(textBox_AlignRight.Text, 0, 1000);
            textBox_AlignRight.Text = input.ToString();
            UpdatePictureBox();

        }

        private int ValueLimit(string txt, int lower, int upper)
        {
            var temp = 0.0;
            var ret = double.TryParse(txt, out temp);

            int input = (int)temp;

            if (ret == false)
            {
                input = lower;
            }

            input = Math.Max(lower, input);
            input = Math.Min(upper, input);

            return input;
        }

        private void checkBox_AlignTop_CheckedChanged(object sender, EventArgs e)
        {
            if (m_EventEnable == false) { return; }
            CheckBoxCheckedChanged(EXEC_TYPE.TOP_ALIGN);
            UpdatePictureBox();
        }

        private void checkBox_AlignLeft_CheckedChanged(object sender, EventArgs e)
        {
            if (m_EventEnable == false) { return; }
            CheckBoxCheckedChanged(EXEC_TYPE.LEFT_ALIGN);
            UpdatePictureBox();

        }

        private void checkBox_AlignRight_CheckedChanged(object sender, EventArgs e)
        {
            if (m_EventEnable == false) { return; }
            CheckBoxCheckedChanged(EXEC_TYPE.RIGHT_ALIGN);
            UpdatePictureBox();
        }

        private void button_ExecClear_Click(object sender, EventArgs e)
        {
            ClearExecCheckBox();
        }

        private void ClearExecCheckBox()
        {
            for(int i = 0; i < m_ExecOrder.Length; i++)
            {
                m_ExecOrder[i] = -1;
            }
            UpdateUI();
            UpdatePictureBox();
        }

    }
}