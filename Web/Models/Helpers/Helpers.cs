using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

    public static class Helpers
    {
        /// <summary>
        /// Criar imagem em miniatura [thumb]
        /// </summary>
        /// <param name="myImage"></param>
        /// <param name="thumbWidth"></param>
        /// <param name="thumbHeight"></param>
        /// <returns></returns>
        internal static byte[] MakeThumbnail(byte[] myImage, int thumbWidth, int thumbHeight)
        {
            using (MemoryStream ms = new MemoryStream())
            using (Image thumbnail = Image.FromStream(new MemoryStream(myImage)).GetThumbnailImage(thumbWidth, thumbHeight, null, new IntPtr()))
            {
                thumbnail.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }

    /// <summary>
    /// Verifica se o arquivo é imagem valida
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    public static bool IsValidImage(byte[] bytes)
    {
        try
        {
            using (MemoryStream ms = new MemoryStream(bytes))
                Image.FromStream(ms);
        }
        catch (ArgumentException)
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// Verifica se uma string codificada é Base64 válida
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static bool IsBase64String(this string s)
    {
        s = s.Trim();
        return (s.Length % 4 == 0) && Regex.IsMatch(s, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);

    }
}