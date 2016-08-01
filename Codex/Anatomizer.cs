using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codex {
    public class Anatomizer {
        #region Properties
        public string Input { get; }
        public List<Paragraph> Paragraphs = new List<Paragraph>();
        private char DefaultSentenceEnding = '.';
        private char DefaultParagraphStart = '\t';
        private char DefaultParagraphEnding = '\n';
        private char DefaultParagraphDelimiter = '\n';

        public string FormattedText {
            get {
                return RebuildText();
            }
        }
        #endregion

        #region Constructors
        // Optional char[] looks like [ParagraphStart, SentenceEnding, ParagraphEnding, ParagraphDelimiter]
        public Anatomizer(string input, params char[] startAndEndingChars) {
            this.Input = input;

            for (int i = 0; i < startAndEndingChars.Length; i++) {
                switch (i) {
                    case 0:
                        this.DefaultParagraphStart = startAndEndingChars[i];
                        break;
                    case 1:
                        this.DefaultSentenceEnding = startAndEndingChars[i];
                        break;
                    case 2:
                        this.DefaultParagraphEnding = startAndEndingChars[i];
                        break;
                    case 3:
                        this.DefaultParagraphDelimiter = startAndEndingChars[i];
                        break;
                    default:
                        i = startAndEndingChars.Length;
                        break;
                }
            }

            ParseText();
        }
        #endregion
        
        #region Internal Functions
        private void ParseText() {
            List<Word> words = new List<Word>();
            List<Sentence> sentences = new List<Sentence>();

            List<Letter> word = new List<Letter>();
            for (int i = 0; i < Input.Length; i++) {
                if (Input[i] == Constants.wDelim) {
                    if (word.Count > 0) {
                        words.Add(new Word(word.ToArray<Letter>()));
                        word = new List<Letter>();
                    }
                }
                else if (Constants.sDelims.Contains<char>(Input[i])) {
                    if (word.Count > 0) {
                        words.Add(new Word(word.ToArray<Letter>()));
                        word = new List<Letter>();
                    }

                    sentences.Add(new Sentence(words.ToArray<Word>(), Input[i]));
                    words = new List<Word>();
                }
                else {
                    word.Add(new Letter(Input[i]));
                }
            }

            // Check if a word and/or sentence wasn't completed previously when the end of input was reached
            if (word.Count > 0) {
                words.Add(new Word(word.ToArray<Letter>()));
            }

            if (words.Count > 0) {
                sentences.Add(new Sentence(words.ToArray<Word>(), '\0'));
            }

            Paragraphs.Add(new Paragraph(sentences.ToArray<Sentence>()));

            return;
        }
        
        private string RebuildText() {
            StringBuilder text = new StringBuilder();

            for (int i = 0; i < Paragraphs.Count; i++) {
                if (i != 0) {
                    text.Append(this.DefaultParagraphDelimiter.ToStringFromChar());
                }
                text.Append(this.DefaultParagraphStart.ToStringFromChar());

                for (int j = 0; j < Paragraphs[i].Sentences.Length; j++) {
                    if (j != 0) {
                        text.Append(Constants.wDelim);
                    }

                    for (int k = 0; k < Paragraphs[i].Sentences[j].Words.Length; k++) {
                        string word = Paragraphs[i].Sentences[j].Words[k].Text;

                        if (k != 0) {
                            text.Append(Constants.wDelim.ToString() + word);
                        }
                        else {
                            text.Append(word[0].ToString().ToUpper() + word.Substring(1, (word.Length - 1)));
                        }
                    }

                    if (Paragraphs[i].Sentences[j].Ending == null) {
                        text.Append(this.DefaultSentenceEnding.ToStringFromChar());
                    }
                    else {
                        text.Append(Paragraphs[i].Sentences[j].Ending.ToString());
                    }
                }

                if (i < (Paragraphs.Count - 1)) {
                    text.Append(this.DefaultSentenceEnding.ToStringFromChar());
                }
            }

            return text.ToString();
        }
        #endregion

        #region Public Functions
        // Get the word count of the longest sentence (by number of words)
        public int GetMaxWordCountOfSentences() {
            int count = 0;

            foreach (Paragraph paragraph in Paragraphs) {
                int wCount = paragraph.GetLongestSentenceLength();
                if (wCount > count) {
                    count = wCount;
                }
            }

            return count;
        }

        // Get the total word count
        public int GetTotalWordCount() {
            int count = 0;

            foreach (Paragraph paragraph in Paragraphs) {
                count += paragraph.WordCount;
            }

            return count;
        }

        // Get the longest word length
        public int GetMaxWordLength() {
            int count = 0;

            foreach (Paragraph paragraph in Paragraphs) {
                int max = paragraph.GetLongestWordLength();
                if (max > count) {
                    count = max;
                }
            }

            return count;
        }

        // Get longest words
        public List<Word> GetLongestWords() {
            int count = GetMaxWordLength();
            List<Word> retWords = new List<Word>();

            foreach (Paragraph paragraph in Paragraphs) {
                if (paragraph.GetLongestWordLength() == count) {
                    retWords.AddRange(paragraph.GetLongestWords());
                }
            }

            return retWords;
        }
        public List<string> GetLongestWords(bool textOnly) {
            List<string> retWords = new List<string>();

            foreach (Word word in GetLongestWords()) {
                retWords.Add(word.Text);
            }

            return retWords;
        }
        #endregion
    }
}
