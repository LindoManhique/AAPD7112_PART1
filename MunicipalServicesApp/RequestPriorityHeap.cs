
using System;
using System.Collections.Generic;

namespace MunicipalServicesApp
{
    public class RequestPriorityHeap
    {
        private List<Issue> heap;

        public RequestPriorityHeap()
        {
            heap = new List<Issue>();
        }

        public int Count
        {
            get { return heap.Count; }
        }

        public void Insert(Issue issue)
        {
            if (issue == null)
            {
                throw new ArgumentNullException("issue");
            }

            heap.Add(issue);

            int currentIndex = heap.Count - 1;

            while (currentIndex > 0)
            {
                int parentIndex = (currentIndex - 1) / 2;

                if (heap[parentIndex].DateReported <=
                    heap[currentIndex].DateReported)
                {
                    break;
                }

                Swap(parentIndex, currentIndex);

                currentIndex = parentIndex;
            }
        }

        public Issue Peek()
        {
            if (heap.Count == 0)
            {
                return null;
            }

            return heap[0];
        }

        public Issue ExtractMin()
        {
            if (heap.Count == 0)
            {
                return null;
            }

            Issue oldestRequest = heap[0];

            heap[0] = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);

            HeapifyDown(0);

            return oldestRequest;
        }

        private void HeapifyDown(int index)
        {
            int smallest = index;

            int leftChild = (2 * index) + 1;
            int rightChild = (2 * index) + 2;

            if (leftChild < heap.Count &&
                heap[leftChild].DateReported <
                heap[smallest].DateReported)
            {
                smallest = leftChild;
            }

            if (rightChild < heap.Count &&
                heap[rightChild].DateReported <
                heap[smallest].DateReported)
            {
                smallest = rightChild;
            }

            if (smallest != index)
            {
                Swap(index, smallest);
                HeapifyDown(smallest);
            }
        }

        private void Swap(int firstIndex, int secondIndex)
        {
            Issue temporary = heap[firstIndex];

            heap[firstIndex] = heap[secondIndex];

            heap[secondIndex] = temporary;
        }
    }
}
