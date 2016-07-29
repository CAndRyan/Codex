using System;
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

        public string Text {
            get {
                char[] characters = new char[Letters.Length];
                for (int i = 0; i < Letters.Length; i++) {
                    characters[i] = Letters[i].Character;
                }

                return new string(characters);
            }
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

        public Sentence(Word[] words, char ending) {
            this.Words = words;
            this._Ending = ending;
        }
    }

    public class Paragraph {
        public Sentence[] Sentences { get; }

        public Paragraph(Sentence[] sentences) {
            this.Sentences = sentences;
        }
    }
}
