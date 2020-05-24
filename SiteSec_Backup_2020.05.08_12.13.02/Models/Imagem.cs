using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace SiteSec.Models
{

    public class Imagem
    {
        #region propriedades de persitência

        [JsonProperty("id")]
        [ScaffoldColumn(false)]
        public int Id { get; set; } = 0;

        [JsonProperty("nome")]
        [Display(Name = "Nome", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Imagem de referência")]
        public string Nome { get; set; } = "";

        [JsonProperty("file")]
        [Required]
        [Display(Name = "Imagem", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Imagem")]
        public byte[] File { get; set; } = null;

        [JsonProperty("principal")]
        [Display(Name = "Referência", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Imagem de referência")]
        public bool Principal { get; set; } = true;

        #endregion


        #region propriedades de visualização    

        [Display(Name = "Imagem", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Imagem de referência")]
        public string Image64 { get { return File != null ? string.Format("<img src ='{0}' />", "data:image/jpg;base64," + Convert.ToBase64String(Thumbnail(File, altura: 120, largura: 120))) : null; } }

        #endregion


        #region propriedades de transferência

        /// <summary>
        /// Gerar uma miniatura de uma imagem 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="altura"></param>
        /// <param name="largura"></param>
        /// <returns></returns>
        private static byte[] Thumbnail(byte[] file, int altura, int largura)
        {
            using (MemoryStream ms = new MemoryStream())
            using (Image thumbnail = Image.FromStream(new MemoryStream(file)).GetThumbnailImage(altura, largura, null, new IntPtr()))
            {
                thumbnail.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }

        #endregion
    }

}

