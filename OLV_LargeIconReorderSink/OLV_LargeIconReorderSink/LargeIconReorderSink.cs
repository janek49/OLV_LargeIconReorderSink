using BrightIdeasSoftware;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace OLV_LargeIconReorderSink
{
    //Dropsink for Objectlistview which works exclusive for largeicon view.
    //Above and below item states (which make sense in row style list only) get chosen for left and right side respectively
    //Correct insertion mark is drawn, on the left or right instead down or above item
    public class LargeIconReorderSink : SimpleDropSink
    {
        /// <summary>
        /// Tolerance how far the mouse can be inside an item to be still considered outside, and in such, inbetween items.
        /// Default: 75
        /// </summary>
        public int BoundsTolerance { get; set; } = 75;


        /// <summary>
        /// If enabled, the sink will take care of reordering the items when moved.
        /// If disabled, the sink only does the drawing and locating of the items, you will have to reorder them yourself in code.
        /// Default: true
        /// </summary>
        public bool UseInternalReordering { get; set; } = true;

        public LargeIconReorderSink()
        {
            this.CanDropBetween = true;
            this.CanDropOnItem = false;
        }

        protected override void OnModelCanDrop(ModelDropEventArgs args)
        {
            if (UseInternalReordering)
            {
                if (args.DropTargetLocation == DropTargetLocation.BelowItem || args.DropTargetLocation == DropTargetLocation.AboveItem)
                    args.Effect = DragDropEffects.Move;
            }
            base.OnModelCanDrop(args);
        }

        protected override void OnModelDropped(ModelDropEventArgs e)
        {
            if (UseInternalReordering && CanDropBetween &&
                (e.DropTargetLocation == DropTargetLocation.BelowItem || e.DropTargetLocation == DropTargetLocation.AboveItem))
            {
                e.Effect = DragDropEffects.Move;
                var sources = e.SourceModels;
                var target = e.TargetModel;

                if (sources != null && sources.Count > 0 && target != null)
                {
                    int targetIndex = ListView.IndexOf(target);

                    if (e.DropTargetLocation == DropTargetLocation.BelowItem)
                        targetIndex += 1;

                    if (targetIndex != ListView.IndexOf(sources[0]))
                        ListView.MoveObjects(targetIndex, sources);

                    base.OnModelDropped(e);
                }
            }
            else
                base.OnModelDropped(e);

            ListView.SetObjects(ListView.Objects);

            if (ListView.ShowGroups)
                ListView.BuildGroups();
        }

        protected override void DrawFeedbackAboveItemTarget(Graphics g, Rectangle bounds)
        {
            if (DropTargetItem == null)
                return;

            Rectangle r = CalculateDropTargetRectangle(DropTargetItem, DropTargetSubItemIndex);

            DrawBetweenLine(g, r.Left, r.Bottom, r.Left, r.Top);
        }

        protected override void DrawFeedbackBelowItemTarget(Graphics g, Rectangle bounds)
        {
            if (DropTargetItem == null)
                return;

            Rectangle r = CalculateDropTargetRectangle(DropTargetItem, DropTargetSubItemIndex);

            DrawBetweenLine(g, r.Right, r.Bottom, r.Right, r.Top);
        }

        protected override void CalculateDropTarget(OlvDropEventArgs args, Point pt)
        {
            int SMALL_VALUE = BoundsTolerance;
            DropTargetLocation location = DropTargetLocation.None;
            int targetIndex = -1;
            int targetSubIndex = 0;

            if (this.CanDropOnBackground)
                location = DropTargetLocation.Background;

            // Which item is the mouse over?
            // If it is not over any item, it's over the background.
            //ListViewHitTestInfo info = this.ListView.HitTest(pt.X, pt.Y);
            OlvListViewHitTestInfo info = this.ListView.OlvHitTest(pt.X, pt.Y);
            if (info.Item != null && this.CanDropOnItem)
            {

                location = DropTargetLocation.Item;
                targetIndex = info.Item.Index;
                if (info.SubItem != null && this.CanDropOnSubItem)
                    targetSubIndex = info.Item.SubItems.IndexOf(info.SubItem);
            }

            // Check to see if the mouse is "between" items.
            if (location != DropTargetLocation.Item && location != DropTargetLocation.SubItem && this.CanDropBetween && this.ListView.GetItemCount() > 0)
            {
                //code adpoted for right and left (large icons)

                // Is there an item a little left of the mouse?
                // If so, we say the drop point is above that row
                info = this.ListView.OlvHitTest(pt.X + SMALL_VALUE, pt.Y);
                if (info.Item != null)
                {
                    targetIndex = info.Item.Index;
                    location = DropTargetLocation.AboveItem;
                }
                else
                {
                    // Is there an item a little right of the mouse?
                    info = this.ListView.OlvHitTest(pt.X - SMALL_VALUE, pt.Y);
                    if (info.Item != null)
                    {
                        targetIndex = info.Item.Index;
                        location = DropTargetLocation.BelowItem;
                    }
                }

            }

            args.DropTargetLocation = location;
            args.DropTargetIndex = targetIndex;
            args.DropTargetSubItemIndex = targetSubIndex;
        }

        protected override void DrawBetweenLine(Graphics g, int x1, int y1, int x2, int y2)
        {
            using (Brush b = new SolidBrush(this.FeedbackColor))
            {
                int x = x1;
                int y = y1;
                using (GraphicsPath gp = new GraphicsPath())
                {
                    gp.AddLine(x, y + 5, x, y - 5);
                    //gp.AddBezier(x, y - 6, x + 3, y - 2, x + 6, y - 1, x + 11, y);
                    //gp.AddBezier(x + 11, y, x + 6, y + 1, x + 3, y + 2, x, y + 6);

                    gp.AddPolygon(new Point[] { new Point(x - 8, y + 4), new Point(x + 8, y + 4), new Point(x, y - 4) });
                    gp.CloseFigure();
                    g.FillPath(b, gp);
                }
                x = x2;
                y = y2;
                using (GraphicsPath gp = new GraphicsPath())
                {
                    gp.AddLine(x, y + 6, x, y - 6);

                    // the original method didnt work in horizontal drawing, maybe someone can figure it out. 
                    //       gp.AddBezier(x, y - 7, x - 3, y - 2, x - 6, y - 1, x - 11, y);
                    //       gp.AddBezier(x - 11, y, x - 6, y + 1, x - 3, y + 2, x, y + 7);

                    // looks just as good (triangle)
                    gp.AddPolygon(new Point[] { new Point(x - 8, y - 4), new Point(x + 8, y - 4), new Point(x, y + 4) });
                    gp.CloseFigure();
                    g.FillPath(b, gp);
                }
            }
            using (Pen p = new Pen(this.FeedbackColor, 3.0f))
            {
                g.DrawLine(p, x1, y1, x2, y2);
            }
        }
    }

}
