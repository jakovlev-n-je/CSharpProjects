using System.Collections.Generic;

namespace Implementation
{
    public class Trie
    {
        public Node Root { get; set; }

        public Trie()
        {
            Root = new Node();
        }

        public void Add(string key, int value)
        {
            Node current = Root;
            foreach (char symbol in key)
            {
                int index = symbol - 'а';
                current.Subnodes[index] = current.Subnodes[index] ?? new Node((char)(index + 1072));
                current = current.Subnodes[index];
            }
            current.Value = value;
        }

        public void Remove(string key)
        {
            Node parent = Root;
            int childIndex = 0;
            Node current = Root;
            foreach (char symbol in key)
            {
                int index = symbol - 'а';
                if (current.Subnodes[index] == null)
                    return;
                if (current.Subnodes[index].Value > 0)
                {
                    parent = current;
                    childIndex = index;
                }
                current = current.Subnodes[index];
            }
            if (parent.Subnodes[childIndex].Value > 0)
                parent.Subnodes[childIndex].Value = 0;
            else
                parent.Subnodes[childIndex] = null;
        }

        public bool TryGetValue(string key, out int value)
        {
            value = 0;
            Node current = Root;
            foreach (char symbol in key)
            {
                int index = symbol - 'а';
                if (current.Subnodes[index] == null)
                    return false;
                current = current.Subnodes[index];
            }
            value = current.Value;
            return value > 0;
        }

        public Word[] FindSimilarWords(string key)
        {
            List<Word> words = new List<Word>();
            foreach (Node subnode in GetSubnodes(Root))
            {
                Traverse(words, subnode, key, subnode.Key.ToString(), 0, 0).ToArray();
            }
            return words.ToArray();
        }

        private Node[] GetSubnodes(Node parent)
        {
            List<Node> subnodes = new List<Node>();
            foreach (Node subnode in parent.Subnodes)
            {
                if (subnode != null)
                    subnodes.Add(subnode);
            }
            return subnodes.ToArray();
        }

        private List<Word> Traverse(List<Word> words, Node root, string key, string current, int index, int typosCount)
        {
            if (index < key.Length && root.Key != key[index])
                typosCount++;
            if (typosCount > 3)
                return words;
            foreach (Node subnode in GetSubnodes(root))
            {
                Traverse(words, subnode, key, current + subnode.Key, index + 1, typosCount);
            }
            if (root.Value != 0 && current.Length == key.Length)
                words.Add(new Word(current, typosCount));
            return words;
        }
    }
}
