using Sec.QRCoder;
using Sec.Helpers.Cripto;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sec.Helpers.IBGE;
using Sec.Helpers.Errors;

namespace SistemaSec.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            try
            {

                List<ErrorBlock> erros = ErrorManager.ErrorList(new DateTime(2000, 01, 01), DateTime.Now);

                Endereco item = (new IbgeClient()).EnderecoDoCEP("11.015-160");

                ViewBag.UFs = (new IbgeClient())
                    .GetUFs()
                    .OrderByDescending(p => p.Nome);

                //Onde gravo?
                string arquivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "QRC.png");

                if (System.IO.File.Exists(arquivo))
                    System.IO.File.Delete(arquivo);
                var level = "Q";
                FileStream fs = new FileStream(path: arquivo, mode: FileMode.CreateNew);
                ECCLevel eccLevel = (ECCLevel)(level == "L" ? 0 : level == "M" ? 1 : level == "Q" ? 2 : 3);
                using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
                using (QRCodeData qrCodeData = qrGenerator.CreateQrCode("Este é um teste do componente de QR Code para o Janlon.", eccLevel))
                using (QRCode qrCode = new QRCode(qrCodeData))
                {
                    Bitmap basemap = new Bitmap(200, 200);
                    Bitmap bmp = qrCode.GetGraphic(20, Color.Black, Color.White, basemap, (int)150);
                    bmp.Save(fs, format: ImageFormat.Png);
                }
                fs.Close();
                ViewBag.QRCode = (new FileInfo(arquivo)).Name;

            }
            catch (Exception ex) { ex.Log(); }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}