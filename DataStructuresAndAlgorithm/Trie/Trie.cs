using System.Collections.Generic;

namespace DataStructuresAndAlgorithm.Trie
{
    public class Trie
    {
        public Dictionary<char, Trie> Children;

        public bool IsCompleteWord;

        /** Initialize your data structure here. */
        public Trie() : this(false)
        {
        }

        public Trie(bool isCompleteWord)
        {
            Children = new Dictionary<char, Trie>();
            IsCompleteWord = isCompleteWord;
        }

        /** Inserts a word into the trie. */
        public void Insert(string word)
        {
            Trie currentNode = this;

            for (int i = 0; i < word.Length; i++)
            {
                if (currentNode.Children.ContainsKey(word[i]))
                {
                    if (i == word.Length - 1)
                        currentNode.Children[word[i]].IsCompleteWord = true;

                    currentNode = currentNode.Children[word[i]];
                }
                else
                {
                    Trie node = new Trie(i == word.Length - 1);

                    currentNode.Children.Add(word[i], node);
                    currentNode = currentNode.Children[word[i]];
                }
            }
        }

        /** Returns if the word is in the trie. */
        public bool Search(string word)
        {
            var currentNode = this;

            for (int j = 0; j < word.Length; j++)
            {
                if (!currentNode.Children.ContainsKey(word[j]))
                    return false;
                else if (currentNode.Children[word[j]].IsCompleteWord && j == word.Length - 1)
                    return true;

                currentNode = currentNode.Children[word[j]];
            }

            return false;
        }

        /** Returns if there is any word in the trie that starts with the given prefix. */
        public bool StartsWith(string prefix)
        {
            var currentNode = this;

            for (int j = 0; j < prefix.Length; j++)
            {
                if (!currentNode.Children.ContainsKey(prefix[j]))
                    return false;

                currentNode = currentNode.Children[prefix[j]];
            }

            return true;
        }
    }
}
