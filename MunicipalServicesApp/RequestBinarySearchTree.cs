using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalServicesApp
{
    public class RequestBinarySearchTree
    {
        // Represents one node in the tree
        private class Node
        {
            public Issue Data;
            public Node Left;
            public Node Right;

            public Node(Issue issue)
            {
                Data = issue;
                Left = null;
                Right = null;
            }
        }

        private Node root;

      
        public void Insert(Issue issue)
        {
            if (issue == null || string.IsNullOrWhiteSpace(issue.RequestID))
            {
                return;
            }

            root = InsertRecursive(root, issue);
        }

        private Node InsertRecursive(Node current, Issue issue)
        {
            if (current == null)
            {
                return new Node(issue);
            }

            int comparison = string.Compare(
                issue.RequestID,
                current.Data.RequestID,
                StringComparison.OrdinalIgnoreCase);

            if (comparison < 0)
            {
                current.Left = InsertRecursive(current.Left, issue);
            }
            else if (comparison > 0)
            {
                current.Right = InsertRecursive(current.Right, issue);
            }

         
            return current;
        }

       
        public Issue Search(string requestID)
        {
            if (string.IsNullOrWhiteSpace(requestID))
            {
                return null;
            }

            Node result = SearchRecursive(root, requestID);

            return result == null ? null : result.Data;
        }

        private Node SearchRecursive(Node current, string requestID)
        {
            if (current == null)
            {
                return null;
            }

            int comparison = string.Compare(
                requestID,
                current.Data.RequestID,
                StringComparison.OrdinalIgnoreCase);

            if (comparison == 0)
            {
                return current;
            }

            if (comparison < 0)
            {
                return SearchRecursive(current.Left, requestID);
            }

            return SearchRecursive(current.Right, requestID);
        }

     
        public int Count()
        {
            return CountRecursive(root);
        }

        private int CountRecursive(Node current)
        {
            if (current == null)
            {
                return 0;
            }

            return 1 + CountRecursive(current.Left)
                     + CountRecursive(current.Right);
        }
    }
}