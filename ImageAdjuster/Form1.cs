

using Accessibility;
using CatHut;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
using Image = System.Drawing.Image;
using TextBox = System.Windows.Forms.TextBox;

namespace ImageAdjuster
{


    public partial class Form1 : Form
    {

        enum EXEC_TYPE
        {
            NONE = 0,        //処理なし
            MARGIN_ADJUST,
            FLIP_HORIZONTAL,
            FLIP_VIRTICAL,
            TOP_ALIGN,
            BOTTOM_ALIGN,
            LEFT_ALIGN,
            RIGHT_ALIGN,
            RESIZE_HORIZONTAL,
            RESIZE_VIRTICAL
        }

        enum DIRECTION
        {
            UP,
            DOWN,
            LEFT,
            RIGHT
        }

        enum LISTVIEW_COLUMN_HEADER
        {
            FILE = 0,
            STATE,
            PATH
        }
        enum LISTVIEW_STATE
        {
            WAIT = 0,
            WORKING,
            COMPLETED,
        }


        private List<Image> images = new List<Image>();

        private Image m_SampleBefore;
        private Image m_SampleAfter;

        private int[] m_ExecOrder;
        private CheckBox[] m_ExecCheckBoxArr;
        private Label[] m_ExecOrderLabelArr;
        private TextBox[] m_ParamTextBoxArr;
        private AppSetting m_APS;
        private Dictionary<LISTVIEW_STATE, string> m_StateDic = new Dictionary<LISTVIEW_STATE, string>()
        {
            { LISTVIEW_STATE.WAIT, "未" },
            { LISTVIEW_STATE.WORKING, "処理中" },
            { LISTVIEW_STATE.COMPLETED, "処理済" }
        };

        bool m_EventEnable = true;
        bool m_Exec = false;
        int m_preSelectedIdx = int.MinValue;


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
            m_EventEnable = false;

            //実行順序初期化
            m_ExecOrder = new int[Enum.GetNames(typeof(EXEC_TYPE)).Length];
            for (int i = 0; i < m_ExecOrder.Length; i++)
            {
                m_ExecOrder[i] = -1;
            }

            //保存された値を読み取り
            SetExecOrder(m_APS.Settings.m_ExecOrder);

            textBox_MarginAdjust.Text = m_APS.Settings.m_MarginAdjustPix.ToString();
            textBox_AlignTop.Text = m_APS.Settings.m_TopAlignPix.ToString();
            textBox_AlignBottom.Text = m_APS.Settings.m_BottomAlignPix.ToString();
            textBox_AlignLeft.Text = m_APS.Settings.m_LeftAlignPix.ToString();
            textBox_AlignRight.Text = m_APS.Settings.m_RightAlignPix.ToString();
            textBox_ResizeHorizontal.Text = m_APS.Settings.m_ResizeHorizontalPix.ToString();
            textBox_ResizeVirtical.Text = m_APS.Settings.m_ResizeVirticalPix.ToString();

            textBox_ThuresholdAlpha.Text = m_APS.Settings.m_ThuresholdAlpha.ToString();
            trackBar_ThuresholdAlpha.Value = m_APS.Settings.m_ThuresholdAlpha;

            m_EventEnable = true;
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
            m_ExecCheckBoxArr[(int)EXEC_TYPE.RESIZE_HORIZONTAL] = checkBox_ResizeHorizontal;
            m_ExecCheckBoxArr[(int)EXEC_TYPE.RESIZE_VIRTICAL] = checkBox_ResizeVirtical;

            m_ExecOrderLabelArr = new Label[Enum.GetNames(typeof(EXEC_TYPE)).Length];
            m_ExecOrderLabelArr[(int)EXEC_TYPE.MARGIN_ADJUST] = label_MarginAlignOrder;
            m_ExecOrderLabelArr[(int)EXEC_TYPE.FLIP_HORIZONTAL] = label_FlipHorizontalOrder;
            m_ExecOrderLabelArr[(int)EXEC_TYPE.FLIP_VIRTICAL] = label_FlipVirticalOrder;
            m_ExecOrderLabelArr[(int)EXEC_TYPE.TOP_ALIGN] = label_AlignTopOrder;
            m_ExecOrderLabelArr[(int)EXEC_TYPE.BOTTOM_ALIGN] = label_AlignBottomOrder;
            m_ExecOrderLabelArr[(int)EXEC_TYPE.LEFT_ALIGN] = label_AlignLeftOrder;
            m_ExecOrderLabelArr[(int)EXEC_TYPE.RIGHT_ALIGN] = label_AlignRightOrder;
            m_ExecOrderLabelArr[(int)EXEC_TYPE.RESIZE_HORIZONTAL] = label_ResizeHorizontal;
            m_ExecOrderLabelArr[(int)EXEC_TYPE.RESIZE_VIRTICAL] = label_ResizeVirtical;

