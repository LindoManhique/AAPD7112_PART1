
using System;

namespace MunicipalServicesApp
{
    public class RequestRedBlackTree
    {
       
        private class Node
        {
            public Issue Data;
            public Node Left;
            public Node Right;
            public Node Parent;
            public bool IsRed;

            public Node(Issue issue)
            {
                Data = issue;
            }
        }

        private Node root;
        private Node nil;
        private int count;

        public RequestRedBlackTree()
        {
            
            nil = new Node(null);
            nil.IsRed = false;
            nil.Left = nil;
            nil.Right = nil;
            nil.Parent = nil;

            root = nil;
            count = 0;
        }

       
        public void Insert(Issue issue)
        {
            if (issue == null ||
                string.IsNullOrWhiteSpace(issue.RequestID))
            {
                throw new ArgumentException(
                    "A valid service request with a Request ID is required.");
            }

            Node newNode = new Node(issue);
            newNode.Left = nil;
            newNode.Right = nil;
            newNode.Parent = nil;
            newNode.IsRed = true;

            Node parent = nil;
            Node current = root;

           
            while (current != nil)
            {
                parent = current;

                int comparison = string.Compare(
                    newNode.Data.RequestID,
                    current.Data.RequestID,
                    StringComparison.OrdinalIgnoreCase);

                if (comparison < 0)
                {
                    current = current.Left;
                }
                else if (comparison > 0)
                {
                    current = current.Right;
                }
                else
                {
                    
                    return;
                }
            }

            newNode.Parent = parent;

            if (parent == nil)
            {
                root = newNode;
            }
            else if (string.Compare(
                newNode.Data.RequestID,
                parent.Data.RequestID,
                StringComparison.OrdinalIgnoreCase) < 0)
            {
                parent.Left = newNode;
            }
            else
            {
                parent.Right = newNode;
            }

            count++;

            
            FixInsert(newNode);
        }

        
        private void FixInsert(Node node)
        {
            while (node.Parent.IsRed)
            {
                if (node.Parent == node.Parent.Parent.Left)
                {
                    Node uncle = node.Parent.Parent.Right;

                    // Case 1: Uncle is red.
                    if (uncle.IsRed)
                    {
                        node.Parent.IsRed = false;
                        uncle.IsRed = false;
                        node.Parent.Parent.IsRed = true;

                        node = node.Parent.Parent;
                    }
                    else
                    {
                        // Case 2: Node is a right child.
                        if (node == node.Parent.Right)
                        {
                            node = node.Parent;
                            RotateLeft(node);
                        }

                        // Case 3: Node is a left child.
                        node.Parent.IsRed = false;
                        node.Parent.Parent.IsRed = true;

                        RotateRight(node.Parent.Parent);
                    }
                }
                else
                {
                    Node uncle = node.Parent.Parent.Left;

                    // Case 1: Uncle is red.
                    if (uncle.IsRed)
                    {
                        node.Parent.IsRed = false;
                        uncle.IsRed = false;
                        node.Parent.Parent.IsRed = true;

                        node = node.Parent.Parent;
                    }
                    else
                    {
                        // Case 2: Node is a left child.
                        if (node == node.Parent.Left)
                        {
                            node = node.Parent;
                            RotateRight(node);
                        }

                        // Case 3: Node is a right child.
                        node.Parent.IsRed = false;
                        node.Parent.Parent.IsRed = true;

                        RotateLeft(node.Parent.Parent);
                    }
                }
            }

            
            root.IsRed = false;
        }

     
        private void RotateLeft(Node node)
        {
            Node rightChild = node.Right;

            node.Right = rightChild.Left;

            if (rightChild.Left != nil)
            {
                rightChild.Left.Parent = node;
            }

            rightChild.Parent = node.Parent;

            if (node.Parent == nil)
            {
                root = rightChild;
            }
            else if (node == node.Parent.Left)
            {
                node.Parent.Left = rightChild;
            }
            else
            {
                node.Parent.Right = rightChild;
            }

            rightChild.Left = node;
            node.Parent = rightChild;
        }

        
        private void RotateRight(Node node)
        {
            Node leftChild = node.Left;

            node.Left = leftChild.Right;

            if (leftChild.Right != nil)
            {
                leftChild.Right.Parent = node;
            }

            leftChild.Parent = node.Parent;

            if (node.Parent == nil)
            {
                root = leftChild;
            }
            else if (node == node.Parent.Right)
            {
                node.Parent.Right = leftChild;
            }
            else
            {
                node.Parent.Left = leftChild;
            }

            leftChild.Right = node;
            node.Parent = leftChild;
        }

        
        public Issue Search(string requestID)
        {
            if (string.IsNullOrWhiteSpace(requestID))
            {
                return null;
            }

            Node current = root;

            while (current != nil)
            {
                int comparison = string.Compare(
                    requestID,
                    current.Data.RequestID,
                    StringComparison.OrdinalIgnoreCase);

                if (comparison == 0)
                {
                    return current.Data;
                }
                else if (comparison < 0)
                {
                    current = current.Left;
                }
                else
                {
                    current = current.Right;
                }
            }

            return null;
        }

        
        public int Count()
        {
            return count;
        }
    }
}
