
using System;

namespace MunicipalServicesApp
{
    public class RequestAVLTree
    {
        // Represents a node in the AVL tree
        private class Node
        {
            public Issue Data;
            public Node Left;
            public Node Right;
            public int Height;

            public Node(Issue issue)
            {
                Data = issue;
                Height = 1;
            }
        }

        private Node root;

        // Get the height of a node
        private int GetHeight(Node node)
        {
            return node == null ? 0 : node.Height;
        }

        // Calculate the balance factor
        private int GetBalance(Node node)
        {
            return node == null ? 0 :
                GetHeight(node.Left) - GetHeight(node.Right);
        }

        // Update node height
        private void UpdateHeight(Node node)
        {
            node.Height = 1 + Math.Max(
                GetHeight(node.Left),
                GetHeight(node.Right));
        }

        // Right rotation
        private Node RotateRight(Node y)
        {
            Node x = y.Left;
            Node temporary = x.Right;

            x.Right = y;
            y.Left = temporary;

            UpdateHeight(y);
            UpdateHeight(x);

            return x;
        }

        // Left rotation
        private Node RotateLeft(Node x)
        {
            Node y = x.Right;
            Node temporary = y.Left;

            y.Left = x;
            x.Right = temporary;

            UpdateHeight(x);
            UpdateHeight(y);

            return y;
        }

        // Insert a service request
        public void Insert(Issue issue)
        {
            if (issue == null ||
                string.IsNullOrWhiteSpace(issue.RequestID))
            {
                return;
            }

            root = InsertRecursive(root, issue);
        }

        private Node InsertRecursive(Node node, Issue issue)
        {
            if (node == null)
            {
                return new Node(issue);
            }

            int comparison = string.Compare(
                issue.RequestID,
                node.Data.RequestID,
                StringComparison.OrdinalIgnoreCase);

            if (comparison < 0)
            {
                node.Left = InsertRecursive(node.Left, issue);
            }
            else if (comparison > 0)
            {
                node.Right = InsertRecursive(node.Right, issue);
            }
            else
            {
                return node;
            }

            // Update height after insertion
            UpdateHeight(node);

            // Check whether the node is unbalanced
            int balance = GetBalance(node);

            // Left Left case
            if (balance > 1 &&
                string.Compare(issue.RequestID, node.Left.Data.RequestID,
                    StringComparison.OrdinalIgnoreCase) < 0)
            {
                return RotateRight(node);
            }

            // Right Right case
            if (balance < -1 &&
                string.Compare(issue.RequestID, node.Right.Data.RequestID,
                    StringComparison.OrdinalIgnoreCase) > 0)
            {
                return RotateLeft(node);
            }

            // Left Right case
            if (balance > 1 &&
                string.Compare(issue.RequestID, node.Left.Data.RequestID,
                    StringComparison.OrdinalIgnoreCase) > 0)
            {
                node.Left = RotateLeft(node.Left);
                return RotateRight(node);
            }

            // Right Left case
            if (balance < -1 &&
                string.Compare(issue.RequestID, node.Right.Data.RequestID,
                    StringComparison.OrdinalIgnoreCase) < 0)
            {
                node.Right = RotateRight(node.Right);
                return RotateLeft(node);
            }

            return node;
        }

        // Search for a request using its ID
        public Issue Search(string requestID)
        {
            if (string.IsNullOrWhiteSpace(requestID))
            {
                return null;
            }

            Node current = root;

            while (current != null)
            {
                int comparison = string.Compare(
                    requestID,
                    current.Data.RequestID,
                    StringComparison.OrdinalIgnoreCase);

                if (comparison == 0)
                {
                    return current.Data;
                }

                current = comparison < 0
                    ? current.Left
                    : current.Right;
            }

            return null;
        }

        // Count requests stored in the AVL tree
        public int Count()
        {
            return CountRecursive(root);
        }

        private int CountRecursive(Node node)
        {
            if (node == null)
            {
                return 0;
            }

            return 1 + CountRecursive(node.Left)
                     + CountRecursive(node.Right);
        }
    }
}