            m_ParamTextBoxArr = new TextBox[Enum.GetNames(typeof(EXEC_TYPE)).Length];
            m_ParamTextBoxArr[(int)EXEC_TYPE.MARGIN_ADJUST] = textBox_MarginAdjust;
            m_ParamTextBoxArr[(int)EXEC_TYPE.TOP_ALIGN] = textBox_AlignTop;
            m_ParamTextBoxArr[(int)EXEC_TYPE.BOTTOM_ALIGN] = textBox_AlignBottom;
            m_ParamTextBoxArr[(int)EXEC_TYPE.LEFT_ALIGN] = textBox_AlignLeft;
            m_ParamTextBoxArr[(int)EXEC_TYPE.RIGHT_ALIGN] = textBox_AlignRight;
            m_ParamTextBoxArr[(int)EXEC_TYPE.RESIZE_HORIZONTAL] = textBox_ResizeHorizontal;
            m_ParamTextBoxArr[(int)EXEC_TYPE.RESIZE_VIRTICAL] = textBox_ResizeVirtical;


        }

        private void SaveSetting()
        {
            m_APS.Settings.m_MarginAdjustPix = int.Parse(textBox_MarginAdjust.Text);
            m_APS.Settings.m_TopAlignPix = int.Parse(textBox_AlignTop.Text);
            m_APS.Settings.m_BottomAlignPix = int.Parse(textBox_AlignBottom.Text);
            m_APS.Settings.m_LeftAlignPix = int.Parse(textBox_AlignLeft.Text);
            m_APS.Settings.m_RightAlignPix = int.Parse(textBox_AlignRight.Text);
            m_APS.Settings.m_ResizeHorizontalPix = int.Parse(textBox_ResizeHorizontal.Text);
            m_APS.Settings.m_ResizeVirticalPix = int.Parse(textBox_ResizeVirtical.Text);

            m_APS.Settings.m_ThuresholdAlpha = int.Parse(textBox_ThuresholdAlpha.Text);

            m_APS.SaveData();

        }


        private void SetExecOrder(int[] ExecOrder)
        {
            if(ExecOrder == null) { return; }

            for(int i = 0; i < ExecOrder.Length; i++)
            {
                m_ExecOrder[i] = ExecOrder[i];
            }

        }

