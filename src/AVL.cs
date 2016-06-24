namespace NASP_Labos1
{
    public class AVL
    {
        public Node root;
        
        public class Node
        {
            public int  value;
            //public Node Parent;
            public Node Left;
            public Node Right;
            public int  height;

            public Node(int val)
            {
                value = val;
                //Parent = null;
                Left = null;
                Right = null;
                height = 0;
            }
        }

        private int max(int v1, int v2)
        {
            return ((v1 > v2) ? v1 : v2);
        }
        private void setHeight(Node node)
        {
            int m,n,d;
            m = height(node.Left);
            n = height(node.Right);
            d = max(m, n);
            node.height = d+1;
        }

        public int height(Node nd)
        {
            int h;
            if (nd == null) return -1;
            else
            {
                h = nd.height;
                return h;
            }
        }

        public Node insert(int val, Node node)
        {
            if (node == null)
            {
                Node newNode = new Node(val);
                setHeight(newNode);
                return newNode;
            }
            else
            {
                if (val < node.value)
                {
                    node.Left = insert(val, node.Left);
                    if ((height(node.Left)-height(node.Right))==2)
                    {
                        if (val < node.Left.value) node = RotateLeft(node);
                        else node = RotateLeftRight(node);
                    }
                    setHeight(node);
                    return node;
                }
                else if (val > node.value)
                {
                    node.Right = insert(val, node.Right);
                    if ((height(node.Right) - height(node.Left)) == 2)
                    {
                        if (val > node.Right.value) node = RotateRight(node);
                        else node = RotateRightLeft(node);
                    }
                    setHeight(node);
                    return node;
                }
                else throw new System.ArgumentException("Broj je vec u stablu!");
            }
        }

        public Node delete(int val, ref Node node)
        {
            bool flag = true;

            if (node == null)
                throw new System.ArgumentException("Element ne postoji!");
            else if (val < node.value)
                delete(val, ref node.Left);
            else if (val > node.value)
                delete(val, ref node.Right);
            else
            {
                if ((node.Left == null) && (node.Right == null))
                {
                    node = null;
                    flag = true;
                }
                else if (node.Left == null)
                {
                    node = node.Right;
                    flag = true;
                }
                else if (node.Right == null)
                {
                    node = node.Left;
                    flag = true;
                }
                else
                    node.value = findMin(node.Right);
            }
            if (!flag) balance(node);
            return node;
        }

        private void balance(Node node)
        {
            int fx, dfx;

            fx = height(node.Right) - height(node.Left);
            if (fx == 2)
            {
                dfx = height(node.Right.Right) - height(node.Right.Left);
                if ((dfx == 0) || (dfx == 1))
                    node = RotateRight(node);
                else
                    node = RotateRightLeft(node);
            }
            else if (fx == -2)
            {
                dfx = height(node.Left.Right) - height(node.Left.Left);
                if ((dfx == -1) || (dfx == 0))
                    node = RotateLeft(node);
                else
                    node = RotateLeftRight(node);
            }
            setHeight(node);
        }

        private int findMin(Node node)
        {
            int temp;

            if (node.Left == null)
            {
                temp = node.value;
                node = node.Right;
                return temp;
            }
            else
            {
                temp = findMin(node.Left);
                return temp;
            }
        }

        private Node RotateLeft(Node node)
        {
            Node temp;

            temp = node.Left;
            node.Left = temp.Right;
            temp.Right = node;
            node.height = max(height(node.Left), height(node.Right)) + 1;
            temp.height = max(node.height, height(temp.Left)) + 1;
            
            return temp;
        }

        private Node RotateRight(Node node)
        {
            Node temp;

            temp = node.Right;
            node.Right = temp.Left;
            temp.Left = node;
            node.height = max(height(node.Left), height(node.Right)) + 1;
            temp.height = max(node.height, height(temp.Right)) + 1;
            
            return temp;
        }

        private Node RotateLeftRight(Node node)
        {
            node.Left = RotateRight(node.Left);
            return RotateLeft(node);
        }

        private Node RotateRightLeft(Node node)
        {
            node.Right = RotateLeft(node.Right);
            return RotateRight(node);
        }  
    }
}
