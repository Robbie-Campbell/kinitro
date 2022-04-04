namespace PsiBot.Services.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using PsiBot.Model.Constants;
    using System.Net.Http;
    using System.Text;
    /// <summary>
    /// Generates a URL for the display of user participation data
    /// </summary>
    public class DisplayDataController : ControllerBase
    {
        private string participantId;

        /// <summary>
        /// The join call async.
        /// </summary>
        /// <returns>
        /// The <see cref="HttpResponseMessage"/>.
        /// </returns>
        [HttpGet]
        [Route(" ")]
        public ContentResult DisplayDataPage()
            {
            return Content("hello", "text/html", Encoding.UTF8);
        }

    }
}
