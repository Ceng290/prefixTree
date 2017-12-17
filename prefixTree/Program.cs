using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prefixTree
{
    class Program
    {
        internal static TrieNode createTree()
        {
            return (new TrieNode('\0'));
        }

        internal static void insertWord(TrieNode root, string word)
        {
            int offset = 97;
            int l = word.Length;
            char[] letters = word.ToCharArray();
            TrieNode curNode = root;

            for (int i = 0; i < l; i++)
            {
                if (curNode.links[letters[i] - offset] == null)
                {
                    curNode.links[letters[i] - offset] = new TrieNode(letters[i]);
                }
                curNode = curNode.links[letters[i] - offset];
            }
            curNode.fullWord = true;
        }

        internal static bool find(TrieNode root, string word)
        {
            char[] letters = word.ToCharArray();
            int l = letters.Length;
            int offset = 97;
            TrieNode curNode = root;

            int i;
            for (i = 0; i < l; i++)
            {
                if (curNode == null)
                {
                    return false;
                }
                curNode = curNode.links[letters[i] - offset];
            }

            if (i == l && curNode == null)
            {
                return false;
            }

            if (curNode != null && !curNode.fullWord)
            {
                return false;
            }

            return true;
        }

        internal static void printTree(TrieNode root, int level, char[] branch)
        {
            if (root == null)
            {
                return;
            }

            for (int i = 0; i < root.links.Length; i++)
            {
                branch[level] = root.letter;
                printTree(root.links[i], level + 1, branch);
            }

            if (root.fullWord)
            {
                for (int j = 1; j <= level; j++)
                {
                    Console.Write(branch[j]);
                }
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            TrieNode tree = createTree();

            string[] words = new string[] { "an", "ant", "all", "allot", "alloy", "aloe", "are", "ate", "be"};
            for (int i = 0; i < words.Length; i++)
            {
                insertWord(tree, words[i]);
            }

            char[] branch = new char[50];
            printTree(tree, 0, branch);

            string searchWord = "all";
            if (find(tree, searchWord))
            {
                Console.WriteLine("The word was found");
            }
            else
            {
                Console.WriteLine("The word was NOT found");
            }
            Console.ReadLine();
        }
    }
    internal class TrieNode
    {
        internal char letter;
        internal TrieNode[] links;
        internal bool fullWord;

        internal TrieNode(char letter)
        {
            this.letter = letter;
            links = new TrieNode[26];
            this.fullWord = false;
        }
    }


}
