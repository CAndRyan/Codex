﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codex {
    public enum SoundType {
        unknown,
        vowel,
        consonant
    }

    public enum WordClass {
        unknown,
        noun,
        verb,
        adjective,
        adverb,
        pronoun,
        proposition,
        conjunction,
        determiner
        //exclamation
    }

    public class Letter {
        public char Character { get; }
        private SoundType _Type;
        public string Type {
            get {
                return Enum.GetName(typeof(SoundType), _Type);
            }
        }

        public Letter(char character) {
            this.Character = character;
            this._Type = SoundType.unknown;
        }
    }

    public class Word {
        public Letter[] Letters { get; }
        private WordClass _Class;
        public string Class {
            get {
                return Enum.GetName(typeof(WordClass), _Class);
            }
        }

        public int WordLength {
            get {
                return Letters.Length;
            }
        }

        public string Text {
            get {
                char[] characters = new char[Letters.Length];
                for (int i = 0; i < Letters.Length; i++) {
                    characters[i] = Letters[i].Character;
                }

                return new string(characters);
            }
        }

        public Word() {
            this.Letters = new Letter[] { };
            this._Class = WordClass.unknown;
        }

        public Word(Letter[] letters) {
            this.Letters = letters;
            this._Class = WordClass.unknown;
        }
    }

    public class Sentence {
        public Word[] Words { get; }
        private char _Ending;
        public char? Ending {
            get {
                if (_Ending == '\0') {
                    return null;
                }
                else {
                    return _Ending;
                }
            }
        }

        public int WordCount {
            get {
                return Words.Length;
            }
        }

        public Sentence(Word[] words, char ending) {
            this.Words = words;
            this._Ending = ending;
        }

        public int GetLongestWordLength() {
            int count = 0;

            foreach(Word word in Words) {
                if (word.WordLength > count) {
                    count = word.WordLength;
                }
            }

            return count;
        }

        public List<Word> GetLongestWords() {
            int count = GetLongestWordLength();
            List<Word> retWords = new List<Word>();

            foreach (Word word in Words) {
                if (word.WordLength == count) {
                    retWords.Add(word);
                }
            }

            return retWords;
        }
    }

    public class Paragraph {
        public Sentence[] Sentences { get; }

        public int SentenceCount {
            get {
                return Sentences.Length;
            }
        }

        public int WordCount {
            get {
                int count = 0;

                foreach (Sentence sentence in Sentences) {
                    count += sentence.WordCount;
                }

                return count;
            }
        }

        public Paragraph(Sentence[] sentences) {
            this.Sentences = sentences;
        }

        public int GetLongestWordLength() {
            int count = 0;
            
            foreach (Sentence sentence in Sentences) {
                int sCount = sentence.GetLongestWordLength();
                if (sCount > count) {
                    count = sCount;
                }
            }

            return count;
        }

        public List<Word> GetLongestWords() {
            int count = GetLongestWordLength();
            List<Word> retWords = new List<Word>();

            foreach (Sentence sentence in Sentences) {
                if (sentence.GetLongestWordLength() == count) {
                    retWords.AddRange(sentence.GetLongestWords());
                }
            }

            return retWords;
        }

        public int GetLongestSentenceLength() {
            int count = 0;

            foreach (Sentence sentence in Sentences) {
                int sCount = sentence.WordCount;
                if (sCount > count) {
                    count = sCount;
                }
            }

            return count;
        }
    }

    public static class Extensions {
        public static string ToStringFromChar(this Char cha) {
            if (cha != Char.MinValue) {
                return cha.ToString();
            }
            else {
                return String.Empty;
            }
        }
    }
}
