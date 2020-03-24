using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics.QRCoder
{
    public struct AlignmentPattern
    {
        public int Version;
        public List<Point> PatternPositions;
    }

    public struct CodewordBlock
    {
        public CodewordBlock(int groupNumber, int blockNumber, string bitString, List<string> codeWords,
            List<string> eccWords, List<int> codeWordsInt, List<int> eccWordsInt)
        {
            this.GroupNumber = groupNumber;
            this.BlockNumber = blockNumber;
            this.BitString = bitString;
            this.CodeWords = codeWords;
            this.ECCWords = eccWords;
            this.CodeWordsInt = codeWordsInt;
            this.ECCWordsInt = eccWordsInt;
        }

        public int GroupNumber { get; }
        public int BlockNumber { get; }
        public string BitString { get; }
        public List<string> CodeWords { get; }
        public List<int> CodeWordsInt { get; }
        public List<string> ECCWords { get; }
        public List<int> ECCWordsInt { get; }
    }

    public struct ECCInfo
    {
        public ECCInfo(int version, ECCLevel errorCorrectionLevel, int totalDataCodewords, int eccPerBlock, int blocksInGroup1,
            int codewordsInGroup1, int blocksInGroup2, int codewordsInGroup2)
        {
            this.Version = version;
            this.ErrorCorrectionLevel = errorCorrectionLevel;
            this.TotalDataCodewords = totalDataCodewords;
            this.ECCPerBlock = eccPerBlock;
            this.BlocksInGroup1 = blocksInGroup1;
            this.CodewordsInGroup1 = codewordsInGroup1;
            this.BlocksInGroup2 = blocksInGroup2;
            this.CodewordsInGroup2 = codewordsInGroup2;
        }
        public int Version { get; }
        public ECCLevel ErrorCorrectionLevel { get; }
        public int TotalDataCodewords { get; }
        public int ECCPerBlock { get; }
        public int BlocksInGroup1 { get; }
        public int CodewordsInGroup1 { get; }
        public int BlocksInGroup2 { get; }
        public int CodewordsInGroup2 { get; }
    }

    public struct VersionInfo
    {
        public VersionInfo(int version, List<VersionInfoDetails> versionInfoDetails)
        {
            this.Version = version;
            this.Details = versionInfoDetails;
        }
        public int Version { get; }
        public List<VersionInfoDetails> Details { get; }
    }

    public struct VersionInfoDetails
    {
        public VersionInfoDetails(ECCLevel errorCorrectionLevel, Dictionary<EncodingMode, int> capacityDict)
        {
            this.ErrorCorrectionLevel = errorCorrectionLevel;
            this.CapacityDict = capacityDict;
        }

        public ECCLevel ErrorCorrectionLevel { get; }
        public Dictionary<EncodingMode, int> CapacityDict { get; }
    }

    public struct Antilog
    {
        public Antilog(int exponentAlpha, int integerValue)
        {
            this.ExponentAlpha = exponentAlpha;
            this.IntegerValue = integerValue;
        }
        public int ExponentAlpha { get; }
        public int IntegerValue { get; }
    }

    public struct PolynomItem
    {
        public PolynomItem(int coefficient, int exponent)
        {
            this.Coefficient = coefficient;
            this.Exponent = exponent;
        }

        public int Coefficient { get; }
        public int Exponent { get; }
    }

    public class Polynom
    {
        public Polynom()
        {
            this.PolyItems = new List<PolynomItem>();
        }

        public List<PolynomItem> PolyItems { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            //this.PolyItems.ForEach(x => sb.Append("a^" + x.Coefficient + "*x^" + x.Exponent + " + "));
            foreach (var polyItem in this.PolyItems)
            {
                sb.Append("a^" + polyItem.Coefficient + "*x^" + polyItem.Exponent + " + ");
            }

            return sb.ToString().TrimEnd(new[] { ' ', '+' });
        }
    }

    public class Point
    {
        public int X { get; }
        public int Y { get; }
        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }

    public class Rectangle
    {
        public int X { get; }
        public int Y { get; }
        public int Width { get; }
        public int Height { get; }

        public Rectangle(int x, int y, int w, int h)
        {
            this.X = x;
            this.Y = y;
            this.Width = w;
            this.Height = h;
        }
    }
}
