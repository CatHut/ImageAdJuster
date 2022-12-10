

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
            NONE        //処理なし
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
            var input = ValueLimit(textBox_AlignBottom.Text, 0, 1000);
            textBox_AlignBottom.Text = input.ToString();
            UpdatePictureBox();

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
                    var item = new string[] { "ここにファイルをドラッグ＆ドロップ", "" };
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
            // PictureBoxのサイズを取得する
            int pictureBoxWidth = pb.Width;
            int pictureBoxHeight = pb.Height;

            // 元画像のサイズを取得する
            int imageWidth = img.Width;
            int imageHeight = img.Height;

            // 縦横比を計算する
            float aspectRatio = (float)imageWidth / (float)imageHeight;

            // 元画像がPictureBoxより小さい場合は、拡大しない
            if (imageWidth <= pictureBoxWidth && imageHeight <= pictureBoxHeight)
            {

                // 画像のGraphicsオブジェクトを取得する
                Graphics g = Graphics.FromImage(img);

                // 枠を描く
                g.DrawRectangle(Pens.Red, 0, 0, img.Width - 1, img.Height - 1);

                // 画像をそのまま表示する
                return img;
            }
            else
            {
                Image resizedImage;
                if (aspectRatio > 1.0f)
                {
                    // 横幅を縮小する倍率を計算する
                    float scale = (float)pictureBoxWidth / imageWidth;

                    // 元画像を縮小する
                    resizedImage = new Bitmap(img, new Size((int)(pictureBoxWidth), (int)(imageHeight * scale)));
                }
                else
                {
                    // 縦幅を縮小する倍率を計算する
                    float scale = (float)pictureBoxHeight / imageHeight;

                    // 元画像を縮小する
                    resizedImage = new Bitmap(img, new Size((int)(imageWidth * scale), (int)(pictureBoxHeight)));

                }

                // 画像のGraphicsオブジェクトを取得する
                Graphics g = Graphics.FromImage(resizedImage);

                // 枠を描く
                var frameWidth = resizedImage.Width - 1;
                var frameHeight = resizedImage.Height - 1;

                frameWidth -= Math.Max(0, 2 - (pictureBoxWidth - resizedImage.Width));
                frameHeight -= Math.Max(0, 2 - (pictureBoxHeight - resizedImage.Height));

                g.DrawRectangle(Pens.Red, 0, 0, frameWidth, frameHeight);

                //// 縮小した画像を表示する
                return resizedImage;
            }
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
                        //何もしない
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

            // 新しいBitmapオブジェクトを作成する
            Bitmap newImg = new Bitmap(img.Width + pix * 2, img.Height + pix * 2);

            // 既存の画像を新しい画像にコピーする
            for (int x = 0; x < img.Width; x++)
            {
                for (int y = 0; y < img.Height; y++)
                {
                    // 既存の画像からピクセルを取得する
                    Color pixel = img.GetPixel(x, y);

                    // 新しい画像にピクセルを設定する
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
            // 余白を検出するための最小値
            int minX = img.Width;
            int minY = img.Height;
            int maxX = 0;
            int maxY = 0;

            // 画像をスキャンする
            for (int x = 0; x < img.Width; x++)
            {
                for (int y = 0; y < img.Height; y++)
                {
                    // ピクセルを取得する
                    Color pixel = img.GetPixel(x, y);

                    // 透明でないピクセルの場合は、余白の境界を更新する
                    if (pixel.A != 0)
                    {
                        if (x < minX) minX = x;
                        if (y < minY) minY = y;
                        if (x > maxX) maxX = x;
                        if (y > maxY) maxY = y;
                    }
                }
            }

            // 新しいBitmapオブジェクトを作成する
            Bitmap newImg = new Bitmap(maxX - minX + 1, maxY - minY + 1);

            // 余白を削除した画像を作成する
            for (int x = minX; x <= maxX; x++)
            {
                for (int y = minY; y <= maxY; y++)
                {
                    // ピクセルを取得する
                    Color pixel = img.GetPixel(x, y);

                    // 新しい画像にピクセルを設定する
                    newImg.SetPixel(x - minX, y - minY, pixel);
                }
            }

            return newImg;
        }


        private Image RemoveTopMargin(Bitmap img)
        {
            // 透明な余白の上端を探す
            int top = 0;
            for (int y = 0; y < img.Height; y++)
            {
                bool isBlank = true;
                for (int x = 0; x < img.Width; x++)
                {
                    // 各ピクセルのアルファ値を取得
                    Color pixel = img.GetPixel(x, y);
                    if (pixel.A > 0)
                    {
                        // アルファ値が0より大きいピクセルが見つかった場合、そこが透明な余白の上端
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

            // 出力画像を作成
            Bitmap outputImage = new Bitmap(img.Width, img.Height - top);

            CopyPixel(img, 0, top, img.Width, img.Height, ref outputImage, 0, 0);

            // 出力画像を保存
            return outputImage;
        }

        private void CopyPixel(Bitmap src, int s_x, int s_y, int width, int height, ref Bitmap dest, int d_x, int d_y) 
        {
            // 出力画像に入力画像から透明な余白を除いた部分をコピーする
            // 既存の画像を新しい画像にコピーする
            for (int x = s_x; x < width; x++)
            {
                for (int y = s_y; y < height; y++)
                {
                    // 既存の画像からピクセルを取得する
                    Color pixel = src.GetPixel(x, y);

                    // 新しい画像にピクセルを設定する
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
            // 透明な余白の下端を探す
            int bottom = 0;
            for (int y = img.Height - 1; y >= 0; y--)
            {
                bool isBlank = true;
                for (int x = 0; x < img.Width; x++)
                {
                    // 各ピクセルのアルファ値を取得
                    Color pixel = img.GetPixel(x, y);
                    if (pixel.A > 0)
                    {
                        // アルファ値が0より大きいピクセルが見つかった場合、そこが透明な余白の下端
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

            // 出力画像を作成
            Bitmap outputImage = new Bitmap(img.Width, bottom + 1);

            CopyPixel(img, 0, 0, img.Width, bottom, ref outputImage, 0, 0);

            // 出力画像を保存
            return outputImage;
        }


        private Image RemoveLeftMargin(Bitmap img)
        {
            // 透明な余白の左端を探す
            int left = 0;
            for (int x = 0; x < img.Width; x++)
            {
                bool isBlank = true;
                for (int y = 0; y < img.Height; y++)
                {
                    // 各ピクセルのアルファ値を取得
                    Color pixel = img.GetPixel(x, y);
                    if (pixel.A > 0)
                    {
                        // アルファ値が0より大きいピクセルが見つかった場合、そこが透明な余白の左端
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

            // 出力画像を作成
            Bitmap outputImage = new Bitmap(img.Width - left, img.Height);

            CopyPixel(img, left, 0, img.Width, img.Height, ref outputImage, 0, 0);

            // 出力画像を保存
            return outputImage;
        }


        private Image RemoveRightMargin(Bitmap img)
        {
            // 透明な余白の右端を探す
            int right = 0;
            for (int x = img.Width - 1; x >= 0; x--)
            {
                bool isBlank = true;
                for (int y = 0; y < img.Height; y++)
                {
                    // 各ピクセルのアルファ値を取得
                    Color pixel = img.GetPixel(x, y);
                    if (pixel.A > 0)
                    {
                        // アルファ値が0より大きいピクセルが見つかった場合、そこが透明な余白の右端
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

            // 出力画像を作成
            Bitmap outputImage = new Bitmap(right + 1, img.Height);

            CopyPixel(img, 0, 0, right, img.Height, ref outputImage, 0, 0);

            // 出力画像を保存
            return outputImage;
        }

        private Image AddMargin(Image img, DIRECTION direction, int pix)
        {

            if(pix == 0) { return img; }

            // 元の画像の幅と高さを取得する
            int width = img.Width;
            int height = img.Height;

            // 新しい画像の大きさを計算する
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

            // 新しい画像を作成する
            Bitmap newImage = new Bitmap(newWidth, newHeight);


            // 元の画像を新しい画像に重ねる
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


            // 変更された画像を返す
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