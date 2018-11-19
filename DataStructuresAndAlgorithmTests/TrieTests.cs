using DataStructuresAndAlgorithm.Trie;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructuresAndAlgorithmTests
{
    [TestClass]
    public class TrieTests
    {
        [TestMethod]
        public void TestMethod()
        {
            Trie trie = new Trie();

            trie.Insert("apple");
            var x = trie.Search("apple");   // returns true
            x = trie.Search("app");     // returns false
            x = trie.StartsWith("app"); // returns true
            trie.Insert("app");
            x = trie.Search("app");     // returns true

            //    ["Trie","insert","search","search","search","startsWith","startsWith","startsWith"]
            //    [[],["hello"],["hell"],["helloa"],["hello"],["hell"],["helloa"],["hello"]]

            trie = new Trie();

            trie.Insert("hello");

            x = trie.Search("hell");   // returns false
            x = trie.Search("helloa");     // returns false
            x = trie.Search("hello");  // returns true
            x = trie.StartsWith("hell"); // returns true
            x = trie.StartsWith("helloa");  // returns false
            x = trie.StartsWith("hello");  // returns true
        }
    }
}
