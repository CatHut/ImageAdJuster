

using Accessibility;
using CatHut;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

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
            //実行順序初期化
            m_ExecOrder = new int[Enum.GetNames(typeof(EXEC_TYPE)).Length];
            for (int i = 0; i < m_ExecOrder.Length; i++)
            {
                m_ExecOrder[i] = -1;
            }

            //保存された値を読み取り
            SetExecOrder(m_APS.Settings.m_ExecOrder);

        }

        private void InitializeMember()
        {
            m_ExecCheckBoxArr = new CheckBox[Enum.GetNames(typeof(EXEC_TYPE)).Length];
            m_ExecCheckBoxArr[(int)EXEC_TYPE.MARGIN_ADJUST] = checkBox_MarginAdjust;
            m_ExecCheckBoxArr[(int)EXEC_TYPE.FLIP_HORIZONTAL] = checkBox_FlipHorizontal;
            m_ExecCheckBoxArr[(int)EXEC_TYPE.FLIP_VIRTICAL] = checkBox_FlipVirtical;
            m_ExecCheckBoxArr[(int)EXEC_TYPE.TOP_ALIGN] = null;
            m_ExecCheckBoxArr[(int)EXEC_TYPE.BOTTOM_ALIGN] = checkBox_AlignBottom;
            m_ExecCheckBoxArr[(int)EXEC_TYPE.LEFT_ALIGN] = null;
            m_ExecCheckBoxArr[(int)EXEC_TYPE.RIGHT_ALIGN] = null;



            m_ExecOrderLabelArr = new Label[Enum.GetNames(typeof(EXEC_TYPE)).Length];
            m_ExecOrderLabelArr[(int)EXEC_TYPE.MARGIN_ADJUST] = label_MarginAlignOrder;
            m_ExecOrderLabelArr[(int)EXEC_TYPE.FLIP_HORIZONTAL] = label_FlipHorizontalOrder;
            m_ExecOrderLabelArr[(int)EXEC_TYPE.FLIP_VIRTICAL] = label_FlipVirticalOrder;
            m_ExecOrderLabelArr[(int)EXEC_TYPE.TOP_ALIGN] = null;
            m_ExecOrderLabelArr[(int)EXEC_TYPE.BOTTOM_ALIGN] = label_AlignBottomOrder;
            m_ExecOrderLabelArr[(int)EXEC_TYPE.LEFT_ALIGN] = null;
            m_ExecOrderLabelArr[(int)EXEC_TYPE.RIGHT_ALIGN] = null;
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

            //設定状況取得









            //// ドロップされたデータを取得
            //string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            //// ドロップされた画像をリストに追加
            //foreach (string file in files)
            //{

            //    // 移動した画像を保存する
            //    bmp.Save(file, ImageFormat.Png);

            //}
        }

        private static Bitmap AlignBottom(Image img, int pix)
        {
            // 画像を読み込む
            Bitmap bmp = new Bitmap(img);

            // 画像中のイラストの位置を検出する
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
                        // イラストが見つかった
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

            // 画像を移動する
            using (Graphics graphics = Graphics.FromImage(bmp))
            {
                // 一番下の位置が画像の下から10ピクセルになるように移動する
                int newX = x;
                int newY = y + pix;
                graphics.DrawImage(bmp, newX, newY);
            }

            return bmp;
        }

        private void textBox_AlignBottom_TextChanged(object sender, EventArgs e)
        {
            var temp = 0.0;
            var ret = double.TryParse(textBox_AlignBottom.Text, out temp);

            int input = (int)temp;

            if(ret == false)
            {
                input = 10;
            }
            
            if(input < 0)
            {
                input = 0;
            }

            if (input > 100000)
            {
                input = 10;
            }


            textBox_AlignBottom.Text = input.ToString();



        }

        private void listView_FileList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                listView_FileList.BeginUpdate();
                // 選択されているアイテムをリストから削除する
                if (listView_FileList.SelectedItems.Count > 0)
                {
                    foreach (ListViewItem item in listView_FileList.SelectedItems)
                    {
                        listView_FileList.Items.Remove(item);
                    }
                }


                if (listView_FileList.Items.Count == 0)
                {
                    var item = new string[] { "ここにファイルをドラッグ＆ドロップ", "" };
                    listView_FileList.Items.Add(new ListViewItem(item));
                }
                listView_FileList.EndUpdate();

            }
        }

        private void listView_FileList_DragDrop(object sender, DragEventArgs e)
        {
            // ドロップされたデータを取得
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            listView_FileList.BeginUpdate();
            {
                listView_FileList.Items.Clear();

                var hashset = new HashSet<string>();
                foreach (ListViewItem item in listView_FileList.Items)
                {
                    hashset.Add(item.SubItems[1].ToString());
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

                var itemList = new List<ListViewItem>();
                foreach (var file in hashset)
                {

                    var item = new string[] { Path.GetFileName(file), file };
                    itemList.Add(new ListViewItem(item));
                }

                listView_FileList.Items.AddRange(itemList.ToArray());


                if (listView_FileList.Items.Count == 0)
                {
                    var item = new string[] { "ここにファイルをドラッグ＆ドロップ", "" };
                    listView_FileList.Items.Add(new ListViewItem(item));
                }
            }
            listView_FileList.EndUpdate();

        }

        private void listView_FileList_DragEnter(object sender, DragEventArgs e)
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

        private void listView_FileList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView_FileList_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            //// 背景色を赤にする
            //e.Graphics.FillRectangle(Brushes.WhiteSmoke, e.Bounds);

            //// テキストを描画する
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
                    case EXEC_TYPE.FLIP_HORIZONTAL:
                        {
                            img.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        }
                        break;


                    case EXEC_TYPE.BOTTOM_ALIGN:
                        {
                            var pix = 10;
                            img = AlignBottom(img, pix);
                        }
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
                ret[m_ExecOrder[i]] = (EXEC_TYPE)i;
            }

            return ret;
        }

        private void checkBox_AlignBottom_CheckedChanged(object sender, EventArgs e)
        {
            if (m_EventEnable == false) { return; }
            CheckBoxCheckedChanged(EXEC_TYPE.BOTTOM_ALIGN);
        }

        private void checkBox_MarginAdjust_CheckedChanged(object sender, EventArgs e)
        {
            if (m_EventEnable == false) { return; }
            CheckBoxCheckedChanged(EXEC_TYPE.MARGIN_ADJUST);
        }

        private void checkBox_FlipVirtical_CheckedChanged(object sender, EventArgs e)
        {
            if (m_EventEnable == false) { return; }
            CheckBoxCheckedChanged(EXEC_TYPE.FLIP_VIRTICAL);
        }

        private void checkBox_FlipHorizontal_CheckedChanged(object sender, EventArgs e)
        {
            if (m_EventEnable == false) { return; }
            CheckBoxCheckedChanged(EXEC_TYPE.FLIP_HORIZONTAL);
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

            //中間の値が消されたら前詰めする。
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
                //ラベル更新
                if(m_ExecOrderLabelArr[i] == null) { continue; }

                m_ExecOrderLabelArr[i].Text = (m_ExecOrder[i] + 1).ToString();
                if (m_ExecOrder[i] == -1)
                {
                    m_ExecOrderLabelArr[i].Text = "";
                }

                //チェックボックス更新
                if (m_ExecCheckBoxArr[i] == null) { continue; }

                m_ExecCheckBoxArr[i].Checked = true;
                if (m_ExecOrder[i] == -1)
                {
                    m_ExecCheckBoxArr[i].Checked = false;
                }

            }

            m_EventEnable = true;

        }

    }
}