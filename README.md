# OLV_LargeIconReorderSink
Makes it possible to properly reorder items in the 'Large Icon' View in ObjectListView C#

Example code for simple reordering:
```csharp
objectListView1.DragSource = new SimpleDragSource();
objectListView1.DropSink = new LargeIconReorderSink();
```

The dropsink also works well with grouping as seen below in the video. The full example application is included in this repository.

How it can look with just a bit more work:
![how-it-looks](https://user-images.githubusercontent.com/68037466/117361799-c6f49580-aeba-11eb-9482-d993b92fd114.gif)
