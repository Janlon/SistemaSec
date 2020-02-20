namespace Sec.QRCoder
{
    public enum ImageType
    {
        Gif,
        Jpeg,
        Png
    }

    public enum SizingMode
    {
        WidthHeightAttribute,
        ViewBoxAttribute
    }
    public enum EciMode
    {
        Default = 0,
        Iso8859_1 = 3,
        Iso8859_2 = 4,
        Utf8 = 26
    }
    /// <summary>
    /// Error correction level. These define the tolerance levels for how much of the code can be lost before the code cannot be recovered.
    /// </summary>
    public enum ECCLevel
    {
        /// <summary>
        /// 7% may be lost before recovery is not possible
        /// </summary>
        L,
        /// <summary>
        /// 15% may be lost before recovery is not possible
        /// </summary>
        M,
        /// <summary>
        /// 25% may be lost before recovery is not possible
        /// </summary>
        Q,
        /// <summary>
        /// 30% may be lost before recovery is not possible
        /// </summary>
        H
    }

    public enum EncodingMode
    {
        Numeric = 1,
        Alphanumeric = 2,
        Byte = 4,
        Kanji = 8,
        ECI = 7
    }
}