        private void ControlSetEnable(bool enable)
        {
            foreach(var item in m_ExecCheckBoxArr)
            {
                if(item != null)
                {
                    item.Enabled = enable;
                }
            }

            foreach (var item in m_ParamTextBoxArr)
            {
                if (item != null)
                {
                    item.Enabled = enable;
                }
            }

            trackBar_ThuresholdAlpha.Enabled = enable;
            textBox_ThuresholdAlpha.Enabled = enable;
            button_ExecClear.Enabled = enable;
            button_StateReset.Enabled = enable;
            listView_FileList.Enabled = enable;

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

        //private static Bitmap AlignBottom(Image img, int pix)
        //{
        //    // 画像を読み込む
        //    Bitmap bmp = new Bitmap(img);

        //    // 画像中のイラストの位置を検出する
        //    int x = 0;
        //    int y = 0;
        //    bool found = false;
        //    for (int i = bmp.Height - 1; i >= 0; i--)
        //    {
        //        for (int j = 0; j < bmp.Width; j++)
        //        {
        //            Color pixel = bmp.GetPixel(j, i);
        //            if (pixel.A > 0)
        //            {
        //                // イラストが見つかった
        //                x = j;
        //                y = i;
        //                found = true;
        //                break;
        //            }
        //        }

        //        if (found)
        //        {
        //            break;
        //        }
        //    }

        //    // 画像を移動する
        //    using (Graphics graphics = Graphics.FromImage(bmp))
        //    {
        //        // 一番下の位置が画像の下から10ピクセルになるように移動する
        //        int newX = x;
        //        int newY = y + pix;
        //        graphics.DrawImage(bmp, newX, newY);
        //    }

        //    return bmp;
        //}

        private void textBox_AlignBottom_TextChanged(object sender, EventArgs e)
        {
            if (m_EventEnable == false) { return; }

            var input = ValueLimit(textBox_AlignBottom.Text, 0, 1000);
            textBox_AlignBottom.Text = input.ToString();
            SaveSetting();
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

                //m_preSelectedIdx = int.MinValue;

            }

            if ((Control.ModifierKeys & Keys.Control) == Keys.Control) 
            {
                if (e.KeyCode == Keys.A)
                {
                    listView_FileList.BeginUpdate();
                    foreach (ListViewItem itm in this.listView_FileList.Items)
                    {
                        itm.Selected = true;
                    }
                    listView_FileList.EndUpdate();
                }
                return;
            }


            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                var idx = listView_FileList.FocusedItem.Index - 1;

                if(e.KeyCode == Keys.Down)
                {
                    idx = listView_FileList.FocusedItem.Index + 1;
                }

                Debug.WriteLine("focus:" + listView_FileList.FocusedItem.Index.ToString());

                idx = Math.Max(0, idx);
                idx = Math.Min(listView_FileList.Items.Count - 1, idx);

                UpdatePictureBox(idx);

            }
        }

        private async void listView_FileList_DragDrop(object sender, DragEventArgs e)
        {
            if(m_EventEnable == false) { return; }

            m_EventEnable = false;
            button_Exec.Enabled = false;
            button_StateReset.Enabled = false;
            button_SelectedExec.Enabled = false;
            listView_FileList.Enabled = false;


            // ドロップされたデータを取得
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);


            // 非同期でFuncAを実行する
            await Task.Run(() => RegistFiles(files));
            //RegistFiles(files);

            //何も選択されていない状態であれば、一番上を選択する
            if (listView_FileList.SelectedItems.Count == 0)
            {
                listView_FileList.Items[0].Selected = true;
                listView_FileList.EnsureVisible(0);
            }

            listView_FileList.Focus();
            UpdatePictureBox();

            button_Exec.Enabled = true;
            button_StateReset.Enabled = true;
            button_SelectedExec.Enabled = true;
            listView_FileList.Enabled = true;
            label_Status.Text = "何もしてないよ";
            m_EventEnable = true;

        }


        private void RegistFiles(string[] files)
        {

            var hashset = new HashSet<string>();
            //すでに登録されているモノを保持する

            Invoke((MethodInvoker)delegate
            {
                label_Status.Text = "ファイルカウント中";

                foreach (ListViewItem item in listView_FileList.Items)
                {
                    if (item.SubItems.Count > (int)LISTVIEW_COLUMN_HEADER.PATH)
                    {
                        if (File.Exists(item.SubItems[(int)LISTVIEW_COLUMN_HEADER.PATH].Text))
                        {
                            hashset.Add(item.SubItems[(int)LISTVIEW_COLUMN_HEADER.PATH].Text);
                        }
                    }
                }
            });

            //ファイルリストを作成する。
            CreateFileList(files, hashset);

            Invoke((MethodInvoker)delegate
            {
                label_Status.Text = hashset.Count.ToString() + "ファイル見つかりました";
            });

            var itemList = new List<ListViewItem>();
            foreach (var file in hashset)
            {
                var item = new string[] { Path.GetFileName(file), m_StateDic[LISTVIEW_STATE.WAIT], file };
                itemList.Add(new ListViewItem(item));
            }

            Invoke((MethodInvoker)delegate
            {
                label_Status.Text = hashset.Count.ToString() + "ファイル追加中";
                listView_FileList.BeginUpdate();
                {
                    //リストビューを一旦クリア
                    listView_FileList.Items.Clear();
                    listView_FileList.Items.AddRange(itemList.ToArray());

                    if (listView_FileList.Items.Count == 0)
                    {
                        var item = new string[] { "ここにファイルをドラッグ＆ドロップ", "" };
                        listView_FileList.Items.Add(new ListViewItem(item));
                    }
                }
                listView_FileList.EndUpdate();
            });

        }

        private static void CreateFileList(string[] files, HashSet<string> hashset)
        {
            string[] patterns = { ".jpg", ".png", ".bmp", "gif" };

            foreach (var file in files)
            {
                if (Directory.Exists(file))
                {
                    IEnumerable<string> filesList = Directory.EnumerateFiles(file, "*", System.IO.SearchOption.AllDirectories);
                    var ret = filesList.Where(file => patterns.Any(pattern => file.ToLower().EndsWith(pattern)));

                    foreach (var temp in ret)
                    {
                        hashset.Add(temp);
                    }

                }

                if (File.Exists(file))
                {
                    var ext = Path.GetExtension(file);
                    //拡張子が画像じゃないものはスルー
                    if (ext != ".jpg" && ext != ".png" && ext != ".bmp" && ext != ".gif")
                    {
                        continue;
                    }
                    hashset.Add(file);
                }
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
            //Debug.WriteLine("listView_FileList_SelectedIndexChanged Called");
            //UpdatePictureBox();
        }

        private void UpdatePictureBox()
        {
            if (listView_FileList.FocusedItem != null)
            {
                if(listView_FileList.FocusedItem.SubItems.Count < 2) { return; }

                label_Status.Text = "画像を表示するよ　ちょっとまってね！";
                this.Refresh();

                var item = listView_FileList.FocusedItem;
                var path = item.SubItems[(int)LISTVIEW_COLUMN_HEADER.PATH].Text;
                var file = item.SubItems[(int)LISTVIEW_COLUMN_HEADER.FILE].Text;

                if (!File.Exists(path)) { return; }

                var img = Exec(path);
                //SaveImage(img, path);
                var imgBefore = CreateImage(path);

                pictureBox_Before.Image = AdjustImage(pictureBox_Before, imgBefore);
                pictureBox_After.Image = AdjustImage(pictureBox_After, img);
                label_BeforeSize.Text = imgBefore.Width.ToString() + " ✕ " + imgBefore.Height.ToString();
                label_AfterSize.Text = img.Width.ToString() + " ✕ " + img.Height.ToString();
                label_FileName.Text = file;

                label_Status.Text = "何もしてないよ";
            }
        }

        private void UpdatePictureBox(int idx)
        {

            if (listView_FileList.Items[idx].SubItems.Count < 2) { return; }

            label_Status.Text = "画像を表示するよ　ちょっとまってね！";
            this.Refresh();

            var item = listView_FileList.Items[idx];
            var path = item.SubItems[(int)LISTVIEW_COLUMN_HEADER.PATH].Text;
            var file = item.SubItems[(int)LISTVIEW_COLUMN_HEADER.FILE].Text;

            if (!File.Exists(path)) { return; }

            var img = Exec(path);
            //SaveImage(img, path);
            var imgBefore = CreateImage(path);

            pictureBox_Before.Image = AdjustImage(pictureBox_Before, imgBefore);
            pictureBox_After.Image = AdjustImage(pictureBox_After, img);
            label_BeforeSize.Text = imgBefore.Width.ToString() + " ✕ " + imgBefore.Height.ToString();
            label_AfterSize.Text = img.Width.ToString() + " ✕ " + img.Height.ToString();
            label_FileName.Text = file;

            label_Status.Text = "何もしてないよ";
        }

        private async void button_Exec_Click(object sender, EventArgs e)
        {

            if(m_Exec == true)
            {
                m_Exec = false;
                return;
            }

            if(m_EventEnable == false) { return; }

            m_EventEnable = false;
            ControlSetEnable(false);
            button_SelectedExec.Enabled = false;
            button_Exec.Text = "キャンセル";

            m_Exec = true;

            // 非同期でFuncAを実行する
            await Task.Run(() => ExecAdjust());

            m_Exec = false;
            button_SelectedExec.Enabled = true;
            ControlSetEnable(true);
            m_EventEnable = true;
            button_Exec.Text = "一括実行";

            label_Status.Text = "何もしてないよ";
            label_Progress.Text = "";

        }

        private void SaveImage(Image img, string path)
        {
            var result = false;
            while (!result)
            {
                try
                {
                    img.Save(path, ImageFormat.Png);
                    Debug.WriteLine("保存成功");
                    result = true;
                }
                catch 
                {
                    Debug.WriteLine("保存失敗:" + path);
                    Thread.Sleep(500);
                }
            }
        }


        private void ExecAdjust()
        {
            HashSet<string> pathList = new HashSet<string>();

            Invoke((MethodInvoker)delegate
            {
                foreach (ListViewItem item in listView_FileList.Items)
                {
                    if(item.SubItems.Count < 2) { continue; }
                    pathList.Add(item.SubItems[(int)LISTVIEW_COLUMN_HEADER.PATH].Text);
                }
            });

            int i = 1;
            foreach (var path in pathList) { 

                if(m_Exec == false) { break; }

                if(GetListViewState(path) == m_StateDic[LISTVIEW_STATE.COMPLETED]) { continue; }

                if (!File.Exists(path)) { continue; }

                SetListViewState(path, LISTVIEW_STATE.WORKING);

                Invoke((MethodInvoker)delegate
                {
                    label_Status.Text = " 処理中:" + Path.GetFileName(path);
                    label_Progress.Text = i.ToString().PadLeft(5, ' ') + "/" + pathList.Count.ToString().PadLeft(5, ' ');
                });

                var img = Exec(path);
                SaveImage(img, path);

                SetListViewState(path, LISTVIEW_STATE.COMPLETED);

                i++;

            }
        }


        private void SelectedExecAdjust()
        {
            HashSet<string> pathList = new HashSet<string>();

            Invoke((MethodInvoker)delegate
            {
                foreach (ListViewItem item in listView_FileList.SelectedItems)
                {
                    if (item.SubItems.Count < 2) { continue; }
                    pathList.Add(item.SubItems[(int)LISTVIEW_COLUMN_HEADER.PATH].Text);
                }
            });

            int i = 1;
            foreach (var path in pathList)
            {

                if (m_Exec == false) { break; }

                if (GetListViewState(path) == m_StateDic[LISTVIEW_STATE.COMPLETED]) { continue; }

                if (!File.Exists(path)) { continue; }

                SetListViewState(path, LISTVIEW_STATE.WORKING);

                Invoke((MethodInvoker)delegate
                {
                    label_Status.Text = " 処理中:" + Path.GetFileName(path);
                    label_Progress.Text = i.ToString().PadLeft(5, ' ') + "/" + pathList.Count.ToString().PadLeft(5, ' ');
                });

                var img = Exec(path);
                SaveImage(img, path);

                SetListViewState(path, LISTVIEW_STATE.COMPLETED);

                i++;

            }
        }

        private void SetListViewState(string path, LISTVIEW_STATE state) 
        {
            Invoke((MethodInvoker)delegate
            {
                foreach (ListViewItem item in listView_FileList.Items)
                {
                    if (item.SubItems[(int)LISTVIEW_COLUMN_HEADER.PATH].Text == path)
                    {
                        item.SubItems[(int)LISTVIEW_COLUMN_HEADER.STATE].Text = m_StateDic[state];
                        if (state == LISTVIEW_STATE.COMPLETED)
                        {
                            listView_FileList.EnsureVisible(item.Index);
                            //item.UseItemStyleForSubItems = false;
                            //item.SubItems[(int)LISTVIEW_COLUMN_HEADER.STATE].BackColor = Color.Gray;
                            //item.BackColor = Color.Gray;
                        }
                        else if(state == LISTVIEW_STATE.WAIT)
                        {
                            //item.UseItemStyleForSubItems = false;
                            //item.SubItems[(int)LISTVIEW_COLUMN_HEADER.STATE].BackColor = Color.White;
                            //item.BackColor = Color.White;
                        }

                        break;
                    }
                }
            });
        }

        private string GetListViewState(string path)
        {
            var ret = "";
            Invoke((MethodInvoker)delegate
            {
                foreach (ListViewItem item in listView_FileList.Items)
                {
                    if (item.SubItems[(int)LISTVIEW_COLUMN_HEADER.PATH].Text == path)
                    {
                        ret = item.SubItems[(int)LISTVIEW_COLUMN_HEADER.STATE].Text;
                    }
                }
            });
            return ret;
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


        public static System.Drawing.Image CreateImage(string filename)
        {
            System.IO.FileStream fs = new System.IO.FileStream(
                filename,
                System.IO.FileMode.Open,
                System.IO.FileAccess.Read);
            System.Drawing.Image img = System.Drawing.Image.FromStream(fs);
            fs.Close();
            return img;
        }

        private Image Exec(string file)
        {
            var orderedArray = GetExecOrderedArray();

            Image img = CreateImage(file);

            foreach (var exeType in orderedArray)
            {
                switch (exeType)
                {
                    case EXEC_TYPE.MARGIN_ADJUST:
                        {
                            var pix = m_APS.Settings.m_MarginAdjustPix;
                            var alpha = m_APS.Settings.m_ThuresholdAlpha;

                            img = MarginRemove(img, alpha);
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
                            var pix = m_APS.Settings.m_BottomAlignPix;
                            var alpha = m_APS.Settings.m_ThuresholdAlpha;

                            img = RemoveBottomMargin(img, alpha);
                            img = AddMargin(img, DIRECTION.DOWN, pix);

                        }
                        break;

                    case EXEC_TYPE.TOP_ALIGN:
                        {
                            var pix = m_APS.Settings.m_TopAlignPix;
                            var alpha = m_APS.Settings.m_ThuresholdAlpha;

                            img = RemoveTopMargin(img, alpha);
                            img = AddMargin(img, DIRECTION.UP, pix);

                        }
                        break;

                    case EXEC_TYPE.LEFT_ALIGN:
                        {
                            var pix = m_APS.Settings.m_LeftAlignPix;
                            var alpha = m_APS.Settings.m_ThuresholdAlpha;

                            img = RemoveLeftMargin(img, alpha);
                            img = AddMargin(img, DIRECTION.LEFT, pix);

                        }
                        break;

                    case EXEC_TYPE.RIGHT_ALIGN:
                        {
                            var pix = m_APS.Settings.m_RightAlignPix;
                            var alpha = m_APS.Settings.m_ThuresholdAlpha;

                            img = RemoveRightMargin(img, alpha);
                            img = AddMargin(img, DIRECTION.RIGHT, pix);

                        }
                        break;


                    case EXEC_TYPE.RESIZE_HORIZONTAL:
                        {
                            var pix = m_APS.Settings.m_ResizeHorizontalPix;
                            img = ResizeHorizontal(img, pix);
                        }
                        break;

                    case EXEC_TYPE.RESIZE_VIRTICAL:
                        {
                            var pix = m_APS.Settings.m_ResizeVirticalPix;
                            img = ResizeVertical(img, pix);
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

        private void ExecOrderUpdate(EXEC_TYPE et)
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
            for (int i = 0; i < m_ExecOrder.Length; i++)
            {
                if (m_ExecOrder[i] > preValue)
                {
                    m_ExecOrder[i]--;
                }
            }
        }



        //private void CheckBoxCheckedChanged(EXEC_TYPE et1, EXEC_TYPE et2)
        //{
        //    var preValue = int.MaxValue;
        //    if (m_ExecCheckBoxArr[(int)et1].Checked)
        //    {
        //        m_ExecOrder[(int)et1] = GetOrder();
        //    }
        //    else
        //    {
        //        preValue = m_ExecOrder[(int)et1];
        //        m_ExecOrder[(int)et1] = -1;
        //    }

        //    //中間の値が消されたら前詰めする。
        //    for (int i = 0; i < m_ExecOrder.Length; i++)
        //    {
        //        if (m_ExecOrder[i] > preValue)
        //        {
        //            m_ExecOrder[i]--;
        //        }
        //    }


        //    if (m_ExecCheckBoxArr[(int)et2].Checked)
        //    {
        //        m_ExecOrder[(int)et2] = GetOrder();
        //    }
        //    else
        //    {
        //        preValue = m_ExecOrder[(int)et1];
        //        m_ExecOrder[(int)et2] = -1;
        //    }


        //    //中間の値が消されたら前詰めする。
        //    for (int i = 0; i < m_ExecOrder.Length; i++)
        //    {
        //        if (m_ExecOrder[i] > preValue)
        //        {
        //            m_ExecOrder[i]--;
        //        }
        //    }

        //    UpdateUI();
        //    m_APS.Settings.m_ExecOrder = m_ExecOrder;
        //    m_APS.SaveData();
        //}

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

        private Image MarginRemove(Image img, int alpha)
        {
            return MarginRemove((Bitmap)img, alpha);
        }


        private Image MarginRemove(Bitmap img, int alpha)
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
                    if (pixel.A > alpha)
                    {
                        if (x < minX) minX = x;
                        if (y < minY) minY = y;
                        if (x > maxX) maxX = x;
                        if (y > maxY) maxY = y;
                    }
                }
            }

            // minX, maxX, minY, maxY の値が正しいかチェックする
            // 値が不正な場合は、それぞれ0を設定する
            maxX = Math.Max(0, maxX);
            maxY = Math.Max(0, maxY);
            minX = Math.Min(maxX, minX);
            minY = Math.Min(maxY, minY);

            if(maxY == minY || maxX == minX)
            {
                return CreateErrorImage();
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


        private Image RemoveTopMargin(Bitmap img, int alpha)
        {
            // 透明な余白の上端を探す
            int top = 0;
            bool isBlank = true;
            for (int y = 0; y < img.Height; y++)
            {
                isBlank = true;
                for (int x = 0; x < img.Width; x++)
                {
                    // 各ピクセルのアルファ値を取得
                    Color pixel = img.GetPixel(x, y);
                    if (pixel.A > alpha)
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

            //見つからなかった場合エラー回避用処理
            if (isBlank == true)
            {
                return CreateErrorImage();
            }

            // 値が正しいかチェックする
            // 値が不正な場合は、それぞれ0を設定する
            top = Math.Max(0, top);
            top = Math.Min(img.Height, top);

            // 出力画像を作成
            Bitmap outputImage = new Bitmap(img.Width, img.Height - top);

            CopyPixel(img, 0, top, img.Width, img.Height, outputImage, 0, 0);

            // 出力画像を保存
            return outputImage;
        }

        //private void CopyPixel(Bitmap src, int s_x, int s_y, int width, int height, Bitmap dest, int d_x, int d_y)
        //{
        //    // 出力画像に入力画像から透明な余白を除いた部分をコピーする
        //    // 既存の画像を新しい画像にコピーする
        //    for (int x = s_x; x < width; x++)
        //    {
        //        for (int y = s_y; y < height; y++)
        //        {
        //            // 既存の画像からピクセルを取得する
        //            Color pixel = src.GetPixel(x, y);

        //            if(pixel.A == 0) { continue; }

        //            // 新しい画像にピクセルを設定する
        //            dest.SetPixel(x + d_x - s_x, y + d_y - s_y, pixel);
        //        }
        //    }
        //}

        private void CopyPixel(Bitmap src, int s_x, int s_y, int width, int height, Bitmap dest, int d_x, int d_y)
        {
            // 出力画像に入力画像をそのままコピーする
            // 既存の画像を新しい画像にコピーする

            // 1 行分のデータを取得/設定するためのバッファを用意する
            byte[] srcBuffer = new byte[width * 4];
            byte[] destBuffer = new byte[width * 4];

            for (int y = s_y; y < height; y++)
            {
                // 既存の画像から 1 行分のデータを取得する
                var srcData = src.LockBits(new Rectangle(s_x, y, width, 1), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                Marshal.Copy(srcData.Scan0, srcBuffer, 0, srcBuffer.Length);
                src.UnlockBits(srcData);

                // 全てのピクセルを新しい画像に設定する
                Array.Copy(srcBuffer, destBuffer, srcBuffer.Length);

                // 新しい画像に 1 行分のデータを設定する
                var destData = dest.LockBits(new Rectangle(d_x, y + d_y - s_y, width, 1), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
                Marshal.Copy(destBuffer, 0, destData.Scan0, destBuffer.Length);
                dest.UnlockBits(destData);
            }
        }



        //private void CopyPixel(Bitmap src, int s_x, int s_y, int width, int height, Bitmap dest, int d_x, int d_y)
        //{
        //    // 出力画像に入力画像から透明な余白を除いた部分をコピーする
        //    // 既存の画像を新しい画像にコピーする

        //    // 並列処理で実行するためのオプションを作成する
        //    ParallelOptions options = new ParallelOptions();
        //    options.MaxDegreeOfParallelism = Environment.ProcessorCount;

        //    // ピクセルを並列処理でコピーする
        //    Parallel.For(s_x, width, options, x =>
        //    {
        //        for (int y = s_y; y < height; y++)
        //        {
        //            // 既存の画像からピクセルを取得する
        //            Color pixel = src.GetPixel(x, y);

        //            if(pixel.A == 0) { continue; }

        //            // 新しい画像にピクセルを設定する
        //            dest.SetPixel(x + d_x - s_x, y + d_y - s_y, pixel);
        //        }
        //    });
        //}


        private Image RemoveBottomMargin(Image img, int alpha)
        {
            return RemoveBottomMargin((Bitmap)img, alpha);
        }

        private Image RemoveTopMargin(Image img, int alpha)
        {
            return RemoveTopMargin((Bitmap)img, alpha);
        }

        private Image RemoveLeftMargin(Image img, int alpha)
        {
            return RemoveLeftMargin((Bitmap)img, alpha);
        }

        private Image RemoveRightMargin(Image img, int alpha)
        {
            return RemoveRightMargin((Bitmap)img, alpha);
        }

        private Image ResizeHorizontal(Image img, int width)
        {
            int height = width * img.Height / img.Width;

            if (width < 1 || height < 1)
            {
                return CreateErrorImage();
            }

            Bitmap canvas = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(canvas))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(img, 0, 0, width, height);
            }
            return canvas;
        }

        private Image ResizeVertical(Image img, int height)
        {
            int width = height * img.Width / img.Height;

            if(width < 1 || height < 1)
            {
                return CreateErrorImage();
            }

            Bitmap canvas = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(canvas))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(img, 0, 0, width, height);
            }
            return canvas;
        }



        private Image RemoveBottomMargin(Bitmap img, int alpha)
        {
            // 透明な余白の下端を探す
            int bottom = 0;
            bool isBlank = true;
            for (int y = img.Height - 1; y >= 0; y--)
            {
                isBlank = true;
                for (int x = 0; x < img.Width; x++)
                {
                    // 各ピクセルのアルファ値を取得
                    Color pixel = img.GetPixel(x, y);
                    if (pixel.A > alpha)
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

            //見つからなかった場合エラー回避用処理
            if (isBlank == true)
            {
                return CreateErrorImage();
            }

            // 値が正しいかチェックする
            // 値が不正な場合は、それぞれ0を設定する
            bottom = Math.Max(0, bottom);
            bottom = Math.Min(img.Height, bottom);

            // 出力画像を作成
            Bitmap outputImage = new Bitmap(img.Width, bottom + 1);

            CopyPixel(img, 0, 0, img.Width, bottom, outputImage, 0, 0);

            // 出力画像を保存
            return outputImage;
        }


        private Image RemoveLeftMargin(Bitmap img, int alpha)
        {
            // 透明な余白の左端を探す
            int left = 0;
            bool isBlank = true;
            for (int x = 0; x < img.Width; x++)
            {
                isBlank = true;
                for (int y = 0; y < img.Height; y++)
                {
                    // 各ピクセルのアルファ値を取得
                    Color pixel = img.GetPixel(x, y);
                    if (pixel.A > alpha)
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

            //見つからなかった場合エラー回避用処理
            if (isBlank == true)
            {
                return CreateErrorImage();
            }

            // 値が正しいかチェックする
            // 値が不正な場合は、それぞれ0を設定する
            left = Math.Max(0, left);
            left = Math.Min(img.Width, left);


            // 出力画像を作成
            Bitmap outputImage = new Bitmap(img.Width - left, img.Height);

            CopyPixel(img, left, 0, img.Width - left, img.Height, outputImage, 0, 0);

            // 出力画像を保存
            return outputImage;
        }


        private Image RemoveRightMargin(Bitmap img, int alpha)
        {
            // 透明な余白の右端を探す
            int right = 0;
            bool isBlank = true;
            for (int x = img.Width - 1; x >= 0; x--)
            {
                isBlank = true;
                for (int y = 0; y < img.Height; y++)
                {
                    // 各ピクセルのアルファ値を取得
                    Color pixel = img.GetPixel(x, y);
                    if (pixel.A > alpha)
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

            //見つからなかった場合エラー回避用処理
            if (isBlank == true)
            {
                return CreateErrorImage();
            }

            // 値が正しいかチェックする
            // 値が不正な場合は、それぞれ0を設定する
            right = Math.Max(0, right);
            right = Math.Min(img.Width, right);


            // 出力画像を作成
            Bitmap outputImage = new Bitmap(right + 1, img.Height);

            CopyPixel(img, 0, 0, right, img.Height, outputImage, 0, 0);

            // 出力画像を保存
            return outputImage;
        }


        private Image CreateErrorImage()
        {
            // フォントとフォントスタイルを指定する
            Font font = new Font("Arial", 8, FontStyle.Bold);

            // Imageオブジェクトを作成する
            Image img = new Bitmap(200, 100);

            // usingステートメントを使用してGraphicsオブジェクトを作成する
            using (Graphics g = Graphics.FromImage(img))
            {
                // テキストを描画する
                g.DrawString("出力できる画像がありません" + Environment.NewLine + "アルファしきい値を見直してください", font, Brushes.Red, 10, 10);
            }

            return img;
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

            CopyPixel((Bitmap)img, 0, 0, img.Width, img.Height, newImage, x, y);


            // 変更された画像を返す
            return newImage;
        }


        private void textBox_MarginAdjust_TextChanged(object sender, EventArgs e)
        {
            if (m_EventEnable == false) { return; }

            var input = ValueLimit(textBox_MarginAdjust.Text, 0, 1000);
            textBox_MarginAdjust.Text = input.ToString();
            SaveSetting();
            UpdatePictureBox();
        }

        private void textBox_AlignTop_TextChanged(object sender, EventArgs e)
        {
            if (m_EventEnable == false) { return; }

            var input = ValueLimit(textBox_AlignTop.Text, 0, 1000);
            textBox_AlignTop.Text = input.ToString();
            SaveSetting();
            UpdatePictureBox();

        }

        private void textBox_AlignLeft_TextChanged(object sender, EventArgs e)
        {
            if (m_EventEnable == false) { return; }

            var input = ValueLimit(textBox_AlignLeft.Text, 0, 1000);
            textBox_AlignLeft.Text = input.ToString();
            SaveSetting();
            UpdatePictureBox();

        }

        private void textBox_AlignRight_TextChanged(object sender, EventArgs e)
        {
            if (m_EventEnable == false) { return; }

            var input = ValueLimit(textBox_AlignRight.Text, 0, 1000);
            textBox_AlignRight.Text = input.ToString();
            SaveSetting();
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

        private void listView_FileList_Click(object sender, EventArgs e)
        {

            Debug.WriteLine("listView_FileList_Click Called");
            Debug.WriteLine("SelectedIndex: " );
            foreach(var i in listView_FileList.SelectedIndices)
            {
                Debug.WriteLine(i.ToString());
            }

            if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
            {
                if (listView_FileList.FocusedItem != null)
                {
                    if(listView_FileList.FocusedItem.Selected == true)
                    {
                        UpdatePictureBox();
                    }
                    else
                    {
                        if (listView_FileList.SelectedItems.Count > 0)
                        {
                            var idx = listView_FileList.SelectedItems[0].Index;
                            UpdatePictureBox(idx);
                        }
                    }

                }
            }
            else
            {
                UpdatePictureBox();
            }
        }

        private void textBox_ThuresholdAlpha_TextChanged(object sender, EventArgs e)
        {
            if (m_EventEnable == false) { return; }

            var input = ValueLimit(textBox_ThuresholdAlpha.Text, 0, 255);
            textBox_ThuresholdAlpha.Text = input.ToString();
            trackBar_ThuresholdAlpha.Value = input;
            SaveSetting();
            UpdatePictureBox();
        }

        private void trackBar_ThuresholdAlpha_ValueChanged(object sender, EventArgs e)
        {
            if (m_EventEnable == false) { return; }

            textBox_ThuresholdAlpha.Text = trackBar_ThuresholdAlpha.Value.ToString();
            SaveSetting();
            UpdatePictureBox();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_APS.Exit();
        }

        private void button_StateReset_Click(object sender, EventArgs e)
        {
            if (m_EventEnable == false) { return; }

            //処理済みをリセット
            {
                m_EventEnable = false;
                var pathList = new HashSet<String>();
                foreach (ListViewItem item in listView_FileList.Items)
                {
                    if (item.SubItems.Count < 2) { continue; }
                    pathList.Add(item.SubItems[(int)LISTVIEW_COLUMN_HEADER.PATH].Text);
                }

                foreach (var path in pathList)
                {
                    SetListViewState(path, LISTVIEW_STATE.WAIT);
                }
                m_EventEnable = true;
            }
        }

        private void textBox_ResizeHorizontal_TextChanged(object sender, EventArgs e)
        {
            if (m_EventEnable == false) { return; }

            var input = ValueLimit(textBox_ResizeHorizontal.Text, 0, 1000);
            textBox_AlignRight.Text = input.ToString();
            SaveSetting();
            UpdatePictureBox();

        }

        private void textBox_ResizeVirtical_TextChanged(object sender, EventArgs e)
        {
            if (m_EventEnable == false) { return; }

            var input = ValueLimit(textBox_ResizeVirtical.Text, 0, 1000);
            textBox_AlignRight.Text = input.ToString();
            SaveSetting();
            UpdatePictureBox();

        }

        private void checkBox_ResizeHorizontal_CheckedChanged(object sender, EventArgs e)
        {
            if (m_EventEnable == false) { return; }

            {
                m_EventEnable = false;
                //縦指定がチェックされていたら外す
                if (checkBox_ResizeVirtical.Checked == true)
                {
                    checkBox_ResizeVirtical.Checked = false;
                    ExecOrderUpdate(EXEC_TYPE.RESIZE_VIRTICAL);
                }
                m_EventEnable = true;
            }

            CheckBoxCheckedChanged(EXEC_TYPE.RESIZE_HORIZONTAL);
            UpdatePictureBox();
        }

        private void checkBox_ResizeVirtical_CheckedChanged(object sender, EventArgs e)
        {
            if (m_EventEnable == false) { return; }

            {
                m_EventEnable = false;
                //横指定がチェックされていたら外す
                if (checkBox_ResizeHorizontal.Checked == true)
                {
                    checkBox_ResizeHorizontal.Checked = false;
                    ExecOrderUpdate(EXEC_TYPE.RESIZE_HORIZONTAL);
                }
                m_EventEnable = true;
            }

            CheckBoxCheckedChanged(EXEC_TYPE.RESIZE_VIRTICAL);
            UpdatePictureBox();
        }

        private async void button_SelectedExec_Click(object sender, EventArgs e)
        {
            if (m_Exec == true)
            {
                m_Exec = false;
                return;
            }

            if (m_EventEnable == false) { return; }

            m_EventEnable = false;
            ControlSetEnable(false);
            button_Exec.Enabled = false;
            button_SelectedExec.Text = "キャンセル";

            m_Exec = true;

            // 非同期でFuncAを実行する
            await Task.Run(() => SelectedExecAdjust());

            m_Exec = false;
            button_Exec.Enabled = true;
            ControlSetEnable(true);
            m_EventEnable = true;
            button_SelectedExec.Text = "選択実行";

            label_Status.Text = "何もしてないよ";
            label_Progress.Text = "";
        }
    }
}