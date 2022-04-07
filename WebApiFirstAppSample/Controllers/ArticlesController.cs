using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using FirstMvcApp.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using WebApiFirstAppSample.Models.Requests;

namespace WebApiFirstAppSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticlesService _articleService;
        private readonly ILogger<ArticlesController> _logger;

        public ArticlesController(IArticlesService articleService, ILogger<ArticlesController> logger)
        {
            _articleService = articleService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                if (Guid.Empty == id)
                {
                    return BadRequest();
                }
                var article = await _articleService.GetArticleAsync(id);
                if (article != null)
                    return Ok(article);
                
                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }

        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var newsText = "Апрельская зарплата, по данным Белстата, снова выросла. На этот раз +13,5 рублей за месяц. При этом зарплата минчан перевалила за 2 тысячи рублей в месяц, хотя в Могилеве и области насчитали в среднем всего 1118,4 рубля.Средняя начисленная зарплата в Беларуси в апреле составила 1398,2 рубля, сообщает Белстат. Это значит, что по сравнению с мартом она выросла на 13,5 рубля. После вычета налогов у среднестатистического белоруса на руках осталось 1202,45 рубля.Напомним, в марте этого года средняя заработная плата работников была 1384,7, в феврале — 1277,1 рубля.Топ зарплат в апреле выглядит так. Больше всех по традиции в апреле получили работники IT-сферы — 4684,2 рубля, за ними идут финансисты и страховщики (3505,2 рубля), работники грузового авиатранспорта (2956,3 рубля).Меньше всех получают работники сферы красоты и парикмахеры — 663,6 рубля до вычета налогов. За ними идут деятели сферы искусств и творческие работники (746,1 рубля). Библиотекари и музейные работники замыкают топ самых низких зарплат с показателем 751,9 рубля.При этом средняя зарплата минчан перевалила за 2 тысячи рублей в месяц и составила 2003,4 рубля. Неплохо. В минской области в апреле получали в среднем 1409,5 рублям, а в аутсайдерах, по традиции, жители Могилева и области (1118,4 рубля).";


                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders
                        .Accept
                        .Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header

                    var request = new HttpRequestMessage(HttpMethod.Post, "http://api.ispras.ru/texterra/v1/nlp?targetType=lemma&apikey=15031bb039d704a3af5d07194f427aa3bf297058")
                    {
                        Content = new StringContent("[{\"text\":\"" + newsText + "\"}]",

                            Encoding.UTF8,
                            "application/json")
                    };
                    var response = await httpClient.SendAsync(request);

                    var responseString = await response.Content.ReadAsStringAsync();

                    return Ok(responseString);
                }
               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }



    }
}
