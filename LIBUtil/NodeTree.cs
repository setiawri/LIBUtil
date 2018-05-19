using System.Collections.Generic;

namespace LIBUtil
{
    public class NodeTree
    {
        private List<Node> _nodes;

        public NodeTree()
        {
            _nodes = new List<Node>();
        }

        public void Add(params Node[] nodes)
        {
            foreach(Node node in nodes)
                _nodes.Add(node);
        }

        public List<Node> Nodes()
        {
            return _nodes;
        }
    }
    
    public class Node
    {
        private List<Node> _nodes;
        object _data;

        public Node(object data)
        {
            _nodes = new List<Node>();
            _data = data;
        }

        public void Add(params Node[] nodes)
        {
            foreach (Node node in nodes)
                _nodes.Add(node);
        }

        public List<Node> Nodes()
        {
            return _nodes;
        }

        public object Data()
        {
            return _data;
        }
    }
}