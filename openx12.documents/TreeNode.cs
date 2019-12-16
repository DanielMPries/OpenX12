using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace openx12.documents
{
    public class TreeNode<T> : IEnumerable<TreeNode<T>>
    {

        /// <summary>
        /// The data associated with the node
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// The Parent Tree Node
        /// </summary>
        public TreeNode<T> Parent { get; set; }

        /// <summary>
        /// The child Tree Nodes
        /// </summary>
        public ICollection<TreeNode<T>> Children { get; set; }

        /// <summary>
        /// Determines if the current node is the root node
        /// </summary>
        public Boolean IsRoot
        {
            get { return Parent == null; }
        }

        /// <summary>
        /// Detemines if the current node is a leaf node
        /// </summary>
        public Boolean IsLeaf
        {
            get { return Children.Count == 0; }
        }

        /// <summary>
        /// Determines the depth of the tree node
        /// </summary>
        public int Level
        {
            get => this.IsRoot ? 0 : (Parent.Level + 1);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="data">The associated data</param>
        public TreeNode(T data)
        {
            this.Data = data;
            this.Children = new LinkedList<TreeNode<T>>();

            this.ElementsIndex = new LinkedList<TreeNode<T>>();
            this.ElementsIndex.Add(this);
        }

        /// <summary>
        /// Adds a child item to the current node children
        /// </summary>
        /// <param name="child">The child item</param>
        public TreeNode<T> AddChild(T child)
        {
            TreeNode<T> childNode = new TreeNode<T>(child) { Parent = this };
            this.Children.Add(childNode);
            this.RegisterChildForSearch(childNode);
            return childNode;
        }

        public override string ToString()
        {
            return Data != null ? Data.ToString() : "[data null]";
        }


        #region searching
        
        private ICollection<TreeNode<T>> ElementsIndex { get; set; }

        /// <summary>
        /// Regiesters a child for search within the collection
        /// </summary>
        /// <param name="node">The child node to register</param>
        private void RegisterChildForSearch(TreeNode<T> node)
        {
            ElementsIndex.Add(node);
            if (Parent != null)
                Parent.RegisterChildForSearch(node);
        }

        /// <summary>
        /// Locates the first tree node that matches the defined predicate
        /// </summary>
        /// <param name="predicate">The predicate</param>
        public TreeNode<T> FindTreeNode(Func<TreeNode<T>, bool> predicate)
        {
            return this.ElementsIndex.FirstOrDefault(predicate);
        }

        /// <summary>
        /// Locates the last tree node that matches the defined predicate
        /// </summary>
        /// <param name="predicate">The predicate</param>
        public TreeNode<T> FindTreeNodeReverse(Func<TreeNode<T>, bool> predicate)
        {
            return this.ElementsIndex.LastOrDefault(predicate);
        }

        #endregion


        #region iterating
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Iterates over the current tree node and its subsequent child nodes
        /// </summary>
        public IEnumerator<TreeNode<T>> GetEnumerator()
        {
            yield return this;
            foreach (var directChild in this.Children)
            {
                foreach (var anyChild in directChild)
                    yield return anyChild;
            }
        }

        #endregion
    }
}