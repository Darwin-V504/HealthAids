namespace HealthAids.Structures
{
    public class TreeNode<T>
    {
        public T Data { get; set; }
        public TreeNode<T>? Left { get; set; }
        public TreeNode<T>? Right { get; set; }

        public TreeNode(T data)
        {
            Data = data;
        }
    }

    public class BinaryTree<T> where T : IComparable<T>
    {
        private TreeNode<T>? root;

        // Insertar
        public void Insert(T data)
        {
            root = InsertRec(root, data);
        }

        private TreeNode<T> InsertRec(TreeNode<T>? node, T data)
        {
            if (node == null)
                return new TreeNode<T>(data);

            if (data.CompareTo(node.Data) < 0)
                node.Left = InsertRec(node.Left, data);
            else if (data.CompareTo(node.Data) > 0)
                node.Right = InsertRec(node.Right, data);

            return node;
        }

        // Buscar
        public T? Find(T data)
        {
            return FindRec(root, data);
        }

        private T? FindRec(TreeNode<T>? node, T data)
        {
            if (node == null)
                return default;

            if (data.CompareTo(node.Data) == 0)
                return node.Data;

            if (data.CompareTo(node.Data) < 0)
                return FindRec(node.Left, data);

            return FindRec(node.Right, data);
        }

     
        public Structures.List<T> InOrderTraversal()
        {
            var result = new Structures.List<T>();
            InOrderRec(root, result);
            return result;
        }

        private void InOrderRec(TreeNode<T>? node, Structures.List<T> result)
        {
            if (node != null)
            {
                InOrderRec(node.Left, result);
                result.Add(node.Data);
                InOrderRec(node.Right, result);
            }
        }
       
        public T? FindByPredicate(Func<T, bool> predicate)
        {
            return FindByPredicateRec(root, predicate);
        }

        private T? FindByPredicateRec(TreeNode<T>? node, Func<T, bool> predicate)
        {
            if (node == null)
                return default;

            if (predicate(node.Data))
                return node.Data;

            T? left = FindByPredicateRec(node.Left, predicate);
            if (left != null)
                return left;

            return FindByPredicateRec(node.Right, predicate);
        }

        // Obtener la altura del arbol
        public int GetHeight()
        {
            return GetHeightRec(root);
        }

        private int GetHeightRec(TreeNode<T>? node)
        {
            if (node == null)
                return 0;
            int leftHeight = GetHeightRec(node.Left);
            int rightHeight = GetHeightRec(node.Right);
            return Math.Max(leftHeight, rightHeight) + 1;
        }
    }

}