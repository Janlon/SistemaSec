using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace Sec.Models
{
    [Table("Imagens", Schema = "Sec")]
    public class Imagem
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        //[Required]
        //[MaxLength(100)]
        //[Column("Nome", TypeName = "VARCHAR")]
        //[Display(Name = "Nome", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Nome da imagem")]
        //public string Nome { get; set; } = "";

        //[Required]
        //[DefaultValue(false)]
        //[Display(Name = "Referencia", AutoGenerateField = true, AutoGenerateFilter = true, Description ="Imagem de referência", Prompt = "Principal")]
        //public bool Principal { get; set; } = false;

        [Required]
        [Column("Imagem")]
        [Display(Name = "Imagem")]
        public byte[] File { get; set; }

        /// <summary>
        ///  Automatização de minificação de imagens, que converte imagens para WebP.
        /// </summary>
        [NotMapped]
        public byte[] WebP
        {
            get
            {
                using (var ms = new MemoryStream(File))
                {
                    //usar para criar o nome da imagem
                    Guid guid = new Guid();

                    //CodecInfo para imagens Jpeg
                    ImageCodecInfo codec = ImageCodecInfo.GetImageEncoders().First(enc => enc.FormatID == ImageFormat.Jpeg.Guid);
                    //EncoderParameters que vai setar o nível de qualidade (compressão)
                    EncoderParameters imgParams = new EncoderParameters(1)
                    {
                        //Qualidade em 0L = máximo de compressão
                        Param = new[] { new EncoderParameter(Encoder.Quality, 0L) }
                    };

                    //diminuir o tamanho
                    Image originalImage = Image.FromStream(ms, true, true);
                    Image resizedImage = originalImage.GetThumbnailImage(800, (800 * originalImage.Height) / originalImage.Width, null, IntPtr.Zero);

                    // Create a bitmap.
                    Bitmap bmp = new Bitmap(resizedImage);
                    bmp.Save(guid.ToString(), codec, imgParams);

                    //convertendo para o tipo webp
                    return Dynamicweb.WebP.Encoder.Encode(bmp);
                }
            }

            set
            {
                Dynamicweb.WebP.Decoder.Decode(File);
            }
        }

        [Display(Name ="Pessoas")]
        public virtual List<Pessoa> Pessoas { get; set; } = new List<Pessoa>();


        [Display(Name = "Equipamentos")]
        public virtual List<Equipamento> Equipamentos { get; set; } = new List<Equipamento>();

    }
}