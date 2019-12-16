using openx12.Models;

namespace openx12.documents
{
    public static class SegmentTreeNodeExtensions {
        /// <summary>
        /// Adds a segment node as a child node matching the specified loop
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="segment"></param>
        /// <param name="loop"></param>
        public static void ParentToLoop(this TreeNode<Segment> tree, Segment segment, string loop) {
            var node = tree.FindTreeNodeReverse(s => s.Data != null && s.Data.Index.Loop == loop);
            node.AddChild(segment);
        }

        /// <summary>
        /// Appends a segment node of the specfied loop
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="segment"></param>
        /// <param name="loop"></param>
        public static void AppendToLoop(this TreeNode<Segment> tree, Segment segment, string loop) {
            var node = tree.FindTreeNodeReverse(s => s.Data != null && s.Data.Index.Loop == loop);
            node.Parent.AddChild(segment);
        }
    }
}