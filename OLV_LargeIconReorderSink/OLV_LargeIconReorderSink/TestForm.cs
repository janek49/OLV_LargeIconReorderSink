using BrightIdeasSoftware;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OLV_LargeIconReorderSink
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        public class ImageFile
        {
            public string Path;
            public Image Image;
            public int GroupNo = 0;

            public ImageFile(string path)
            {
                Path = path;
            }

            public void LoadImage()
            {
                Image = Image.FromFile(Path);
            }
        }


        private void btnAddItems_Click(object sender, EventArgs e)
        {
            var imagePaths = new string[] { "testfiles\\kot.jpg", "testfiles\\pies.jpg", "testfiles\\lew.jpg" };

            var list = new List<ImageFile>();
            ImageList imgList = new ImageList();
            imgList.ImageSize = new Size(128, 128);
            imgList.ColorDepth = ColorDepth.Depth24Bit;

            foreach (var path in imagePaths)
            {
                var imgFile = new ImageFile(path);
                imgFile.LoadImage();
                list.Add(imgFile);
                imgList.Images.Add(imgFile.Path, imgFile.Image);
            }

            objectListView1.LargeImageList = imgList;
            objectListView1.OwnerDraw = true;

            olvcImage.ImageGetter = delegate (object row)
            {
                return ((ImageFile)row).Path;
            };
            olvcImage.AspectGetter = delegate (object row)
            {
                return ((ImageFile)row).Path;
            };
         
            olvcGroup.AspectGetter = delegate (object row)
            {
                return ((ImageFile)row).GroupNo;
            };

            olvcImage.Sortable = false;
            olvcGroup.Sortable = true;

            objectListView1.SetObjects(list);

            objectListView1.DragSource = new SimpleDragSource();
            
            var sink = new LargeIconReorderSink();

            sink.CanDropOnItem = true;
            sink.CanDropOnBackground = true;
            sink.CanDropOnSubItem = true;

            sink.CanDropBetween = true;
            
            sink.CanDrop += Sink_CanDrop;
            sink.ModelDropped += Sink_ModelDropped;
            objectListView1.DropSink = sink;

            objectListView1.AllowDrop = true;

            objectListView1.ShowGroups = true;
            objectListView1.AlwaysGroupByColumn = olvcGroup;
            objectListView1.BeforeSorting += ObjectListView1_BeforeSorting;

            objectListView1.ShowItemCountOnGroups = true;

            objectListView1.RowHeight = 128;
            objectListView1.SortGroupItemsByPrimaryColumn = false;
            objectListView1.UseCompatibleStateImageBehavior = false;

            RefreshListView();
        }

        private void ObjectListView1_BeforeSorting(object sender, BeforeSortingEventArgs e)
        {
            var s = new OLVColumn();
            s.AspectGetter = (o) => objectListView1.IndexOf(o);

            objectListView1.ListViewItemSorter = new ColumnComparer(
                        s, SortOrder.Ascending, e.ColumnToSort, e.SortOrder);
            e.Handled = true;
        }

        private void RefreshListView()
        {
            objectListView1.SetObjects(objectListView1.Objects);
            objectListView1.BuildList(true);
            objectListView1.BuildGroups();
            objectListView1.Groups.Clear();
            objectListView1.Sort();
            objectListView1.BuildGroups();
            objectListView1.Update();
            objectListView1.Refresh();
        }

        private void Sink_ModelDropped(object sender, ModelDropEventArgs e)
        {
            if (e.DropTargetLocation != DropTargetLocation.AboveItem && e.DropTargetLocation != DropTargetLocation.BelowItem)
                return;

            var sources = e.SourceModels;
            var target = e.TargetModel as ImageFile;

            foreach (var obj in sources)
            {
                var model = obj as ImageFile;
                if (model != null)
                    model.GroupNo = target.GroupNo;
            }
            e.RefreshObjects();
            RefreshListView();
        }

        private void Sink_CanDrop(object sender, OlvDropEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
            e.DropSink.FeedbackColor = Color.Green;

        }

        private void btnCreateGroups_Click(object sender, EventArgs e)
        {
            int counter = 0;
            foreach (var obj in objectListView1.Objects)
            {
                (obj as ImageFile).GroupNo = counter++;
            }
            RefreshListView();
        }
    }
}
