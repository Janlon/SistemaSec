using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Web.Models
{
    public class Channel
    {
        [Display(Name = "Título")]
        [JsonProperty("title")]
        public string Titulo { get; set; }

        [Display(Name = "Descrição")]
        [JsonProperty("description")]
        public string Descricao { get; set; } = "";

        [JsonProperty("category")]
        public string Categoria { get; set; } = "";

        [JsonProperty("logo")]
        public string Logo
        {
            get
            {
               // if (string.IsNullOrEmpty(Logo))
                    return string.Format("<img src ='{0}' style='width: 60px;' />", "data:image/jpeg;base64,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");
                
                //if (!Helpers.IsBase64String(Logo))
                  //  return string.Format("<img src ='{0}' />", "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAAAQABAAD/2wBDAAEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQH/2wBDAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQH/wAARCAAnACgDAREAAhEBAxEB/8QAGgAAAgMBAQAAAAAAAAAAAAAABwoGCAkDBf/EACkQAAEEAgEDAwQDAQAAAAAAAAMBAgQFBhEHCBIhABQxCRMiQRZCUYH/xAAZAQEBAQEBAQAAAAAAAAAAAAAAAwQBAgX/xAAiEQACAgICAwEBAQEAAAAAAAABAgMRABIEIQUTMUEiFIH/2gAMAwEAAhEDEQA/AN6OOqUZECvb3b7G+EV2lVGKq60mtp4XS73pE/W2MunitBDjwkknGT7QWtc9AxjyzqryMG1GRooiyDvcRzG9ohvVV340iu9MZGsx5dxPE2T1sMIzmVX1MuyiXNtXRMOOCjZWjlP97fVpswi5JURJ7IhX1aT6UUqxCvuxRkhikyAMZzzGjiyIQpscb2jlRRSRfcE8JftHEhRtIEjWlETscneN7Uex/wCLtK1U9MZSHkSqGIhVVO3aOVF7VRV0qbVFVNLp2tKiJ8+fO9sYTON542JH25q67e5U+URGtVV/Sb7tp4XX/FRVYwpZb1xdLHT3VOn8r83YBjsiKVYyY+C8jXmXSpiRzSxwomJ0LrLICnIKMTytcwA1b3SDBZ+aMZPuXfqEcG8SdOA+onLbCQ3D4+NYJmFzDrRjsrSuxTP5cGPT3YBw3mbaR5vuyrRNg9/8jlRXwqt5JSkYGJmUHUq+9gahSeia22A11H0m+vn3rOgA/SB0ezf/AAdA/fmB7C+s/ph6maWPd8G81YJnobMZnAqYVuysywKxSOaZlhhV4yry6qeN7V7mWdHEVzNFZ3Dc162zmBnkiWwqlRqppjXMX5/srSefH+f58a0vlNemMhmHZWyIPvV34ME5XI1V3+LV2ifGtKion72vai+U9MYj3lFhn+ZcsgpKGpnZPJsZ2ZZLTY7CGtheyAw0mTpDI8Jj/cz5lbCZJENsVsqaQBpBnpIGExFnLNFAhlmdY4wQGdjSrsaBY/FF9bGlBIsixlIoZZ39cKNJIQWCILYgVdD6SLuhZP4Dnq2vMXK3PTMG4irJuc3oY2P0+Jz8RiZRfWIMmsMTyC2k4HSmxeRKbWITFhW0iHVRStSBTkhunQ2V8ibanP4klgSM8p5USGJGkebYesRaklmf5oAbBus9xwzvL/ljhd+RK6xJCEPtMmwpFQ0dyRrRF/nXeBGlv8x6eupPjiBeDG4vFXK3GGZ5hX0swwIdbIg5ZV5DNopeUni+wmyIbIJ2WNlTDuKZ5GPiRJ1o4EqWWPj+fx/J8ZeXxGLwOzqjmgW9bFGNAkr/AECADRIANAEZo8l43k+K5R4fMUJyEjjeSMENp7F3VSwJUnQqxKkj+quwQHtMtycU8aSQlaQUkbTCc13c14zDY5jmuTaK1zNK1doit/WlT1tzBgmDaSgRpIhORSljmGJXvcNikIN7WIR7EVw2o9Wq57WuVrUVUaq/LGJDc3ciZtwVydf45mlVb4bmlBNnVsmbKZLrCjjxpgysjUUxqBWwrp0mNCtAWQT+zlxFiTER7HRnvnJFFKNZYo5V7/iVFkTsUbRwyno/oOUjllhJaGWWFjQLRSPG1Ahq2Qqw7APRyRdGf1CK3pt6j6DnTJQ2Oc0q0XJGP5ljsKa1kjK6zkbBslxKaIxzGhe6ck67BYkaSRGOR0R5WyochBnjy5ECy8aTjCOMxyRmExFVEZiZdGTWtQupoKBQ6FV1lIJmj5KchpZBIkgmEoZjL7VbdX3vYsXAJa9vpu+8qXkvK6XdtaXcidMuLO6KonknEBIUABSJZq8ccYgq1ZYEmlY85PcOOV73K9dDcyXjvHcbxnGHG4sfrj2MjC7uRgAzfii6ApQq0BQzV5XyvL8xyzzOa6yTaJEGVQg9cd6AizZGxskkn9x6Xge3yq44G4TmZsA0TLZHE3HhsmiSUI2RHvSYnUusxSWlRpGSWy1L7lhGNeM/exdK31uz5uFL0xiwX1zLMOSc08ZYTcQYRavHOORX1e5UI+QabkWRXMaW4u2IMbBDx6KwbGucr0cVz3fkxiMZgPksKqC1IcWrijT7aEcqCG1oxte9FY1ukRUV6d+0RV35VEXfpjNuvoJYtimRcvc2jyHBMWyZafCcZu6PIL6mp7afilrGyGRCaKifZRZE2uLbx50kkmXWuD4pwDkP2+MisY138fHpjP/Z");

             //   return string.Format("<img src ='{0}' />", Logo);
                   // return string.Format("<img src ='{0}' />", "data:image/jpg;base64," + Convert.ToBase64String(Helpers.MakeThumbnail(this.Logo, 40, 40)));
            }
        }

        [Display(Name = "Endereço")]
        [JsonProperty("stream_url")]
        public string Endereco { get; set; }
    }

    public class ListaCanais
    {
        [JsonProperty("channel")]
        public List<Channel> Canal { get; set; }
    }

}